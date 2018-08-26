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
* 	Sept 24th, 2004 -- target handler module
*   handles target requests from client, server and IRW
* 
\******************************************************************************/


#ifndef _UOTARGET_H_INCLUDED
#define _UOTARGET_H_INCLUDED

typedef void (*IRWTarget) (unsigned int, unsigned short, unsigned short, unsigned short, int);

#define USE_DISTANCE 3
#define TARGET_OBJECT 0
#define TARGET_TILE 1

/* INTERNALS */
void CleanTarget(void);
int IRWServer_Target(unsigned char *Packet, int Len);
int IRWClient_Target(unsigned char *Packet, int Len);
void IRWCmd_CancelTarget(char **Arg, int ArgCount);
void IRWCmd_WaitTarget(char **Arg, int ArgCount);
void IRWTarget_WaitTarget(unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z);
void IRWCmd_WaitTargetGraphic(char **Arg, int ArgCount);
void IRWCmd_WaitTargetTile(char **Arg, int ArgCount);
void IRWTarget_WaitTargetTile(unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z);
void IRWCmd_CancelWaitTarget(char **Arg, int ArgCount);

/* EXPORTS */
unsigned int GetLastTarget(void);
unsigned short GetLastTileX(void);
unsigned short GetLastTileY(void);
int GetLastTileZ(void);
void SetLastTarget(unsigned int Serial);
void SetLastTile(unsigned short X, unsigned short Y, int Z);
void RequestTarget(int Type, void *Handler);
void WaitTarget(int Type, unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z);

/* interaction functions */
void TargetRequest(int Type, int Sequence);
void TargetReplyObj(unsigned int Serial, unsigned short Graphic, int Sequence);
void TargetReplyTile(unsigned short Graphic, unsigned short X, unsigned short Y, int Z, int Sequence);
void CancelTargetRequest(int Sequence);

#endif
