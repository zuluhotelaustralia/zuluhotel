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
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
* 
* 	Sept 18th, 2004 -- packet handler module
* 
\******************************************************************************/

#ifndef _UOPACKETHANDLER_H_INCLUDED
#define _UOPACKETHANDLER_H_INCLUDED

typedef void (*IRWWorldCallback) (int, unsigned int, int, unsigned char*, int);
typedef int (*IRWPacketHandler) (unsigned char*, int);
typedef void (*IRWPacketReader) (unsigned char*, int);
typedef void (*IRWCommandHandler) (char **, int);

typedef struct tagIRWCmd
{
	const char *Command;
	IRWCommandHandler Handler;
}IRWCmd;

typedef struct tagIRWAlias
{
	char Alias[1024];
	unsigned int Value;
}IRWAlias;


/* INTERNALS */
void InitAliases(void);
void InitPacketHandler(void);
int AddInternalPacketHandler(int From, int PacketID, void *Handler);

/* packet handlers */
int IRWServer_PredefinedMessage(unsigned char *Packet, int Len);
int IRWServer_Speech(unsigned char *Packet, int Len);
int IRWServer_UnicodeSpeech(unsigned char *Packet, int Len);
int IRWClient_AsciiSpeech(unsigned char *Packet, int Len);
int IRWClient_UnicodeSpeech(unsigned char *Packet, int Len);
int IRWClient_Login(unsigned char *Packet, int Len);
int IRWClient_GameLogin(unsigned char *Packet, int Len);
int IRWServer_GeneralInfo(unsigned char *Packet, int Len);
void IRWCmd_Say(char **Arg, int ArgCount);
void IRWCmd_Quit(char **Arg, int ArgCount);
void IRWCmd_Help(char **Arg, int ArgCount);
void IRWCmd_Set(char **Arg, int ArgCount);
void IRWCmd_ListPlugins(char **Arg, int ArgCount);
void IRWCmd_ListAliases(char **Arg, int ArgCount);
void IRWTarget_SetLastTarget(unsigned int Serial, unsigned short X, unsigned short Y, int Z);
void IRWTarget_SetCatchBag(unsigned int Serial, unsigned short X, unsigned short Y, int Z);


/* EXPORTS */
/* packet handler functions */
int AddWorldCallback(void *Callback);
int AddPacketHandler(int From, int PacketID, void *Handler);
int AddPacketReader(int From, int PacketID, void *Handler);
void HandleWorld(int Type, unsigned int Serial, int Idx, unsigned char *Buf, int Len);
int HandlePacket(unsigned char *Buf, int Len, int From);

/* command stuff */
int AddCommand(const char *Name, void *Handler);
void HandleCommand(char *Text);
int ArgToInt(char *Arg);

/* alias functions */
int AddAlias(const char *Name);
int RemoveAlias(const char *Name);
int SetAlias(const char *Name, unsigned int Value);
int GetAlias(const char *Name, unsigned int *Value);

/* these functions will return direct access to the lists, be careful! */
void ListCommands(const IRWCmd **List, unsigned int *Size);
void ListAliases(const IRWAlias **List, unsigned int *Size);

#endif
