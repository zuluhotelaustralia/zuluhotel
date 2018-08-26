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


#ifndef _MAIN_H_INCLUDED
#define _MAIN_H_INCLUDED

int InitPlugin(void);
void UnloadPlugin(void);
void GetPluginInfo(char *Text, int Size);

void Command_SetArm(char **Arg, int ArgCount);
void Command_SetDress(char **Arg, int ArgCount);
void Command_Arm(char **Arg, int ArgCount);
void Command_Dress(char **Arg, int ArgCount);
void Command_Disarm(char **Arg, int ArgCount);
void Command_Undress(char **Arg, int ArgCount);
void Command_Mount(char **Arg, int ArgCount);
void Command_Dismount(char **Arg, int ArgCount);

void Target_Mount(unsigned int Serial, unsigned short X, unsigned short Y, int Z);

#endif
