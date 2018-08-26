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


#include <windows.h>
#include "Registry.h"


static char IRWPath[4096];
static char UseProfile[4096];

char* GetIRWPath(void)
{
	DWORD Size = sizeof(IRWPath);
	GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, IRWPath, &Size);
	return IRWPath;
}

char* GetCurrentProfile(void)
{
	DWORD Size = sizeof(UseProfile);
	GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "UseProfile", NULL, UseProfile, &Size);
	return UseProfile;
}

BOOL GetRegistryString(HKEY Key, const char *SubKey, const char* Name, DWORD *dDisp, char *Buf, DWORD *Size)
{
	HKEY rKey;
	DWORD Disp = 0;
    BOOL Ret = 0;

	if(Size != NULL)
		memset(Buf, 0, *Size);

    RegCreateKeyEx(Key, SubKey, 0, NULL, 0, KEY_ALL_ACCESS, NULL, &rKey, &Disp);
	Ret = RegQueryValueEx(rKey, Name, NULL, NULL, (BYTE*)Buf, (DWORD*)Size);
	RegCloseKey(rKey);

    if(dDisp != NULL)
		*dDisp = Disp;

	return (Ret == ERROR_SUCCESS);
}

BOOL GetRegistryDword(HKEY Key, const char *SubKey, const char* Name, DWORD *dDisp, DWORD *Value)
{
	HKEY rKey;
	DWORD Disp = 0, Size = 4;
    BOOL Ret = 0;

	if(Value != NULL)
		*Value = 0;

    RegCreateKeyEx(Key, SubKey, 0, NULL, 0, KEY_ALL_ACCESS, NULL, &rKey, &Disp);
	Ret = RegQueryValueEx(rKey, Name, NULL, NULL, (BYTE*)Value, &Size);
	RegCloseKey(rKey);

    if(dDisp != NULL)
		*dDisp = Disp;

	return (Ret == ERROR_SUCCESS);
}

BOOL SetRegistryString(HKEY Key, const char *SubKey, const char* Name, DWORD *dDisp, char *Buf)
{
	HKEY rKey;
	DWORD Disp = 0;
    BOOL Ret = 0;

    RegCreateKeyEx(Key, SubKey, 0, NULL, 0, KEY_ALL_ACCESS, NULL, &rKey, &Disp);
	Ret = RegSetValueEx(rKey, Name, 0, REG_SZ, (BYTE*)Buf, (DWORD)strlen(Buf));
	RegCloseKey(rKey);

    if(dDisp != NULL)
		*dDisp = Disp;

	return (Ret == ERROR_SUCCESS);
}

BOOL SetRegistryDword(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, DWORD *Value)
{
	HKEY rKey;
	DWORD Disp = 0;
    BOOL Ret = 0;


    RegCreateKeyEx(Key, SubKey, 0, NULL, 0, KEY_ALL_ACCESS, NULL, &rKey, &Disp);
	Ret = RegSetValueEx(rKey, Name, 0, REG_DWORD, (BYTE*)Value, 4);
	RegCloseKey(rKey);

    if(dDisp != NULL)
		*dDisp = Disp;

	return (Ret == ERROR_SUCCESS);
}

BOOL DeleteKey(HKEY Key, const char *SubKey)
{
	return (RegDeleteKey(Key, SubKey) == ERROR_SUCCESS);
}
