////////////////////////////////////////////////////////////////////////////////
//
// world.cpp
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
//  This file contains code that was not present in Sniffy.
//  All code that manipulates GameObjects is here.
//
////////////////////////////////////////////////////////////////////////////////

#include <stdio.h>

#include "common.h"
#include "world.h"
#include "iconfig.h"
#include "client.h"

////////////////////////////////////////////////////////////////////////////////

int g_Life, g_STR, g_Mana, g_INT, g_Stamina, g_DEX,
    g_Armor, g_Weight, g_Gold,
    g_BM, g_BP, g_GA, g_GS, g_MR, g_NS, g_SA, g_SS,
    g_VA, g_EN, g_WH, g_FD, g_BR,
    g_H, g_C, g_M, g_L, g_B,
    g_AR, g_BT;

static const char* STREMPTY="";

////////////////////////////////////////////////////////////////////////////////

Counter::Counter(CounterManager & manager)
: m_manager(manager), m_n(0)
{
}

int Counter::operator+=(int n)
{
    m_n += n;
    m_manager.update();
    return m_n;
}

int Counter::operator-=(int n)
{
    m_n -= n;
    m_manager.update();
    return m_n;
}

#pragma warning( disable: 4355 )	// 'this' used in base member init
CounterManager::CounterManager(CounterCallbackInterface & callback, CharacterConfig *& character)
: m_callback(callback), m_character(character),
  m_bm_counter(*this), m_bp_counter(*this),
  m_ga_counter(*this), m_gs_counter(*this),
  m_mr_counter(*this), m_ns_counter(*this),
  m_sa_counter(*this), m_ss_counter(*this),
  m_va_counter(*this), m_en_counter(*this),
  m_wh_counter(*this), m_fd_counter(*this),
  m_br_counter(*this), m_h_counter(*this),
  m_c_counter(*this),  m_m_counter(*this),
  m_l_counter(*this),  m_b_counter(*this),
  m_ar_counter(*this), m_bt_counter(*this),
  m_connected(false) 
{
    m_hp=m_max_hp=m_mana=m_max_mana=m_stamina=m_max_stamina=m_ar=
        m_weight=m_gold=0;
    update();
}

void CounterManager::set_object_graphic(GameObject * obj, uint8 * buf)
{
    set_object_graphic(obj, unpack_big_uint16(buf));
}

void CounterManager::set_object_graphic(GameObject * obj, uint16 graphic)
{
    if(graphic == obj->get_graphic())
        return;
    obj->set_graphic(graphic);
    switch(graphic)
    {
    case 0xe21:
        obj->set_counter(&m_b_counter);
        break;
    case 0xf07:
        obj->set_counter(&m_c_counter);
        break;
    case 0xf09:
        obj->set_counter(&m_m_counter);
        break;
    case 0xf0b:
        obj->set_counter(&m_l_counter);
        break;
    case 0xf0c:
        obj->set_counter(&m_h_counter);
        break;
    case 0xf3f:
        obj->set_counter(&m_ar_counter);
        break;
    case 0xf79:
        obj->set_counter(&m_br_counter);
        break;
    case 0xf7a:
        obj->set_counter(&m_bp_counter);
        break;
    case 0xf7b:
        obj->set_counter(&m_bm_counter);
        break;
    case 0xf81:
        obj->set_counter(&m_fd_counter);
        break;
    case 0xf84:
        obj->set_counter(&m_ga_counter);
        break;
    case 0xf85:
        obj->set_counter(&m_gs_counter);
        break;
    case 0xf86:
        obj->set_counter(&m_mr_counter);
        break;
    case 0xf87:
        obj->set_counter(&m_en_counter);
        break;
    case 0xf88:
        obj->set_counter(&m_ns_counter);
        break;
    case 0xf8c:
        obj->set_counter(&m_sa_counter);
        break;
    case 0xf8d:
        obj->set_counter(&m_ss_counter);
        break;
    case 0xf8f:
        obj->set_counter(&m_va_counter);
        break;
    case 0xf91:
        obj->set_counter(&m_wh_counter);
        break;
    case 0x1bfb:
        obj->set_counter(&m_bt_counter);
        break;
    default:
        obj->set_counter(0);
    }
}

void CounterManager::update() const
{

    if(m_connected)
    {
        char buf[400];
        buf[0] = '\0';
//      strcat(buf, "Ultima Online - ");
        strcat(buf, "UO - ");

        g_Life=m_hp; g_STR=m_max_hp;
        g_Mana=m_mana; g_INT=m_max_mana;
        g_Stamina=m_stamina; g_DEX=m_max_stamina;
        g_Armor=m_ar; g_Weight=m_weight;
        g_Gold=m_gold;
        g_BM=m_bm_counter.get_value();
        g_BP=m_bp_counter.get_value();
        g_GA=m_ga_counter.get_value();
        g_GS=m_gs_counter.get_value();
        g_MR=m_mr_counter.get_value();
        g_NS=m_ns_counter.get_value();
        g_SA=m_sa_counter.get_value();
        g_SS=m_ss_counter.get_value();

        g_VA=m_va_counter.get_value();
        g_EN=m_en_counter.get_value();
        g_WH=m_wh_counter.get_value();
        g_FD=m_fd_counter.get_value();
        g_BR=m_br_counter.get_value();

        g_H=m_h_counter.get_value();
        g_C=m_c_counter.get_value();
        g_M=m_m_counter.get_value();
        g_L=m_l_counter.get_value();
        g_B=m_b_counter.get_value();

        g_AR=m_ar_counter.get_value();
        g_BT=m_bt_counter.get_value();

        char str[16];
        if(m_character->get_m_hp_display()){
			if(m_hp!=m_max_hp) sprintf(str, "h:%d/%d ", m_hp, m_max_hp);
			              else sprintf(str, "h:%d ", m_hp);
            strcat(buf, str);
        }
        if(m_character->get_m_mana_display()){
            if(m_mana!=m_max_mana)	sprintf(str, "m:%d/%d ", m_mana, m_max_mana);
							   else sprintf(str, "m:%d ", m_mana);
            strcat(buf, str);
        }
        if(m_character->get_m_stamina_display()){
            char str[16];
			if(m_stamina!=m_max_stamina) sprintf(str, "s:%d/%d ", m_stamina, m_max_stamina);
			else  sprintf(str, "s:%d ", m_stamina);
            strcat(buf, str);
        }
        if(m_character->get_m_armor_display()){
            char str[16];
			if(m_ar>4)
			{
            sprintf(str, "ar:%d ", m_ar);
            strcat(buf, str);
			}
        }
        if(m_character->get_m_weight_display()){
            char str[16];
            sprintf(str, "w:%d ", m_weight);
            strcat(buf, str);
        }
        if(m_character->get_m_gold_display()){
            char str[20];
            sprintf(str, "g:%ld ", m_gold);
            strcat(buf, str);
        }
        if(m_character->get_m_hp_display()||
            m_character->get_m_mana_display()||
            m_character->get_m_stamina_display()||
            m_character->get_m_armor_display()||
            m_character->get_m_weight_display()||
            m_character->get_m_gold_display())
            strcat(buf, "| ");
        if(m_character->get_m_bm_display()){
            char str[12];
            sprintf(str, "bm:%d ", m_bm_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_bp_display()){
            char str[12];
            sprintf(str, "bp:%d ", m_bp_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_ga_display()){
            char str[12];
            sprintf(str, "ga:%d ", m_ga_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_gs_display()){
            char str[12];
            sprintf(str, "gs:%d ", m_gs_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_mr_display()){
            char str[12];
            sprintf(str, "mr:%d ", m_mr_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_ns_display()){
            char str[12];
            sprintf(str, "ns:%d ", m_ns_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_sa_display()){
            char str[12];
            sprintf(str, "sa:%d ", m_sa_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_ss_display()){
            char str[12];
            sprintf(str, "ss:%d ", m_ss_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_bm_display()||
            m_character->get_m_bp_display()||
            m_character->get_m_ga_display()||
            m_character->get_m_gs_display()||
            m_character->get_m_mr_display()||
            m_character->get_m_ns_display()||
            m_character->get_m_sa_display()||
            m_character->get_m_ss_display())
            strcat(buf, "| ");
        if(m_character->get_m_va_display()){
            char str[12];
            sprintf(str, "va:%d ", m_va_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_en_display()){
            char str[12];
            sprintf(str, "en:%d ", m_en_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_wh_display()){
            char str[12];
            sprintf(str, "wh:%d ", m_wh_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_fd_display()){
            char str[12];
            sprintf(str, "fd:%d ", m_fd_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_br_display()){
            char str[12];
            sprintf(str, "br:%d ", m_br_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_va_display()||
            m_character->get_m_en_display()||
            m_character->get_m_wh_display()||
            m_character->get_m_fd_display()||
            m_character->get_m_br_display())
            strcat(buf, "| ");
        if(m_character->get_m_h_display()){
            char str[12];
            sprintf(str, "h:%d ", m_h_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_c_display()){
            char str[12];
            sprintf(str, "c:%d ", m_c_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_m_display()){
            char str[12];
            sprintf(str, "m:%d ", m_m_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_l_display()){
            char str[12];
            sprintf(str, "l:%d ", m_l_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_h_display()||
            m_character->get_m_c_display()||
            m_character->get_m_m_display()||
            m_character->get_m_l_display())
            strcat(buf, "| ");
        if(m_character->get_m_b_display()){
            char str[12];
            sprintf(str, "b:%d ", m_b_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_ar_display()){
            char str[12];
            sprintf(str, "ar:%d ", m_ar_counter.get_value());
            strcat(buf, str);
        }
        if(m_character->get_m_bt_display()){
            char str[12];
            sprintf(str, "bt:%d ", m_bt_counter.get_value());
            strcat(buf, str);
        }
        m_callback.update_counter(buf);
		trace_printf("%s\n",buf);
    }
    else
    {
/*
        // This should prevent changing the caption at UO start
        static bool FirstTry=true;
        if(!FirstTry)
            m_callback.update_counter("Ultima Online (not connected)");
        FirstTry=false;
*/
    }
}

void CounterManager::connected()
{
    m_bm_counter.reset();
    m_bp_counter.reset();
    m_ga_counter.reset();
    m_gs_counter.reset();
    m_mr_counter.reset();
    m_ns_counter.reset();
    m_sa_counter.reset();
    m_ss_counter.reset();
    m_va_counter.reset();
    m_en_counter.reset();
    m_wh_counter.reset();
    m_fd_counter.reset();
    m_br_counter.reset();
    m_h_counter.reset();
    m_c_counter.reset();
    m_m_counter.reset();
    m_l_counter.reset();
    m_b_counter.reset();
    m_ar_counter.reset();
    m_bt_counter.reset();

    m_connected = true;
    update();
}

void CounterManager::disconnected()
{
    m_connected = false;
    update();
}

////////////////////////////////////////////////////////////////////////////////

GameObject::GameObject(uint32 serial)
: m_prev(0), m_next(0),
  m_serial(serial), m_interesting(false),
  m_graphic(0), m_colour(0), m_x(INVALID_XY), m_y(INVALID_XY),
  m_z(0), m_direction(0), m_flags(0), m_quantity(0),
  m_container(INVALID_SERIAL), m_layer(LAYER_NONE),
  m_graphic_increment(0), m_counter(0), m_head(0),
  m_notoriety(0),name(NULL)
{
}

void GameObject::set_interesting(bool interesting)
{
    if(m_interesting != interesting)
    {
        if(m_counter != 0)
        {
            if(interesting) // become interesting
                *m_counter += m_quantity;
            else    // no longer interesting
                *m_counter -= m_quantity;
        }
        m_interesting = interesting;
        // Recursively make all of the contents interesting
        GameObject * obj = m_head;
        while(obj != 0)
        {
            obj->set_interesting(interesting);
            obj = obj->m_next;
        }
    }
}

void GameObject::set_graphic(uint16 graphic)
{
    // For interesting objects, this function should NOT be called directly.
    // Instead, CounterManager::set_object_graphic() should be used.
    m_graphic = graphic;
}

void GameObject::set_quantity(uint8 * buf)
{
    if(m_interesting && m_counter != 0)
    {
        *m_counter -= m_quantity;
        m_quantity = unpack_big_uint16(buf);
        *m_counter += m_quantity;
    }
    else
        m_quantity = unpack_big_uint16(buf);
}

void GameObject::set_layer(int layer)
{
    // Bank container should not be 'interesting', because we don't want
    // to count the items in it.
    if(layer == LAYER_BANK)
        set_interesting(false);
    m_layer = layer;
}

void GameObject::set_counter(Counter * counter)
{
    if(counter != m_counter)
    {
        if(m_interesting && m_counter != 0)
            *m_counter -= m_quantity;
        m_counter = counter;
        if(m_interesting && m_counter != 0)
            *m_counter += m_quantity;
    }
}

void GameObject::remove(GameObject * obj)
{
    ASSERT(obj->m_container == m_serial);
    if(obj->m_prev == 0)
    {
        ASSERT(m_head == obj);
        m_head = obj->m_next;
    }
    else
        obj->m_prev->m_next = obj->m_next;  // unlink from previous
    if(obj->m_next != 0)
    {
        obj->m_next->m_prev = obj->m_prev;  // unlink from next
        obj->m_next = 0;
    }
    obj->m_prev = 0;
    obj->m_container = INVALID_SERIAL;
    obj->m_layer = LAYER_NONE;
    obj->m_x = obj->m_y = INVALID_XY;
    obj->m_z = 0;
}

void GameObject::add(GameObject * obj)
{
    ASSERT(obj->m_container == INVALID_SERIAL);
    // Add to the head of the list
    if(m_head != 0)
        m_head->m_prev = obj;
    obj->m_next = m_head;
    m_head = obj;
    obj->m_container = m_serial;
    obj->m_z = 0;
}

void GameObject::move_to_head(GameObject * obj)
{
    ASSERT(obj->m_container == m_serial);
    // First remove the object from the list.
    if(obj->m_prev == 0)
    {
        ASSERT(m_head == obj);
        // Already at the head.
        return;
    }
    else
        obj->m_prev->m_next = obj->m_next;  // unlink from previous
    if(obj->m_next != 0)
    {
        obj->m_next->m_prev = obj->m_prev;  // unlink from next
        //obj->m_next = 0;
    }
    obj->m_prev = 0;
    // Now, insert the object at the head.
    if(m_head != 0)
        m_head->m_prev = obj;
    obj->m_next = m_head;
    m_head = obj;
}

GameObject * GameObject::find_layer(int layer)
{
    // NOTE: if more than one object is on the layer, just return the first
    // one.
    for(GameObject * obj = m_head; obj != 0; obj = obj->m_next)
        if(obj->m_layer == layer)
            return obj;
    return 0;
}

// Recursively search a container for an item having the specified graphic
GameObject * GameObject::find_graphic(uint16 graphic)
{
    GameObject * found = 0;
    GameObject * obj = m_head;
    while(obj != 0 && found == 0)
    {
        if(obj->get_graphic() == graphic)
            found = obj;
        else
        {
            // When activating something like a potion, don't search the bank.
            //if(!obj->is_empty() && obj->m_layer != LAYER_BANK)
            if(!obj->is_empty() && obj->get_interesting())
                // recurse into container
                found = obj->find_graphic(graphic);
            obj = obj->m_next;
        }
    }
    return found;
}

// Recursively search a container for items having the specified graphic
int GameObject::count_graphic(uint16 graphic)
{
    int found = 0;
    GameObject * obj = m_head;
    while(obj != 0)
    {
        if(obj->get_graphic() == graphic)
            found+=obj->get_quantity()?obj->get_quantity():1;
        else
        {
            if(!obj->is_empty() && obj->get_interesting())
                // recurse into container
                found += obj->count_graphic(graphic);
        }
        obj = obj->m_next;
    }
    return found;
}

// Recursively search a container for an item having the specified graphic and color
GameObject * GameObject::find_graphic(uint16 graphic, uint16 color)
{
    GameObject * found = 0;
    GameObject * obj = m_head;
    while(obj != 0 && found == 0)
    {
        if((obj->get_graphic() == graphic)&&(obj->get_colour() == color))
            found = obj;
        else
        {
            // When activating something like a potion, don't search the bank.
            //if(!obj->is_empty() && obj->m_layer != LAYER_BANK)
            if(!obj->is_empty() && obj->get_interesting())
                // recurse into container
                found = obj->find_graphic(graphic, color);
            obj = obj->m_next;
        }
    }
    return found;
}

// Recursively search a container for items having the specified graphic and color
int GameObject::count_graphic(uint16 graphic, uint16 color)
{
    int found = 0;
    GameObject * obj = m_head;
    while(obj != 0)
    {
        if((obj->get_graphic() == graphic)&&(obj->get_colour() == color))
            found+=obj->get_quantity()?obj->get_quantity():1;
        else
        {
            if(!obj->is_empty() && obj->get_interesting())
                // recurse into container
                found += obj->count_graphic(graphic, color);
        }
        obj = obj->m_next;
    }
    return found;
}

////////////////////////////////////////////////////////////////////////////////

World::World(uint32 player_serial)
{
    m_player = get_object(player_serial);
    m_player->set_interesting(true);
}

World::~World()
{
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
        delete (*i).second;
    m_player = 0;
}

void World::set_player(uint32 serial)
{
    if(serial != m_player->get_serial())
    {
        m_player->set_interesting(false);
        m_player = get_object(serial);
        m_player->set_interesting(true);
    }
}

GameObject * World::find_object(uint32 serial)
{
    GameObject * obj;
    map_t::iterator i = m_map.find(serial);
    if(i == m_map.end())
    {
        obj = new GameObject(serial);
        m_map[serial] = obj;
    }
    else
        obj = (*i).second;
    return obj;
}

GameObject * World::get_object(uint32 serial)
{
    GameObject * obj = find_object(serial);
    if(obj == 0)
        obj = new GameObject(serial);
    return obj;
}

GameObject * World::find_inventory_graphic(uint16 graphic)
{
    return m_player->find_graphic(graphic);
}

GameObject * World::find_inventory_graphic(uint16 graphic, uint16 color)
{
    return m_player->find_graphic(graphic, color);
}

GameObject * World::find_world_graphic(uint16 graphic, int distance)
{
    GameObject * found = 0;
	int pX=m_player->m_x;
	int pY=m_player->m_y;
	RECT r={pX-distance,pY-distance,pX+distance+1,pY+distance+1};
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
    {
        GameObject * obj = (*i).second;

        if(obj->get_graphic() == graphic && !obj->get_interesting())
		{	// ignore all items in backpack and that are too far
			POINT p={obj->m_x,obj->m_y};
			if(PtInRect(&r,p))
				found = obj;
		}
    }
    return found;
}

GameObject * World::find_world_graphic(uint16 graphic, uint16 color, int distance)
{
    GameObject * found = 0;
	int pX=m_player->m_x;
	int pY=m_player->m_y;
	RECT r={pX-distance,pY-distance,pX+distance+1,pY+distance+1};
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
    {
        GameObject * obj = (*i).second;

        if(obj->get_graphic() == graphic && obj->get_colour() == color && !obj->get_interesting())
		{	// ignore all items in backpack and that are too far
			POINT p={obj->m_x,obj->m_y};
			if(PtInRect(&r,p))
				found = obj;
		}
    }
    return found;
}

int World::count_on_ground(uint16 graphic, int distance)
{
	int Count=0;
	int pX=m_player->m_x;
	int pY=m_player->m_y;
	RECT r={pX-distance,pY-distance,pX+distance+1,pY+distance+1};
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
    {
        GameObject * obj = (*i).second;

        if(obj->get_graphic() == graphic && !obj->get_interesting())
		{	// ignore all items in backpack and that are too far
			POINT p={obj->m_x,obj->m_y};
			if(PtInRect(&r,p))
				Count++;
		}
    }
    return Count;
}

int World::count_on_ground(uint16 graphic, uint16 color, int distance)
{
    int Count=0;
	int pX=m_player->m_x;
	int pY=m_player->m_y;
	RECT r={pX-distance,pY-distance,pX+distance+1,pY+distance+1};
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
    {
        GameObject * obj = (*i).second;

        if(obj->get_graphic() == graphic && obj->get_colour() == color && !obj->get_interesting())
		{	// ignore all items in backpack and that are too far
			POINT p={obj->m_x,obj->m_y};
			if(PtInRect(&r,p))
				Count++;
		}
    }
    return Count;
}

int World::count_inventory_graphic(uint16 graphic)
{
    return m_player->count_graphic(graphic);
}

int World::count_inventory_graphic(uint16 graphic, uint16 color)
{
    return m_player->count_graphic(graphic, color);
}

void GameObject::make_invalid() 
{
	if(is_empty())
	{
		m_serial=INVALID_SERIAL;
		m_container = INVALID_SERIAL;
	    m_layer = LAYER_NONE;
		m_x = m_y = INVALID_XY; m_z = 0;
	}
}

void World::remove_object(GameObject * obj)
{
	obj->set_interesting(false);
	obj->set_layer(0);

    if(obj->m_container != INVALID_SERIAL)
		remove_container(obj);

	if(obj->is_empty())
		m_map.erase(obj->get_serial());

	obj->make_invalid();
// it is not safe to delete obj when it is not is_empty()
}

void World::remove_container(GameObject * obj)
{
    if(obj->m_container != INVALID_SERIAL)
    {
        GameObject * container = find_object(obj->m_container);
        if(container != 0)  // shouldn't happen
            container->remove(obj);

        obj->m_container = INVALID_SERIAL;
        obj->set_interesting(false);
	}
}

void World::put_container(GameObject * obj, GameObject * container)
{
    if(obj->m_container != INVALID_SERIAL)
    {
        GameObject * old_container = find_object(obj->m_container);
        if(old_container == 0)  // shouldn't happen
            obj->m_container = INVALID_SERIAL;
        else if(old_container == container) // already there
        {
            // If the item is already in the container, it just needs to be
            // made 'topmost'.
            old_container->move_to_head(obj);
            return;
        }
        else
            old_container->remove(obj);
    }
    container->add(obj);
    obj->set_interesting(container->get_interesting());
}

void World::dump()
{
    log_printf("World Dump:\n\n");
    for(map_t::iterator i = m_map.begin(); i != m_map.end(); i++)
    {
        GameObject * obj = (*i).second;
        if(obj == m_player)
            log_printf("---Player---\n");
        if((obj->get_name())[0])
            log_printf(obj->get_name());
		else
			log_printf("Object");
        log_printf(" 0x%08lx:  my: %s\n", obj->get_serial(),
            obj->get_interesting() ? "true" : "false");
        log_printf("Graphic: 0x%04x  Colour: 0x%04x  Quantity: %d\n",
            obj->get_graphic(), obj->m_colour, obj->m_quantity);
        log_printf("X: %4d  Y: %4d  Z: %4d\n", obj->m_x, obj->m_y, obj->m_z);
        log_printf("In container: 0x%08lx  Layer: %d\n",
            obj->m_container, obj->get_layer());
        log_printf("\n");
    }
}

GameObject::~GameObject()
{
 if(name) delete name;
}
void GameObject::set_name(const char* nam)
{
	if(name) delete name; name=NULL;
	if(nam)
	{
		name=new char[strlen(nam)+1];
		strcpy(name,nam);
		name[strlen(nam)]=0;
	}
}
	
const char* GameObject::get_name()
{
	if(!name) return STREMPTY;
	return name;
}

bool GameObject::isHidden()
{
 return (m_flags&0x80)!=0;
}

bool GameObject::isPoisoned()
{
 return (m_flags&0x04)!=0;
}
