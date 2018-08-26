////////////////////////////////////////////////////////////////////////////////
//
// dress.h
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
//  Declarations for functions in dress.cpp
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _DRESS_H_
#define _DRESS_H_

#include <string>
using std::string;

class ClientInterface;
class GameObject;
class CharacterConfig;

class DressHandler
{
private:
    ClientInterface & m_client;
    GameObject & m_player;
    CharacterConfig & m_config;

public:
	long dress_speed;

    DressHandler(ClientInterface & client, GameObject & player,
        CharacterConfig & config);
    ~DressHandler();

    void set(const string & key);
    void unset(const string & key);
    void dress(const string & key);
    void undress();

    void setarm(const string & key);
    void unsetarm(const string & key);
    void arm(const string & key);
    void disarm();

    void removehat();
    void removeneckless();
    void removeearrings();
    void removering();
};

#endif

