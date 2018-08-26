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
* 	Dec 28th, 2004 -- this module contains an assembly of nifty functions ;)
* 
\******************************************************************************/

#include <windows.h>
#include "CrackTools.h"


/* given a position inside a function, track the beginning of the function and the address */
/* slower than GetFunctionRef haha */
int GetFunctionByRef(BYTE *Buf, DWORD BufSize, DWORD PosInFunction)
{
	DWORD i = 0;

	/* go down from where we are in the buffer till the beginning */
	for(i = PosInFunction; i > 0; i--)
	{
		/* search for a CALL <this position> */
		if(GetFunctionRef(Buf, BufSize, i, 1) != -1)
			return i;
	}

	return -1;
}

/* tries to find a "CALL func" in buf. returns -1 if not found */
/* VERY slow... reliable though */
int GetFunctionRef(BYTE *Buf, DWORD BufSize, DWORD Function, DWORD Which)
{
	DWORD Count = 0, Call = 0, i = 0;
	char Ref[5] = { 0xe8, 0x00, 0x00, 0x00, 0x00 };

	for(i = 0; i < BufSize - sizeof(Ref); i++)
    {
		/* if this doesn't even look like a call, skip */
		if(Buf[i] != 0xe8)
			continue;

		/* search for CALL (to-from-5) */
        Call = Function - i - 5;
		memcpy(Ref + 1, &Call, sizeof(DWORD));
		if(!memcmp(Buf + i, Ref, 5))
		{
			Count++;
			if(Count >= Which)
				return i;
		}
	}

	return -1;
}

/* searches for PUSH <offset of text> */
int GetTextRef(BYTE *Buf, DWORD BufSize, DWORD ImageBase, const char *Text, BOOL Exact, int Which)
{
	char PushText[5] = { 0x68, 0x00, 0x00, 0x00, 0x00 };
	unsigned int TextPos = 0, TextPush = 0;

	/* find the text's position, if Exact is true, it will include the null char in the search */
	TextPos = ImageBase + FleXSearch((BYTE*)Text, Buf, (DWORD)(strlen(Text) + Exact), BufSize, 0, 0x100, 1);
	if(TextPos == ImageBase - 1)
		return -2;

	/* find PUSH <offset of text> */
	memcpy(PushText + 1, &TextPos, sizeof(TextPos));
	TextPush = FleXSearch(PushText, Buf, sizeof(PushText), BufSize, 0, 0x100, 1);

	/* returns -1 if not found */
	return TextPush;
}

int FleXSearch(BYTE *Src, BYTE *Buf, DWORD SrcSize, DWORD BufSize, DWORD StartAt, int FlexByte, DWORD Which)
{
    DWORD Count = 0, i = 0, j = 0;
	BOOL UseFlex = FlexByte & 100;
	BYTE FByte = FlexByte & 0xff;
    
    for(i = StartAt; i < BufSize - SrcSize; i++)
    {
        /* check if we can read src_size bytes from this location */
        if(IsBadReadPtr((void*)(Buf + 1), SrcSize))
            return 0;
            
		/* compare src_size from src against src_size bytes from buf */
		for(j = 0; j < SrcSize; j++)
		{
			/* if there's a difference and this isn't the flex_byte, move on */
			if((UseFlex && Src[j] != FByte) && Src[j] != Buf[i + j])
				break;
			else if(!UseFlex && Src[j] != Buf[i + j])
				break;

			/* if its the last byte for comparison, everything matched */
			if(j == (SrcSize - 1))
			{
				Count++;
				if(Count >= Which)
					return i;
			}
		}
  }

	/* if it got this far, then couldnt find it */
	return -1;
}

BYTE ByteNotInBuf(BYTE *Buf, DWORD BufSize)
{
	BYTE i = 0;
	DWORD j = 0;

	for(i = 0; i < 0xFF; i++)
	{
        for(j = 0; j < BufSize; j++)
		{
			if(Buf[j] == i)
				break;

			if(j == (BufSize - 1))
				return i;
		}
	}

	return 0xFF;
}
