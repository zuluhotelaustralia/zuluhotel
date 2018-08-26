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
* 	Jun 07th, 2004 -- yay, started
* 
\******************************************************************************/

#include <windows.h>
#include "Main.h"
#include "GUI.h" /* start up the GUI */
#include "UONetwork.h" /* hook the winsock code */
#include "UOPacketHandler.h" /* hook the packet handlers, commands and etc.. */
#include "UOWnd.h" /* hook the window proc */
#include "ImportHook.h"
#include "Registry.h"
#include "INIProfile.h"
#include "Plugins.h" /* init plugins */
#include "Logger.h"
#include "irw.h" /* for the version record */


static HINSTANCE DLLInst = NULL;
static BOOL Inited = FALSE;

BOOL APIENTRY DllMain(HINSTANCE hDLLInst, DWORD fdwReason, LPVOID lpvReserved)
{
    switch (fdwReason)
    {
        case DLL_PROCESS_ATTACH:
		{
			DisableThreadLibraryCalls(hDLLInst);
			DLLInst = hDLLInst;
		}break;
        case DLL_PROCESS_DETACH:
		{
			/* to make sure irw.dll wasn't simply loaded and Install() wasnt called */
			if(Inited == TRUE)
				UnloadIRW();
		}break;
        case DLL_THREAD_ATTACH:		break;
        case DLL_THREAD_DETACH:		break;
    }
    return TRUE;
}

char *GetIRWVersion(void)
{
	return IRW_VERSION;
}

HINSTANCE GetIRWInstance(void)
{
	return DLLInst;
}

void Install(void)
{
	/* check if the registry data has been initialized */
	if(!strcmp(GetIRWPath(), "") || !strcmp(GetCurrentProfile(), ""))
	{
		MBOut("IRW ERROR", "IRW has not been properly initialized");
		ExitProcess(0);
		return;
	}

	/* initialize the player configuration */
	SetProfilesPath(GetIRWPath());
	SetCurrentProfile(GetCurrentProfile());

	/* open the log */
	OpenLog();
	LogPrint(NOFILTER_LOG, "IRW (%s) initializing...\r\n\r\n", IRW_VERSION);

	/* for a clean world ;) */
	LogBlock(NETWORK_LOG | COMPRESSOR_LOG | BUILDER_LOG | API_LOG);

	LogPrint(NOFILTER_LOG, "Initializing the socket list\r\n");
	CleanSocketList();

	LogPrint(NOFILTER_LOG, "Initializing internal aliases\r\n");
	InitAliases();

	LogPrint(NOFILTER_LOG, "Initializing the packet handler\r\n");
	InitPacketHandler();

	LogPrint(NOFILTER_LOG, "Initializing window hooks\r\n");
	if(!HookImportedFunction(BASE_IMGADDR, "user32.dll", "RegisterClassA", 0, hook_RegisterClassA) ||
	   !HookImportedFunction(BASE_IMGADDR, "user32.dll", "RegisterClassW", 0, hook_RegisterClassW) ||
	   !HookImportedFunction(BASE_IMGADDR, "user32.dll", "PeekMessageA", 510, hook_PeekMessage))
	{
		MBOut("IRW FATAL ERROR", "Could not apply one of the window hooks");
		ExitProcess(0);
		return;
	}

	LogPrint(NOFILTER_LOG, "Initializing api hooks\r\n");
	if(!HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "send", 19, hook_send) ||
	   !HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "recv", 16, hook_recv) ||
	   !HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "select", 18, hook_select) ||
	   !HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "socket", 23, hook_socket) ||
	   !HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "closesocket", 3, hook_closesocket) ||
	   !HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "connect", 4, hook_connect))
   {
		MBOut("IRW FATAL ERROR", "Could not apply one of the winsock hooks");
		ExitProcess(0);
		return;
	}

	/* used when the raw packets are to be seen. for debugging */
	/*HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "send", 19, hook_senddbg);
	HookImportedFunction(BASE_IMGADDR, "wsock32.dll", "recv", 16, hook_recvdbg);*/

	LogPrint(NOFILTER_LOG, "Initializing GUI\r\n");
	InitGUI();

	LogPrint(NOFILTER_LOG, "Initializing Plugins\r\n");
	InitPlugins();

	Inited = TRUE;

	LogPrint(NOFILTER_LOG, "Init done\r\n\r\n");
	return;
}

void UnloadIRW(void)
{
	/*
	* RTD: UnloadPlugin serves only as a warning to plugins that IRW is wrapping up
	* RTD2: do not undo the api hooks... that would mean the client could go playing
	* withouth irw and irw already leaves a few memory pointers open
	*/
	LogPrint(NOFILTER_LOG, ">> Closed %d open sockets on exit\r\n", CloseOpenSockets());

	FreePlugins();
	DestroyGUI();

	LogPrint(NOFILTER_LOG, "n0p3 rlz :)");
	CloseLog();

	return;
}
