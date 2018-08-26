////////////////////////////////////////////////////////////////////////////////
//
// hotkeyhook.h
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
//  Declarations for classes in hotkeyhook.cpp
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _HOTKEYHOOK_H_
#define _HOTKEYHOOK_H_

#define WIN32_LEAN_AND_MEAN
#include <windows.h>


class ClientInterface;
class Hotkeys;

class HotkeyHook
{

private:
    ClientInterface & m_client;
    Hotkeys & m_hotkeys;
    HHOOK m_hHook;

    static HotkeyHook * m_instance;

    void do_command(uint16 hash);
public:
    static BOOL KeyboardHook(WPARAM wParam, LPARAM lParam);
    BOOL keyboard_hook(WPARAM xParam, LPARAM lParam);
    inline bool IsKeyDown(int vKey)
    {
        return (GetAsyncKeyState(vKey) & 0x80000000) != 0;
    }

public:
    HotkeyHook(ClientInterface & client, Hotkeys & hotkeys);
    ~HotkeyHook();
    bool install_hook(HINSTANCE hMod, DWORD dwThreadId);
    void remove_hook();
};

#endif
