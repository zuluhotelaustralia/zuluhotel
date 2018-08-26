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
* 	Dec 10th, 2004 -- weather plugin. handles weather and light changes
* 
\******************************************************************************/

#include <windows.h>
#include "Main.h"
#include "irw.h"


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;

static BOOL ControlWeather = FALSE;
static int LastSeason = 0; /* last season the server sent */
static BOOL ChangeLight = FALSE;
static int LastLight = 0; /* last light intensity the server sent */
static int LightIntensity = 0;


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

	AddPacketHandler(SERVER_MESSAGE, 0x4f, HookServer_Light);
	AddPacketHandler(SERVER_MESSAGE, 0x65, HookServer_Weather);
	AddPacketHandler(SERVER_MESSAGE, 0xbc, HookServer_Season);
	AddPacketHandler(SERVER_MESSAGE, 0x53, HookServer_IdleWarning);

	AddCommand("weather", Command_Weather);
	AddCommand("setweather", Command_SetWeather);
	AddCommand("setseason", Command_SetSeason);
	AddCommand("light", Command_Light);

	GetIRWProfileInt(NULL, "Enviroment", "ControlWeather", &ControlWeather);
	GetIRWProfileInt(NULL, "Enviroment", "ChangeLight", &ChangeLight);
	GetIRWProfileInt(NULL, "Enviroment", "LightIntensity", &LightIntensity);


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
	strncpy(Text, "Handle weather, season and light changes", Size);
	return;
}


void Command_Weather(char **Arg, int ArgCount)
{
	if(GetPlayerSerial() == INVALID_SERIAL) return;

	GetIRWProfileInt(NULL, "Enviroment", "ControlWeather", &ControlWeather);

	if(ArgCount == 1)
		ControlWeather = ControlWeather ? FALSE : TRUE;

	if(ArgCount >= 2)
	{
		if(!strcmp(Arg[1], "on"))
			ControlWeather = TRUE;
		else if(!strcmp(Arg[1], "off"))
			ControlWeather = FALSE;
		else
		{
			ClientPrint("Usage: weather [value]");
			ClientPrint("Value is optional. It can be: on off");
			return;
		}
	}

	SetIRWProfileInt(NULL, "Enviroment", "ControlWeather", ControlWeather);
	ClientPrint("Control weather is now: %s", ControlWeather == TRUE ? "on" : "off");
	return;
}

void Command_SetWeather(char **Arg, int ArgCount)
{
	char Buf[4] = { 0x65, 0x00, 0x0F, 0x00 };

	if(GetPlayerSerial() == INVALID_SERIAL) return;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: setweather [value]");
		ClientPrint("Sets the current weather to the specified value");
		ClientPrint("Value can be 0-3 or one of the following:");
		ClientPrint("rain storm1 snow storm2");

		return;
	}

	if(!strcmp(Arg[1], "rain"))
		Buf[1] = 0;
	else if(!strcmp(Arg[1], "storm1"))
		Buf[1] = 1;
	else if(!strcmp(Arg[1], "snow"))
		Buf[1] = 2;
	else if(!strcmp(Arg[1], "storm2"))
		Buf[1] = 3;
	else
		Buf[1] = ArgToInt(Arg[1]);

	SendToClient(Buf, sizeof(Buf));

	return;
}

void Command_SetSeason(char **Arg, int ArgCount)
{
	char Buf[3] = { 0xbc, 0x00, 0x01 };

	if(GetPlayerSerial() == INVALID_SERIAL) return;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: setseason [value]");
		ClientPrint("Sets the current season to the specified value");
		ClientPrint("Value can be 0-5 or one of the following:");
		ClientPrint("spring summer fall winter desolation default");

		return;
	}

	if(!strcmp(Arg[1], "spring"))
		Buf[1] = 0;
	else if(!strcmp(Arg[1], "summer"))
		Buf[1] = 1;
	else if(!strcmp(Arg[1], "fall"))
		Buf[1] = 2;
	else if(!strcmp(Arg[1], "winter"))
		Buf[1] = 3;
	else if(!strcmp(Arg[1], "desolation"))
		Buf[1] = 4;
	else if(!strcmp(Arg[1], "default") || ArgToInt(Arg[1]) == 5)
		Buf[1] = LastSeason & 0xff;
	else
		Buf[1] = ArgToInt(Arg[1]);

	SendToClient(Buf, 3);

	return;
}

void Command_Light(char **Arg, int ArgCount)
{
	/* a global light packet */
	unsigned char Buf[2] = { 0x4f, 0x00 };

	if(GetPlayerSerial() == INVALID_SERIAL) return;

	GetIRWProfileInt(NULL, "Enviroment", "ChangeLight", &ChangeLight);
	GetIRWProfileInt(NULL, "Enviroment", "LightIntensity", &LightIntensity);

	if(ArgCount == 1)
		ChangeLight = ChangeLight ? FALSE : TRUE;

	if(ArgCount >= 2)
	{
		if(!strcmp(Arg[1], "on"))
			ChangeLight = TRUE;
		else if(!strcmp(Arg[1], "off"))
			ChangeLight = FALSE;
		else
			LightIntensity = ArgToInt(Arg[1]);
	}

	/* send a new light packet to the client */
	if(ChangeLight)
	{
		Buf[1] = LightIntensity & 0xff;
		SendToClient(Buf, 2);
	}
	else /* if it's off, send the last light intensity */
	{
		Buf[1] = LastLight & 0xff;
		SendToClient(Buf, 2);
	}

	ClientPrint("Change light is now: %s", ChangeLight == TRUE ? "on" : "off");
	ClientPrint("Light intensity: %d", LightIntensity);

	SetIRWProfileInt(NULL, "Enviroment", "ChangeLight", ChangeLight);
	SetIRWProfileInt(NULL, "Enviroment", "LightIntensity", LightIntensity);
	return;
}

int HookServer_Season(unsigned char *Packet, int Len)
{
	/* if it's a season change, save the season */
	if(Packet[2] == 1)
		LastSeason = Packet[1] & 0xff;

	return SEND_PACKET;
}

int HookServer_Weather(unsigned char *Packet, int Len)
{
	if(ControlWeather)
		return EAT_PACKET;

	return SEND_PACKET;
}

int HookServer_Light(unsigned char *Packet, int Len)
{
	LastLight = Packet[1] & 0xff;

	if(ChangeLight)
		return EAT_PACKET;

	return SEND_PACKET;
}

int HookServer_IdleWarning(unsigned char *Packet, int Len)
{
	unsigned char SClick[5] = { 0x09, 0x00, 0x00, 0x00, 0x00 };

	/* if its an idle warning send a single click on the player */
	if(Packet[1] == 0x07)
	{
		PackUInt32(SClick + 1, GetPlayerSerial());
		SendToServer(SClick, 5);
		return EAT_PACKET;
	}

	return SEND_PACKET;
}