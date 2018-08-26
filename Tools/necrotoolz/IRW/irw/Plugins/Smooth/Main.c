/******************************************************************************\
* 
* 
*  Copyright (C) 2004 Nelson 'Illusion' Hargreaves
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
* 	Dec 18th, 2004 -- enables smoothwalk and asks for resync. first IRW plugin
* 
\******************************************************************************/

#include <windows.h>
#include "Main.h"
#include "irw.h"


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;

static BOOL SmoothWalk = FALSE;

BOOL APIENTRY DllMain(HINSTANCE hDLLInst, DWORD fdwReason, LPVOID lpvReserved)
{
    switch(fdwReason)
    {
        case DLL_PROCESS_ATTACH:
		{
			DisableThreadLibraryCalls(hDLLInst);
			DLLInst = hDLLInst;
		}break;
        case DLL_PROCESS_DETACH:
		{
			/* to make sure the dll wasn't simply loaded and InitPlugin() wasnt called */
			if(Inited == TRUE)
				UnloadPlugin();
		}break;
        case DLL_THREAD_ATTACH:		break;
        case DLL_THREAD_DETACH:		break;
    }
    return TRUE;
}


int InitPlugin(void)
{
	if(Inited == TRUE)
		return FALSE;

	GetIRWProfileInt(NULL, "Smooth", "SmoothWalk", &SmoothWalk);

	AddPacketHandler(SERVER_MESSAGE, 0x22, HookServer_WalkAck);
	AddPacketHandler(CLIENT_MESSAGE, 0x02, HookClient_WalkRequest);
	
	AddCommand("smoothwalk", Command_SmoothWalk);
	AddCommand("resync", Command_Resync);
	
	Inited = TRUE;

	return TRUE;
}

void UnloadPlugin(void)
{
	if(Inited == FALSE)
		return;	

	return;
}

void GetPluginInfo(char *Text, int Size)
{
	strncpy(Text, "Handler for the walk request packet, infamous smoothwalk", Size);
	return;
}


int HookServer_WalkAck(unsigned char *Packet, int Len)
{
	/* this way the client won't receive two movement acks */
	if(SmoothWalk)
		return EAT_PACKET;

	return SEND_PACKET;
}

int HookClient_WalkRequest(unsigned char *Packet, int Len)
{
	if(SmoothWalk)
    {
		unsigned char MoveAck[3]= { 0x22, 0x00, 0x00 };
		MoveAck[1] = Packet[2];
		SendToClient(MoveAck, 3);
    }

	return SEND_PACKET;
}

void Command_SmoothWalk(char **Arg, int ArgCount)
{
	GetIRWProfileInt(NULL, "Smooth", "SmoothWalk", &SmoothWalk);

	if(ArgCount == 1)
		SmoothWalk = SmoothWalk ? FALSE : TRUE;

	if(ArgCount >= 2)
	{
		if(!strcmp(Arg[1], "on"))
			SmoothWalk = TRUE;
		else if(!strcmp(Arg[1], "off"))
			SmoothWalk = FALSE;
		else
		{
			ClientPrint("Usage: smoothwalk on/off");
			return;
		}
	}

	SetIRWProfileInt(NULL, "Smooth", "SmoothWalk", SmoothWalk);
	ClientPrint("SmoothWalk is now: %s", SmoothWalk == TRUE ? "on" : "off");

	return;
}

void Command_Resync(char **Arg, int ArgCount)
{
	unsigned char ResyncReq[3]= { 0x22, 0x00, 0x00 };
	
	if(!GetPlayerSerial()) return;
	
	ClientPrint("Sending resync request..");
	SendToServer(ResyncReq, 3);     

	return;
}
