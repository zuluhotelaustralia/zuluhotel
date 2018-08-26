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
* 	Oct 4th, 2004  -- most of this file comes from UOInjection
* 
\******************************************************************************/


#ifndef _REGISTRY_H_INCLUDED
#define _REGISTRY_H_INCLUDED

char* GetIRWPath(void);
char* GetCurrentProfile(void);

BOOL SetRegistryString(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, char *Buf);
BOOL GetRegistryString(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, char *Buf, DWORD *Size);

BOOL SetRegistryDword(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, DWORD *Value);
BOOL GetRegistryDword(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, DWORD *Value);

BOOL DeleteKey(HKEY Key, const char *SubKey);

#endif
