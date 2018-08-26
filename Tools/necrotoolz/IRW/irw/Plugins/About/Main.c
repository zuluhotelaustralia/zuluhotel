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
* 	Dec 10th, 2004 -- irw is pretty much done. now its time for plugins!
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Main.h"
#include "resource.h"
#include "irw.h"


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;

static HWND AboutWnd = NULL;

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
	AboutWnd = CreateDialog(DLLInst, MAKEINTRESOURCE(DIALOG_ABOUT), GetMainTabWindow(), (DLGPROC)AboutDlgProc);

	if(AboutWnd == NULL)
		MBOut("ABOUT PLUGIN", "Could not create the dialog");
	else
		AddTab(AboutWnd, "About", FALSE);

	return TRUE;
}

void UnloadPlugin(void)
{
	if(Inited == FALSE)
		return;

	/* any wrap up routines go here */

	return;
}

void GetPluginInfo(char *Text, int Size)
{
	strncpy(Text, "IRW official release info", Size);
	return;
}

LRESULT CALLBACK AboutDlgProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	char Text[4096];

	switch(uMsg)
	{
		case WM_INITDIALOG:
		{
			sprintf(Text, "Compiled with core: %s\r\nCurrent core: %s", IRW_VERSION, GetIRWVersion());
			SetDlgItemText(hWnd, EDIT_ABOUT, Text);
			return TRUE;
		}break;
		case WM_SHOWWINDOW:
		{
			int ObjCount = 0, MobCount = 0, TotalCount = 0;
			WorldCount(&ObjCount, &MobCount, &TotalCount);

			sprintf(Text, "Compiled with core: %s\r\nCurrent core: %s\r\nWorld buffer has: %d slots\r\nUsed slots: %d\r\nCharacter slots: %d", IRW_VERSION, GetIRWVersion(), TotalCount, ObjCount, MobCount);
			SetDlgItemText(hWnd, EDIT_ABOUT, Text);
			return TRUE;
		}break;
	}

	return FALSE;
}
