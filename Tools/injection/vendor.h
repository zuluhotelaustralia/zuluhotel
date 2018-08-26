////////////////////////////////////////////////////////////////////////////////
//
// vendor.h
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
//  This module handles automated buying and selling from NPC vendors
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _VENDOR_H_
#define _VENDOR_H_

#include "common.h"
#include "hashstr.h"

#include "world.h"  // for GameObject::iterator

#include <deque>

class ClientInterface;
class World;
class GameObject;
class VendorBuyList;

class VendorBuyItem
{
public:
    sint32 m_price;
    string m_name;

    void set_name(char * name, int name_len);
    const char * get_name() const { return m_name.c_str(); }
};

class VendorBuyIterator
{
private:
    VendorBuyList & m_list;
    VendorBuyItem * m_item;
    int m_index;
    GameObject & m_container;
    GameObject::iterator m_oi;
    bool m_sequential;

public:
    VendorBuyIterator(VendorBuyList & list, GameObject & container);
    // copy constructor
    VendorBuyIterator(const VendorBuyIterator & other);

    // preincrement
    // After increment, is_valid() needs to be called.
    void operator++();
    bool is_valid() const { return m_item != 0; }

    const string & get_name() const
    {
        ASSERT(m_item != 0);
        return m_item->m_name;
    }
    sint32 get_price() const
    {
        ASSERT(m_item != 0);
        return m_item->m_price;
    }
    uint32 get_serial() const
    {
        ASSERT(m_item != 0);
        return m_oi->get_serial();
    }
    uint16 get_quantity() const
    {
        ASSERT(m_item != 0);
        return m_oi->m_quantity;
    }
    int get_layer() const
    {
        ASSERT(m_item != 0);
        return m_oi->get_layer();
    }
};

class VendorBuyList
{
public:
    typedef VendorBuyIterator iterator;

private:
    GameObject & m_container;
    int m_length;
    VendorBuyItem * m_list;

    friend class VendorBuyIterator;

public:
    VendorBuyList(GameObject & container, uint8 * buf, int size);
    ~VendorBuyList();

    int get_layer() const { return m_container.get_layer(); }
    iterator begin();
};

////////////////////////////////////////////////////////////////////////////////

class VendorSellList;

class VendorSellItem
{
public:
    uint32 m_serial;
    uint16 m_quantity;
    sint32 m_price;
    string m_name;

    void set_name(char * name, int name_len);
    const char * get_name() const { return m_name.c_str(); }
};

class VendorSellIterator
{
private:
    VendorSellList * m_list;
    VendorSellItem * m_item;

public:
    VendorSellIterator(VendorSellList & list);
    // copy constructor
    VendorSellIterator(const VendorSellIterator & other);

    // preincrement
    // After increment, is_valid() needs to be called.
    void operator++();
    bool is_valid() const { return m_item != 0; }

    const string & get_name() const
    {
        ASSERT(m_item != 0);
        return m_item->m_name;
    }
    sint32 get_price() const
    {
        ASSERT(m_item != 0);
        return m_item->m_price;
    }
    uint32 get_serial() const
    {
        ASSERT(m_item != 0);
        return m_item->m_serial;
    }
    uint16 get_quantity() const
    {
        ASSERT(m_item != 0);
        return m_item->m_quantity;
    }
};

class VendorSellList
{
public:
    typedef VendorSellIterator iterator;

private:
    int m_length;
    VendorSellItem * m_list;

    friend class VendorSellIterator;

public:
    VendorSellList(uint8 * buf, int size);
    ~VendorSellList();

    iterator begin();
};

////////////////////////////////////////////////////////////////////////////////

class ShoppingList;
class ChooseListDialog;
class EditListDialog;
class ShopListDialog;

class ReplyItem
{
public:
    int m_layer;
    uint32 m_serial;
    uint16 m_quantity;

    ReplyItem(int layer, uint32 serial, uint16 quantity)
    : m_layer(layer), m_serial(serial), m_quantity(quantity)
    {
    }
    // Selling doesn't use a layer:
    ReplyItem(uint32 serial, uint16 quantity)
    : m_layer(0), m_serial(serial), m_quantity(quantity)
    {
    }
};

class ConfigManager;

class VendorHandler
{
public:

private:
    typedef std::deque<ReplyItem> list_t;

    ConfigManager & m_config;
    ClientInterface & m_client;
    World & m_world;
    ServerConfig & m_server;

    enum
    {
        NORMAL, WAITING,
        CHOOSE_LIST, EDIT_LIST, SHOP_LIST
    } m_state;
    bool m_buying, m_selling, m_error;
    GameObject * m_vendor;
    VendorBuyList * m_buy_restock, * m_buy_nonrestock;
    list_t m_reply_list;    // Items that we have selected to buy/sell
    ShoppingList * m_list;

    ChooseListDialog * m_choose_dialog;
    EditListDialog * m_edit_dialog;
    ShopListDialog * m_shop_dialog;

    void client_talk(const string & text);

    void begin_buy(const string & npc_name);
    void begin_sell(const string & npc_name);
    void ignore_buy();
    void do_buy(VendorBuyList & vendor_list);
    void do_sell(VendorSellList & vendor_list);
    void finish_buy();
    void finish_sell(VendorSellList & sell_list);

public:
    VendorHandler(ConfigManager & config, ClientInterface & client,
        World & world, ServerConfig & server);
    ~VendorHandler();

    void buy(const string & name, const string & npc_name);
	void repeat_buy(int rept);
    void sell(const string & name, const string & npc_name);
    void shop();

    bool handle_open_container(uint8 * buf, int size);
    bool handle_vendor_buy_list(uint8 * buf, int size);
    bool handle_vendor_sell_list(uint8 * buf, int size);
    void handle_buy_reply(uint8 * buf, int size);

    // Callbacks from GUI:
    void cancel_choose();
    void edit_list(const string & name);
    void delete_list(const string & name);
    void shop_list(const string & name);
    void create_list(const string & name);
    void done_edit(ShoppingList & list);
    void done_shop();
    void shop_buy(ShoppingList & list, const string & npc_name);
    void shop_sell(ShoppingList & list, const string & npc_name);
    // Returns true if buying/selling was cancelled.
    bool shop_cancel();
};

#endif

