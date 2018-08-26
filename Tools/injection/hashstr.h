////////////////////////////////////////////////////////////////////////////////
//
// hashstr.h
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


#ifndef _HASHSTR_H_
#define _HASHSTR_H_

#include <string>
using std::string;

#include <hash_map>

#ifdef __GNUC__
class hash<string>
{
public:
    size_t operator() (const string & s) const
    {
        unsigned long h = 0;

        for(string::const_iterator i = s.begin(); i != s.end(); i++)
            h = (5 * h) + *i;
        return size_t(h);
    }
};
#endif

#endif

