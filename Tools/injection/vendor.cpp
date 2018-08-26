////////////////////////////////////////////////////////////////////////////////
//
// vendor.cpp
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

#include "common.h"
#include "world.h"
#include "client.h"
#include "hotkeyhook.h"
#include "igui.h"
#include "iconfig.h"

#include "vendor.h"

const maxcount=40;
int buysize=0;
uint8 buybuf[8 + 7 * (maxcount+1)];

////////////////////////////////////////////////////////////////////////////////

//// Members of VendorBuyIterator/VendorBuyItem/VendorBuyList:

VendorBuyIterator::VendorBuyIterator(VendorBuyList & list,
    GameObject & container)
: m_list(list), m_index(0),
  m_container(container), m_oi(container.begin())
{
    if(m_oi == m_container.end())
    {
        m_item = 0;
        m_index = m_list.m_length;
        m_sequential = true;    // unused
    }
    else
    {
        if(m_oi->m_x == m_list.m_length - 1 && m_oi->m_y == m_oi->m_x)
        {
            trace_printf("Buy list is NOT sequential\n");
            m_index = m_oi->m_x;
            m_item = m_list.m_list + m_index;
            m_sequential = false;
        }
        else
        {
            trace_printf("Buy list is sequential\n");
            m_index = 0;
            m_item = m_list.m_list;
            m_sequential = true;
        }
    }
}

// copy constructor
VendorBuyIterator::VendorBuyIterator(const VendorBuyIterator & other)
: m_list(other.m_list), m_item(other.m_item), m_index(other.m_index),
  m_container(other.m_container), m_oi(other.m_oi),m_sequential(other.m_sequential)
{
}

void VendorBuyIterator::operator++()
{
    ASSERT(m_item != 0);
    m_oi++;
    if(m_oi == m_container.end())
    {
        m_index++;
        m_item = 0;
        // Buy list message has more items than the actual container.
        if(m_sequential && m_index < m_list.m_length)
            error_printf("buy list too long: #%d\n", m_index);
    }
    else if(m_sequential)
    {
        m_index++;
        if(m_index >= m_list.m_length)
        {
            error_printf("buy list too short: %d\n", m_index);
            m_item = 0;
        }
        else
            m_item++;
    }
    else
    {
        if(m_oi->m_x != m_oi->m_y)
        {
            error_printf("x != y for buy item\n");
            m_item = 0;
        }
        else if(m_oi->m_x >= m_list.m_length)
        {
            error_printf("buy item index invalid: %d\n", m_oi->m_x);
            m_item = 0;
        }
        else
        {
            m_index = m_oi->m_x;
            m_item = m_list.m_list + m_index;
        }
    }
}

void VendorBuyItem::set_name(char * name, int name_len)
{
    m_name.assign(name, name_len);
    // NOTE: must remove null bytes at end of string
    string::size_type null = m_name.find_first_of((char)'\0');
    if(null != string::npos)
        m_name.erase(null);
}
    
VendorBuyList::VendorBuyList(GameObject & container, uint8 * buf, int size)
: m_container(container)
{
    // Store buy list
    m_length = buf[7];
    trace_printf("number of items: %d\n", m_length);
    m_list = new VendorBuyItem[m_length];
    uint8 * ptr = buf + 8;
	int i=		Shard==AoP?m_length-1:0;
	int iend=	Shard==AoP?-1:m_length;
	int iadd=	Shard==AoP?-1:1;
//    for(int i = 0; i < m_length; i++) //Yoko
    for(;i!=iend; i+=iadd)
    {
        m_list[i].m_price = unpack_big_sint32(ptr);
        ptr += 4;
        int name_len = *ptr++;
        m_list[i].set_name(reinterpret_cast<char *>(ptr), name_len);
        ptr += name_len;
        trace_printf("#%d: %s  $%ld\n",
            i, m_list[i].get_name(), m_list[i].m_price); 
    }
    // Check message size
    if(ptr - buf != size)
        warning_printf("buy list expected message size %d\n", ptr - buf);
}

VendorBuyList::~VendorBuyList()
{
    delete [] m_list;
}

VendorBuyList::iterator VendorBuyList::begin()
{
    return iterator(*this, m_container);
}

////////////////////////////////////////////////////////////////////////////////

VendorSellIterator::VendorSellIterator(VendorSellList & list)
: m_list(&list), m_item(list.m_list)
{
}

VendorSellIterator::VendorSellIterator(const VendorSellIterator & other)
: m_list(other.m_list), m_item(other.m_item)
{
    if(m_list->m_length == 0)
        m_item = 0;
}

void VendorSellIterator::operator++()
{
    ASSERT(m_item != 0);
    m_item++;
    if(m_item == m_list->m_list + m_list->m_length)
        m_item = 0;
}

void VendorSellItem::set_name(char * name, int name_len)
{
    m_name.assign(name, name_len);
    // NOTE: must remove null bytes at end of string
    string::size_type null = m_name.find_first_of((char)'\0');
    if(null != string::npos)
        m_name.erase(null);
}

VendorSellList::VendorSellList(uint8 * buf, int size)
{
    // Store sell list
    m_length = unpack_big_uint16(buf + 7);
    trace_printf("number of items: %d\n", m_length);
    m_list = new VendorSellItem[m_length];
    uint8 * ptr = buf + 9;
    for(int i = 0; i < m_length; i++)
    {
        m_list[i].m_serial = unpack_big_uint32(ptr);
        m_list[i].m_quantity = unpack_big_uint16(ptr + 8);
        m_list[i].m_price = unpack_big_uint16(ptr + 10);
        int name_len = unpack_big_uint16(ptr + 12);
        ptr += 14;
        m_list[i].set_name(reinterpret_cast<char *>(ptr), name_len);
        ptr += name_len;
    }
    // Check message size
    if(ptr - buf != size)
        warning_printf("expected sell list message size %d\n", ptr - buf);
}

VendorSellList::~VendorSellList()
{
    delete [] m_list;
}

VendorSellList::iterator VendorSellList::begin()
{
    return iterator(*this);
}

////////////////////////////////////////////////////////////////////////////////

/*
    When the player says "buy" to a vendor, the server sends:

    - Server Equip messages for containers on layers 0x1a and 0x1b of
      the vendor's body (LAYER_VENDOR_BUY_RESTOCK, LAYER_VENDOR_BUY).
    - Update Contained Items message for layer 0x1a container.
    - Vendor Buy List message for layer 0x1a container.
    - Update Contained Items message for layer 0x1b container.
    - Vendor Buy List message for layer 0x1b container.
    - Open Container message for the vendor character, gump 0x30.

    NOTE: items in the Buy List are in reverse order to the Contained Items
    message, but this is okay because GameObject::add() adds items to the
    beginning of the container's linked list.

    However, when the player says "sell" to a vendor, it is much simpler
    because only a single message is sent to the client: Vendor Sell List.
*/

VendorHandler::VendorHandler(ConfigManager & config, ClientInterface & client,
    World & world, ServerConfig & server)
: m_config(config), m_client(client), m_world(world), m_server(server),
  m_state(NORMAL), m_buying(false), m_selling(false), m_vendor(0),
  m_buy_restock(0), m_buy_nonrestock(0), m_list(0),
  m_choose_dialog(0), m_edit_dialog(0), m_shop_dialog(0)
{
}

VendorHandler::~VendorHandler()
{
    if(m_shop_dialog != 0)
        m_shop_dialog->destroy();
    if(m_edit_dialog != 0)
        m_edit_dialog->destroy();
    if(m_choose_dialog != 0)
        m_choose_dialog->destroy();
    delete m_buy_restock;
    delete m_buy_nonrestock;
}

// private
void VendorHandler::client_talk(const string & text)
{
    int size = 8 + text.length() + 5;
    uint8 * buf = new uint8[size];
    buf[0] = 0x03;  // client talk
    pack_big_uint16(buf + 1, size);
    buf[3] = 0; // talk mode normal
    pack_big_uint16(buf + 4, 0x03b6);   // colour: grey
    pack_big_uint16(buf + 6, 3);    // font: normal
    memcpy(buf + 8, text.c_str(), text.length() + 1);
    m_client.send_server(buf, size);
    delete /*[]*/ buf;
}

// private
void VendorHandler::begin_buy(const string & npc_name)
{
    ASSERT(m_list != 0);

    m_state = WAITING;
    m_buying = true;
    m_error = false;
    m_client.client_print("Waiting to buy from vendor...");

    client_talk(npc_name + " " + m_server.get_buy_text());
}

// private
void VendorHandler::begin_sell(const string & npc_name)
{
    ASSERT(m_list != 0);

    m_state = WAITING;
    m_selling = true;
    m_client.client_print("Waiting to sell to vendor...");

    client_talk(npc_name + " " + m_server.get_sell_text());
}

// private
void VendorHandler::ignore_buy()
{
    if(m_buying)
    {
        if(m_state == WAITING)
            m_client.client_print("Buy cancelled.");
        m_error = true;
        if(m_shop_dialog != 0)
            m_shop_dialog->buy_error();
    }
}

// private
void VendorHandler::do_buy(VendorBuyList & vendor_list)
{
	trace_printf("do_buy %p\n",&vendor_list);
    ASSERT(m_list != 0);
    VendorBuyList::iterator vi = vendor_list.begin();
	int items=0;
    while(vi.is_valid())
    {
        if (items>=maxcount)
		{trace_printf("do_buy: maxcount reached\n");return;}

		int quantity = vi.get_quantity();
        uint32 serial = vi.get_serial();
        sint32 price = vi.get_price();
        // No newline printed yet:
        trace_printf("do_buy %s: q:%d, $%ld, 0x%08lx  ", vi.get_name().c_str(),
            quantity, vi.get_price(), serial);
        // Check for the item on the 'shopping list':
        int index = m_list->index_of(vi.get_name());
        if(index == -1)
            trace_printf("(not wanted)\n");
        else
        {
            ShoppingItem & item=((*m_list)[index]);
            item.m_available += quantity;
            if(item.m_want == item.m_got)
                trace_printf("(bought enough)\n");
            else if(item.m_want == WANT_ALL)
            {
                trace_printf("(want all, %u available)\n", quantity);
                m_reply_list.push_back(ReplyItem(vendor_list.get_layer(),
                    serial, quantity)); 
				items++;
				trace_printf("pusha %i of 0x%08lx\n",quantity,serial);
                item.m_got += quantity;
                item.m_paid += quantity * price;
            }
            else
            {
                int want = item.m_want - item.m_got;
                trace_printf("(want %d, %u available)\n", want, quantity);
                if(want <= quantity)
                {
                    item.m_got += want;
                    item.m_paid += want * price;
                    // We can buy what the player wanted.
                    m_reply_list.push_back(ReplyItem(vendor_list.get_layer(),
                        serial, want));
					items++;
				trace_printf("push! %i of 0x%08lx\n",quantity,serial);
                }
                else    // not enough available
                {
                    item.m_got += quantity;
                    item.m_paid += quantity * price;
                    m_reply_list.push_back(ReplyItem(vendor_list.get_layer(),
                        serial, quantity));
					items++;
				trace_printf("push< %i of 0x%08lx\n",quantity,serial);
                }
            }
        }
        ++vi;
    }
}

// private
void VendorHandler::do_sell(VendorSellList & vendor_list)
{

	int totalcount=0;
    ASSERT(m_list != 0);
    VendorSellList::iterator vi = vendor_list.begin();
    while(vi.is_valid()&&totalcount<maxcount)
    {
        int quantity = vi.get_quantity();
        uint32 serial = vi.get_serial();
        sint32 price = vi.get_price();
        // No newline printed yet:
        trace_printf("%s: q:%d, $%ld, 0x%08lx  ", vi.get_name().c_str(),
            quantity, vi.get_price(), serial);
        // Check for the item on the 'shopping list':
        int index = m_list->index_of(vi.get_name());
        if(index == -1)
            trace_printf("(not for sale)\n");
        else
        {
            ShoppingItem & item=((*m_list)[index]);
            item.m_available += quantity;
            if(item.m_want == item.m_got)
                trace_printf("(sold enough)\n");
            else if(item.m_want == WANT_ALL)
            {
				if(totalcount+quantity>maxcount)
				{
					quantity=maxcount-totalcount;
					trace_printf(" maxcount reached ");
				}
                trace_printf("(want to sell all, selling %u)\n", quantity);
                m_reply_list.push_back(ReplyItem(serial, quantity));
                item.m_got += quantity;
                item.m_paid += quantity * price;
				totalcount+=quantity;
            }
            else
            {
                int want = item.m_want - item.m_got;
                trace_printf("(want to sell %d, selling %u)\n", want, quantity);
                if(want > quantity) want=quantity;
				if(totalcount+want>maxcount)
				{
					want=maxcount-totalcount;
					trace_printf(" maxcount reached, selling %u\n", want);
				}
                    item.m_got += want;
                    item.m_paid += want * price;
                    m_reply_list.push_back(ReplyItem(serial, want));
            }
        }
        ++vi;
    }
	char bf[80];sprintf(bf,"For sold: %i items.",totalcount);
	m_client.client_print(bf);
}

// private
void VendorHandler::finish_buy()
{
    m_reply_list.clear();
    m_list->reset();
    if(m_buy_restock == 0)
        warning_printf("no buy list received for restock layer\n");
    else
        do_buy(*m_buy_restock);
    if(m_buy_nonrestock == 0)
        warning_printf("no buy list received for non-restock layer\n");
    else
        do_buy(*m_buy_nonrestock);

    if(m_reply_list.size() == 0)
        m_client.client_print("No items bought.");
    else if(m_reply_list.size() > 255)  // Can this happen?
        m_client.client_print("Cannot buy: more than 255 items.");
    else
    {
		char bf[80]; sprintf(bf,"%i types of items bought", m_reply_list.size());
        m_client.client_print(bf);
        // Construct a Vendor Buy Reply message
        buysize = 8 + 7 * m_reply_list.size();
        //uint8 * buf = new uint8[size];
        buybuf[0] = CODE_VENDOR_BUY_REPLY;
        pack_big_uint16(buybuf + 1, buysize);
        pack_big_uint32(buybuf + 3, m_vendor->get_serial());
        buybuf[7] = m_reply_list.size();
        uint8 * ptr = buybuf + 8;
        for(list_t::iterator i = m_reply_list.begin(); i != m_reply_list.end(); i++)
        {
            ptr[0] = (*i).m_layer;
            pack_big_uint32(ptr + 1, (*i).m_serial);
            pack_big_uint16(ptr + 5, (*i).m_quantity);
            ptr += 7;
			trace_printf("buyreply: q:%d of 0x%08lx\n",(*i).m_quantity,(*i).m_serial);
        }
        m_client.send_server(buybuf, buysize);
        //delete /*[]*/ buf;
    }
}

void VendorHandler::handle_buy_reply(uint8 * buf, int size)
{
 memcpy(buybuf,buf,size);
 buysize=size;
}

// private
void VendorHandler::finish_sell(VendorSellList & sell_list)
{
    m_reply_list.clear();
    m_list->reset();
    do_sell(sell_list);

    if(m_reply_list.size() == 0)
        m_client.client_print("No items sold.");
    else
    {
        m_client.client_print("Items sold.");
        // Construct a Vendor Sell Reply message
        int size = 9 + 6 * m_reply_list.size();
        uint8 * buf = new uint8[size];
        buf[0] = 0x9f;  // Vendor Sell Reply
        pack_big_uint16(buf + 1, size);
        pack_big_uint32(buf + 3, m_vendor->get_serial()); ///
        pack_big_uint16(buf + 7, m_reply_list.size());
        uint8 * ptr = buf + 9;
        for(list_t::iterator i = m_reply_list.begin(); i != m_reply_list.end(); i++)
        {
            pack_big_uint32(ptr, (*i).m_serial);
            pack_big_uint16(ptr + 4, (*i).m_quantity);
            ptr += 6;
        }
        m_client.send_server(buf, size);
        delete /*[]*/ buf;
    }
}

void VendorHandler::buy(const string & name, const string & npc_name)
{
    if(m_state == WAITING)
    {
        m_client.client_print("Previous waiting cancelled.");
        m_state = NORMAL;
    }
    else if(m_buying)
    {
        m_client.client_print("Already buying. Cancelling...");
        shop_cancel();
        return;
    }
    else if(m_state != NORMAL)
    {
//      m_client.client_print("Please cancel the current operation or dialog before using 'buy'.");
        m_client.client_print("Cancelling previous operation...");
        m_state = NORMAL;
//      return;
    }
    if(!m_config.list_exists(name))
    {
        m_client.client_print(string("Shopping list does not exist: ") +
            name);
        m_state = NORMAL;
        return;
    }
    m_list = &m_config.find_list(name);
    begin_buy(npc_name);
}

void VendorHandler::sell(const string & name, const string & npc_name)
{
    if(m_state == WAITING)
    {
        m_client.client_print("Previous waiting cancelled.");
        m_state = NORMAL;
    }
    else if(m_selling)
    {
        m_client.client_print("Previous sell was unsuccessful. Restarting...");
        m_state = NORMAL;
        m_list = 0;
        m_buying = m_selling = false;
//      return;
    }
    else if(m_state != NORMAL)
    {
//      m_client.client_print("Please cancel the current operation or dialog before using 'sell'.");
        m_client.client_print("Cancelling current operation...");
        m_state = NORMAL;
//      return;
    }
    if(!m_config.list_exists(name))
    {
        m_client.client_print(string("Shopping list does not exist: ") +
            name);
        m_state = NORMAL;
        return;
    }
    m_list = &m_config.find_list(name);
    begin_sell(npc_name);
}

// This method opens the vendor GUI (choose list)
void VendorHandler::shop()
{
    if(m_state == CHOOSE_LIST)
    {
        m_client.client_print("The shop window is already open!");
        return;
    }
    else if(m_state != NORMAL)
    {
        m_client.client_print("Please cancel the current operation or dialog before using 'shop'.");
        return;
    }
    m_state = CHOOSE_LIST;
    m_choose_dialog = new ChooseListDialog(m_config, *this);
    m_choose_dialog->create();
}

bool VendorHandler::handle_open_container(uint8 * buf, int /*size*/)
{
    if(!m_buying)
        return true;
    uint16 gump = unpack_big_uint16(buf + 5);
    if(gump != 0x30)    // Buy gump
        return true;

    uint32 serial = unpack_big_uint32(buf + 1);
    if(m_error)
        warning_printf("open container (vendor) ignored\n");
    else if(m_vendor == 0)
    {
        m_client.client_print("Buy error: no list");
        error_printf("open container without buy list\n");
        ignore_buy();
    }
    else if(serial != m_vendor->get_serial())
    {
		if(Shard==AoP)
			m_vendor = m_world.find_object(serial);
		else
		{
		char buf[80];
		sprintf(buf,"Buy error: wrong vendor (0x%08lx, waited: 0x%08lx)\n", serial, m_vendor->get_serial());
        m_client.client_print(buf);
        error_printf("open buy for different vendor\n");
        ignore_buy();
		}
    }

    if(!m_error)
    {
        if(m_list != 0)     // m_state == WAITING
            finish_buy();
        else                // m_state == EDIT_LIST
        {
            ASSERT(m_edit_dialog != 0);
            m_edit_dialog->clear_vendor_list();
            if(m_buy_restock != 0)
                m_edit_dialog->add_buy_list(*m_buy_restock);
            if(m_buy_nonrestock != 0)
                m_edit_dialog->add_buy_list(*m_buy_nonrestock);
            m_client.client_print("Buy list loaded.");
        }
    }
    else
        m_error = false;
    if(m_state == WAITING)
    {
        m_buying = false;
        // If we are buying using the dialog, go back to that state.
        if(m_shop_dialog == 0)
            m_state = NORMAL;
        else
        {
            m_state = SHOP_LIST;
            m_shop_dialog->finished_buy();
        }
    }
    m_vendor = 0;
    delete m_buy_restock;
    m_buy_restock = 0;
    delete m_buy_nonrestock;
    m_buy_nonrestock = 0;
    return false;
}

bool VendorHandler::handle_vendor_buy_list(uint8 * buf, int size)
{
    if(!m_buying)
        return true;
    if(m_error)
    {
        warning_printf("vendor buy list ignored\n");
        return false;
    }

    // Lookup the container object
    uint32 serial = unpack_big_uint32(buf + 3);
    GameObject * container = m_world.find_object(serial);
		//char bf[80];
		//sprintf(bf,"Vendor container 0x%08lx\n", serial);
        //m_client.client_print(bf);

	if(container == 0)
    {
        m_client.client_print("Buy error: unknown container");
        error_printf("unknown buy container: 0x%08lx\n", serial);
        ignore_buy();
        return false;
    }

    // Check for valid vendor object, and remember it
    GameObject * vendor = m_world.find_object(container->m_container);

    if(vendor == 0)
    {
        m_client.client_print("Buy error: bad vendor");
        error_printf("unknown vendor NPC: 0x%08lx",
            container->m_container);
        ignore_buy();
        return false;
    }
    else if(m_vendor != 0)  // Vendor is already known
    {
        if(vendor != m_vendor)
        {
            m_client.client_print("Buy error: different vendors");
            error_printf("different vendor NPCs: 0x%08lx, 0x%08lx",
                m_vendor->get_serial(), container->m_container);
            // Just ignore this buy list and continue.
            return false;
        }
    }
    else
        m_vendor = vendor;

    // Check for container on correct layer, and store list
    if(container->get_layer() == LAYER_VENDOR_BUY_RESTOCK)
    {
        trace_printf("Buy List layer: restock\n");
        if(m_buy_restock == 0)
            m_buy_restock = new VendorBuyList(*container, buf, size);
        else
            warning_printf("duplicate buy list ignored (restock)\n");
    }
    else if(container->get_layer() == LAYER_VENDOR_BUY)
    {
        trace_printf("Buy List layer: non-restock\n");
        if(m_buy_nonrestock == 0)
            m_buy_nonrestock = new VendorBuyList(*container, buf, size);
        else
            warning_printf("duplicate buy list ignored (non-restock)\n");
    }
    else
    {
		if (Shard!=AoP)
		{
			char bf[40]; sprintf(bf,"Buy error: bad layer (%i)",container->get_layer());
			m_client.client_print(bf);
			error_printf("buy container on invalid layer: %d\n",
            container->get_layer());
			ignore_buy();
		}
		else
		{
		container->set_layer(LAYER_VENDOR_BUY_RESTOCK);
        if(m_buy_restock == 0)
            m_buy_restock = new VendorBuyList(*container, buf, size);
        else
            warning_printf("duplicate buy list ignored (non-restock)\n");
		}
        return false;
    }

    return false;
}

bool VendorHandler::handle_vendor_sell_list(uint8 * buf, int size)
{
    if(!m_selling)
        return true;

    uint32 vserial = unpack_big_uint32(buf + 3);
    m_vendor = m_world.get_object(vserial);
    VendorSellList list(buf, size);

    if(m_list != 0)     // m_state == WAITING
        finish_sell(list);
    else                // m_state == EDIT_LIST
    {
        ASSERT(m_edit_dialog != 0);
        m_edit_dialog->clear_vendor_list();
        m_edit_dialog->add_sell_list(list);
        m_client.client_print("Sell list loaded.");
    }

    if(m_state == WAITING)
    {
        m_selling = false;
        // If we are selling using the dialog, go back to that state.
        if(m_shop_dialog == 0)
            m_state = NORMAL;
        else
        {
            m_state = SHOP_LIST;
            m_shop_dialog->finished_sell();
        }
    }
    m_vendor = 0;

    return false;
}

void VendorHandler::cancel_choose()
{
    ASSERT(m_state == CHOOSE_LIST);
    m_choose_dialog->destroy();
    m_choose_dialog = 0;
    m_state = NORMAL;
}

void VendorHandler::edit_list(const string & name)
{
    ASSERT(m_state == CHOOSE_LIST);
    m_choose_dialog->destroy();
    m_choose_dialog = 0;
    m_state = EDIT_LIST;
    // Start buying/selling. In theory we could be part way through a buy. :/
    m_buying = m_selling = true;
    m_error = false;
    m_list = 0;
    m_edit_dialog = new EditListDialog(*this, m_config.find_list(name));
    m_edit_dialog->create();
}

void VendorHandler::delete_list(const string & name)
{
    ASSERT(m_state == CHOOSE_LIST);
    m_config.delete_list(name);
}

void VendorHandler::shop_list(const string & name)
{
    ASSERT(m_state == CHOOSE_LIST);
    m_choose_dialog->destroy();
    m_choose_dialog = 0;
    m_state = SHOP_LIST;
    m_shop_dialog = new ShopListDialog(*this, m_config.find_list(name));
    m_shop_dialog->create();
}

void VendorHandler::create_list(const string & name)
{
    ASSERT(m_state == CHOOSE_LIST);
    if(m_config.list_exists(name))
        m_client.client_print("Cannot create list: name already exists");
    else
        m_config.create_list(name);
    edit_list(name);
}

void VendorHandler::done_edit(ShoppingList & list)
{
    ASSERT(m_state == EDIT_LIST);
    m_edit_dialog->destroy();
    m_edit_dialog = 0;

    // Stop buying. If we are half way through buying this could cause
    // problems. :/
    m_buying = m_selling = false;
    m_vendor = 0;
    delete m_buy_restock;
    m_buy_restock = 0;
    delete m_buy_nonrestock;
    m_buy_nonrestock = 0;

    m_state = SHOP_LIST;
    m_shop_dialog = new ShopListDialog(*this, list);
    m_shop_dialog->create();
}

void VendorHandler::done_shop()
{
    if(m_state == WAITING)
        shop_cancel();
    else
        ASSERT(m_state == SHOP_LIST);

    if(m_shop_dialog)
        m_shop_dialog->destroy();

    m_shop_dialog = 0;
    m_state = NORMAL;
}

void VendorHandler::shop_buy(ShoppingList & list, const string & npc_name)
{
    ASSERT(m_state == SHOP_LIST);
    m_list = &list;
    begin_buy(npc_name);
}

void VendorHandler::shop_sell(ShoppingList & list,
    const string & npc_name)
{
    ASSERT(m_state == SHOP_LIST);
    m_list = &list;
    begin_sell(npc_name);
}

bool VendorHandler::shop_cancel()
{
//-mamaich- if(m_state == WAITING)
    {
        m_state = SHOP_LIST;
        m_list = 0;
        m_buying = m_selling = false;
        m_client.client_print("Waiting cancelled.");
        return true;
    }
    ASSERT(m_state == SHOP_LIST);
    return false;
}


void VendorHandler::repeat_buy(int rept)
{
	if(buysize)
	{
		m_client.client_print("Repeating last buy...");
	if(rept<2)
	{
		trace_printf("buyrepeat\n");
		m_client.send_server(buybuf, buysize);
	}
	else
	{
		int qnow=(buysize-8)/7;
		int times=254/qnow;
		if(rept<times) times=rept;
		for(int i=1;i<times;i++)
			memcpy(buybuf+8+(buysize-8)*i,buybuf+8,buysize-8);
        int newsize = 8 + (buysize-8) * times;
		trace_printf("qnow=%i\ttimes=%i\tnewsize=%i\n",qnow,times,newsize);
		m_client.send_server(buybuf, newsize);
	}
    }
	else
    m_client.client_print("Nothing to repeat");
}
