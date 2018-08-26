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
* 	Dec 13th, 2004 -- info plugin. gives info about a target
* 
\******************************************************************************/


#include <windows.h>
#include <commctrl.h>
#include <stdio.h>
#include "Main.h"
#include "LayersDlg.h"
#include "PlayersDlg.h"
#include "resource.h"
#include "irw.h"


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;
static HWND LayersWnd = NULL;
static HWND PlayersWnd = NULL;

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

	Inited = TRUE;

	AddCommand("info", Command_Info);
	AddCommand("tileinfo", Command_TileInfo);

	/* not quite sure if this is needed */
	/* InitCommonControls(); */
	LayersWnd = CreateDialog(DLLInst, MAKEINTRESOURCE(DIALOG_LAYERINFO), GetMainTabWindow(), (DLGPROC)LayersDlgProc);
	PlayersWnd = CreateDialog(DLLInst, MAKEINTRESOURCE(DIALOG_PLAYERINFO), GetMainTabWindow(), (DLGPROC)PlayersDlgProc);

	if(LayersWnd == NULL || PlayersWnd == NULL)
		MBOut("INFO PLUGIN", "Could not create the dialog"); 
	else
	{
		AddTab(LayersWnd, "Layers", FALSE);
		AddTab(PlayersWnd, "Players", FALSE);
	}

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
	strncpy(Text, "Gives information about a target and layers", Size);
	return;
}

void Command_TileInfo(char **Arg, int ArgCount)
{
	ClientPrint("Choose a spot on the ground");
	RequestTarget(TARGET_TILE, &Target_TileInfo);

	return;
}

void Command_Info(char **Arg, int ArgCount)
{
	ClientPrint("Choose your target");
	RequestTarget(TARGET_OBJECT, &Target_Info);

	return;
}

void Target_TileInfo(unsigned int Serial, unsigned short X, unsigned short Y, int Z)
{
	GameObject Obj;

	if(Serial == INVALID_SERIAL)
		ClientPrint("X: %d Y: %d Z: %d", X, Y, Z);
	else
	{
		if(GetObjectInfo(Serial, INVALID_IDX, &Obj) == OBJECT_NOTFOUND)
		{
			ClientPrintWarning("The targeted object does not exist in the world list... wtf");
			return;
		}

		ClientPrint("You targeted an object, serial: 0x%08X", Serial);
		ClientPrint("Position: X: %d Y: %d Z: %d", Obj.X, Obj.Y, Obj.Z);
	}

	return;
}

void Target_Info(unsigned int Serial, unsigned short X, unsigned short Y, int Z)
{
	GameObject Obj;

	if(Serial == INVALID_SERIAL)
	{
		ClientPrint("Invalid target");
		return;
	}

	if(GetObjectInfo(Serial, INVALID_IDX, &Obj) == OBJECT_NOTFOUND)
	{
		ClientPrint("Object not in the world list");
		return;
	}

	ClientPrint("Serial 0x%08X", Obj.Serial);
	ClientPrint("Graphic: 0x%04X Color: 0x%04X", Obj.Graphic, Obj.Color);
	ClientPrint("Container: 0x%08X Quantity: %d", Obj.Container, Obj.Quantity);
	
	/* if the object wasn't a character, GetObjectInfo frees the pointer */
	if(Obj.Character == NULL) return;
	
	/* if it's a character display the info */
	ClientPrint("Name %s", Obj.Character->Name);
	ClientPrint("HP: %d MaxHP: %d", Obj.Character->HitPoints, Obj.Character->MaxHitPoints);
	ClientPrint("Current HP: %d%%", (Obj.Character->HitPoints*100)/Obj.Character->MaxHitPoints);

	FREECHAR(Obj);
	return;
}
