#include <stdio.h>

int main(int args, char **arg)
{
    char *Spells[90]=
	{
		/* Mage Spell */
		"Clumsy","Create Food","Feeblemind","Heal","Magic Arrow","Night Sight","Reactive Armor","Weaken",
		"Agility","Cunning","Cure","Harm","Magic Trap","Magic Untrap","Protection","Strength",
		"Bless","Fireball","Magic Lock","Poison","Telekinesis","Teleport","Unlock","Wall of Stone",
		"Arch Cure","Arch Protection","Curse","Fire Field","Greater Heal","Lightning","Mana Drain","Recall",
		"Blade Spirit","Dispel Field","Incognito","Spell Reflection","Mind Blast","Paralyze","Poison Field","Summon Creature",
		"Dispel","Energy Bolt","Explosion","Invisibility","Mark","Mass Curse","Paralyze Field","Reveal",
		"Chain Lightning","Energy Field","Flame Strike","Gate Travel","Mana Vampire","Mass Dispel","Meteor Swarm","Polymorph",
		"Earthquake","Energy Vortex","Resurrection","Summon Air Elemental","Summon Daemon","Summon Earth Elemental","Summon Fire Elemental","Summon Water Elemental",

		/* Necromancer Spell */
		"Animate Dead","Blood Oath","Corpse Skin","Curse Weapon","Evil Omen","Horrific Beast","Lich Form","Mind Rot",
		"Pain Spike","Poison Strike","Strangle","Summon Familiar","Vampiric Embrace","Vengeful Spirit","Wither","Wraith Form",

		/* Paladin Spell */
		"Cleanse by Fire","Close Wounds","Consecrate Weapon","Dispel Evil","Divine Fury",
		"Enemy of One","Holy Light","Noble Sacrifice","Remove Curse","Sacred Journey"
	};
	
	FILE *f = NULL;
	int i = 0;
	
    f =	fopen("dump.txt", "wb+");
    for(i = 0; i < 90; i++)
    {
       if(i < 64)
       fprintf(f, "\"%s\", \"%d\",\r\n", Spells[i], i+1);
       else if(i < 80)
       fprintf(f, "\"%s\", \"%d\",\r\n", Spells[i], i-64+1+100);
       else if(i < 90)
       fprintf(f, "\"%s\", \"%d\",\r\n", Spells[i], i-80+1+200);
    }
    
    fclose(f);
    
    return 1;
}