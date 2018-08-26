/******************************************************************************\
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


#ifndef _MAIN_H_INCLUDED
#define _MAIN_H_INCLUDED

HANDLE GetCurrentInstance(void);
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, INT nCmdShow);
BOOL CALLBACK MainDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam);


void UpdateList(HWND Wnd, int DlgItem, const char *Path, const char *FType);
HANDLE GetCurInstance(void);
BOOL CheckCurSel(void);
BOOL IRWFilesPresent(char *Path);
void MBOut(char *title, char *msg, ...);
int LoadLibraryOnProcess(HANDLE hProcess, HANDLE hThread, char** LibData, int LibCount);

#endif
