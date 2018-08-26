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
* 	Oct 4th, 2004  -- rewrote the launcher (now with gui! ;))
* 
\******************************************************************************/


#include <windows.h>
#include <stdio.h>
#include "INIProfile.h"

/* IRWInstallDir\\Profiles */
static char ProfilesFolder[4096];
static char CurProfile[4096];
static BOOL PathSet = FALSE;

char* GetProfilesPath(void)
{
	return ProfilesFolder;
}

void SetProfilesPath(char *IRWPath)
{
	strcpy(ProfilesFolder, IRWPath);
	strcat(ProfilesFolder, "\\Profiles");

	PathSet = TRUE;
	return;
}

void SetCurrentProfile(char *ProfileName)
{
	strcpy(CurProfile, ProfileName);
	return;
}

BOOL CleanIRWProfileSection(const char *ProfileName, const char *Section)
{
	char ProfilePath[MAX_PATH];

	if(!PathSet)
		return FALSE;

	/* if its a drive letter, full path was given */
	if(ProfileName != NULL && ProfileName[1] == ':' && ProfileName[2] == '\\')
		strcpy(ProfilePath, ProfileName);
	else if(ProfileName != NULL) /* build the full path */
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, ProfileName);

		/* check if the profile name ends with .ini, if not, append it */
		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	if(ProfileName == NULL)
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, CurProfile);

		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	WritePrivateProfileSection(Section, NULL, ProfilePath);
    
	return TRUE;
}

BOOL GetIRWProfileString(const char *ProfileName, const char *Section,  const char *Key, char *Out, DWORD Size)
{
	char Dummy[1] = "";
	char ProfilePath[MAX_PATH];

	if(!PathSet || Out == NULL)
		return FALSE;

	/* if its a drive letter, full path was given */
	if(ProfileName != NULL && ProfileName[1] == ':' && ProfileName[2] == '\\')
		strcpy(ProfilePath, ProfileName);
	else if(ProfileName != NULL)/* build the full path */
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, ProfileName);

		/* check if the profile name ends with .ini, if not, append it */
		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	if(ProfileName == NULL)
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, CurProfile);

		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

    if(GetPrivateProfileString(Section, Key, Dummy, Out, Size, ProfilePath) == 0)
		return FALSE;

	return TRUE;
}

BOOL GetIRWProfileInt(const char *ProfileName, const char *Section, const char *Key, int *Out)
{
	char Dummy[5];
	char Tmp[MAX_PATH];
	char ProfilePath[MAX_PATH];

	if(!PathSet || Out == NULL)
		return FALSE;

	memset(Dummy, 0, sizeof(Dummy));
	memset(Tmp, 0, sizeof(Tmp));

	/* if its a drive letter, full path */
	if(ProfileName != NULL && ProfileName[1] == ':' && ProfileName[2] == '\\')
		strcpy(ProfilePath, ProfileName);
	else if(ProfileName != NULL)/* build the full path */
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, ProfileName);

		/* check if the profile name ends with .ini, if not, append it */
		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	if(ProfileName == NULL)
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, CurProfile);

		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

    if(GetPrivateProfileString(Section, Key, Dummy, Tmp, MAX_PATH, ProfilePath) == 0)
		return FALSE;

	/* check if its a hex number */
	if(!strncmp(Tmp, "0x", 2))
		*Out = strtol(Tmp, NULL, 16);
	else
		*Out = strtol(Tmp, NULL, 10);

	return TRUE;
}

BOOL SetIRWProfileString(const char *ProfileName, const char *Section,  const char *Key, const char *Text)
{
	char ProfilePath[MAX_PATH];

	if(!PathSet)
		return FALSE;

	/* if its a drive letter, full path */
	if(ProfileName != NULL && ProfileName[1] == ':' && ProfileName[2] == '\\')
		strcpy(ProfilePath, ProfileName);
	else if(ProfileName != NULL) /* build the full path */
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, ProfileName);

		/* check if the profile name ends with .ini, if not, append it */
		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	if(ProfileName == NULL)
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, CurProfile);

		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

    if(WritePrivateProfileString(Section, Key, Text, ProfilePath) == 0)
		return FALSE;

	return TRUE;
}

BOOL SetIRWProfileInt(const char *ProfileName, const char *Section, const char *Key, DWORD Value)
{
	char Text[4096];
	char ProfilePath[MAX_PATH];

	if(!PathSet)
		return FALSE;

	memset(Text, 0, sizeof(Text));

	/* if its a drive letter, full path */
	if(ProfileName != NULL && ProfileName[1] == ':' && ProfileName[2] == '\\')
		strcpy(ProfilePath, ProfileName);
	else if(ProfileName != NULL) /* build the full path */
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, ProfileName);

		/* check if the profile name ends with .ini, if not, append it */
		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	if(ProfileName == NULL)
	{
		strcpy(ProfilePath, ProfilesFolder);
		strcat(ProfilePath, "\\");
		strcat(ProfilePath, CurProfile);

		if(strcmp(ProfilePath + strlen(ProfilePath) - 1 - 3, ".ini"))
			strcat(ProfilePath, ".ini");
	}

	sprintf(Text, "%d", Value);

    if(WritePrivateProfileString(Section, Key, Text, ProfilePath) == 0)
		return FALSE;

	return TRUE;
}
