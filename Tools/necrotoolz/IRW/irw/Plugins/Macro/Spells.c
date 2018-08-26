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

#include <windows.h>
#include <stdio.h>
#include "Spells.h"
#include "irw.h"


static BOOL DisarmOnCast = FALSE;
static const char *Spells[90 * 2] =
{
	/* magery spells - base 0 */
	"Clumsy", "1",
	"Create Food", "2",
	"Feeblemind", "3",
	"Heal", "4",
	"Magic Arrow", "5",
	"Night Sight", "6",
	"Reactive Armor", "7",
	"Weaken", "8",
	"Agility", "9",
	"Cunning", "10",
	"Cure", "11",
	"Harm", "12",
	"Magic Trap", "13",
	"Magic Untrap", "14",
	"Protection", "15",
	"Strength", "16",
	"Bless", "17",
	"Fireball", "18",
	"Magic Lock", "19",
	"Poison", "20",
	"Telekinesis", "21",
	"Teleport", "22",
	"Unlock", "23",
	"Wall of Stone", "24",
	"Arch Cure", "25",
	"Arch Protection", "26",
	"Curse", "27",
	"Fire Field", "28",
	"Greater Heal", "29",
	"Lightning", "30",
	"Mana Drain", "31",
	"Recall", "32",
	"Blade Spirit", "33",
	"Dispel Field", "34",
	"Incognito", "35",
	"Spell Reflection", "36",
	"Mind Blast", "37",
	"Paralyze", "38",
	"Poison Field", "39",
	"Summon Creature", "40",
	"Dispel", "41",
	"Energy Bolt", "42",
	"Explosion", "43",
	"Invisibility", "44",
	"Mark", "45",
	"Mass Curse", "46",
	"Paralyze Field", "47",
	"Reveal", "48",
	"Chain Lightning", "49",
	"Energy Field", "50",
	"Flame Strike", "51",
	"Gate Travel", "52",
	"Mana Vampire", "53",
	"Mass Dispel", "54",
	"Meteor Swarm", "55",
	"Polymorph", "56",
	"Earthquake", "57",
	"Energy Vortex", "58",
	"Resurrection", "59",
	"Summon Air Elemental", "60",
	"Summon Daemon", "61",
	"Summon Earth Elemental", "62",
	"Summon Fire Elemental", "63",
	"Summon Water Elemental", "64",

	/* necromancer spells */
	"Animate Dead", "101",
	"Blood Oath", "102",
	"Corpse Skin", "103",
	"Curse Weapon", "104",
	"Evil Omen", "105",
	"Horrific Beast", "106",
	"Lich Form", "107",
	"Mind Rot", "108",
	"Pain Spike", "109",
	"Poison Strike", "110",
	"Strangle", "111",
	"Summon Familiar", "112",
	"Vampiric Embrace", "113",
	"Vengeful Spirit", "114",
	"Wither", "115",
	"Wraith Form", "116",

	/* paladin spells */
	"Cleanse by Fire", "201",
	"Close Wounds", "202",
	"Consecrate Weapon", "203",
	"Dispel Evil", "204",
	"Divine Fury", "205",
	"Enemy of One", "206",
	"Holy Light", "207",
	"Noble Sacrifice", "208",
	"Remove Curse", "209",
	"Sacred Journey", "210"
};


void UpdateSpellsOptions(void)
{
	GetIRWProfileInt(NULL, "Macro", "DisarmOnCast", &DisarmOnCast);

	return;
}

void Command_DisarmOnCast(char **Arg, int ArgCount)
{
	GetIRWProfileInt(NULL, "Macro", "DisarmOnCast", &DisarmOnCast);

	/* switch on/off */
	if(ArgCount == 1) DisarmOnCast = DisarmOnCast ? FALSE : TRUE;
	else DisarmOnCast = ArgToInt(Arg[1]);

	ClientPrint("Disarm on cast is now: %s", DisarmOnCast == TRUE ? "on" : "off");
	SetIRWProfileInt(NULL, "Macro", "DisarmOnCast", DisarmOnCast);

	return;
}

void Command_Cast(char **Arg, int ArgCount)
{
	int i = 0;
	unsigned char *CastPkt = NULL;
	int PktLen = 0;

	if(ArgCount < 2)
	{
		ClientPrint("Usage: cast [spell name]");
		return;
	}

	for(i = 0; i < 90; i++)
	{
		/* if the spell was found in the list, cast it */
		if(!strnicmp(Spells[i * 2], Arg[1], strlen(Arg[1])))
		{
			/* Disarm first if necessary */
			if(DisarmOnCast)
			{
				UnequipItem(LAYER_ONE_HANDED);
				UnequipItem(LAYER_TWO_HANDED);
			}

			PktLen = 4 + ((int)(strlen(Spells[(i * 2) + 1]) & 0xFFFFFFFF) + 1);

			CastPkt = malloc(PktLen);
			CastPkt[0] = 0x12;
			PackUInt16(CastPkt + 1, PktLen);
			CastPkt[3] = 0x56;
			strcpy(CastPkt + 4, Spells[(i * 2) + 1]);

			SendToServer(CastPkt, PktLen);

			return;
		}
	}

	ClientPrint("Unknown spell: %s", Arg[1]);
    
	return;
}
