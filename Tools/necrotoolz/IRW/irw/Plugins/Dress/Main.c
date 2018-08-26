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
* 	Dec 14th, 2004 -- dresses/arms and undresses/unarms the character
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Main.h"
#include "irw.h"

/* IN THE INI:
* dresses are set up as
* [DRESS|name]
* LayerX(3-24)=serial of item
* arm as
* [ARM|name]
* RHand=serial of item
* LHand=serial of item
*/


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;

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
	AddCommand("setarm", Command_SetArm);
	AddCommand("setdress", Command_SetDress);
	AddCommand("arm", Command_Arm);
	AddCommand("dress", Command_Dress);
	AddCommand("disarm", Command_Disarm);
	AddCommand("undress", Command_Undress);
	AddCommand("mount", Command_Mount);
	AddCommand("dismount", Command_Dismount);

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
	strncpy(Text, "Dresses and arms the character", Size);
	return;
}

void Command_SetArm(char **Arg, int ArgCount)
{
	char Section[1024];
	unsigned int ObjSerial = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: setarm [name]");
		return;
	}
    
	sprintf(Section, "ARM|%s", Arg[1]);
	CleanIRWProfileSection(NULL, Section);

	ObjSerial = GetItemInLayer(GetPlayerSerial(), LAYER_ONE_HANDED);
	SetIRWProfileInt(NULL, Section, "RHand", ObjSerial);
	ObjSerial = GetItemInLayer(GetPlayerSerial(), LAYER_TWO_HANDED);
	SetIRWProfileInt(NULL, Section, "LHand", ObjSerial);

	return;
}

void Command_SetDress(char **Arg, int ArgCount)
{
	char Section[1024], Key[1024];
	int i = 0;
	unsigned int ObjSerial = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: setdress [name]");
		return;
	}
    
	sprintf(Section, "DRESS|%s", Arg[1]);
	CleanIRWProfileSection(NULL, Section);

	/* get the item's serial or INVALID_SERIAL and set it to each one of the layers */
	for(i = 3; i < 25; i++)
	{
		sprintf(Key, "Layer%d", i);
		ObjSerial = GetItemInLayer(GetPlayerSerial(), i);
		SetIRWProfileInt(NULL, Section, Key, ObjSerial);
	}

	return;
}

void Command_Arm(char **Arg, int ArgCount)
{
	char Section[1024];
	unsigned int ObjSerial = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: arm [name]");
		return;
	}
    
	sprintf(Section, "ARM|%s", Arg[1]);

	/* unequip first */
	UnequipItem(LAYER_ONE_HANDED);
	UnequipItem(LAYER_TWO_HANDED);

	/* then equip */
	GetIRWProfileInt(NULL, Section, "RHand", &ObjSerial);
	if(ObjSerial != INVALID_SERIAL)
		EquipItem(ObjSerial, LAYER_ONE_HANDED);

	GetIRWProfileInt(NULL, Section, "LHand", &ObjSerial);
	if(ObjSerial != INVALID_SERIAL)
		EquipItem(ObjSerial, LAYER_TWO_HANDED);

	return;
}

void Command_Dress(char **Arg, int ArgCount)
{
	char Section[1024], Key[1024];
	int i = 0;
	unsigned int ObjSerial = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: dress [name]");
		return;
	}
    
	sprintf(Section, "DRESS|%s", Arg[1]);


	/* unequip all layers first */
	for(i = LAYER_SHOES; i < LAYER_MOUNT; i++)
	{
		/* do not unequip these layers */
		if(i == LAYER_9 || i == LAYER_15 || i == LAYER_BACKPACK)
			continue;

		UnequipItem(i);
	}
	/* get the item's serial or INVALID_SERIAL and set it to each one of the layers */
	for(i = LAYER_SHOES; i < LAYER_MOUNT; i++)
	{
		/* then again, dont fuck witt 'em niggas :P */
		if(i == LAYER_9 || i == LAYER_15 || i == LAYER_BACKPACK)
			continue;

		sprintf(Key, "Layer%d", i);
		GetIRWProfileInt(NULL, Section, Key, &ObjSerial);
		if(ObjSerial != INVALID_SERIAL)
			EquipItem(ObjSerial, i);
	}

	return;
}

void Command_Disarm(char **Arg, int ArgCount)
{
	UnequipItem(LAYER_ONE_HANDED);
	UnequipItem(LAYER_TWO_HANDED);

	return;
}

void Command_Undress(char **Arg, int ArgCount)
{
	int Layer = 0;

	for(Layer = LAYER_SHOES; Layer < LAYER_MOUNT; Layer++)
	{
		/* skip bad layers */
		if(Layer == LAYER_9 || Layer == LAYER_15 || Layer == LAYER_BACKPACK)
			continue;

		UnequipItem(Layer);
	}

	return;
}

void Command_Mount(char **Arg, int ArgCount)
{
	ClientPrint("Target what to mount");
	RequestTarget(TARGET_OBJECT, Target_Mount);

	return;
}

void Target_Mount(unsigned int Serial, unsigned short X, unsigned short Y, int Z)
{
	if(Serial == INVALID_SERIAL)
	{
		ClientPrint("Not a valid target");
		return;
	}

	EquipItem(Serial, LAYER_MOUNT);
	return;
}

void Command_Dismount(char **Arg, int ArgCount)
{
	UnequipItem(LAYER_MOUNT);
	return;
}
