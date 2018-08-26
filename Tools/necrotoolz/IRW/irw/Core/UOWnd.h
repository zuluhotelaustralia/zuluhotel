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
* 	Sept 18th, 2004 -- hooks for uo's window
* 
\******************************************************************************/


#ifndef _UOWND_H_INCLUDED
#define _UOWND_H_INCLUDED

/* INTERNALS */
LRESULT CALLBACK UOWndProc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam);
ATOM WINAPI hook_RegisterClassA(WNDCLASSA *lpWndClass);
ATOM WINAPI hook_RegisterClassW(WNDCLASSW *lpWndClass);
BOOL WINAPI hook_PeekMessage(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg);

/* EXPORTS */
HWND GetUOWindow(void);
unsigned char* GetUOScreenData(BOOL ShowTitleBar, BITMAPINFO *bmInfo);

#endif
