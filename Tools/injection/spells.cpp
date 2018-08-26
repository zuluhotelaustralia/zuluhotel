////////////////////////////////////////////////////////////////////////////////
//
// spells.cpp
//
// Copyright (C) 2001 Wayne 'Chiphead' Hogue
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
////////////////////////////////////////////////////////////////////////////////


#include "spells.h"


////////////////////////////////////////////////////////////////////////////////
//
//  This module deals with spells.
//
////////////////////////////////////////////////////////////////////////////////
// private
void Spells::send_message(string name)
{
    uint8 *buf=new uint8[get_msg_len(name)];
    buf[0] = CODE_CAST_SPELL_USE_SKILL;
    buf[1] = 0;
    buf[2] = get_msg_len(name);
    buf[3] = 0x56;
    strcpy((char *)(buf+4), get_code(name).c_str());
    m_client.send_server(buf, get_msg_len(name));
    delete buf;
}

bool Spells::exists(const string & name)
{
    return m_spells.find(name) != m_spells.end();
}

string Spells::get_code(const string & name)
{
    spell_map_t::iterator i = m_spells.find(name);
    ASSERT(i != m_spells.end());
    return ((*i).second).get_code();
}

bool Spells::can_target(const string & name)
{
    spell_map_t::iterator i = m_spells.find(name);
    ASSERT(i != m_spells.end());
    return ((*i).second).can_target();
}

bool Spells::has_menu(const string & name)
{
    spell_map_t::iterator i = m_spells.find(name);
    ASSERT(i != m_spells.end());
    return ((*i).second).has_menu();
}

int Spells::get_msg_len(const string & name)
{
    spell_map_t::iterator i = m_spells.find(name);
    ASSERT(i != m_spells.end());
    return ((*i).second).get_msg_len();
}

// public
Spells::Spells(ClientInterface & client, TargetInterface & targeting):
        m_client(client), m_targeting(targeting)
{
    m_spells.insert(spell_map_t::value_type("Clumsy",           Spell( "1", true,   false)));
    m_spells.insert(spell_map_t::value_type("Create Food",      Spell( "2", true,   false)));
    m_spells.insert(spell_map_t::value_type("Feeblemind",       Spell( "3", true,   false)));
    m_spells.insert(spell_map_t::value_type("Heal",             Spell( "4", true,   false)));
    m_spells.insert(spell_map_t::value_type("Magic Arrow",      Spell( "5", true,   false)));
    m_spells.insert(spell_map_t::value_type("Night Sight",      Spell( "6", true,   false)));
    m_spells.insert(spell_map_t::value_type("Reactive Armor",   Spell( "7", true,   false)));
    m_spells.insert(spell_map_t::value_type("Weaken",           Spell( "8", true,   false)));
    m_spells.insert(spell_map_t::value_type("Agility",          Spell( "9", true,   false)));
    m_spells.insert(spell_map_t::value_type("Cunning",          Spell("10", true,   false)));
    m_spells.insert(spell_map_t::value_type("Cure",             Spell("11", true,   false)));
    m_spells.insert(spell_map_t::value_type("Harm",             Spell("12", true,   false)));
    m_spells.insert(spell_map_t::value_type("Magic Trap",       Spell("13", true,   false)));
    m_spells.insert(spell_map_t::value_type("Magic Untrap",     Spell("14", true,   false)));
    m_spells.insert(spell_map_t::value_type("Protection",       Spell("15", true,   false)));
    m_spells.insert(spell_map_t::value_type("Strength",         Spell("16", true,   false)));
    m_spells.insert(spell_map_t::value_type("Bless",            Spell("17", true,   false)));
    m_spells.insert(spell_map_t::value_type("Fireball",         Spell("18", true,   false)));
    m_spells.insert(spell_map_t::value_type("Magic Lock",       Spell("19", true,   false)));
    m_spells.insert(spell_map_t::value_type("Poison",           Spell("20", true,   false)));
    m_spells.insert(spell_map_t::value_type("Telekinesis",      Spell("21", true,   false)));
    m_spells.insert(spell_map_t::value_type("Teleport",         Spell("22", true,   false)));
    m_spells.insert(spell_map_t::value_type("Unlock",           Spell("23", true,   false)));
    m_spells.insert(spell_map_t::value_type("Wall of Stone",    Spell("24", true,   false)));
    m_spells.insert(spell_map_t::value_type("Arch Cure",        Spell("25", true,   false)));
    m_spells.insert(spell_map_t::value_type("Arch Protection",  Spell("26", true,   false)));
    m_spells.insert(spell_map_t::value_type("Curse",            Spell("27", true,   false)));
    m_spells.insert(spell_map_t::value_type("Fire Field",       Spell("28", true,   false)));
    m_spells.insert(spell_map_t::value_type("Greater Heal",     Spell("29", true,   false)));
    m_spells.insert(spell_map_t::value_type("Lightning",        Spell("30", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mana Drain",       Spell("31", true,   false)));
    m_spells.insert(spell_map_t::value_type("Recall",           Spell("32", true,   false)));
    m_spells.insert(spell_map_t::value_type("Blade Spirits",    Spell("33", true,   false)));
    m_spells.insert(spell_map_t::value_type("Dispel Field",     Spell("34", true,   false)));
    m_spells.insert(spell_map_t::value_type("Incognito",        Spell("35", true,   false)));
    m_spells.insert(spell_map_t::value_type("Magic Reflection", Spell("36", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mind Blast",       Spell("37", true,   false)));
    m_spells.insert(spell_map_t::value_type("Paralyze",         Spell("38", true,   false)));
    m_spells.insert(spell_map_t::value_type("Poison Field",     Spell("39", true,   false)));
    m_spells.insert(spell_map_t::value_type("Summ. Creature",   Spell("40", true,   true)));
    m_spells.insert(spell_map_t::value_type("Dispel",           Spell("41", true,   false)));
    m_spells.insert(spell_map_t::value_type("Energy Bolt",      Spell("42", true,   false)));
    m_spells.insert(spell_map_t::value_type("Explosion",        Spell("43", true,   false)));
    m_spells.insert(spell_map_t::value_type("Invisibility",     Spell("44", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mark",             Spell("45", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mass Curse",       Spell("46", true,   false)));
    m_spells.insert(spell_map_t::value_type("Paralyze Field",   Spell("47", true,   false)));
    m_spells.insert(spell_map_t::value_type("Reveal",           Spell("48", true,   false)));
    m_spells.insert(spell_map_t::value_type("Chain Lightning",  Spell("49", true,   false)));
    m_spells.insert(spell_map_t::value_type("Energy Field",     Spell("50", true,   false)));
    m_spells.insert(spell_map_t::value_type("Flame Strike",     Spell("51", true,   false)));
    m_spells.insert(spell_map_t::value_type("Gate Travel",      Spell("52", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mana Vampire",     Spell("53", true,   false)));
    m_spells.insert(spell_map_t::value_type("Mass Dispel",      Spell("54", true,   false)));
    m_spells.insert(spell_map_t::value_type("Meteor Swarm",     Spell("55", true,   false)));
    m_spells.insert(spell_map_t::value_type("Polymorph",        Spell("56", false,  true)));
    m_spells.insert(spell_map_t::value_type("Earthquake",       Spell("57", false,  false)));
    m_spells.insert(spell_map_t::value_type("Energy Vortex",    Spell("58", true,   false)));
    m_spells.insert(spell_map_t::value_type("Resurrection",     Spell("59", true,   false)));
    m_spells.insert(spell_map_t::value_type("Air Elemental",    Spell("60", true,   false)));
    m_spells.insert(spell_map_t::value_type("Summon Daemon",    Spell("61", true,   false)));
    m_spells.insert(spell_map_t::value_type("Earth Elemental",  Spell("62", true,   false)));
    m_spells.insert(spell_map_t::value_type("Fire Elemental",   Spell("63", true,   false)));
    m_spells.insert(spell_map_t::value_type("Water Elemental",  Spell("64", true,   false)));
}

void Spells::cast(const string & name)
{
    if(exists(name)){
        send_message(name);
    }
    else
        m_client.client_print("Unknown spell name");
}

void Spells::cast(const string & name, uint32 target)
{
    if(exists(name))
    {
        if(can_target(name))
        {
            m_targeting.wait_target(target,0x000f); //Yoko: needs to be corrected maybe
            send_message(name);
        }
        else
            m_client.client_print("Spell not targetable");
    }
    else
        m_client.client_print("Unknown spell name");
}

// This function is called for 0x1c messages
bool Spells::handle_server_talk(uint8 * buf, int size)
{
    if(m_targeting.waiting())
    {
        // You lack
        if(size > 53)
        {
            if( (strncmp((char *)(buf+44), "You lack", 8) == 0) ||
                (strncmp((char *)(buf+44), "This is ", 8) == 0) ||
                (strncmp((char *)(buf+44), "The spel", 8) == 0))
            {
                m_targeting.cancel_target();
            }
        }
    }
    return true;
}
