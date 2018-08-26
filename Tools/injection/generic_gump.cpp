////////////////////////////////////////////////////////////////////////////////
//
// generic_gump.cpp
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
//  Handles automated generic gump input
//
////////////////////////////////////////////////////////////////////////////////

#include "common.h"
#include "client.h"
#include "hashstr.h"
#include "iconfig.h"

#include "generic_gump.h"

////////////////////////////////////////////////////////////////////////////////
// GenericGump
// public
GenericGump::GenericGump(const string & name, uint32 serial, uint32 id)
:   m_name(name), m_serial(serial), m_id(id)
{
}

GenericGump::~GenericGump()
{
}

void GenericGump::add_button(const string & name, uint32 id)
{
    ASSERT(!button_exists(name));
    m_buttons.insert(button_map_t::value_type(name, id));
}

void GenericGump::remove_button(const string & name)
{
    m_buttons.erase(name);
}

uint32 GenericGump::get_button_id(const string & name)
{
    button_map_t::iterator i = m_buttons.find(name);
    ASSERT(i != m_buttons.end());
    return ((*i).second);
}

void GenericGump::write_config(FILE * fp) const
{
    fprintf(fp,"\t\t\t\t<gump name=\"%s\" serial=\"0x%08lx\" id=\"0x%08lx\"/>\n",
        ConfigManager::escape_attribute(m_name).c_str(), m_serial, m_id);
    for(button_map_t::const_iterator i = m_buttons.begin(); i != m_buttons.end(); i++)
    {
        fprintf(fp,"\t\t\t\t\t<button name=\"%s\" id=\"0x%08lx\"/>\n",
            ConfigManager::escape_attribute((*i).first).c_str(),
            (*i).second);
    }
    fprintf(fp,"\t\t\t\t</gump>\n");
}

/////////////////////////////////////////////////////////////////////////
// GenericGumps
GenericGumps::GenericGumps()
:   m_gumps(0)
{
}

GenericGumps::~GenericGumps()
{
}

void GenericGumps::add_gump(const string & name, uint32 serial, uint32 id)
{
    ASSERT(!gump_exists(name));
    m_gumps.insert(gump_map_t::value_type(name, GenericGump(name, serial, id)));
}

void GenericGumps::add_button(const string & name, const string & button_name, uint32 button_id)
{
    gump_map_t::iterator i = m_gumps.find(name);
    ASSERT(i != m_gumps.end());
    ((*i).second).add_button(button_name, button_id);
}

void GenericGumps::remove_gump(const string & name)
{
    m_gumps.erase(name);
}

void GenericGumps::remove_button(const string & name, const string & button_name)
{
    gump_map_t::iterator i = m_gumps.find(name);
    ASSERT(i != m_gumps.end());
    ((*i).second).remove_button(button_name);
}

uint32 GenericGumps::get_gump_id(const string & name)
{
    gump_map_t::iterator i = m_gumps.find(name);
    ASSERT(i != m_gumps.end());
    return ((*i).second).get_id();
}

uint32 GenericGumps::get_gump_serial(const string & name)
{
    gump_map_t::iterator i = m_gumps.find(name);
    ASSERT(i != m_gumps.end());
    return ((*i).second).get_serial();
}

uint32 GenericGumps::get_button_id(const string & name, const string & button_name)
{
    gump_map_t::iterator i = m_gumps.find(name);
    ASSERT(i != m_gumps.end());
    return ((*i).second).get_button_id(button_name);
}

bool GenericGumps::gump_exists(const string & name)
{
    return m_gumps.find(name) != m_gumps.end();
}

bool GenericGumps::button_exists(const string & name, const string & button_name)
{
    if(gump_exists(name))
        return (*(m_gumps.find(name))).second.button_exists(button_name);
    else
        return false;
}

void GenericGumps::write_config(FILE * fp) const
{
    for(gump_map_t::const_iterator i = m_gumps.begin(); i != m_gumps.end(); i++)
    {
        (*i).second.write_config(fp);
    }
}





/////////////////////////////////////////////////////////////////////////
// GenericGumpHandler
// private
void GenericGumpHandler::send_button(uint32 button_id)
{
    uint8 buf[0x17];
    buf[0] = 0xb1;
    pack_big_uint16(buf + 1,  0x17);
    pack_big_uint32(buf + 3,  m_gump_serial);
    pack_big_uint32(buf + 7,  m_gump_id);
    pack_big_uint32(buf + 11, button_id);
    pack_big_uint32(buf + 15, 0);
    pack_big_uint32(buf + 19, 0);
    m_client.send_server(buf, sizeof(buf));
}

void GenericGumpHandler::choose_button(uint32 button_id)
{
    if(m_state == CHOOSING)
    {
        if(m_has_third_button)
        {
            m_gump_button1_id = m_gump_button2_id;
            m_gump_button2_id = m_gump_button3_id;
            m_gump_button3_id = 0x00;
            m_has_third_button = false;
            m_has_second_button = true;
            m_state = WAITING;
            send_button(button_id);
            return;
        }
        else if(m_has_second_button)
        {
            m_gump_button1_id = m_gump_button2_id;
            m_gump_button2_id = 0x00;
            m_gump_button3_id = 0x00;
            m_has_third_button = false;
            m_has_second_button = false;
            m_state = WAITING;
            send_button(button_id);
            return;
        }
        else
        {
            m_gump_button1_id = 0x00;
            m_gump_button2_id = 0x00;
            m_gump_button3_id = 0x00;
            m_has_third_button = false;
            m_has_second_button = false;
            m_state = NORMAL;
            send_button(button_id);
            m_gump_serial = 0x00;
            m_gump_id = 0x00;
            return;
        }
    }
    else if(m_state == WAITING)
    {
        m_client.client_print("Cannot choose: no gump open, waiting cancelled.");
        m_state = NORMAL;
    }
    else
        m_client.client_print("Cannot choose: no gump open.");
}

// public

GenericGumpHandler::GenericGumpHandler(ClientInterface & client, GenericGumps & gumps)
: m_client(client), m_gumps(gumps), m_state(NORMAL),
  m_new_gump_name(0), m_new_button_name(0),
  m_gump_serial(0), m_gump_id(0),
  m_gump_button1_id(0), m_gump_button2_id(0), m_gump_button3_id(0),
  m_has_second_button(false), m_has_third_button(false)
{
}

GenericGumpHandler::~GenericGumpHandler()
{
}

// This function is called for 0xb0 messages
bool GenericGumpHandler::handle_open_generic_gump(uint8 * buf, int /*size*/)
{
    bool resend = true;
    if(m_state == WAITING)
    {
        uint32 gump_serial = unpack_big_uint32(buf + 3);
        uint32 gump_id = unpack_big_uint32(buf + 7);
        if((gump_serial == m_gump_serial) && (gump_id == m_gump_id))
        {
            m_state = CHOOSING;
            choose_button(m_gump_button1_id);
            resend = false;
        }
    }
    else if(m_state == RECORDING)
    {
        uint32 gump_serial = unpack_big_uint32(buf + 3);
        uint32 gump_id = unpack_big_uint32(buf + 7);
        m_gumps.add_gump(m_new_gump_name, gump_serial, gump_id);
        m_client.client_print(string("Gump captured: ")+m_new_gump_name);
        m_new_gump_name = "";
        m_state = NORMAL;
        resend = false;
    }
    // In state NORMAL, silently pass the menu to the client.
    return resend;
}

// This function is called for 0xb1 messages
bool GenericGumpHandler::handle_send_generic_gump_event(uint8 * buf, int /*size*/)
{
    bool resend = true;
    if(m_state == B_RECORDING)
    {
        uint32 gump_serial = unpack_big_uint32(buf + 3);
        uint32 gump_id = unpack_big_uint32(buf + 7);
        if((gump_serial == m_gumps.get_gump_serial(m_new_gump_name)) &&
           (gump_id == m_gumps.get_gump_id(m_new_gump_name)))
        {
            uint32 button_id = unpack_big_uint32(buf + 11);
            m_gumps.add_button(m_new_gump_name, m_new_button_name, button_id);
            m_client.client_print(string("Gump Button captured: ")+m_new_gump_name
                + " " + m_new_button_name);
            m_new_gump_name = "";
            m_new_button_name = "";
            m_state = NORMAL;
            resend = false;
        }
    }
    // In state NORMAL, silently pass the menu to the client.
    return resend;
}

void GenericGumpHandler::wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button_id)
{
    if(m_state == WAITING)
        m_client.client_print(string("Previous waitgump cancelled: "));
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Previous menu gump cancelled: "));
        send_button(0);
    }
    m_state = WAITING;
    m_gump_serial = gump_serial;
    m_gump_id = gump_id;
    m_gump_button1_id = button_id;
    m_has_second_button = false;
    m_client.client_print("Now waiting for gump...");
}

void GenericGumpHandler::wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button1_id, uint32 button2_id)
{
    if(m_state == WAITING)
        m_client.client_print(string("Previous waitgump cancelled: "));
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Previous menu gump cancelled: "));
        send_button(0);
    }
    m_state = WAITING;
    m_gump_serial = gump_serial;
    m_gump_id = gump_id;
    m_gump_button1_id = button1_id;
    m_has_second_button = true;
    m_gump_button2_id = button2_id;
    m_client.client_print("Now waiting for gump...");
}

void GenericGumpHandler::wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button1_id, uint32 button2_id, uint32 button3_id)
{
    if(m_state == WAITING)
        m_client.client_print(string("Previous waitgump cancelled: "));
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Previous menu gump cancelled: "));
        send_button(0);
    }
    m_state = WAITING;
    m_gump_serial = gump_serial;
    m_gump_id = gump_id;
    m_gump_button1_id = button1_id;
    m_has_second_button = true;
    m_gump_button2_id = button2_id;
    m_has_third_button = true;
    m_gump_button3_id = button3_id;
    m_client.client_print("Now waiting for gump...");
}

void GenericGumpHandler::set_gump(const string & name)
{
    if(m_state == WAITING)
        m_client.client_print(string("Previous waitgump cancelled: "));
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Previous menu gump cancelled: "));
        send_button(0);
    }
    m_new_gump_name = name;
    m_state = RECORDING;
    m_client.client_print("Now waiting for to open gump...");
}

void GenericGumpHandler::unset_gump(const string & name)
{
    m_gumps.remove_gump(name);
}

void GenericGumpHandler::set_button(const string & name, const string & button_name)
{
    if(m_state == B_RECORDING)
        m_client.client_print(string("Previous setgumpbutton cancelled: "));
    m_new_gump_name = name;
    m_new_button_name = button_name;
    m_state = B_RECORDING;
    m_client.client_print("Now waiting for gump responce...");
}

void GenericGumpHandler::unset_button(const string & name, const string & button_name)
{
    m_gumps.remove_button(name, button_name);
}

void GenericGumpHandler::cancel_gump()
{
    if(m_state == NORMAL)
        m_client.client_print("Error: no generic gump to cancel");
    else if(m_state == WAITING)
        m_client.client_print(string("waitgump cancelled: "));
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Generic gump cancelled: "));
        send_button(0);
    }
    else if(m_state == RECORDING)
    {
        m_client.client_print(string("setgump cancelled: "));
    }
    m_state = NORMAL;
}

void GenericGumpHandler::cancel_button()
{
    if(m_state == NORMAL)
        m_client.client_print("Error: setgumpbutton to cancel");
    else if(m_state == B_RECORDING)
    {
        m_client.client_print(string("setgump cancelled: "));
    }
    m_state = NORMAL;
}
