/******************************************************************************\
* 
* 
*  Copyright (C) 2005 Emanuele "Lem" Di Santo
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
* 	Mar 05th, 2005 -- shrinks allocated memory by the process and plays wavs
* 
\******************************************************************************/

#include <windows.h>
#include "Main.h"
#include "irw.h"

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

	AddCommand("memshrink", Command_MemShrink);
	AddCommand("playwav", Command_Playwav);

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
	strncpy(Text, "Lemrey's Suppah Utilities", Size);
	return;
}

void Command_MemShrink(char **Arg,int ArgCount)
{
	SetProcessWorkingSetSize(GetCurrentProcess(),(SIZE_T)-1,(SIZE_T)-1);
	return;
}

void Command_Playwav(char **Arg,int ArgCount)
{
	if(ArgCount == 2)
		PlaySound(Arg[1], NULL, SND_ALIAS | SND_ASYNC);
	else
	{
		ClientPrint("Usage: playwav <path>");
		ClientPrint("<path> is a path to a wave file.");
	}

	return;
}
