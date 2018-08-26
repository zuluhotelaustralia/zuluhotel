////////////////////////////////////////////////////////////////////////////////
//
// target.cpp
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
//  Handles automated target input
//
////////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include <time.h>
#include "common.h"
#include "client.h"
#include "target.h"
#include "injection.h"


TargetHandler::TargetHandler(ClientInterface & client)
: m_client(client), m_state(NORMAL)
{
ZeroMemory(ttile,19);
ttile[0]=0x6C;
}

TargetHandler::~TargetHandler()
{
}


void TargetHandler::send_target(uint32 target,uint16 graphic)
{
    if(target==1)
	{
		uint8 buf[19];
        memcpy(buf, m_requested_target, 19);
		buf[1]=1;
		//pack_big_uint32(buf +  2,ttile.cursorID);
		pack_big_uint16(buf + 11,unpack_little_uint16(ttile+11));
		pack_big_uint16(buf + 13,unpack_little_uint16(ttile+13));
		buf[16]=ttile[16];
		pack_big_uint16(buf + 17,unpack_little_uint16(ttile+17));
		//trace_printf("TargetTile %d:%d=%d\n",ttile.x,ttile.y,ttile.tile);
		m_client.send_server(buf, 19);
		return;
	}
	
	uint8 buf[19];
    memcpy(buf, m_requested_target, 19);
	buf[1]=0;
    pack_big_uint32(buf+7, target);
    //buf[18] = 0x0f; //Yoko: nonsence!!!
	pack_big_uint16(buf+17,graphic);
	if(TargXYZ)
	{
		GameObject* obj=g_injection->m_world->get_object(target);
		if(obj)
		{
			pack_big_uint16(buf+17,obj->get_graphic());
			pack_big_uint16(buf+11,obj->get_x());
			pack_big_uint16(buf+13,obj->get_y());
			buf[16]=obj->get_z();
		}
	}
    m_client.send_server(buf, 19);

	if(VarsLoopback&&target!=g_injection->m_world->get_player()->get_serial())
	{g_injection->m_last_target=target;g_injection->m_last_graphic=graphic;}
}

// This function is called for 0x1c messages
bool TargetHandler::handle_server_talk(uint8 * buf, int size)
{
    bool resend = true;
    if(m_state == WAITING)
    {
        // Select Target
        if(size > 57)
        {
            if(strncmp((char *)(buf+44), "Select Target", 13) == 0)
            {
                // Select Target sent
                resend = false;
            }
        }
    }
    return resend;
}

// This function is called for 0x6c messages
bool TargetHandler::handle_target(uint8 * buf, int /*size*/)
{
    bool resend = true;
    if(m_state == WAITING)
    {
        memcpy(m_requested_target, buf, 19);
        resend = false;
        if(m_has_second_target)
        {
            uint32 t = m_target;
            uint16 t_ = m_graphic;
            m_target = m_target2;
            m_graphic = m_graphic2;
            m_has_second_target = false;
            m_state = WAITING;
            send_target(t,t_);
        }
        else
        {
            m_state = NORMAL;
            send_target(m_target,m_graphic);
        }
    }
    else if(m_state == TARGETING)
    {
        m_client.client_print("Warning: second target request, targeting cancelled");
        m_state = NORMAL;
    }
    // In state NORMAL, silently pass the target request to the client.
    return resend;
}

void TargetHandler::wait_targettile(uint16 x,uint16 y,char z,uint16 tile)
{
    if(m_state == WAITING)
    {
        m_client.client_print("Auto target cancelled");
        send_target(0x0,0);
    }
    else if(m_state == TARGETING)
    {
        m_client.client_print("Previous target cancelled");
        send_target(0x0,0);
    }
    m_target = 1;
    m_has_second_target = false;
    m_state = WAITING;
	*(uint16*)(&ttile[11])=x;
	*(uint16*)(&ttile[13])=y;
	ttile[16]=z;
	*(uint16*)(&ttile[17])=tile;
	//trace_printf("waitTargetTile %d:%d=%d\n",ttile.x,ttile.y,ttile.tile);
}

void TargetHandler::wait_target(uint32 target, uint16 graphic)
{
    if(m_state == WAITING)
    {
        m_client.client_print("Auto target cancelled");
        send_target(0x0,0);
    }
    else if(m_state == TARGETING)
    {
        m_client.client_print("Previous target cancelled");
        send_target(0x0,0);
    }
    m_target = target;
    m_graphic = graphic;
    m_has_second_target = false;
    m_state = WAITING;
}

void TargetHandler::wait_target(uint32 target,uint16 graphic, uint32 target2, uint16 graphic2)
{
    if(m_state == WAITING)
    {
        m_client.client_print("Previous target cancelled");
        send_target(0x0,0);
    }
    else if(m_state == TARGETING)
    {
        m_client.client_print("Previous target cancelled");
        send_target(0x0,0);
    }
    m_target = target;
	m_graphic = graphic;
    m_target2 = target2;
	m_graphic2 = graphic2;
    m_has_second_target = true;
    m_state = WAITING;
}

void TargetHandler::cancel_target()
{
    if(m_state == NORMAL)
        m_client.client_print("Error: no target to cancel");
    else if(m_state == WAITING)
        m_client.client_print("Targeting cancelled");
    else if(m_state == TARGETING)
    {
        send_target(0x0,0);
    }
    m_state = NORMAL;
}

bool TargetHandler::waiting()
{
    return(m_state == WAITING);
}