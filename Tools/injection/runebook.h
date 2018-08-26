////////////////////////////////////////////////////////////////////////////////
//
// runebook.h
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
//  Declarations for classes in runebook.cpp
//  Handles automated targeting commands
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _RUNEBOOK_H_
#define _RUNEBOOK_H_

#include "common.h"

#include <string>
using std::string;

const uint32 M_ADD_CHARGE_CODE = 0x0000012c;
const uint32 M_ADD_RECALL_CODE = 0x0000005c;
const uint32 M_ADD_GATE_CODE = 0x0000005d;

const uint32 M_PAGE_BASE = 0x000000ca;
inline uint32 get_page_code(int page)
{
    int m_page;
    if(page < 1) m_page = 1;
    else if(page > 16) m_page = (page - ((page/16)*16));
    else m_page = page;
    return(( M_PAGE_BASE + (m_page / 2) + (m_page % 2))-1);
}

const uint32 M_SET_DEFAULT_RUNE_BASE = 0x0000001f;
inline uint32 get_set_default_rune_code(int page)
{
    int m_page;
    if(page < 1) m_page = 1;
    else if(page > 16) m_page = (page - ((page/16)*16));
    else m_page = page;
    return((M_SET_DEFAULT_RUNE_BASE + m_page)-1);
}

const uint32 M_RECALL_RUNE_BASE = 0x00000001;
inline uint32 get_recall_rune_code(int page)
{
    int m_page;
    if(page < 1) m_page = 1;
    else if(page > 16) m_page = (page - ((page/16)*16));
    else m_page = page;
    return((M_RECALL_RUNE_BASE + m_page)-1);
}

const uint32 M_GATE_RUNE_BASE = 0x00000047;
inline uint32 get_gate_rune_code(int page)
{
    int m_page;
    if(page < 1) m_page = 1;
    else if(page > 16) m_page = (page - ((page/16)*16));
    else m_page = page;
    return((M_GATE_RUNE_BASE + m_page)-1);
}

class ClientInterface;
class TargetInterface;

// This class used for handling runebooks,
class RunebookHandler
{
private:
    ClientInterface & m_client;
    TargetInterface & m_target;

    enum { NORMAL, ADDCHARGE, ADDSCROLL, TURNPAGE, CLICKBUTTON } m_state;

    uint32 m_runebook_serial;
    uint32 m_code_to_add;
    uint32 m_target_scroll;
    uint32 m_button;
    uint32 m_page;
    uint32 m_runebook_type;

    void send(uint32 code);
    void open_book();

public:
    RunebookHandler(ClientInterface & client, TargetInterface & target);
    ~RunebookHandler();

    // This function returns true if the gump open request should be sent to the client.
    bool handle_runebook(uint8 * buf, int size);
    void add_recall(uint32 runebook, uint32 scroll);
    void add_gate(uint32 runebook, uint32 scroll);
    void set_default(uint32 runebook, int rune);
    void recall(uint32 runebook, int rune);
    void gate(uint32 runebook, int rune);
};

#endif

