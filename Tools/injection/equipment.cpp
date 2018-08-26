////////////////////////////////////////////////////////////////////////////////
//
// equipment.cpp
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
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



////////////////////////////////////////////////////////////////////////////////
//
//  This file contains code that manages the sets of clothing and weapons
//  that are equipped using the arm/disarm and dress/undress commands.
//
////////////////////////////////////////////////////////////////////////////////

#include "common.h"
#include "world.h"
#include "iconfig.h"
#include "client.h"

#include "equipment.h"


////////////////////////////////////////////////////////////////////////////////

//// Methods of DressHandler class:

DressHandler::DressHandler(ClientInterface & client, GameObject & player,
    CharacterConfig & config)
: m_client(client), m_player(player), m_config(config), dress_speed(0)
{
}

DressHandler::~DressHandler()
{
}

void DressHandler::set(const string & key)
{
    if(!ConfigManager::valid_key(key))
    {
        m_client.client_print(string("Invalid dress key: ") + key);
        return;
    }
    DressSet & clothes=(m_config.get_dress_set(key));

    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(DressSet::valid_layer(i->get_layer()))
            clothes[i->get_layer()] = i->get_serial();
    }
    m_client.client_print("Clothing set.");
}

void DressHandler::unset(const string & key)
{
    if(m_config.dress_set_exists(key))
    {
        m_config.remove_dress_set(key);
        m_client.client_print("Clothing unset.");
    }
    else
        m_client.client_print("Cannot unset: not set with setdress.");
}

void DressHandler::dress(const string & key)
{
    // Use a const reference to make sure nothing is changed.
    const CharacterConfig & config=(m_config);
    if(config.dress_set_exists(key))
    {
        const DressSet & clothes=(config.get_dress_set(key));
        // First undress
        uint32 container = m_player.get_serial();
        if(m_config.obj_exists("undressbag"))
            container = m_config.find_obj("undressbag");
		bool no1=true;
        for(GameObject::iterator i = m_player.begin(); i != m_player.end();
                ++i)
        {
            if(DressSet::valid_layer(i->get_layer()))
            {
                if(!clothes.has_layer(i->get_layer()) ||
                        i->get_serial() != clothes[i->get_layer()])
                {
					if(no1) no1=false; else
					if(dress_speed >0)
						Sleep(dress_speed);						
                    m_client.move_container(i->get_serial(), container);
                }
            }
        }
        // Then dress with presets
		no1=true;
        for(int ii = 0; ii < NUM_CLOTHES; ii++)
            if(clothes.has_layer(ii))
            {
				if(no1) no1=false; else
				if(dress_speed >0)
					Sleep(dress_speed);						
                m_client.move_equip(clothes[ii], ii);
            }
        m_client.client_print("Clothing put on.");
    }
    else
        m_client.client_print("No clothing set with setdress.");
}

void DressHandler::undress()
{
	bool no1=true;
    uint32 container = m_player.get_serial();
    if(m_config.obj_exists("undressbag"))
        container = m_config.find_obj("undressbag");

    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(DressSet::valid_layer(i->get_layer()))
        {
			if(no1) no1=false; else
			if(dress_speed >0)
				Sleep(dress_speed);						
            m_client.move_container(i->get_serial(), container);
        }
    }
    m_client.client_print("Clothing removed.");
}

void DressHandler::setarm(const string & key)
{
    if(!ConfigManager::valid_key(key))
    {
        m_client.client_print(string("Invalid arm key: ") + key);
        return;
    }
    ArmSet & weapons=(m_config.get_arm_set(key));

    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(ArmSet::valid_layer(i->get_layer()))
            weapons[i->get_layer()] = i->get_serial();
    }
    m_client.client_print("Arm set.");
}

void DressHandler::arm(const string & key)
{
    // Use a const reference to make sure nothing is changed.
    const CharacterConfig & config=(m_config);
    if(config.arm_set_exists(key))
    {
        const ArmSet & weapons=(config.get_arm_set(key));
        // First unarm
        uint32 container = m_player.get_serial();
        if(m_config.obj_exists("disarmbag"))
            container = m_config.find_obj("disarmbag");

		bool no1=true;
        for(GameObject::iterator i = m_player.begin(); i != m_player.end();
                ++i)
        {
            if(ArmSet::valid_layer(i->get_layer()))
            {
                if(!weapons.has_layer(i->get_layer()) ||
                        i->get_serial() != weapons[i->get_layer()])
				{if(no1) no1=false; else
				  if(dress_speed >0) Sleep(dress_speed);
				 m_client.move_container(i->get_serial(), container);}
					
            }
        }
        // Then dress with presets
		no1=true;
        for(int ii = 0; ii < NUM_ARM; ii++)
            if(weapons.has_layer(ii))
			{if(no1) no1=false; else if(dress_speed >0) Sleep(dress_speed); m_client.move_equip(weapons[ii], ii);}
        m_client.client_print("Weapons armed.");
    }
    else
        m_client.client_print("No weapons set with setarm.");
}

void DressHandler::disarm()
{
    uint32 container = m_player.get_serial();
	bool no1=true;
    if(m_config.obj_exists("disarmbag"))
        container = m_config.find_obj("disarmbag");

    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(ArmSet::valid_layer(i->get_layer()))
				{if(no1) no1=false; else
				  if(dress_speed >0) Sleep(dress_speed);
				 m_client.move_container(i->get_serial(), container);}
    }
    m_client.client_print("Weapons disarmed.");
}

void DressHandler::unsetarm(const string & key)
{
    if(m_config.arm_set_exists(key))
    {
        m_config.remove_arm_set(key);
        m_client.client_print("Weapons unset.");
    }
    else
        m_client.client_print("Cannot unset: not set with setarm.");
}

void DressHandler::removehat()
{
    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(i->get_layer() == 0x06)
            m_client.move_backpack(i->get_serial());
    }
    m_client.client_print("Hat removed.");
}

void DressHandler::removeneckless()
{
    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(i->get_layer() == 0x0a)
            m_client.move_backpack(i->get_serial());
    }
    m_client.client_print("Neckless removed.");
}

void DressHandler::removeearrings()
{
    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(i->get_layer() == 0x12)
            m_client.move_backpack(i->get_serial());
    }
    m_client.client_print("Earrings removed.");
}

void DressHandler::removering()
{
    for(GameObject::iterator i = m_player.begin(); i != m_player.end(); ++i)
    {
        if(i->get_layer() == 0x08)
            m_client.move_backpack(i->get_serial());
    }
    m_client.client_print("Ring removed.");
}

