////////////////////////////////////////////////////////////////////////////////
//
// generic_gump.h
//
// Copyright (C) 2001 Wayne 'Chiphead' Hogue
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
//  Declarations for classes in generic_gump.cpp
//  Handles automated generic gump (0xb0) input
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _GENERIC_GUMP_H_
#define _GENERIC_GUMP_H_

class GenericGump
{
public:
    typedef std::hash_map<string, uint32> button_map_t;

private:
    string m_name;
    uint32 m_serial;
    uint32 m_id;
    button_map_t m_buttons;

public:
    GenericGump(const string & name, uint32 serial, uint32 id);
    ~GenericGump();
    void add_button(const string & name, uint32 id);
    void remove_button(const string & name);
    uint32 get_button_id(const string & name);
    bool button_exists(const string & name){ return m_buttons.find(name) != m_buttons.end(); }
    string get_name() { return m_name; }
    uint32 get_id() { return m_id; }
    uint32 get_serial() { return m_serial; }
    void write_config(FILE * fp) const;
};

///////////////////////////////////////////////////////////////////////////////////
// storage class for gump info

class GenericGumps
{
public:
    typedef std::hash_map<string, GenericGump> gump_map_t;
private:
    gump_map_t m_gumps;

public:
    GenericGumps();
    ~GenericGumps();
    void add_gump(const string & name, uint32 serial, uint32 id);
    void remove_gump(const string & name);
    void add_button(const string & name, const string & button_name, uint32 button_id);
    void remove_button(const string & name, const string & button_name);
    uint32 get_gump_id(const string & name);
    uint32 get_gump_serial(const string & name);
    uint32 get_button_id(const string & name, const string & button_name);
    bool gump_exists(const string & name);
    bool button_exists(const string & name, const string & button_name);
    void write_config(FILE * fp) const;
};

// This class used for handling the generic gumps used to select objects
// to create, etc.

class GenericGumpHandler
{
private:
    ClientInterface & m_client;
    GenericGumps & m_gumps;

    enum { NORMAL, WAITING, CHOOSING, RECORDING, B_RECORDING} m_state;

    string m_new_gump_name;
    string m_new_button_name;

    uint32 m_gump_serial;
    uint32 m_gump_id;
    uint32 m_gump_button1_id;
    uint32 m_gump_button2_id;
    uint32 m_gump_button3_id;
    bool m_has_second_button;
    bool m_has_third_button;

    void send_button(uint32 button_id);
    void choose_button(uint32 button_id);

public:
    GenericGumpHandler(ClientInterface & client, GenericGumps & gumps);
    ~GenericGumpHandler();

    // This function returns true if the gump should be sent to the client.
    bool handle_open_generic_gump(uint8 * buf, int size);
    void wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button_id);
    void wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button1_id, uint32 button2_id);
    void wait_gump(uint32 gump_serial, uint32 gump_id, uint32 button1_id, uint32 button2_id, uint32 button3_id);
    void set_gump(const string & name);
    void unset_gump(const string & name);
    // This function returns true if the gump responce should be sent to the server.
    bool handle_send_generic_gump_event(uint8 * buf, int size);
    void set_button(const string & name, const string & button_name);
    void unset_button(const string & name, const string & button_name);
    void cancel_gump();
    void cancel_button();
};

#endif

