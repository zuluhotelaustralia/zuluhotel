////////////////////////////////////////////////////////////////////////////////
//
// hotkeys.cpp
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
//  Handles hotkeys
//
////////////////////////////////////////////////////////////////////////////////

#include "iconfig.h"
#include "common.h"
#include "hashstr.h"
#include "hotkeys.h"

///////////////////////////////////////////////////////////////////////////////
//
// Class Hotkeys
//
///////////////////////////////////////////////////////////////////////////////

Hotkeys::Hotkeys()
{
//  m_hotkeys[0x006d] = string("bandageself");
}

Hotkeys::~Hotkeys()
{
}

void Hotkeys::add(uint16 key_hash, const string & command)
{
    ASSERT(!exists(key_hash));
    m_hotkeys.insert(hotkey_map_t::value_type(key_hash, command));
}

void Hotkeys::remove(uint16 key_hash)
{
    m_hotkeys.erase(key_hash);
}

void Hotkeys::write_config(FILE * fp) const
{
    for(hotkey_map_t::const_iterator i = m_hotkeys.begin(); i != m_hotkeys.end(); i++)
    {
        fprintf(fp,"\t\t\t\t<hotkey key_hash=\"0x%04x\" command=\"%s\"/>\n",
            (*i).first,
            ConfigManager::escape_attribute((*i).second).c_str());
    }
}

// private
bool Hotkeys::exists(const uint16 hash)
{
    return m_hotkeys.find(hash) != m_hotkeys.end();
}

const string Hotkeys::get_command(uint16 hash)
{
    hotkey_map_t::iterator i = m_hotkeys.find(hash);
    ASSERT(i != m_hotkeys.end());
    return ((*i).second);
}

