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
* 	Sept 24th, 2004 -- target handler module
*   handles target requests from client, server and IRW
* 
\******************************************************************************/


#include <windows.h>
#include "UOTarget.h"
#include "Logger.h"
#include "UOWorld.h"
#include "UONetwork.h"
#include "UOProtocol.h"
#include "UOPacketHandler.h"

static BOOL IRWRequestedTarget = FALSE; /* if irw requested a target, handle it. if we're supposed to cancel, ignore it when the client sends */
static BOOL ServerRequestedTarget = FALSE; /* if the server sent a target request and then irw did, send a cancel reply to the server */
static BOOL CancelTarget = FALSE;
static IRWTarget TargetHandler = NULL;
static int TargetSequence = 0;

/* automatically reply a target request */
static BOOL WaitTargetRequest = FALSE;
static int WaitTargetType = 0;
static unsigned int WaitTargetSerial = INVALID_SERIAL;
static unsigned short WaitTargetGraphic = INVALID_GRAPHIC;
static unsigned short WaitTargetX = INVALID_XY, WaitTargetY = INVALID_XY;
static int WaitTargetZ = 0;


/*******************************************************************************
* 
*   Bools
* 
*******************************************************************************/

unsigned int GetLastTarget(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("lasttarget", &Value);
	return Value;
}

unsigned short GetLastTileX(void)
{
	unsigned int Value = INVALID_XY;
	GetAlias("lasttilex", &Value);
	return Value;
}

unsigned short GetLastTileY(void)
{
	unsigned int Value = INVALID_XY;
	GetAlias("lasttiley", &Value);
	return Value;
}

int GetLastTileZ(void)
{
	signed int Value = 0;
	GetAlias("lasttilex", &Value);
	return Value & 0xFFFF;
}

void SetLastTarget(unsigned int Serial){ SetAlias("lasttarget", Serial); return; }
void SetLastTile(unsigned short X, unsigned short Y, int Z){	SetAlias("lasttilex", X); SetAlias("lasttiley", Y); SetAlias("lasttilez", Z & 0xFFFF); return; }
BOOL ExpectTarget(void){ return ((IRWRequestedTarget == TRUE) | (ServerRequestedTarget == TRUE)); }


/*******************************************************************************
* 
*   Target cleanup, resquet target, target packet handlers and waittarget stuff
* 
*******************************************************************************/

void CleanTarget(void)
{
	IRWRequestedTarget = CancelTarget = ServerRequestedTarget = FALSE;

	SetAlias("lasttaget", INVALID_SERIAL);
	SetAlias("lasttilex", INVALID_XY);
	SetAlias("lasttiley", INVALID_XY);
	SetAlias("lasttilez", 0);
	TargetSequence = 0;
	TargetHandler = NULL;

	return;
}

void WaitTarget(int Type, unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z)
{
	GameObject Tmp;

	if(Type == TARGET_OBJECT)
	{
		GetObjectInfo(Serial, INVALID_IDX, &Tmp);

		WaitTargetRequest = TRUE;
		WaitTargetType = TARGET_OBJECT;
		WaitTargetSerial = Serial;
		WaitTargetGraphic = Tmp.Graphic;
		WaitTargetX = WaitTargetY = WaitTargetZ = INVALID_XY;
	}
	else if(Type == TARGET_TILE)
	{
		WaitTargetRequest = TRUE;
		WaitTargetType = TARGET_TILE;
		WaitTargetSerial = INVALID_SERIAL;
		WaitTargetGraphic = INVALID_GRAPHIC;
		WaitTargetX = X;
		WaitTargetY = Y;
		WaitTargetZ = Z;
	}

	ClientPrint("Waiting for target to reply");

	return;
}

void RequestTarget(int Type, void *Handler)
{
	if(Handler == NULL)
	{
		LogPrint(NOFILTER_LOG | ERROR_LOG, ":TARGET: Target handler request to NULL\r\n");
		ClientPrintWarning("A null target handler was given.. suka :P");
		return;
	}

	if(IRWRequestedTarget)
		ClientPrintWarning("Cancelling previous IRW target by override");

	if(CancelTarget)
		ClientPrintWarning("Cancelling target cancel");

	/* send an invalid selection to the server */
	if(ServerRequestedTarget)
	{
		ClientPrintWarning("Cancelling SERVER target by override");
		ServerRequestedTarget = FALSE;
		CancelTargetRequest(TargetSequence);
	}

	IRWRequestedTarget = TRUE;
	CancelTarget = FALSE;
	TargetHandler = (IRWTarget)Handler;

	if(!WaitTargetRequest) /* if not waiting to reply, send a target request to the client */
		TargetRequest(Type, 0);
	else /* auto reply */
	{
		ClientPrintWarning("Replying IRW target");
		TargetHandler(WaitTargetSerial, WaitTargetGraphic, WaitTargetX, WaitTargetY, WaitTargetZ);

		WaitTargetRequest = FALSE;
		IRWRequestedTarget = FALSE;
		TargetHandler = NULL;
	}

	return;
}

int IRWClient_Target(unsigned char *Packet, int Len)
{
    unsigned int Serial = INVALID_SERIAL;
	unsigned short Graphic = INVALID_GRAPHIC, X = INVALID_XY, Y = INVALID_XY;
	int Z = INVALID_XY, Type = 0;

	/* get the packet info */
	Type = Packet[1] & 0xff; /* 0 == object 1 == xyz */
	if(Type == TARGET_OBJECT)
	{
		Serial = UnpackUInt32(Packet + 7);
		Graphic = UnpackUInt16(Packet + 17);

		if(Serial != INVALID_SERIAL)
			SetAlias("lasttarget", Serial);
	}
	else if(Type == TARGET_TILE)
	{
		X = UnpackUInt16(Packet + 11);
		Y = UnpackUInt16(Packet + 13);
		Z = UnpackUInt16(Packet + 15);
		Graphic = UnpackUInt16(Packet + 17);

		if(X != INVALID_XY)
		{
			SetAlias("lasttilex", X);
			SetAlias("lasttiley", Y);
			SetAlias("lasttilez", Z);
		}
	}
	else
		LogPrint(WARNING_LOG, ":TARGET: Unidentified type: %d\r\n", Type);

	/* this way the client won't send an out of sequence target request */
	if(!IRWRequestedTarget && !ServerRequestedTarget)
	{
		ClientPrintWarning("Received an out of sequence target");
		return EAT_PACKET;
	}

	/* if we have a target request waiting */
	if(IRWRequestedTarget)
	{
		if(CancelTarget)
			ClientPrintWarning("Cancelling target selection");
		else
			TargetHandler(Serial, Graphic, X, Y, Z);

		IRWRequestedTarget = FALSE;
		CancelTarget = FALSE;
		TargetHandler = NULL;

		return EAT_PACKET;
	}
	else if(CancelTarget) /* (normal client->server target) if this target is to be cancelled */
	{
		CancelTargetRequest(TargetSequence);

		CancelTarget = FALSE;
		ServerRequestedTarget = FALSE;
		return EAT_PACKET;
	}

	ServerRequestedTarget = FALSE;
	return SEND_PACKET;
}

int IRWServer_Target(unsigned char *Packet, int Len)
{
	/* cancel IRW target request if the server requests a target */
	if(IRWRequestedTarget)
	{
		ClientPrintWarning("Cancelling target request by server");
		IRWRequestedTarget = FALSE;
		TargetHandler = NULL;
	}

	/* if we were going to cancel the last target reply, forget about it */
	if(CancelTarget)
	{
		ClientPrintWarning("Cancelling target cancel");
		CancelTarget = FALSE;
	}

	/* auto reply if necessary */
	if(WaitTargetRequest)
	{
		ClientPrintWarning("Replying server target");

		if(WaitTargetType == TARGET_OBJECT)
			TargetReplyObj(WaitTargetSerial, WaitTargetGraphic, UnpackUInt32(Packet + 2));
		else if(WaitTargetType == TARGET_TILE)
			TargetReplyTile(WaitTargetGraphic, WaitTargetX, WaitTargetY, WaitTargetZ, UnpackUInt32(Packet + 2));
		
		WaitTargetRequest = FALSE;
		return EAT_PACKET;
	}

	/* if not, just send the target request to the client */
	ServerRequestedTarget = TRUE;
	TargetSequence = UnpackUInt32(Packet + 2);
	return SEND_PACKET;
}

void IRWCmd_CancelTarget(char **Arg, int ArgCount)
{
	if(!IRWRequestedTarget && !ServerRequestedTarget)
		ClientPrintWarning("Not waiting for a target reply. Can't cancel.");
	else /* if we have a target req (from irw or server), signal to cancel it */
	{
		CancelTarget = TRUE; /* the next target selection will be cancelled */
		ClientPrintWarning("Waiting for target selection to cancel");
	}

	return;
}

void IRWCmd_WaitTarget(char **Arg, int ArgCount)
{
	if(ArgCount < 2)
	{
		RequestTarget(TARGET_OBJECT, IRWTarget_WaitTarget);
		return;
	}

	if(ArgCount == 2)
		WaitTarget(TARGET_OBJECT, ArgToInt(Arg[1]), INVALID_GRAPHIC, INVALID_XY, INVALID_XY, INVALID_XY);
	else if(ArgCount == 3)
		WaitTarget(TARGET_OBJECT, ArgToInt(Arg[1]), ArgToInt(Arg[2]), INVALID_XY, INVALID_XY, INVALID_XY);

	return;
}

void IRWTarget_WaitTarget(unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z)
{
	if(Serial == INVALID_SERIAL)
	{
		ClientPrintWarning("You must select a valid target");
		return;
	}

	WaitTarget(TARGET_OBJECT, Serial, Graphic, INVALID_XY, INVALID_XY, INVALID_XY);

	return;
}

void IRWCmd_WaitTargetGraphic(char **Arg, int ArgCount)
{
	unsigned short Graphic = INVALID_GRAPHIC, Color = INVALID_COLOR;
	unsigned int Serial = INVALID_SERIAL;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: waittargetgraphic <graphic> [color]");
		return;
	}

	Graphic = ArgToInt(Arg[1]);
	if(ArgCount == 3)
		Color = ArgToInt(Arg[2]);

	Serial = FindItemInContainer(Graphic, Color, GetPlayerSerial());

	if(Serial == INVALID_SERIAL)
	{
		ClientPrintWarning("Could not find graphic 0x%04X in backpack", Graphic);
		return;
	}

	WaitTarget(TARGET_OBJECT, Serial, Graphic, INVALID_XY, INVALID_XY, INVALID_XY);

	return;
}

void IRWCmd_WaitTargetTile(char **Arg, int ArgCount)
{
	if(ArgCount < 4)
	{
		RequestTarget(TARGET_OBJECT, IRWTarget_WaitTargetTile);
		return;
	}

	if(ArgCount == 4)
		WaitTarget(TARGET_TILE, INVALID_SERIAL, INVALID_GRAPHIC, ArgToInt(Arg[1]), ArgToInt(Arg[2]), ArgToInt(Arg[3]));
	else if(ArgCount == 5)
		WaitTarget(TARGET_TILE, INVALID_SERIAL, ArgToInt(Arg[4]), ArgToInt(Arg[1]), ArgToInt(Arg[2]), ArgToInt(Arg[3]));

	return;
}

void IRWTarget_WaitTargetTile(unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z)
{
	if(X == INVALID_XY)
	{
		ClientPrintWarning("You must select a valid target");
		return;
	}

	WaitTarget(TARGET_TILE, INVALID_SERIAL, Graphic, X, Y, Z);

	return;
}

void IRWCmd_CancelWaitTarget(char **Arg, int ArgCount)
{
	if(!WaitTargetRequest)
		ClientPrintWarning("There is no target wait to cancel");
	else
	{
		ClientPrintWarning("Canceling target wait");
		WaitTargetRequest = FALSE;
	}


	return;
}


/*******************************************************************************
* 
*   Interaction functions
* 
*******************************************************************************/

void TargetRequest(int Type, int Sequence)
{
	unsigned char Target[19];

	memset(Target, 0, sizeof(Target));

	Target[0] = 0x6c;
	Target[1] = Type;
	PackUInt32(Target + 2, Sequence);

	SendToClient(Target, 19);

	return;
}

void TargetReplyObj(unsigned int Serial, unsigned short Graphic, int Sequence)
{
	unsigned char Target[19];

	memset(Target, 0, sizeof(Target));

	Target[0] = 0x6c;
	Target[1] = TARGET_OBJECT;
	PackUInt32(Target + 2, Sequence);
	PackUInt32(Target + 7, Serial);
	PackUInt16(Target + 11, INVALID_XY);
	PackUInt16(Target + 13, INVALID_XY);
	PackUInt16(Target + 15, INVALID_XY);
	PackUInt16(Target + 17, Graphic);

	SendToServer(Target, 19);

	return;
}

void TargetReplyTile(unsigned short Graphic, unsigned short X, unsigned short Y, int Z, int Sequence)
{
	unsigned char Target[19];

	memset(Target, 0, sizeof(Target));

	Target[0] = 0x6c;
	Target[1] = TARGET_TILE;
	PackUInt32(Target + 2, Sequence);
	PackUInt32(Target + 7, INVALID_SERIAL);
	PackUInt16(Target + 11, X);
	PackUInt16(Target + 13, Y);
	PackUInt16(Target + 15, Z);
	PackUInt16(Target + 17, Graphic);

	SendToServer(Target, 19);

	return;
}

void CancelTargetRequest(int Sequence)
{
	unsigned char Target[19];

	memset(Target, 0, sizeof(Target));

	Target[0] = 0x6c;
	PackUInt32(Target + 2, Sequence);
	PackUInt16(Target + 11, INVALID_XY);
	PackUInt16(Target + 13, INVALID_XY);

	SendToServer(Target, 19);

	return;
}
