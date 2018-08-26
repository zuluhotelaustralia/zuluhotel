////////////////////////////////////////////////////////////////////////////////
//
// menus.h
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
//  Declarations for classes in menus.cpp
//  Handles automated menu gump input
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _MENUS_H_
#define _MENUS_H_

#include "common.h"

#include <string>
using std::string;


class ClientInterface;
class MenuOption;

// This class used for handling the menus used to select objects to create,
// etc.
class MenuHandler
{
private:
    ClientInterface & m_client;
    enum { NORMAL, WAITING, CHOOSING } m_state;
    string m_menu_prompt;
    string m_menu_choice;
    string m_menu_prompt2;
    string m_menu_choice2;
    string m_menu_prompt3;
    string m_menu_choice3;
	string m_waitpair[10][2];
	int m_waitpairnum;
    bool m_has_second_menu;
    bool m_has_third_menu;
    uint32 m_id;
    uint16 m_gump;
    int m_num_options;
    MenuOption * m_options;

    void send_choice(int index);
    void choose_menu(const char * desc);
    void choose_menu(const char * desc, const char * prompt);

public:
    MenuHandler(ClientInterface & client);
    ~MenuHandler();

    // This function returns true if the menu should be sent to the client.
    bool handle_open_menu_gump(uint8 * buf, int size);
    void wait_menu(const char * prompt, const char * choice);
    void wait_menu(const char * prompt, const char * choice, const char * prompt2, const char * choice2);
    void wait_menu(const char * prompt, const char * choice, const char * prompt2, const char * choice2, const char * prompt3, const char * choice3);
    void cancel_menu();
    void auto_menu(const char * prompt, const char * choice);
};

#endif

