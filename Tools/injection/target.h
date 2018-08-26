////////////////////////////////////////////////////////////////////////////////
//
// target.h
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
//  Declarations for classes in target.cpp
//  Handles automated targeting commands
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _TARGET_H_
#define _TARGET_H_

#include "common.h"

#include <string>
using std::string;

// Abstract class
class TargetInterface
{
public:
    virtual void cancel_target() = 0;
    virtual bool waiting() = 0;
    virtual void wait_target(uint32 target, uint16 graphic) = 0;
    virtual void wait_target(uint32 target, uint16 graphic, uint32 target2, uint16 graphic2) = 0;
};

class ClientInterface;

// This class used for handling the targeting,
class TargetHandler : public TargetInterface
{
//private:
public:
    ClientInterface & m_client;
	TWState m_state;
    uint8 m_requested_target[19];
    uint32 m_target;
    uint32 m_target2;
    uint16 m_graphic;
    uint16 m_graphic2;
	unsigned char ttile[19];
    bool m_has_second_target;

    void send_target(uint32 target, uint16 graphic/*=0x000f*/);
    void send_targettile();

public:
    TargetHandler(ClientInterface & client);
    virtual ~TargetHandler();

    // This function returns true if the target request should be sent to the client.
    bool handle_target(uint8 * buf, int size);
    bool handle_server_talk(uint8 * buf, int size);
    virtual void cancel_target();
    virtual bool waiting();
    virtual void wait_target(uint32 target, uint16 graphic);
    virtual void wait_targettile(uint16 x,uint16 y,char z,uint16 tile);
    virtual void wait_target(uint32 target, uint16 graphic, uint32 target2, uint16 graphic2);
};

#endif

