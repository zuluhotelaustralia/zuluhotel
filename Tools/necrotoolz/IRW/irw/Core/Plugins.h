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

#ifndef _PLUGINS_H_INCLUDED
#define _PLUGINS_H_INCLUDED

typedef int (*InitPluginFunction) (void);
typedef void (*UnloadPluginFunction) (void);
typedef void (*PluginInfoFunction) (char *, int);

typedef struct tagIRWPlugin
{
	char Name[1024];
	HMODULE BaseAddr;
}IRWPlugin;

/* INTERNALS */
void ListPlugins(const IRWPlugin **List, unsigned int *Size);
void InitPlugins(void);
void FreePlugins(void);
int LoadPlugin(const char *PluginName);
int UnloadPlugin(const char *PluginName);


#endif
