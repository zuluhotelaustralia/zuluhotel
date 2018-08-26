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
* 	Sept 18th, 2004 -- window stuff
* 
\******************************************************************************/


#ifndef _GUI_H_INCLUDED
#define _GUI_H_INCLUDED

typedef struct tagTabList
{
	HWND Wnd;
    const char *Name;
	int Resizeable;
    int Width;
	int Height;
}TabList;

/* INTERNALS */
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
void InitGUI(void);
void DestroyGUI(void);
void LoopGUI(void *Bleh);
int FixTabSize(void);

/* EXPORTS */
HWND GetIRWWindow(void);
HWND GetMainTabWindow(void);

void AddTab(HWND TabWnd, const char *Name, int Resizeable);;
void ActivateTab(int TabIdx);

#endif
