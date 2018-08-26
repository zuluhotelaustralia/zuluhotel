////////////////////////////////////////////////////////////////////////////////
//
// patch.h
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
//
// Parts based on Ignition:
// Copyright (C) 2000 Bruno 'Beosil' Heidelberger
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
// The Patch class loads patch information from a configuration file.
//
////////////////////////////////////////////////////////////////////////////////

#ifndef _PATCH_H_
#define _PATCH_H_

#define PATCH_ELEMENTS_MAX  20
#define PATCH_LINE_MAX      4096
#define PATCH_ELEMENT_MAX   4096
#define PATCH_NAME_MAX      100

class PatchElement
{
public:
    unsigned int m_address;
    unsigned int m_length;
    unsigned char m_buffer[PATCH_ELEMENT_MAX + 1];
};

class Patch
{
public:
    char m_name[PATCH_NAME_MAX];
    int m_num_elements;
    PatchElement m_list[PATCH_ELEMENTS_MAX];

private:
    // Create patch information from a configuration line
    bool load_line(char * line);

public:
    bool load(const char * filename, unsigned int checksum,
        unsigned int length);

    const char * get_name() const { return m_name; }
    int get_num_elements() const { return m_num_elements; }
    const PatchElement * get_elements() const { return m_list; }
};

#endif

