////////////////////////////////////////////////////////////////////////////////
//
// skills.cpp
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


#include "skills.h"
#include "world.h"
#include "injection.h"


////////////////////////////////////////////////////////////////////////////////
//
//  This module deals with skills.
//
////////////////////////////////////////////////////////////////////////////////
// private
void Skills::send_message(string name)
{
    uint8 *buf=new uint8[get_msg_len(name)];
    buf[0] = CODE_CAST_SPELL_USE_SKILL;
    buf[1] = 0;
    buf[2] = get_msg_len(name);
    buf[3] = 0x24;
    strcpy((char *)(buf+4), get_code(name).c_str());
    buf[4+get_code(name).length()] = 0x20;
    buf[5+get_code(name).length()] = 0x30;
    buf[6+get_code(name).length()] = 0x00;
    m_client.send_server(buf, get_msg_len(name));
    delete buf;
}

bool Skills::exists(const string & name)
{
    return m_skills.find(name) != m_skills.end();
}

string Skills::get_code(const string & name)
{
    skill_map_t::iterator i = m_skills.find(name);
    ASSERT(i != m_skills.end());
    return ((*i).second).get_code();
}

int Skills::get_target_cnt(const string & name)
{
    skill_map_t::iterator i = m_skills.find(name);
    ASSERT(i != m_skills.end());
    return ((*i).second).get_target_cnt();
}

bool Skills::has_menu(const string & name)
{
    skill_map_t::iterator i = m_skills.find(name);
    ASSERT(i != m_skills.end());
    return ((*i).second).has_menu();
}

int Skills::get_msg_len(const string & name)
{
    skill_map_t::iterator i = m_skills.find(name);
    ASSERT(i != m_skills.end());
    return ((*i).second).get_msg_len();
}

// public
Skills::Skills(ClientInterface & client, TargetInterface & targeting):
        m_client(client), m_targeting(targeting)
{
    m_skills.insert(skill_map_t::value_type("Anatomy",                  Skill( "1", 1, false)));
    m_skills.insert(skill_map_t::value_type("Animal Lore",              Skill( "2", 1, false)));
    m_skills.insert(skill_map_t::value_type("Animal Taming",            Skill("35", 1, false)));
    m_skills.insert(skill_map_t::value_type("Arms Lore",                Skill( "4", 1, false)));
    m_skills.insert(skill_map_t::value_type("Begging",                  Skill( "6", 1, false)));
    m_skills.insert(skill_map_t::value_type("Cartography",              Skill("12", 1, false)));
    m_skills.insert(skill_map_t::value_type("Detect Hidden",            Skill("14", 0, false)));
    m_skills.insert(skill_map_t::value_type("Enticement",               Skill("15", 1, false)));
    m_skills.insert(skill_map_t::value_type("Evaluating Intelligence",  Skill("16", 1, false)));
    m_skills.insert(skill_map_t::value_type("Forensic Evaluation",      Skill("19", 1, false)));
    m_skills.insert(skill_map_t::value_type("Hiding",                   Skill("21", 0, false)));
    m_skills.insert(skill_map_t::value_type("Inscription",              Skill("23", 0, true)));
    m_skills.insert(skill_map_t::value_type("Item Identification",      Skill( "3", 1, false)));
    m_skills.insert(skill_map_t::value_type("Meditation",               Skill("46", 0, false)));
    m_skills.insert(skill_map_t::value_type("Peacemaking",              Skill( "9", 1, false)));
    m_skills.insert(skill_map_t::value_type("Poisoning",                Skill("30", 2, false)));
    m_skills.insert(skill_map_t::value_type("Provocation",              Skill("22", 2, false)));
    m_skills.insert(skill_map_t::value_type("Remove Trap",              Skill("48", 1, false)));
    m_skills.insert(skill_map_t::value_type("Spirit Speak",             Skill("32", 0, false)));
    m_skills.insert(skill_map_t::value_type("Stealing",                 Skill("33", 1, false)));
    m_skills.insert(skill_map_t::value_type("Stealth",                  Skill("47", 0, false)));
    m_skills.insert(skill_map_t::value_type("Taste Identification",     Skill("36", 1, false)));
    m_skills.insert(skill_map_t::value_type("Tracking",                 Skill("38", 0, true)));
}

void Skills::use(const string & name)
{
    if(exists(name)){
        send_message(name);
    }
    else
        m_client.client_print("Unknown action skill name");
}

void Skills::use(const string & name, uint32 target)
{

    if(exists(name))
    {
        if(get_target_cnt(name) > 0)
        {
            //m_targeting.wait_target(target,0x000f); //Yoko: needs to be corrected maybe
            uint16 type=0;
			GameObject* obj=g_injection->m_world->get_object(target);
			if(obj) type=obj->get_graphic();
            m_targeting.wait_target(target,type); //Yoko: needs to be corrected maybe
            send_message(name);
        }
        else
            m_client.client_print("Skill not targetable");
    }
    else
        m_client.client_print("Unknown action skill name");
}

void Skills::use(const string & name, uint32 target, uint32 target2)
{
    if(exists(name))
    {
        if(get_target_cnt(name) == 2)
        {
            uint16 type=0,type2=0;
			GameObject* obj=g_injection->m_world->get_object(target);
			if(obj) type=obj->get_graphic();
			obj=g_injection->m_world->get_object(target2);
			if(obj) type2=obj->get_graphic();
			m_targeting.wait_target(target, type, target2, type2);
            send_message(name);
        }
        else
            m_client.client_print("Skill not double targetable");
    }
    else
        m_client.client_print("Unknown action skill name");
}

// This function is called for 0x1c messages
bool Skills::handle_server_talk(uint8 * buf, int size)
{
    if(m_targeting.waiting())
    {
        // You lack
        if(size > 56)
        {
            if(strncmp((char *)(buf+44), "You have no", 11) == 0)
                m_targeting.cancel_target();
        }
    }
    return true;
}
