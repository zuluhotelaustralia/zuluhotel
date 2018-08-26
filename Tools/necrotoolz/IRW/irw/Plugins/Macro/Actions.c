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
* 	Fev 26th, 2005 -- opens doors, bows, salutes
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Actions.h"
#include "irw.h"


void Command_Opendoor(char **Arg, int ArgCount)
{
	char OpenPkt[5] = { 0x12, 0x00, 0x05, 0x58, '\0' };

	SendToServer(OpenPkt, 5);
	return;
}

void Command_Action(char **Arg, int ArgCount)
{
	char BowPkt[8] = { 0x12, 0x00, 0x08, 0xC7, 'b', 'o', 'w', '\0' };
	char SalutePkt[11] = { 0x12, 0x00, 0x0B, 0xC7, 's', 'a', 'l', 'u', 't', 'e', '\0' };
	int PktLen = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: action [type]");
		ClientPrint("[type] being: bow salute");
		return;
	}

	if(!stricmp(Arg[1], "bow"))
		SendToServer(BowPkt, 8);
	else if(!stricmp(Arg[1], "salute"))
		SendToServer(SalutePkt, 11);
	else
		ClientPrint("Unknown action: %s", Arg[1]);
    
	return;
}
