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
* 	Jun 07th, 2004 -- re-started IRW, this time I'll finish the damn thing
* 
* 	Used by the network code to manage the winsock data
* 
\*******************************************************************************/


#include <windows.h>
#include "BufferQueue.h"

/*
TODO:
1 - put memory checks on increase/delete of buffers
2 - add some defines for error messages
*/

int BQClean(BufferQueue *Dest)
{
	if(Dest->Buffer) free(Dest->Buffer);
    Dest->Buffer = NULL;
	Dest->Size = 0;
	Dest->Used = 0;

	return 1;
}

int BQAddHead(BufferQueue *Dest, BYTE *Src, DWORD SrcSize)
{
	/* if dest is not big enough, increase it */
	if(Dest->Used + SrcSize > Dest->Size)
	{
		BQIncrease(Dest, SrcSize);

		/* not very probable but just in case */
		if(!Dest->Buffer) return -1;
	}

	/* move dest's data Dest->Used-bytes forward */
	memmove(Dest->Buffer+SrcSize, Dest->Buffer, Dest->Used);

	/* copy src's data to the start of dest */
	memcpy(Dest->Buffer, Src, SrcSize);
	Dest->Used += SrcSize;

	return 1;
}

int BQAddTail(BufferQueue *Dest, BYTE *Src, DWORD SrcSize)
{
	/* if dest is not big enough, increase it */
	if(Dest->Used + SrcSize > Dest->Size)
	{
		BQIncrease(Dest, SrcSize);

		/* not very probable but just in case */
		if(!Dest->Buffer) return -1;
	}

	/* copy the data to the end of dest */
	memcpy(Dest->Buffer+Dest->Used, Src, SrcSize);
	Dest->Used += SrcSize;

	return 1;
}

int BQRemoveHead(BufferQueue *Dest, DWORD Size)
{
	if(Size > Dest->Used)
		return -1;

	/* copy the end of the buffer to the start */
	memmove(Dest->Buffer, Dest->Buffer+Size, Dest->Used-Size);
	Dest->Used -= Size;
    
	return 1;
}

int BQRemoveTail(BufferQueue *Dest, DWORD Size)
{
	/* if we want to remove more data than we have just zero the buffer
	and send a warning */
	if(Size > Dest->Used)
	{
		Dest->Used = 0;
		return 0;
	}

	Dest->Used -= Size;

	return 1;
}

int BQIncrease(BufferQueue *Dest, DWORD Size)
{
	Dest->Buffer = (BYTE*)realloc(Dest->Buffer, Dest->Size + Size);
    Dest->Size += Size;

	return 1;
}

int BQDecrease(BufferQueue *Dest, DWORD Size)
{
	Dest->Buffer = (BYTE*)realloc(Dest->Buffer, Dest->Size - Size);
	Dest->Size -= Size;

	/* correct the data's size if necessary */
	if(Dest->Used > Dest->Size)
		Dest->Used = Dest->Size;

	return 1;
}


