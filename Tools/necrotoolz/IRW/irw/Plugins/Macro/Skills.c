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
* 	Fev 26th, 2005 -- uses skills with the macro packet
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "Spells.h"
#include "irw.h"


static const char *Skills[54 * 2] =
{
	"Alchemy", "0",
	"Anatomy", "1",
	"AnimalLore", "2",
	"ItemID", "3",
	"ArmsLore", "4",
	"Parry", "5",
	"Begging", "6",
	"Blacksmith", "7",
	"Fletching", "8",
	"Peacemaking", "9",
	"Camping", "10",
	"Carpentry", "11",
	"Cartography", "12",
	"Cooking", "13",
	"DetectHidden", "14",
	"Discordance", "15",
	"EvalInt", "16",
	"Healing", "17",
	"Fishing", "18",
	"Forensics", "19",
	"Herding", "20",
	"Hiding", "21",
	"Provocation", "22",
	"Inscribe", "23",
	"Lockpicking", "24",
	"Magery", "25",
	"MagicResist", "26",
	"Tactics", "27",
	"Snooping", "28",
	"Musicianship", "29",
	"Poisoning", "30",
	"Archery", "31",
	"SpiritSpeak", "32",
	"Stealing", "33",
	"Tailoring", "34",
	"AnimalTaming", "35",
	"TasteID", "36",
	"Tinkering", "37",
	"Tracking", "38",
	"Veterinary", "39",
	"Swords", "40",
	"Macing", "41",
	"Fencing", "42",
	"Wrestling", "43",
	"Lumberjacking", "44",
	"Mining", "45",
	"Meditation", "46",
	"Stealth", "47",
	"RemoveTrap", "48",
	"Necromancy", "49",
	"Focus", "50",
	"Chivalry", "51",
	"Bushido", "52",
	"Ninjitsu", "53"
};


void Command_Useskill(char **Arg, int ArgCount)
{
	int i = 0;
	unsigned char *SkillPkt = NULL;
	int PktLen = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: useskill [skill name]");
		return;
	}

	for(i = 0; i < 54; i++)
	{
		/* if the spell was found in the list, cast it */
		if(!strnicmp(Skills[i * 2], Arg[1], strlen(Arg[1])))
		{
			PktLen = 4 + ((int)(strlen(Skills[(i * 2) + 1]) & 0xFFFFFFFF) + 1);

			SkillPkt = malloc(PktLen);
			SkillPkt[0] = 0x12;
			PackUInt16(SkillPkt + 1, PktLen);
			SkillPkt[3] = 0x24;
			strcpy(SkillPkt + 4, Skills[(i * 2) + 1]);

			SendToServer(SkillPkt, PktLen);

			return;
		}
	}

	ClientPrint("Unknown skill: %s", Arg[1]);
    
	return;
}
