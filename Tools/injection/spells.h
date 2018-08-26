////////////////////////////////////////////////////////////////////////////////
//
// spells.h
//
// Copyright (C) 2001 Wayne (Chiphead) Hogue
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
//  Declarations for classes in spells.cpp
//
//  Handles casting spells
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _SPELLS_H_
#define _SPELLS_H_

#include "common.h"
#include "hashstr.h"
#include "client.h"
#include "target.h"

const int SPELLS_BASE_MSG_LEN = 5;

class Spell
{
private:
    string m_code;
    bool m_target;
    bool m_menu;

public:
    Spell():m_code(0), m_target(false), m_menu(false) {}
    Spell(const char * code, bool target, bool menu):m_code(code), m_target(target), m_menu(menu) {}
    ~Spell(){};
    string get_code() { return m_code; }
    bool can_target() { return m_target; }
    bool has_menu() { return m_menu; }
    int get_msg_len() {return SPELLS_BASE_MSG_LEN+m_code.length(); }
};

class ClientInterface;
class TargetInterface;

class Spells
{
private:
    typedef std::hash_map<string, Spell> spell_map_t;
    ClientInterface & m_client;
    TargetInterface & m_targeting;
    spell_map_t m_spells;

    void send_message(string name);
    bool exists(const string & name);
    string get_code(const string & name);
    bool can_target(const string & name);
    bool has_menu(const string & name);
    int get_msg_len(const string & name);

public:
    Spells(ClientInterface & client, TargetInterface & targeting);
    ~Spells(){};
    void cast(const string & name);
    void cast(const string & name, uint32 target);
    bool handle_server_talk(uint8 * buf, int size);

};

#endif
