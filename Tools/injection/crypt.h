//////////////////////////////////////////////////////////////////////
//
// crypt.h
//
// Modified: 2001 Luke 'Infidel' Dunstan
//
// Author:  Beosil
// E-Mail:  beosil@swileys.com
// Version: 1.26.4
// Date:    27. Jan. 2000
//
// Copyright (C) 1999 Bruno 'Beosil' Heidelberger
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
//////////////////////////////////////////////////////////////////////

#if !defined(_CRYPT_H_INCLUDED_)
#define _CRYPT_H_INCLUDED_

//////////////////////////////////////////////////////////////////////
extern "C" {
#include "AES.H"
}

class LoginCrypt
{
private:
    unsigned int m_key[2];
    unsigned int m_k1, m_k2;

public:
    LoginCrypt();
    ~LoginCrypt();

    void encrypt(unsigned char * in, unsigned char * out, int len);
    void init(unsigned char * pseed, unsigned int k1, unsigned int k2);
};

//////////////////////////////////////////////////////////////////////

#define CRYPT_GAMEKEY_LENGTH    6
#define CRYPT_GAMEKEY_COUNT     25

#define CRYPT_GAMESEED_LENGTH   8
#define CRYPT_GAMESEED_COUNT    25

#define CRYPT_GAMETABLE_START   1
#define CRYPT_GAMETABLE_STEP    3
#define CRYPT_GAMETABLE_MODULO  11
#define CRYPT_GAMETABLE_TRIGGER 21036

class GameCrypt
{
public:
    GameCrypt() {};
    virtual ~GameCrypt() {};

    virtual void encrypt(unsigned char * in, unsigned char * out, int len) = 0;
    virtual void decrypt(unsigned char * in, unsigned char * out, int len) = 0;
    virtual void init() =0;
};

class OldGameCrypt:public GameCrypt
{
private:
    static bool m_tables_ready;

    unsigned char m_seed[CRYPT_GAMESEED_LENGTH];
    int m_table_index;
    int m_block_pos;
    int m_stream_pos;

    void init_tables();
    void raw_encrypt(unsigned int * values, int table);

public:
    OldGameCrypt();
    virtual ~OldGameCrypt();

    virtual void encrypt(unsigned char * in, unsigned char * out, int len);
    virtual void decrypt(unsigned char * in, unsigned char * out, int len);
    virtual void init();
};

class NewGameCrypt:public GameCrypt
{
private:
    unsigned long m_IP;
    int m_pos;
    keyInstance ki;     /* key information, including tables */
    cipherInstance ci;          /* keeps mode (ECB, CBC) and IV */
    BYTE m_subData3[256];
    DWORD dwIndex;

public:
    NewGameCrypt(uint8 IP[4]);
    virtual ~NewGameCrypt();

    virtual void encrypt(unsigned char * in, unsigned char * out, int len);
    virtual void decrypt(unsigned char * in, unsigned char * out, int len);
    virtual void init();
};

#endif // !defined(_CRYPT_H_INCLUDED_)

