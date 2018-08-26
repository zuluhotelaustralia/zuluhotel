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
* 	June 7th, 2004  -- re-started IRW, this time I'll finish the damn thing
* 
* 	Used by the network code to manage the winsock data
* 
\******************************************************************************/

#ifndef _BUFFERQUEUE_H_INCLUDED
#define _BUFFERQUEUE_H_INCLUDED

typedef struct tagBufferQueue
{
    BYTE *Buffer;
	DWORD Size;
	DWORD Used;
}BufferQueue;

/* INTERNALS */
int BQClean(BufferQueue *Dest);

int BQAddHead(BufferQueue *Dest, BYTE *Src, DWORD SrcSize);
int BQAddTail(BufferQueue *Dest, BYTE *Src, DWORD SrcSize);

int BQRemoveHead(BufferQueue *Dest, DWORD Size);
int BQRemoveTail(BufferQueue *Dest, DWORD Size);

int BQIncrease(BufferQueue *Dest, DWORD Size);
int BQDecrease(BufferQueue *Dest, DWORD Size);

#endif
