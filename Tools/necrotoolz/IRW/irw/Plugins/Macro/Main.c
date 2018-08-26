/******************************************************************************\
* 
* 
*  Copyright (C) 2005 Daniel 'Necr0Potenc3' Cavalcanti
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
* 	Fev 26th, 2005 -- uses spells, skills and opens doors with the macro
*   packet. in the future will handle IRW macros as well
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Main.h"
#include "Spells.h"
#include "Skills.h"
#include "Actions.h"
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

	UpdateSpellsOptions();

	AddCommand("useskill", Command_Useskill);
	AddCommand("opendoor", Command_Opendoor);
	AddCommand("action", Command_Action);
	AddCommand("disarmoncast", Command_DisarmOnCast);
	AddCommand("cast", Command_Cast);

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
	strncpy(Text, "Spell casting, skill usage and door opening", Size);
	return;
}
