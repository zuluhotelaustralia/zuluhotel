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


#ifndef _PROFILEDLG_H_INCLUDED
#define _PROFILEDLG_H_INCLUDED

typedef struct tagUOCryptInfo
{
	char Name[1024];
	int Type;
	unsigned int Key1;
	unsigned int Key2;
}UOCryptInfo;

BOOL CALLBACK ProfileDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam);

void SetProfileIni(char *Ini);
void UpdateCryptList(HWND Wnd, int DlgItem);
void UpdatePluginList(const char *Profile, HWND Wnd, int DlgItem);
void UpdateProfileFromList(const char *Profile, HWND Wnd, int DlgItem);
BOOL CheckItemInList(HWND Wnd, int DlgItem, const char *Name);

#endif
