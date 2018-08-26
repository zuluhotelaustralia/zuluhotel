/******************************************************************************\
* 
* 
*  Copyright (C) 2000 Bruno 'Beosil' Heidelberger
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
* 	n0p3:
* 	Somewhere in 2004  -- got the code from Sniffy, changed it to pure C
* 
\******************************************************************************/

#ifndef _LOGGER_H_INCLUDED
#define _LOGGER_H_INCLUDED

#define NOFILTER_LOG 1
#define NETWORK_LOG 2
#define BUILDER_LOG 4
#define COMPRESSOR_LOG 8
#define API_LOG 16
#define WARNING_LOG 32
#define ERROR_LOG 64
#define CRYPT_LOG 128
#define INTEREST_LOG 256
#define CLIENTIRW_LOG 512 /* client->IRW packet */
#define IRWSERVER_LOG 1024 /* irw->server */
#define SERVERIRW_LOG 2048 /* server->IRW */
#define IRWCLIENT_LOG 4096 /* irw->client */


/* INTERNALS */
void OpenLog(void);
void CloseLog(void);

/* EXPORTS */
void MBOut(const char *title, const char *msg, ...);

void LogBlock(int Type);
void LogUnblock(int Type);
void LogPrint(int Type, const char *strFormat, ...);
void LogDump(int Type, unsigned char *pBuffer, int length);

#endif
