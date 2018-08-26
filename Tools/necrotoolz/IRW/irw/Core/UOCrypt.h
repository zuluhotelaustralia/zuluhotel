/******************************************************************************\
* 
* 
*  Copyright (C) 2004 Daniel 'Necr0Potenc3' Cavalcanti
* 
* 
*  This program is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
* 
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
* 
* 	Jun 14th, 2004 -- handle client and server encryption and decryption to aliviate
* 	the job of the hooking functions
* 
\******************************************************************************/

#ifndef _UOCRYPT_H_INCLUDED
#define _UOCRYPT_H_INCLUDED

/* INTERNALS */
void StartLoginCrypt(char *Buf, int Len, int LoginSeed);
void StartGameCrypt(char *Buf, int Len, int SeedFromRelay, int GameSeed);

void DecryptFromServer(char *Buf, int Len, int SocketState);
void EncryptToServer(char *Buf, int Len, int SocketState);

void EncryptToClient(char *Buf, int Len, int SocketState);
void DecryptFromClient(char *Buf, int Len, int SocketState);

#endif
