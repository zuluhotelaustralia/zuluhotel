////////////////////////////////////////////////////////////////////////////////
//
// hotkeyhook.cpp
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
//  Hooks the hotkey handler
//
////////////////////////////////////////////////////////////////////////////////

#include "common.h"
#include "hashstr.h"
#include "client.h"
#include "hotkeys.h"
#include "hotkeyhook.h"


///////////////////////////////////////////////////////////////////////////////
//
// Class HotkeyHook
//
///////////////////////////////////////////////////////////////////////////////

HotkeyHook * HotkeyHook::m_instance = 0;

HotkeyHook::HotkeyHook(ClientInterface & client, Hotkeys & hotkeys)
    :m_client(client), m_hotkeys(hotkeys)
{
    m_instance = this;
    m_hHook = 0;
}

HotkeyHook::~HotkeyHook()
{
}

bool HotkeyHook::install_hook(HINSTANCE hMod, DWORD dwThreadId)
{
    return true;
}

void HotkeyHook::remove_hook()
{
}

void HotkeyHook::do_command(uint16 hash)
{
    if(m_hotkeys.exists(hash))
	{
		string coms=m_hotkeys.get_command(hash);
  const string::size_type len = coms.length();
        string::size_type i = 0;
		while(i < len)
		{
         // eat leading whitespace
         i = coms.find_first_not_of (' ', i);
         if(i == string::npos) return;   // nothing left but white space
         string::size_type j = coms.find_first_of(';', i); // find the end of the token
         if(j == string::npos) {m_client.do_command(coms.substr(i).c_str()); return;}
                          else  m_client.do_command(coms.substr(i, j - i).c_str());
         i = j + 1;
		}
	}
}

BOOL HotkeyHook::KeyboardHook(WPARAM wParam, LPARAM lParam)
{
    if(m_instance)
        return m_instance->keyboard_hook(wParam, lParam);
    else
        return 0;
}

BOOL HotkeyHook::keyboard_hook(WPARAM wParam, LPARAM lParam)
{
    uint16 key_hash = Hotkeys::get_key_hash(wParam, (lParam&(1<<24))!=0, IsKeyDown(VK_CONTROL), IsKeyDown(VK_MENU), IsKeyDown(VK_SHIFT));
    if(m_hotkeys.exists(key_hash))
    {
        do_command(key_hash);
        return TRUE;
    }
    return FALSE;
}

