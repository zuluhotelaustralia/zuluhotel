////////////////////////////////////////////////////////////////////////////////
//
// runebook.cpp
//
// Copyright (C) 2001 Wayne 'chiphead' Hogue
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
//  Handles automated runebook recharging
//
////////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <time.h>
#include "common.h"
#include "client.h"
#include "target.h"
#include "runebook.h"


RunebookHandler::RunebookHandler(ClientInterface & client, TargetInterface & target)
: m_client(client), m_target(target), m_state(NORMAL)
{
}

RunebookHandler::~RunebookHandler()
{
}


void RunebookHandler::send(uint32 code)
{
    uint8 buf[0x17];
    buf[0] = 0xb1;
    pack_big_uint16(buf+1, 0x17);
    pack_big_uint32(buf+3, m_runebook_serial);
    pack_big_uint32(buf+7, m_runebook_type);
    pack_big_uint32(buf+11, code);
    pack_big_uint32(buf+15, 0x0);
    pack_big_uint32(buf+19, 0x0);
    m_client.send_server(buf, 0x17);
}

void RunebookHandler::open_book()
{
    uint8 buf[0x5];
    buf[0] = 0x06;
    pack_big_uint32(buf+1, m_runebook_serial);
    m_client.send_server(buf, 0x5);
}

// This function is called for 0xb0 messages
bool RunebookHandler::handle_runebook(uint8 * buf, int /*size*/)
{
    bool resend = true;
    if(m_state == ADDCHARGE)
    {
        uint32 serial = unpack_big_uint32(buf+3);
        uint32 gump_type = unpack_big_uint32(buf+7);
        if(serial == m_runebook_serial)
        {
            resend = false;
            m_state = ADDSCROLL;
            m_runebook_type = gump_type;
            send(M_ADD_CHARGE_CODE);
        }
    }
    else if(m_state == ADDSCROLL)
    {
        uint32 serial = unpack_big_uint32(buf+3);
        uint32 gump_type = unpack_big_uint32(buf+7);
        if(serial == m_runebook_serial)
        {
            resend = false;
            m_state = NORMAL;
            m_target.wait_target(m_target_scroll,0x000f); //Yoko: needs to be corrected maybe
            m_runebook_type = gump_type;
            send(m_code_to_add);
        }
    }
    else if(m_state == TURNPAGE)
    {
        uint32 serial = unpack_big_uint32(buf+3);
        uint32 gump_type = unpack_big_uint32(buf+7);
        if(serial == m_runebook_serial)
        {
            resend = false;
            m_state = CLICKBUTTON;
            m_runebook_type = gump_type;
            send(m_page);
        }
    }
    else if(m_state == CLICKBUTTON)
    {
        uint32 serial = unpack_big_uint32(buf+3);
        uint32 gump_type = unpack_big_uint32(buf+7);
        if(serial == m_runebook_serial)
        {
            resend = false;
            m_state = NORMAL;
            m_runebook_type = gump_type;
            send(m_button);
        }
    }
    // In state NORMAL, silently pass the target request to the client.
    return resend;
}

void RunebookHandler::add_recall(uint32 runebook, uint32 scroll)
{
    if(m_state != NORMAL)
    {
        m_client.client_print("Add recall canceled");
        send(0x0);
    }
    m_runebook_serial = runebook;
    m_code_to_add = M_ADD_RECALL_CODE;
    m_target_scroll = scroll;
    m_state = ADDCHARGE;
    open_book();
}

void RunebookHandler::add_gate(uint32 runebook, uint32 scroll)
{
    if(m_state != NORMAL)
    {
        m_client.client_print("Add gate canceled");
        send(0x0);
    }
    m_runebook_serial = runebook;
    m_code_to_add = M_ADD_GATE_CODE;
    m_target_scroll = scroll;
    m_state = ADDCHARGE;
    open_book();
}

void RunebookHandler::set_default(uint32 runebook, int rune)
{
    if(m_state != NORMAL)
    {
        m_client.client_print("Set default rune canceled");
        send(0x0);
    }
    m_runebook_serial = runebook;
    m_page = get_page_code(rune);
    m_button = get_set_default_rune_code(rune);
    m_state = TURNPAGE;
    open_book();
}

void RunebookHandler::recall(uint32 runebook, int rune)
{
    if(m_state != NORMAL)
    {
        m_client.client_print("Set default rune canceled");
        send(0x0);
    }
    m_runebook_serial = runebook;
    m_button = get_recall_rune_code(rune);
    m_state = CLICKBUTTON;
    open_book();
}

void RunebookHandler::gate(uint32 runebook, int rune)
{
    if(m_state != NORMAL)
    {
        m_client.client_print("Set default rune canceled");
        send(0x0);
    }
    m_runebook_serial = runebook;
    m_button = get_gate_rune_code(rune);
    m_state = TURNPAGE;
    open_book();
}
