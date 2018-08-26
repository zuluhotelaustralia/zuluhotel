/******************************************************************************\
* 
* 
*  Copyright (C) 2004 Nelson 'Illusion' Hargreaves
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
* 	Dec 18th, 2004 -- enables smoothwalk and asks for resync. first IRW plugin
* 
\******************************************************************************/


#ifndef _MAIN_H_INCLUDED
#define _MAIN_H_INCLUDED

int InitPlugin(void);
void UnloadPlugin(void);
void GetPluginInfo(char *Text, int Size);

int HookServer_WalkAck(unsigned char *Packet, int Len);
int HookClient_WalkRequest(unsigned char *Packet, int Len);
void Command_SmoothWalk(char **Arg, int ArgCount);
void Command_Resync(char **Arg, int ArgCount);


#endif
