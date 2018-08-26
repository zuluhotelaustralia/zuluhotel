////////////////////////////////////////////////////////////////////////////////
//
//
// Copyright (C) 2000 Bruno 'Beosil' Heidelberger
//
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
//	n0p3:
//	Somewhere in 2004  -- got the code from Sniffy, changed it to pure C
//
////////////////////////////////////////////////////////////////////////////////

#ifndef _LOGGER_H_INCLUDED
#define _LOGGER_H_INCLUDED

void MBOut(char *title, char *msg, ...);

void LogDump(unsigned char *pBuffer, int length);
void LogPrint(const char *strFormat, ...);
void OpenLog(void);
void CloseLog(void);

#endif
