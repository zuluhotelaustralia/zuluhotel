////////////////////////////////////////////////////////////////////////////////
//
// menus.cpp
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
//  Handles automated menu gump input
//
////////////////////////////////////////////////////////////////////////////////

#include "common.h"
#include "client.h"

#include "menus.h"

extern bool MenuTalk;

class MenuOption
{
public:
    uint16 m_graphic;
    string m_description;
};

MenuHandler::MenuHandler(ClientInterface & client)
: m_client(client), m_state(NORMAL), m_options(0)
{
}

MenuHandler::~MenuHandler()
{
    delete [] m_options;
}

// (private)
// Index should be 0-based, or -1 to cancel the menu
void MenuHandler::send_choice(int index)
{
    uint8 buf[13];
    buf[0] = CODE_MENU_CHOICE;
    pack_big_uint32(buf + 1, m_id);
    pack_big_uint16(buf + 5, m_gump);
    pack_big_uint16(buf + 7, index + 1);
    if(index == -1)
        pack_big_uint16(buf + 9, 0);
    else
        pack_big_uint16(buf + 9, m_options[index].m_graphic);
    pack_big_uint16(buf + 11, 0);
    m_client.send_server(buf, sizeof(buf));
}

// private
void MenuHandler::choose_menu(const char * desc)
{
    if(m_state == CHOOSING)
    {
	if(/*m_waitpairnum&&*/desc[0]=='(')
	{
	if(string(desc).find("last")!= string::npos)
	{send_choice(m_num_options-1); return;}
	if(string(desc).find("random")!= string::npos)
	{send_choice(rand()*m_num_options/RAND_MAX); return;}
	if(string(desc).find("cancel")!= string::npos)
	{send_choice(-1); return;}
	int n=atoi(string(desc).substr(1,string(desc).length()-2).c_str());
	if(n>0&&n<=m_num_options) {if(MenuTalk) m_client.client_print(m_options[n-1].m_description);send_choice(n-1); return;}
	if(MenuTalk) m_client.client_print(string("Warning: ")+string(desc));
	}

		for(int i = 0; i < m_num_options; i++)
            if(m_options[i].m_description.find(desc) != string::npos)
            {
                if(MenuTalk) m_client.client_print("Menu choice successful.");
                
				if(m_waitpairnum)
				{
                    send_choice(i);
                    m_state = WAITING;
                    return;
				}
				if(m_has_third_menu)
                {
                    m_menu_prompt = m_menu_prompt2;
                    m_menu_choice = m_menu_choice2;
                    m_menu_prompt2 = m_menu_prompt3;
                    m_menu_choice2 = m_menu_choice3;
                    m_has_third_menu = false;
                    m_has_second_menu = true;
                    m_state = WAITING;
                    send_choice(i);
                    return;
                }
                else if(m_has_second_menu)
                {
                    m_menu_prompt = m_menu_prompt2;
                    m_menu_choice = m_menu_choice2;
                    m_has_second_menu = false;
                    m_state = WAITING;
                    send_choice(i);
                    return;
                }
                else
                {
                    send_choice(i);
                    m_state = NORMAL;
                    return;
                }
            }
        if(!m_waitpairnum)
		{
		if(MenuTalk) m_client.client_print("Menu choice not found: menu cancelled.");
        send_choice(-1);
        m_state = NORMAL;
		}
		else
		{
		if(MenuTalk) m_client.client_print("Menu choice not found: menu ignored.");
        send_choice(-1);
        m_state = WAITING;
		}
    }
    else if(m_state == WAITING)
    {
        if(MenuTalk) m_client.client_print("Cannot choose: no menu open, waiting cancelled.");
        m_state = NORMAL;
    }
    else
        if(MenuTalk) m_client.client_print("Cannot choose: no menu open.");
}

// This function is called for 0x7c messages
bool MenuHandler::handle_open_menu_gump(uint8 * buf, int /*size*/)
{
    if(m_state == NORMAL) return true; //resend

    if(!m_waitpairnum && m_state == CHOOSING)
    {
        if(MenuTalk) m_client.client_print("Warning: menu opened, choosing cancelled");
        send_choice(-1);
        m_state = NORMAL;
		return true; //resend
    }

        int prompt_length = buf[9];
		int automenu=-1;
        string prompt(reinterpret_cast<char *>(buf + 10), prompt_length);
        // Look for the desired substring within the prompt
        if(m_waitpairnum)
		{
			for(int i=0; i<m_waitpairnum; i++)
				if(prompt.find(m_waitpair[i][0]) != string::npos)
				{automenu=i; break;}
			if(automenu<0)
			{
			if(MenuTalk) m_client.client_print("Unwaited menu");
			//send_choice(-1);
			return true; //resend
			}


		}
		else
        if(prompt.find(m_menu_prompt) == string::npos)
        {
			if(MenuTalk) m_client.client_print(string("Warning: menu '") + prompt +
                string("' opened, waiting cancelled"));
            m_state = NORMAL;
			return true;
        }

            m_id = unpack_big_uint32(buf + 3);
            m_gump = unpack_big_uint16(buf + 7);
            uint8 * ptr = buf + 10 + prompt_length;
            m_num_options = *ptr++;
            delete [] m_options;
            m_options = new MenuOption[m_num_options];
            for(int i = 0; i < m_num_options; i++)
            {
                m_options[i].m_graphic = unpack_big_uint16(ptr);
                int desc_len = ptr[4];
                ptr += 5;
                m_options[i].m_description.assign(
                    reinterpret_cast<char *>(ptr), desc_len);
                ptr += desc_len;
            }
            m_state = CHOOSING;
			char buff[80];
			if(automenu<0) sprintf(buff,"%s -> %s",prompt.c_str(),m_menu_choice.c_str());
			else sprintf(buff,"%i %s -> %s",automenu,prompt.c_str(),m_waitpair[automenu][1].c_str());
			if(MenuTalk) m_client.client_print(buff);
            if(automenu<0) choose_menu(m_menu_choice.c_str());
			else choose_menu(m_waitpair[automenu][1].c_str());
            return false;
}

void MenuHandler::wait_menu(const char * prompt, const char * choice)
{
    if(m_waitpairnum)
	{if(MenuTalk) m_client.client_print("Automenus cancelled");	m_waitpairnum=0; m_state=NORMAL;}
    if(m_state == WAITING)
        if(MenuTalk) m_client.client_print(string("Previous waitmenu cancelled: ") +
            m_menu_prompt);
    else if(m_state == CHOOSING)
    {
        if(MenuTalk) m_client.client_print(string("Previous menu gump cancelled: ") +
            m_menu_prompt);
        send_choice(-1);
    }
    m_state = WAITING;
    m_menu_prompt = prompt;
    m_menu_choice = choice;
    m_has_second_menu = false;
    if(MenuTalk) m_client.client_print("Now waiting for menu...");
}

void MenuHandler::wait_menu(const char * prompt, const char * choice, const char * prompt2, const char * choice2)
{
    if(m_waitpairnum)
	{m_client.client_print("Automenus cancelled");	m_waitpairnum=0; m_state=NORMAL;}
    if(m_state == WAITING)
        if(MenuTalk) m_client.client_print(string("Previous waitmenu cancelled: ") +
            m_menu_prompt);
    else if(m_state == CHOOSING)
    {
        if(MenuTalk) m_client.client_print(string("Previous menu gump cancelled: ") +
            m_menu_prompt);
        send_choice(-1);
    }
    m_state = WAITING;
    m_menu_prompt = prompt;
    m_menu_choice = choice;
    m_has_second_menu = true;
    m_menu_prompt2 = prompt2;
    m_menu_choice2 = choice2;
    if(MenuTalk) m_client.client_print("Now waiting for menu...");
}

void MenuHandler::wait_menu(const char * prompt, const char * choice, const char * prompt2, const char * choice2, const char * prompt3, const char * choice3)
{
    if(m_waitpairnum)
	{m_client.client_print("Automenus cancelled");	m_waitpairnum=0; m_state=NORMAL;}
    if(m_state == WAITING)
        if(MenuTalk) m_client.client_print(string("Previous waitmenu cancelled: ") +
            m_menu_prompt);
    else if(m_state == CHOOSING)
    {
        if(MenuTalk) m_client.client_print(string("Previous menu gump cancelled: ") +
            m_menu_prompt);
        send_choice(-1);
    }
    m_state = WAITING;
    m_menu_prompt = prompt;
    m_menu_choice = choice;
    m_has_second_menu = true;
    m_menu_prompt2 = prompt2;
    m_menu_choice2 = choice2;
    m_has_third_menu = true;
    m_menu_prompt3 = prompt3;
    m_menu_choice3 = choice3;
    if(MenuTalk) m_client.client_print("Now waiting for menu...");
}

void MenuHandler::cancel_menu()
{
    if(m_waitpairnum)
	{m_client.client_print("Automenus cancelled");	m_waitpairnum=0; m_state=NORMAL; return;}
    if(m_state == NORMAL)
        m_client.client_print("Error: no menu to cancel");
    else if(m_state == WAITING)
        m_client.client_print(string("waitmenu cancelled: ") + m_menu_prompt);
    else if(m_state == CHOOSING)
    {
        m_client.client_print(string("Menu gump cancelled: ") + m_menu_prompt);
        send_choice(-1);
    }
    m_state = NORMAL;
}

void MenuHandler::auto_menu(const char * prompt, const char * choice)
{
    char buf[80];
	int m=-1;
	if(prompt&&*prompt)
	{
	for(int i=0; i<m_waitpairnum; i++)
		if(string(m_waitpair[i][0])==string(prompt))
		{m_client.client_print("Warning: Autochoice replaced"); m=i; break;}

	if(m<0) {if(m_waitpairnum>=10)
			 {m_client.client_print("Too many automenus");return;}
	m=m_waitpairnum;m_waitpairnum++;}
	
    if(m_state == CHOOSING)
    {
        m_client.client_print(string("Previous menu gump cancelled: ") +
            m_menu_prompt);
        send_choice(-1);
    }
	m_waitpair[m][0]=string(prompt);
	m_waitpair[m][1]=string(choice);
    m_state = WAITING;
	sprintf(buf,"Automenu added. Waiting for %i choices.",m_waitpairnum);
    m_client.client_print(buf);
	}
	for(int j=0; j<m_waitpairnum; j++)
	{sprintf(buf,"%i: [%s] => [%s]",j,m_waitpair[j][0].c_str(),m_waitpair[j][1].c_str());
	 m_client.client_print(buf);
	}
}
