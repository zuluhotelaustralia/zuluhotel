////////////////////////////////////////////////////////////////////////////////
//
// client.h
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
//  Declarations of the ClientInterface class
//
////////////////////////////////////////////////////////////////////////////////

#ifndef _CLIENT_H_
#define _CLIENT_H_

#include "common.h"

#include <string>
using std::string;

// Abstract class
class ClientInterface
{
public:
    ClientInterface() {}
    virtual ~ClientInterface() {}
    // Send some arbitrary data to the server:
    virtual void send_server(uint8 * buf, int size) = 0;
    // Send some arbitrary data to the client:
    virtual void send_client(uint8 * buf, int size) = 0;
    // Print a string to the client in the form of a server talk message
    virtual void client_print(const char * text) = 0;
    virtual void client_print(const string & text) = 0;
    // Pick up an item and move it into a container:
    virtual void move_container(uint32 serial, uint32 cserial) = 0;
    virtual void move_container(uint32 serial, uint16 quantity, uint32 cserial) = 0;
    // Pick up an item and move it into the player's backpack:
    virtual void move_backpack(uint32 serial) = 0;
    virtual void move_backpack(uint32 serial, uint16 quantity) = 0;
    // Pick up an item and move it onto the player's body:
    virtual void move_equip(uint32 serial, int layer) = 0;
    virtual void do_command(const char * cmd) = 0;
};

#endif

