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
* 	Fev 26th, 2005 -- uses spells with the macro packet
*   based on Xan's Cast plugin
* 
\******************************************************************************/


#ifndef _SPELLS_H_INCLUDED
#define _SPELLS_H_INCLUDED

int InitPlugin(void);
void UnloadPlugin(void);
void GetPluginInfo(char *Text, int Size);

void UpdateSpellsOptions(void);
void Command_DisarmOnCast(char **Arg, int ArgCount);
void Command_Cast(char **Arg, int ArgCount);

#endif
