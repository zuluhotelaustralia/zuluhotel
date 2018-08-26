////////////////////////////////////////////////////////////////////////////////
//
// ignition.cpp
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

#include <stdio.h>
#include <string.h>

#include "patch.h"


////////////////////////////////////////////////////////////////////////////////
//
// The Patch class loads patch information from a configuration file.
//
////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////
//
//  Create patch information from a configuration line
//
//  PARAMETERS:
//      char * line     Configuration line
//
//  RETURNS:
//      bool                "true" if successful, "false" if not
//
////////////////////////////////////////////////////////////////////////////////

bool Patch::load_line(char * line)
{
    m_num_elements = 0;
    char *pToken = strtok(line, ",\n\r");

    while(pToken != 0)
    {
        pToken += strspn(pToken, " \t");
        if(*pToken == 0) return true;

        unsigned int address;
        if(sscanf(pToken, "%x", &address) != 1) return false;

        if((pToken = strchr(pToken, '=')) == 0) continue;
        pToken++;

        pToken += strspn(pToken, " \t");

        unsigned int length = 0;
        unsigned char * buffer = m_list[m_num_elements].m_buffer;

        while(*pToken != 0)
        {
            unsigned int value;

            if(sscanf(pToken, "%x", &value) != 1) return false;

            if((value > 255) || (length >= PATCH_ELEMENT_MAX)) return false;

            buffer[length++] = (unsigned char)value;

            pToken += strspn(pToken, "01234567890abcdefABCDEF");
            pToken += strspn(pToken, " \t");
        }

        if(length > 0)
        {
            m_list[m_num_elements].m_address = address;
            m_list[m_num_elements].m_length = length;
            m_num_elements++;
        }
        pToken = strtok(0, ",\n\r");
    }

    return true;
}


////////////////////////////////////////////////////////////////////////////////
//
//  Load patch information for a given checksum/length pair
//
//  PARAMETERS:
//      const char * filename           Filename of configuration file
//      unsigned int checksum           Checksum of target
//      unsigned int length             Length of target
//
//  RETURNS:
//      bool                            "true" if successful, "false" if not
//
////////////////////////////////////////////////////////////////////////////////

bool Patch::load(const char * filename, unsigned int checksum,
    unsigned int length)
{
    FILE *hFile;
    if((hFile = fopen(filename, "rb")) == 0) return false;

    while(true)
    {
        char buffer[PATCH_LINE_MAX];
        fgets(buffer, PATCH_LINE_MAX, hFile);

        if(feof(hFile)) break;

        char *pToken = &buffer[strspn(buffer, " \t\n\r")];

        if(*pToken++ != '\"') continue;

        char *strName = pToken;

        if((pToken = strchr(pToken, '\"')) == 0) continue;

        *pToken++ = 0;

        unsigned int currentChecksum;
        unsigned int currentLength;

        if(sscanf(pToken, "%x %x", &currentChecksum, &currentLength) != 2) continue;

        if((currentChecksum != checksum) || (currentLength != length)) continue;

        if((pToken = strchr(pToken, ':')) == 0) continue;

        if(!load_line(pToken + 1)) continue;

        strcpy(m_name, strName);

        fclose(hFile);
        return true;
    }

    fclose(hFile);

    return false;
}


