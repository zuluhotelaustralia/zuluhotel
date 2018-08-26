////////////////////////////////////////////////////////////////////////////////
//
// skills.h
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
//  Declarations for classes in skills.cpp
//
//  Handles casting skills
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _SKILLS_H_
#define _SKILLS_H_

#include "common.h"
#include "hashstr.h"
#include "client.h"
#include "target.h"

const int SKILL_BASE_MSG_LEN = 7;

class Skill
{
private:
    string m_code;
    int m_target_cnt;
    bool m_menu;

public:
    Skill():m_code(0), m_target_cnt(0), m_menu(false) {}
    Skill(const char * code, int target_cnt, bool menu):m_code(code), m_target_cnt(target_cnt), m_menu(menu) {}
    ~Skill(){};
    string get_code() { return m_code; }
    int get_target_cnt() { return m_target_cnt; }
    bool has_menu() { return m_menu; }
    int get_msg_len() {return SKILL_BASE_MSG_LEN+m_code.length(); }
};

class ClientInterface;
class TargetInterface;

class Skills
{
private:
    typedef std::hash_map<string, Skill> skill_map_t;
    ClientInterface & m_client;
    TargetInterface & m_targeting;
    skill_map_t m_skills;

    void send_message(string name);
    bool exists(const string & name);
    string get_code(const string & name);
    int get_target_cnt(const string & name);
    bool has_menu(const string & name);
    int get_msg_len(const string & name);

public:
    Skills(ClientInterface & client, TargetInterface & targeting);
    ~Skills(){};
    void use(const string & name);
    void use(const string & name, uint32 target);
    void use(const string & name, uint32 target, uint32 target2);
    bool handle_server_talk(uint8 * buf, int size);
};

#endif
