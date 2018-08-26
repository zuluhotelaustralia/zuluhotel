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
* 	Oct 09th, 2004 -- started it
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Plugins.h"
#include "Registry.h"
#include "INIProfile.h"
#include "Logger.h"


static char PluginsPath[MAX_PATH];
static IRWPlugin *PluginList = NULL;
static int ListSize = 0;

void ListPlugins(const IRWPlugin **List, unsigned int *Size)
{
	*List = PluginList;
	*Size = ListSize;
	return;
}

void InitPlugins(void)
{
	DWORD Size = 0;
	char Item[1024], PluginName[1024];
	int i = 0;

	memset(PluginsPath, 0, sizeof(PluginsPath));

	Size = sizeof(PluginsPath);
	GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, PluginsPath, &Size);
	if(!strcmp(PluginsPath, ""))
	{
		MBOut("PLUGIN ERROR", "Could not find irw's path in the registry");
		ExitProcess(0);
		return;
	}
	strcat(PluginsPath, "\\Plugins");

	/* load all plugins in the profile */
	sprintf(Item, "%d", i);
	while(GetIRWProfileString(NULL, "Plugins", Item, PluginName, sizeof(PluginName)))
	{
        if(LoadPlugin(PluginName))
			LogPrint(NOFILTER_LOG, "%d: Plugin %s loaded\r\n", i, PluginName);
		else
			LogPrint(NOFILTER_LOG, "%d: Could not load plugin %s\r\n", i, PluginName);

		i++;
		sprintf(Item, "%d", i);
	}

	return;
}

void FreePlugins(void)
{
	int i = 0;

	for(i = 0; i < ListSize; i++)
	{
		if(UnloadPlugin(PluginList[i].Name) == FALSE)
			LogPrint(WARNING_LOG, "Could not unload the plugin: %s\r\n", PluginList[i].Name);
	}

	return;
}

int LoadPlugin(const char *PluginName)
{
	char PluginPath[MAX_PATH];
	HMODULE PluginHandle = NULL;
	InitPluginFunction InitPlugin = NULL;
	UnloadPluginFunction UnloadPlugin = NULL;
	PluginInfoFunction GetPluginInfo = NULL;


	strcpy(PluginPath, PluginsPath);
    strcat(PluginPath, "\\");
	strcat(PluginPath, PluginName);

	/* already loaded */
	if((PluginHandle = GetModuleHandle(PluginName)) == NULL)
		PluginHandle = LoadLibrary(PluginPath);
	else
		LogPrint(WARNING_LOG, "PLUGIN:WARNING: Plugin %s is already loaded. Reloading it\r\n", PluginName);

	if(PluginHandle == NULL)
	{
		LogPrint(ERROR_LOG, "PLUGIN:ERROR: Could not load: %s at %s\r\n", PluginName, PluginPath);
		return FALSE;
	}

    
	InitPlugin = (InitPluginFunction)GetProcAddress(PluginHandle, "InitPlugin");
	UnloadPlugin = (UnloadPluginFunction)GetProcAddress(PluginHandle, "UnloadPlugin");
	if(InitPlugin == NULL || UnloadPlugin == NULL)
	{
		LogPrint(ERROR_LOG, "PLUGIN:ERROR: Could not load %s at %s. Init: 0x%08X Unload: 0x%08X\r\n", PluginName, PluginPath, InitPlugin, UnloadPlugin);
		FreeLibrary(PluginHandle);
		return FALSE;
	}

	/* not really important */
	GetPluginInfo = (PluginInfoFunction)GetProcAddress(PluginHandle, "GetPluginInfo");
	if(GetPluginInfo == NULL)
		LogPrint(WARNING_LOG, "PLUGIN:WARNING: Plugin %s at %s has no GetInfoFunction\r\n", PluginName, PluginPath);

    if(InitPlugin() == FALSE)
	{
		LogPrint(ERROR_LOG, "PLUGIN:ERROR: The plugin %s at %s could not be loaded\r\n", PluginName, PluginPath);
		return FALSE;
	}

	ListSize++;
	PluginList = (IRWPlugin*)realloc(PluginList, ListSize*sizeof(IRWPlugin));
	strcpy(PluginList[ListSize - 1].Name, PluginName);
	PluginList[ListSize - 1].BaseAddr = PluginHandle;
	return TRUE;
}

int UnloadPlugin(const char *PluginName)
{
	char PluginPath[MAX_PATH];
	HMODULE PluginHandle = NULL;
	UnloadPluginFunction ImpUnloadPlugin = NULL;


	strcpy(PluginPath, PluginsPath);
    strcat(PluginPath, "\\");
	strcat(PluginPath, PluginName);

	if((PluginHandle = GetModuleHandle(PluginName)) == NULL)
		return FALSE;

	if((ImpUnloadPlugin = (UnloadPluginFunction)GetProcAddress(PluginHandle, "UnloadPlugin")) == NULL)
		return FALSE;

	ImpUnloadPlugin();
	FreeLibrary(PluginHandle);

    return TRUE;
}
