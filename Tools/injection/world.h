////////////////////////////////////////////////////////////////////////////////
//
// world.h
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
//  Classes and functions related to game objects such as items and characters
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _WORLD_H_
#define _WORLD_H_

#include <hash_map>

#include "common.h"
#include "iconfig.h"
#include "client.h"

extern int g_Life, g_STR, g_Mana, g_INT, g_Stamina, g_DEX,
    g_Armor, g_Weight, g_Gold,
    g_BM, g_BP, g_GA, g_GS, g_MR, g_NS, g_SA, g_SS,
    g_VA, g_EN, g_WH, g_FD, g_BR,
    g_H, g_C, g_M, g_L, g_B,
    g_AR, g_BT;


class GameObject;

const uint16 INVALID_XY = 0xffff;

const int LAYER_NONE = 0;
const int LAYER_ONE_HANDED = 1;
const int LAYER_TWO_HANDED = 2;
const int LAYER_SHOES = 3;
const int LAYER_PANTS = 4;
const int LAYER_SHIRT = 5;
const int LAYER_HELM = 6;  // hat
const int LAYER_GLOVES = 7;
const int LAYER_RING = 8;
const int LAYER_9 = 9;      // unused
const int LAYER_NECK = 10;
const int LAYER_HAIR = 11;
const int LAYER_WAIST = 12; // half apron
const int LAYER_TORSO = 13; // chest armour
const int LAYER_BRACELET = 14;
const int LAYER_15 = 15;        // unused
const int LAYER_FACIAL_HAIR = 16;
const int LAYER_TUNIC = 17; // surcoat, tunic, full apron, sash
const int LAYER_EARRINGS = 18;
const int LAYER_ARMS = 19;
const int LAYER_CLOAK = 20;
const int LAYER_BACKPACK = 21;
const int LAYER_ROBE = 22;
const int LAYER_SKIRT = 23; // skirt, kilt
const int LAYER_LEGS = 24;  // leg armour
const int LAYER_MOUNT = 25; // horse, ostard, etc
const int LAYER_VENDOR_BUY_RESTOCK = 26;
const int LAYER_VENDOR_BUY = 27;
const int LAYER_VENDOR_SELL = 28;
const int LAYER_BANK = 29;

class CounterCallbackInterface
{
public:
    virtual void update_counter(const char * str) = 0;
};

class CounterManager;

class Counter
{
private:
    CounterManager & m_manager;
    int m_n;

public:
    Counter(CounterManager & manager);

    void reset() { m_n = 0; }
    int operator+=(int n);
    int operator-=(int n);
    int get_value() const { return m_n; }
};

class InjectionGUI;

class CounterManager
{
private:
    CounterCallbackInterface & m_callback;
    CharacterConfig *& m_character;
    Counter m_bm_counter, m_bp_counter, m_ga_counter, m_gs_counter,
        m_mr_counter, m_ns_counter, m_sa_counter, m_ss_counter,
        m_va_counter, m_en_counter, m_wh_counter,   m_fd_counter, m_br_counter,
        m_h_counter, m_c_counter, m_m_counter, m_l_counter,
        m_b_counter, m_ar_counter, m_bt_counter;

    bool m_connected;

    uint16 m_hp, m_max_hp, m_mana, m_max_mana, m_stamina, m_max_stamina, m_ar, m_weight;
    uint32 m_gold;

public:

    CounterManager(CounterCallbackInterface & callback, CharacterConfig *& character);

    void set_m_hp(uint16 i){ m_hp = i;}
    void set_m_max_hp(uint16 i){ m_max_hp = i;}
    void set_m_mana(uint16 i){ m_mana = i;}
    void set_m_max_mana(uint16 i){ m_max_mana = i;}
    void set_m_stamina(uint16 i){ m_stamina = i;}
    void set_m_max_stamina(uint16 i){ m_max_stamina = i;}
    void set_m_ar(uint16 i){ m_ar = i;}
    void set_m_weight(uint16 i){ m_weight = i;}
    void set_m_gold(uint32 i){ m_gold = i;}

    void set_object_graphic(GameObject * obj, uint8 * buf);
    void set_object_graphic(GameObject * obj, uint16 graphic);
    void update() const;

    void connected();
    void disconnected();
};

class GameObject
{
private:
    GameObject * m_prev, * m_next;

    uint32 m_serial;
    // An object is interesting if it is carried by the player.
    bool m_interesting;
    uint16 m_graphic;
    int m_layer;    // LAYER_NONE if not equipped (really uint8)

public:
	bool isPoisoned();
	bool isHidden();
	virtual  ~GameObject();
    class iterator
    {
    private:
        GameObject * m_obj;
    public:
        iterator(GameObject * obj) : m_obj(obj) { }
        iterator(const iterator & other) : m_obj(other.m_obj) { }
        GameObject * ptr() { return m_obj; }
        bool operator ! () { return m_obj == 0; }
        GameObject * operator -> () { return m_obj; }
        const GameObject * operator -> () const { return m_obj; }
        GameObject & operator * () { return *m_obj; }
        bool operator == (const iterator & other)
        { return m_obj == other.m_obj; }
        bool operator != (const iterator & other)
        { return m_obj != other.m_obj; }
        iterator operator++(int)
        {
            iterator old = *this;
            m_obj = m_obj->m_next;
            return old;
        }
        const iterator & operator++()
        {
            m_obj = m_obj->m_next;
            return *this;
        }
    };
    friend class iterator;

    // Common fields:
    uint16 m_colour;
    uint16 m_x, m_y;    // INVALID_XY if equipped
    int m_z;    // (really signed 8 bit)
    // Misc:
    uint8 m_direction;
    uint8 m_flags;
    // Items only:
    uint16 m_quantity;  // zero if unknown or not an item
    uint32 m_container; // serial of object containing this (or 0xffffffff)
    uint16 m_graphic_increment;
    Counter * m_counter;    // pointer to reagent, etc counter
    // Containers/characters only:
    GameObject * m_head;
    // Characters only:
    uint8 m_notoriety;  // zero if unknown or not a character?

    GameObject(uint32 serial);

    uint32 get_serial() const { return m_serial; }
    bool is_empty() const { return m_head == 0; }

    bool get_interesting() const { return m_interesting; }
    void set_interesting(bool interesting);
    uint16 get_graphic() const { return m_graphic; }
    uint16 get_colour() const { return m_colour; }
    void set_graphic(uint8 * buf) { set_graphic(unpack_big_uint16(buf)); }
    void set_graphic(uint16 graphic);
    void set_colour(uint8 * buf) { m_colour = unpack_big_uint16(buf); }
    void set_colour(uint16 v) { m_colour = v;}
    void set_x(uint8 * buf) { m_x = unpack_big_uint16(buf); }
    void set_y(uint8 * buf) { m_y = unpack_big_uint16(buf)&0x3fff; }
    void set_z(uint8 * buf) { m_z = static_cast<char>(*buf); }
    void set_x(uint16 v) { m_x = v;}
    void set_y(uint16 v) { m_y = v;}
    void set_z(uint8 v) { m_z = v;}
    uint16 get_x() { return m_x; }
    uint16 get_y() { return m_y; }
    int    get_z() { return m_z; }
    void set_direction(uint8 * buf) { m_direction = *buf; }
    void set_flags(uint8 * buf) { m_flags = *buf; }
    uint16 get_quantity() { return m_quantity; }
    void set_quantity(uint8 * buf);
    int get_layer() const { return m_layer; }
    void set_layer(int layer);
    void set_increment(uint8 * buf)
    { m_graphic_increment = unpack_big_uint16(buf); }
    void set_counter(Counter * counter);
    void set_notoriety(uint8 * buf) { m_notoriety = *buf; }

    // Remove an object from this container.
    void remove(GameObject * obj);

    // make this object invalid.
    void make_invalid();

    // Add an object to this container. The object must not already be in
    // a container.
    void add(GameObject * obj);
    // Move an object (that is already in this container) to the head
    // of the list.
    void move_to_head(GameObject * obj);
    // Look for an object inside this container on the given layer. If none
    // are found, returns 0.
    GameObject * find_layer(int layer);
    GameObject * find_graphic(uint16 graphic);
    GameObject * find_graphic(uint16 graphic, uint16 color);
    int count_graphic(uint16 graphic);
    int count_graphic(uint16 graphic, uint16 color);
    iterator begin() { return iterator(m_head); }
    iterator end() { return iterator(0); }

	void set_name(const char* nam);
	const char* get_name();
	protected:
		// Name ptr
		char* name;
};
class World
{
private:
    typedef std::hash_map<uint32, GameObject *> map_t;

    map_t m_map;
    uint32 m_player_serial;
    GameObject * m_player;

public:
    World(uint32 player_serial);
    ~World();

    GameObject * get_player() { return m_player; }
    void set_player(uint32 serial);

    // Find an existing object, or return 0 if the serial was not found
    GameObject * find_object(uint32 serial);
    // Find an existing object, or create one if the serial was not found
    GameObject * get_object(uint32 serial);
    // Find an item in the player's inventory having the specific graphic.
    // Returns 0 if not found.
    GameObject * find_inventory_graphic(uint16 graphic);
    GameObject * find_inventory_graphic(uint16 graphic, uint16 color);
	GameObject * find_world_graphic(uint16 graphic, int distance);
	GameObject * find_world_graphic(uint16 graphic, uint16 color, int distance);
    int count_inventory_graphic(uint16 graphic);
    int count_inventory_graphic(uint16 graphic, uint16 color);

    int count_on_ground(uint16 graphic, int distance);
    int count_on_ground(uint16 graphic, uint16 color, int distance);

    void remove_object(GameObject * obj);

    // If the given object is in a container, remove it. Otherwise do nothing.
    void remove_container(GameObject * obj);
    void put_container(GameObject * obj, uint32 container_serial)
    {
        put_container(obj, get_object(container_serial));
    }
    void put_container(GameObject * obj, GameObject * container);
    void put_equipment(GameObject * obj, uint32 container_serial, int layer)
    {
        put_equipment(obj, get_object(container_serial), layer);
    }
    void put_equipment(GameObject * obj, GameObject * container, int layer)
    {
        put_container(obj, container);
        obj->set_layer(layer);
    }

    void dump();
};

#endif

