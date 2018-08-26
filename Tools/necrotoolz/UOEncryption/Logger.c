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


/* based on Beosil's log class */

#include <windows.h>
#include <stdio.h>
#include <stdarg.h>
#include "logger.h"

FILE *logFile=0;


void MBOut(char *title, char *msg, ...)
{
	char final[4096];
	va_list list;
	va_start(list, msg);
	vsprintf((char*)final, msg, list);
	va_end(list);

	MessageBox(NULL, final, title, 0);
	
	return;
}

void OpenLog(void)
{
	logFile = fopen("UOCrypt.txt", "wb");
	/*
	make your program as endurable as possible
	but if it fails, make it fail noisily
	*/
	if(!logFile)
		MessageBox(NULL, "fuck, couldnt open the log", "ERROR!", 0);
}

void CloseLog(void)
{
	if(logFile) fclose(logFile);

	return;
}


void LogPrint(const char *strFormat, ...)
{
	char final[4096];
	va_list args;

	if(!logFile) return;

	va_start(args, strFormat);
	memset(final, 0, sizeof(final));
	vsprintf(final, strFormat, args);
	va_end(args);

	fprintf(logFile, final);
	fflush(logFile);

	return;
}

void LogDump(unsigned char *pBuffer, int length)
{
	int actLine = 0, actRow = 0;

	if(!logFile) return;

	for(actLine = 0; actLine < (length / 16) + 1; actLine++)
	{
		fprintf(logFile, "%04x: ", actLine * 16);

		for(actRow = 0; actRow < 16; actRow++)
		{
			if(actLine * 16 + actRow < length) fprintf(logFile, "%02x ", (unsigned int)((unsigned char)pBuffer[actLine * 16 + actRow]));
			else fprintf(logFile, "-- ");
		}

		fprintf(logFile, ": ");

		for(actRow = 0; actRow < 16; actRow++)
		{
			if(actLine * 16 + actRow < length) fprintf(logFile, "%c", isprint(pBuffer[actLine * 16 + actRow]) ? pBuffer[actLine * 16 + actRow] : '.');
		}

		fprintf(logFile, "\r\n");
	}

	fprintf(logFile, "\r\n");

	fflush(logFile);

	return;
}
