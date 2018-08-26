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
* 	Jun 08th, 2004 -- started it
* 
\******************************************************************************/

#ifndef _UONETWORK_H_INCLUDED
#define _UONETWORK_H_INCLUDED

/* INTERNALS */
/* handles the constant socket changes. inits encryption */
int HandleSocket(SOCKET Socket, char *Buf, int Len);

/* builds packets in the partial buffer and passes them through the packet handler when built */
void BuildPacket(unsigned char *Buf, unsigned int BufLen, int From);

/* I had something to say about this... cant remember what :P */
void AddSocket(SOCKET s);
void RemoveSocket(SOCKET s);
void CleanSocketList(void);
int CloseOpenSockets(void);

/* these are the functions that actually send the data, they are called in conjunction with select() */
int InternalSendToServer(void);
int InternalSendToClient(char *Buf, int Len);

/* when we need to debug raw packets */
int WINAPI hook_senddbg(SOCKET s, char *buf, int len, int flags);
int WINAPI hook_recvdbg(SOCKET s, char *buf, int len, int flags);

/* winsock hooks */
SOCKET WINAPI hook_socket(int af, int type, int protocol);
int WINAPI hook_closesocket(SOCKET s);
int WINAPI hook_send(SOCKET s, char *buf, int len, int flags);
int WINAPI hook_recv(SOCKET s, char *buf, int len, int flags);
int WINAPI hook_select(int nfds, fd_set *readfds, fd_set *writefds, fd_set *exceptfds, const struct timeval *timeout);
int WINAPI hook_connect(SOCKET s, const struct sockaddr *name, int namelen);

/* EXPORTS */
/* network bools */
BOOL GetDropBadPackets(void);
void SetDropBadPackets(BOOL drop);

/* puts the data in the full buffer and doesnt pass it through the packet handler */
void SendToClient(unsigned char *Buf, unsigned int Size);
void SendToServer(unsigned char *Buf, unsigned int Size);

#endif
