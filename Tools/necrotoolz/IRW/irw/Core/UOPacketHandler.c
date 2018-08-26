/******************************************************************************\
* 
* 
*  Copyright (C) 2004 Daniel 'Necr0Potenc3' Cavalcanti
* 
* 
*  This program is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
* 
*  This program is distributed in th7e hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
* 
* 	Sept 18th, 2004 -- packet handler module
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "UOPacketHandler.h"
#include "UOProtocol.h"
#include "UONetwork.h"
#include "UOWorld.h"
#include "UOTarget.h"
#include "UOJournal.h"
#include "Plugins.h"
#include "Logger.h"
#include "INIProfile.h"

/* TODO: make the callbacks, packet readers and commands as dynamic arrays */
#define MAX_WORLDCALLBACKS 100
#define MAX_PACKETREADERS 100

/*
* for safety reasons, you can have only *ONE* packet handler for each packet.
* Internal handlers come first they should always let the packet pass (return TRUE)
* but if necessary. They can eat the packet and it won't go to the readonly or
* the external handler.
* then the readonly handlers. they are more than one cause they are supposed
* to only read the packet, although they can alter it cause there is no check.
* I'm not going to add a check because its just too slow and plugin devs should
* have responsability.
* By last, the external handlers which can tell the packetbuilder to eat the
* packet or let it pass.
*/

/* [direction][handlercount][packetid] */
static IRWWorldCallback WorldCallbacks[MAX_WORLDCALLBACKS];
static IRWPacketReader ReadOnlyHandlers[2][MAX_PACKETREADERS][256];
static IRWPacketHandler InternalHandlers[2][256];
static IRWPacketHandler ExternalHandlers[2][256];

static IRWCmd *Commands = NULL;
static unsigned int CmdListSize = 0;

static IRWAlias *Aliases = NULL;
static unsigned int AliasListSize = 0;

static unsigned char COMMAND_PREFIX = ',';


/*******************************************************************************
* 
*   Packet initer, builders, handler
* 
*******************************************************************************/

void InitAliases(void)
{
	AddAlias("on"); SetAlias("on", TRUE);
	AddAlias("off"); SetAlias("off", FALSE);
	AddAlias("dropbadpackets"); SetAlias("dropbadpackets", TRUE);
	AddAlias("self");
	AddAlias("backpack");
	AddAlias("lasttarget");
	AddAlias("lastattack");
	AddAlias("lastcontainer");
	AddAlias("catchbag");

	return;
}

void InitPacketHandler(void)
{
	/* clean the handlers */
	memset(WorldCallbacks, 0, sizeof(WorldCallbacks));
	memset(ReadOnlyHandlers, 0, sizeof(ReadOnlyHandlers));
	memset(InternalHandlers, 0, sizeof(InternalHandlers));
	memset(ExternalHandlers, 0, sizeof(ExternalHandlers));

	/* speech handlers for command handling */
	AddInternalPacketHandler(SERVER_MESSAGE, 0xc1, IRWServer_PredefinedMessage);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x1c, IRWServer_Speech);
	AddInternalPacketHandler(SERVER_MESSAGE, 0xae, IRWServer_UnicodeSpeech);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x03, IRWClient_AsciiSpeech);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0xad, IRWClient_UnicodeSpeech);

	/* player(s)-world interaction handlers */
	AddInternalPacketHandler(SERVER_MESSAGE, 0x1b, IRWServer_EnterWorld);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x11, IRWServer_CharStatus);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x1a, IRWServer_UpdateItem);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x1d, IRWServer_DeleteItem);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x20, IRWServer_UpdateCreature);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x24, IRWServer_OpenContainer);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x08, IRWClient_DropItem); 
	AddInternalPacketHandler(SERVER_MESSAGE, 0x25, IRWServer_AddToContainer);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x28, IRWServer_ClearSquare);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x2e, IRWServer_EquipItem);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x3a, IRWServer_UpdateSkills);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x3c, IRWServer_UpdateContainer);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x77, IRWServer_UpdatePlayerPos);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x78, IRWServer_UpdatePlayer);
	AddInternalPacketHandler(SERVER_MESSAGE, 0xa1, IRWServer_UpdateHealth);
	AddInternalPacketHandler(SERVER_MESSAGE, 0xa2, IRWServer_UpdateMana);
	AddInternalPacketHandler(SERVER_MESSAGE, 0xa3, IRWServer_UpdateStamina);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x72, IRWServer_WarPeace);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x88, IRWServer_Paperdoll);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x2f, IRWServer_Fight);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x05, IRWClient_Attack);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x06, IRWClient_DoubleClick);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x02, IRWClient_PlayerRequestWalk);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x22, IRWServer_PlayerConfirmWalk);

	/* target cursor handlers */
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x6c, IRWClient_Target);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x6c, IRWServer_Target);
	AddInternalPacketHandler(SERVER_MESSAGE, 0x99, IRWServer_Target);

	/* login handlers, add the Login and Pwd to the packet */
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x80, IRWClient_Login);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0xCF, IRWClient_Login);
	AddInternalPacketHandler(CLIENT_MESSAGE, 0x91, IRWClient_GameLogin);

	/* handle the client's subroutines */
	AddInternalPacketHandler(SERVER_MESSAGE, 0xBF, IRWServer_GeneralInfo);

	AddCommand("say", IRWCmd_Say);
	AddCommand("quit", IRWCmd_Quit);
	AddCommand("help", IRWCmd_Help);
	AddCommand("set", IRWCmd_Set);
	AddCommand("listplugins", IRWCmd_ListPlugins);
	AddCommand("listaliases", IRWCmd_ListAliases);

	AddCommand("waittarget", IRWCmd_WaitTarget);
	AddCommand("waittargetgraphic", IRWCmd_WaitTargetGraphic);
	AddCommand("waittargettile", IRWCmd_WaitTargetTile);
	AddCommand("canceltarget", IRWCmd_CancelTarget);
	AddCommand("cancelwaittarget", IRWCmd_CancelWaitTarget);

	/* cmds just to fool around :P */
	AddCommand("isinjournal", IRWCmd_IsInJournal);
	AddCommand("dumpjournal", IRWCmd_JournalDump);
	AddCommand("getlastjournalline", IRWCmd_GetLastJournalLine);
	AddCommand("cleanjournal", IRWCmd_CleanJournal);

	return;
}

int AddWorldCallback(void *Callback)
{
	int i = 0;

	for(i = 0; i < MAX_WORLDCALLBACKS; i++)
	{
		if(WorldCallbacks[i] == 0)
		{
			WorldCallbacks[i] = (IRWWorldCallback)Callback;
			LogPrint(NOFILTER_LOG, ":WORLDCALLBACK:ADDED: Callback: %X Slot: %d\r\n", Callback, i);
			return 1;
		}

	}

	LogPrint(ERROR_LOG, ":WORLDCALLBACK:FULL: Callback: %X\r\n", Callback);
	return 0;
}

int AddInternalPacketHandler(int From, int PacketID, void *Handler)
{
	if(InternalHandlers[From][PacketID] != 0)
	{
		LogPrint(ERROR_LOG, ":PACKETHANDLER:INT:FULL: Handler: %X Packet: %X From: %d\r\n", Handler, PacketID, From);
		return 0;
	}

	InternalHandlers[From][PacketID] = (IRWPacketHandler)Handler;

	return 1;
}

int AddPacketHandler(int From, int PacketID, void *Handler)
{
	if(ExternalHandlers[From][PacketID] != 0)
	{
		LogPrint(ERROR_LOG, ":PACKETHANDLER:FULL: Handler: %X Packet: %X From: %d\r\n", Handler, PacketID, From);
		return 0;
	}

	ExternalHandlers[From][PacketID] = (IRWPacketHandler)Handler;
	LogPrint(ERROR_LOG, ":PACKETHANDLER:ADDED: Handler: %X Packet: %X From: %d\r\n", Handler, PacketID, From);
	return 1;
}

int AddPacketReader(int From, int PacketID, void *Handler)
{
	int i = 0;

	for(i = 0; i < MAX_PACKETREADERS; i++)
	{
		if(ReadOnlyHandlers[From][i][PacketID] == 0)
		{
			ReadOnlyHandlers[From][i][PacketID] = (IRWPacketReader)Handler;
			LogPrint(NOFILTER_LOG, ":PACKETHANDLER:READ:ADDED: Handler: %X Packet: %X From: %d Slot: %d\r\n", Handler, PacketID, From, i);
			return 1;
		}

	}

	LogPrint(ERROR_LOG, ":PACKETHANDLER:READ:FULL: Handler: %X Packet: %X From: %d\r\n", Handler, PacketID, From);
	return 0;
}

void HandleWorld(int Type, unsigned int Serial, int Idx, unsigned char *Buf, int Len)
{
	int i = 0;

	for(i = 0; i < MAX_WORLDCALLBACKS; i++)
	{
		if(!WorldCallbacks[i])
			break;

		WorldCallbacks[i](Type, Serial, Idx, Buf, Len);
	}

	return;
}

int HandlePacket(unsigned char *Buf, int Len, int From)
{
	int i = 0, PacketID = GetPacketID(Buf);

	if(InternalHandlers[From][PacketID])
		return InternalHandlers[From][PacketID](Buf, Len);

	for(i = 0; i < MAX_PACKETREADERS; i++)
	{
		if(!ReadOnlyHandlers[From][i][PacketID])
			break;

		ReadOnlyHandlers[From][i][PacketID](Buf, Len);
	}

	if(ExternalHandlers[From][PacketID])
		return ExternalHandlers[From][PacketID](Buf, Len);

	return SEND_PACKET;
}

int AddCommand(const char *Name, void *Handler)
{
	unsigned int i = 0;

	/* can't redefine a command */
	for(i = 0; i < CmdListSize; i++)
	{
		if(!strcmp(Commands[i].Command, Name))
			return 1;
	}

	/* name must always be present. and either alias or callback */
	if(Name == NULL || Handler == NULL)
		return 2;

	CmdListSize++;
    Commands = (IRWCmd*)realloc(Commands, CmdListSize*sizeof(IRWCmd));
	Commands[CmdListSize - 1].Command = Name;
	Commands[CmdListSize - 1].Handler = (IRWCommandHandler)Handler;

	return 0;
}

int AddAlias(const char *Name)
{
	unsigned int i = 0;

	/* can't redefine an alias */
	for(i = 0; i < AliasListSize; i++)
	{
		if(!strcmp(Aliases[i].Alias, Name))
			return 1;
	}

	/* name must always be present. and either alias or callback */
	if(Name == NULL)
		return 2;

	AliasListSize++;
    Aliases = (IRWAlias*)realloc(Aliases, AliasListSize*sizeof(IRWAlias));
	strcpy(Aliases[AliasListSize - 1].Alias, Name);
	Aliases[AliasListSize - 1].Value = INVALID_SERIAL;

	return 0;
}

int RemoveAlias(const char *Name)
{
	unsigned int i = 0, Idx = -1;

	for(i = 0; i < AliasListSize; i++)
	{
		if(!strcmp(Aliases[i].Alias, Name))
		{
			Idx = i;
			break;
		}
	}

	/* cant delete something that doesnt exists */
	if(Idx == -1)
		return 1;

	memset(Aliases[i].Alias, 0, 1024);

	return 0;
}

int SetAlias(const char *Name, unsigned int Value)
{
	unsigned int i = 0, Idx = -1;

	for(i = 0; i < AliasListSize; i++)
	{
		if(!strcmp(Aliases[i].Alias, Name))
		{
			Idx = i;
			break;
		}
	}

	/* cant do stuff with an alias we dont know */
	if(Idx == -1)
		return 1;

	Aliases[Idx].Value = Value;
	return 0;
}

/* returns the text value of an alias */
int GetAlias(const char *Name, unsigned int *Value)
{
	unsigned int i = 0, Idx = -1;

	for(i = 0; i < AliasListSize; i++)
	{
		if(!strcmp(Aliases[i].Alias, Name))
		{
			Idx = i;
			break;
		}
	}

	/* unknown alias */
	if(Idx == -1)
		return 1;

	*Value = Aliases[Idx].Value;
	return 0;
}

void ListCommands(const IRWCmd **List, unsigned int *Size)
{
	*List = Commands;
	*Size = CmdListSize;
	return;
}

void ListAliases(const IRWAlias **List, unsigned int *Size)
{
	*List = Aliases;
	*Size = AliasListSize;
	return;
}


/*******************************************************************************
* 
*   Commands handler and ArgToInt
* 
*******************************************************************************/

void HandleCommand(char *Text)
{
	char **Args = NULL;
	char *TokenBegin = NULL, *TokenEnd = NULL;
	unsigned int Idx = -1, i = 0, WordCount = 0;
	int InQuote = 0, Len = 0;

	/*
	* separate the command into words. the first being the command
	* ' ' is a word separator and '\'' unifies a word
	*/
	if(strlen(Text) == 0)
		return;

	/* parse the command and it's arguments */
	while(1)
	{
		/* if its the end of the text but we have an open token, add the text */
		if(TokenBegin && Text[i] == '\0')
		{
			TokenEnd = Text + i;
			WordCount++;
			/* allocate one more pointer */
			Args = (char**)realloc(Args, WordCount*sizeof(char**));
			/* allocate space for the text (the word) */
			Len = (int)(TokenEnd - TokenBegin);
			Args[WordCount-1] = (char*)malloc(Len + 1);
            /* copy the text */
            strncpy(Args[WordCount-1], TokenBegin, Len);
			Args[WordCount-1][Len] = '\0';

			break;
		}
		else if(Text[i] == '\0') /* if the text is simply over, stop */
			break;

		/* if we have no token waiting to be read, find one */
        if(TokenBegin == NULL)
		{
			while(1)
			{
				/* skip all tokens */
				if(Text[i] == '\'' || Text[i] == ' ' || Text[i] == '\"')
					i++;
				else /* found a non-token (text) */
					break;
			}

			/*
			* set the start of the text and
			* check if its a quote (allow spaces in the text)
			*/
			TokenBegin = Text + i;
			if(i != 0 && (Text[i-1] == '\'' || Text[i-1] == '\"'))
				InQuote = 1;
			else
				InQuote = 0;
		}

		/*
		* this search method will check char by char and skip (i++)
		* any non-tokens every loop.
		* it gets the pointer of a token and the token after it
		* everything between those pointers is a word
		* current tokens: ' ', '\'', '\"'
		*/

		/* if we aren't expecting quotes and this is a space, the space between the two ' ' is a word */
		if(TokenBegin && InQuote == 0 && Text[i] == ' ')
		{
			TokenEnd = Text + i;
			WordCount++;
			/* allocate one more pointer */
			Args = (char**)realloc(Args, WordCount*sizeof(char**));
			/* allocate space for the text (the word) */
			Len = (int)(TokenEnd - TokenBegin);
			Args[WordCount-1] = (char*)malloc(Len + 1);
            /* copy the text */
            strncpy(Args[WordCount-1], TokenBegin, Len);
			Args[WordCount-1][Len] = '\0';

			TokenBegin = NULL;
		}

		/* if we are expecting quotes and this is one, the space between them is a word */
		if(TokenBegin && InQuote == 1 && (Text[i] == '\'' || Text[i] == '\"'))
		{
			TokenEnd = Text + i;
			WordCount++;
			/* allocate one more pointer */
			Args = (char**)realloc(Args, WordCount*sizeof(char**));
			/* allocate space for the text (the word) */
			Len = (int)(TokenEnd - TokenBegin);
			Args[WordCount-1] = (char*)malloc(Len + 1);
            /* copy the text */
            strncpy(Args[WordCount-1], TokenBegin, Len);
			Args[WordCount-1][Len] = '\0';

			TokenBegin = NULL;
		}

		/* check the next character */
		i++;
	}

    for(i = 0; i < CmdListSize; i++)
	{
		if(!strcmp(Commands[i].Command, Args[0]))
            Idx = i;
	}

    if(Idx == -1)
		ClientPrintWarning("Command %s not found", Args[0]);
	else
		Commands[Idx].Handler(Args, WordCount);

	/* delete the word list */
	for(i = 0; i < WordCount; i++)
		free(Args[i]);

	free(Args);

	return;
}

int ArgToInt(char *Arg)
{
	unsigned int Value = 0;

	if(!strlen(Arg))
		return 0;

	/* check if its a serial (hexa or decimal) */
	if(!strncmp(Arg, "0x", 2) && (strlen(Arg) > 2 && isxdigit(Arg[2])))
		return strtol(Arg, NULL, 16);
	else if(isdigit(Arg[0]))
		return strtol(Arg, NULL, 10);

	/* if its not a serial, it should be an alias */
	GetAlias(Arg, &Value);
	/* if it wasnt an alias, the return is 0 */
	return Value;
}


/*******************************************************************************
* 
*   General packet hooks for commands, login, fixes and etc...
* 
*******************************************************************************/

int IRWServer_PredefinedMessage(unsigned char *Packet, int Len)
{
   char Name[20], Text[128];
   int ClilocID = 0;

   strncpy(Name, (const char*)(Packet + 18), 20);
   Name[20] = '\0';

   /* cliloc id to text... RTD: imagine an use for this :P */
   sprintf(Text, "cliloc# 0x%x", UnpackUInt16(Packet + 16));
   if((Packet[9] & 0x0F) == 6) JournalAdd(NULL, Text, JOURNAL_SYSMSG);
   else JournalAdd(Name, Text, JOURNAL_SPEECH);

   LogPrint(NOFILTER_LOG, "Cliloc text Type: %d %s: %s\r\n", Packet[9] & 0x0F, Name, Text);

   return SEND_PACKET;
} 

int IRWServer_Speech(unsigned char *Packet, int Len)
{
	char Name[30 + 1], *Text = NULL;

	strncpy(Name, (const char*)(Packet + 14), 30);
	Name[30] = '\0';

	Text = Packet + 44;

	/* add the text to the journal */
	if((Packet[9] & 0x0F) == JOURNAL_YOUSEE) JournalAdd("You see", Text, Packet[9] & 0x0F);
	else JournalAdd(Name, Text, Packet[9] & 0x0F);

	LogPrint(NOFILTER_LOG, "Server speech from %s: %s\r\n", Name, Text);

	return SEND_PACKET;
}


int IRWServer_UnicodeSpeech(unsigned char *Packet, int Len)
{
	char Name[30 + 1], *Text = NULL;

	strncpy(Name, (const char*)(Packet + 18), 30);
	Name[30] = '\0';

	/* grab the pointer and convert it to ascii */
	Text = (char*)malloc(Len - 48);
	UnicodeToAscii((char*)(Packet + 48), Len - 48, Text);

	/* add it to the journal */
	if((Packet[9] & 0x0F) == JOURNAL_YOUSEE) JournalAdd("You see", Text, Packet[9] & 0x0F);
	else JournalAdd(Name, Text, Packet[9] & 0x0F);

	LogPrint(NOFILTER_LOG, "Unicode server speech from %s: %s\r\n", Name, Text);
	free(Text);

	return SEND_PACKET;
}

int IRWClient_AsciiSpeech(unsigned char *Packet, int Len)
{
	LogPrint(NOFILTER_LOG, "Client speech (ascii): %s\r\n", Packet + 8);

	if(Packet[8] == COMMAND_PREFIX)
	{
		HandleCommand((char*)(Packet + 9));
		return EAT_PACKET;
	}

	return SEND_PACKET;
}

int IRWClient_UnicodeSpeech(unsigned char *Packet, int Len)
{
	char *Text = NULL;
	unsigned char *Ptr = NULL, *NewSpeechPacket = NULL;
	unsigned int KeywordCount = 0;
	unsigned int NewLen = 0, PacketLen = 0;
	int NumMatches = 0, TotalBits = 0;

	/* unicode - no keywords present */
	if((Packet[3] & 0xC0) == 0)
	{
		Text = (char*)malloc(Len - 12);

		UnicodeToAscii((char*)(Packet + 12), Len - 12, Text);
		LogPrint(NOFILTER_LOG, "Client speech (uni): %s\r\n", Text);

		if(Text[0] == COMMAND_PREFIX)
		{
			HandleCommand((char*)(Text + 1));
			free(Text);
			return EAT_PACKET;
		}

		free(Text);
	}

	/* ascii - keywords present */
	if((Packet[3] & 0xC0) != 0)
	{
		Ptr = Packet + 12; /* set the text pointer */
		KeywordCount = (Ptr[0]<<8) | Ptr[1];
		Ptr += 2; /* skip the keyword WORD */

		/* skip the keywords */
		NumMatches = KeywordCount >> 4;
		TotalBits = (NumMatches * 12) - 4;
		Ptr += TotalBits / 8;
		if(TotalBits % 8)
			Ptr++;

		LogPrint(NOFILTER_LOG, "Client speech (w/ keywords): %s\r\n", Ptr + 1);
		if(Ptr[0] == COMMAND_PREFIX)
		{
			HandleCommand((char*)(Ptr + 1));
			return EAT_PACKET;
		}
	}

	return SEND_PACKET;
}

int IRWClient_Login(unsigned char *Packet, int Len)
{
	char Login[1024];
	char Pwd[1024];
	char Loginp[62];

	memset(Login, 0, sizeof(Login));
	memset(Pwd, 0, sizeof(Pwd));

	GetIRWProfileString(NULL, "IRW", "Username", Login, sizeof(Login));
	GetIRWProfileString(NULL, "IRW", "Password", Pwd, sizeof(Pwd));

	strncpy((char*)(Packet + 1), Login, 30);
	strncpy((char*)(Packet + 31), Pwd, 30);

	if(Packet[0] == 0xCF)
	{
		Loginp[0] = 0x80;
		strncpy((char*)(Loginp + 1), Login, 30);
		strncpy((char*)(Loginp + 31), Pwd, 30);
		SendToServer(Loginp, 62);

		return EAT_PACKET;
	}

	return SEND_PACKET;
}

int IRWClient_GameLogin(unsigned char *Packet, int Len)
{
	char Login[1024];
	char Pwd[1024];

	memset(Login, 0, sizeof(Login));
	memset(Pwd, 0, sizeof(Pwd));

	GetIRWProfileString(NULL, "IRW", "Username", Login, sizeof(Login));
	GetIRWProfileString(NULL, "IRW", "Password", Pwd, sizeof(Pwd));

	strncpy((char*)(Packet + 5), Login, 30);
	strncpy((char*)(Packet + 35), Pwd, 30);

	return SEND_PACKET;
}

int IRWServer_GeneralInfo(unsigned char *Packet, int Len)
{
	/*
	* if POL is set to 2.0.0 encryption, for some reason it sends a
	* set hued gold/britannia map... Hey Racalac, tell me why will ya?
	* the problem is that newer clients (40xx for sure) recognize it as a map
	* change packet and they kinda screw up the world
	* 0000: bf 06 00 00 08 01
	* RTD and RTS: I can't believe I wasted 3 hours of code revising
	* just to find out it was pol's fault... grrr
	*/
	if(Len >= 6 && Packet[4] == 0x08 && Packet[5] == 0x01)
        Packet[5] = 0;

	return SEND_PACKET;
}

void IRWCmd_Say(char **Arg, int ArgCount)
{
	unsigned char *SpeechPacket = NULL;
	int Len = 0, Color = 0x3b6, Font = 3;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: say <text> [color] [font]");
		return;
	}
	if(ArgCount >= 3)
		Color = ArgToInt(Arg[2]);
	if(ArgCount >= 4)
		Font = ArgToInt(Arg[3]);

	Len = (int)(8 + strlen(Arg[1]) + 1);
	SpeechPacket = (unsigned char *)malloc(Len);

	SpeechPacket[0] = 0x03;
	PackUInt16(SpeechPacket + 1, Len);
	SpeechPacket[3] = 0; /* normal talk mode */
	PackUInt16(SpeechPacket + 4, Color); /* kept the color the same as injection's */
	PackUInt16(SpeechPacket + 6, Font);
	strncpy((char*)(SpeechPacket + 8), Arg[1], strlen(Arg[1]));
	SpeechPacket[Len - 1] = '\0';

	SendToServer(SpeechPacket, Len);
	free(SpeechPacket);

	return;
}


/*******************************************************************************
* 
*   IRW commands
* 
*******************************************************************************/

void IRWCmd_Quit(char **Arg, int ArgCount)
{
	ExitProcess(0);
	return;
}

void IRWCmd_Help(char **Arg, int ArgCount)
{
	ClientPrint("IRW's help :P");

	return;
}

void IRWCmd_Set(char **Arg, int ArgCount)
{
	int Serial = 0;
	unsigned int i = 0, ListSize = 0;
	IRWAlias *AliasList = NULL;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: set <choice> <value>");
		ClientPrint("Current choices are:");
		ClientPrint("prefix");

		/* list the current aliases */
		ListAliases((const IRWAlias**)&AliasList, &ListSize);
		for(i = 0; i < ListSize; i++)
			ClientPrint(AliasList[i].Alias);

		return;
	}

	if(!strcmp(Arg[1], "prefix"))
	{
		if(ArgCount < 3)
		{
			ClientPrint("Usage: set prefix <new prefix>");
			return;
		}

		COMMAND_PREFIX = Arg[2][0];
		ClientPrint("Prefix set to: %c", Arg[2][0]);
		return;
	}

	if(!strcmp(Arg[1], "lasttarget"))
	{
		if(ArgCount > 2)
		{
            Serial = ArgToInt(Arg[2]);

			if(Serial != 0)
				SetLastTarget(Serial);
			else
				ClientPrint("Invalid serial from: %s", Arg[2]);

			return;
		}

		RequestTarget(TARGET_OBJECT, IRWTarget_SetLastTarget);
		return;
	}

	if(!strcmp(Arg[1], "catchbag"))
	{
		if(ArgCount > 2)
		{
			Serial = ArgToInt(Arg[2]);

			if(Serial != 0)
				SetCatchBag(Serial);
			else
				ClientPrint("Invalid serial from: %s", Arg[2]);

			return;
		}

		RequestTarget(TARGET_OBJECT, IRWTarget_SetCatchBag);
		return;
	}

	if(SetAlias(Arg[1], ArgToInt(Arg[2])))
		ClientPrint("Could not set a value to: %s", Arg[1]);
	else
		ClientPrint("Alias %s is now %d", Arg[1], ArgToInt(Arg[2]));

	return;
}

/* the target handler already sets the target as last target, so don't bother doing anything */
void IRWTarget_SetLastTarget(unsigned int Serial, unsigned short X, unsigned short Y, int Z)
{
    if(Serial == INVALID_SERIAL)
	{
		ClientPrint("Choose a valid target to set as last target");
		return;
	}

	return;
}

void IRWTarget_SetCatchBag(unsigned int Serial, unsigned short X, unsigned short Y, int Z)
{
    if(Serial == INVALID_SERIAL)
	{
		ClientPrint("Choose a valid target to set as catch bag");
		return;
	}

	SetCatchBag(Serial);
	return;
}

void IRWCmd_ListPlugins(char **Arg, int ArgCount)
{
	IRWPlugin *PluginList = NULL;
	unsigned int i = 0, ListSize = 0;

	ClientPrint("Currently loaded plugins:");

	ListPlugins((const IRWPlugin**)&PluginList, &ListSize);
	for(i = 0; i < ListSize; i++)
		ClientPrint("%s 0x%08X", PluginList[i].Name, PluginList[i].BaseAddr);

	return;
}

void IRWCmd_ListAliases(char **Arg, int ArgCount)
{
	IRWAlias *AliasList = NULL;
	unsigned int i = 0, ListSize = 0;

	ClientPrint("Aliases list:");

	ListAliases((const IRWAlias**)&AliasList, &ListSize);
	for(i = 0; i < ListSize; i++)
		ClientPrint("%s 0x%08X", AliasList[i].Alias, AliasList[i].Value);

	return;
}
