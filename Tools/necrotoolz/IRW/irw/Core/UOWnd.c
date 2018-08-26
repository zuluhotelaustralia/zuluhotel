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


#include <windows.h>
#include "UOWnd.h"
#include "UOWorld.h"

static HWND ClientHWND = NULL;
static WNDPROC OldWndProc = NULL;

LRESULT CALLBACK UOWndProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	ClientHWND = hWnd;

	switch(Msg)
	{
		case WM_SHOWWINDOW:
		{
			SetForegroundWindow(hWnd);
		}break;
		case WM_SETTEXT:
		{
			/*
			* if the player is in the world
			* stop the client from updating the window if we already did it
			* irw (and injection) appends "UO - " to the window's caption
			*/
			if(GetPlayerSerial() != INVALID_SERIAL)
			{
				char *Str = (char*)lParam;
				if(Str[1] == 0) /* unicode window */
				{
					if(wcsncmp((wchar_t*)Str, L"UO - ", 4) != 0)
						return TRUE;
				}
				else /* ascii window */
				{
					if(strncmp(Str, "UO - ", 4) != 0)
						return TRUE;
				}
			}
		}break;
		case WM_CLOSE:
		{
			ClientHWND = NULL;
		}break;
	}

	return OldWndProc(hWnd, Msg, wParam, lParam);
}

HWND GetUOWindow(void)
{
	return ClientHWND;
}

ATOM WINAPI hook_RegisterClassA(WNDCLASSA *lpWndClass)
{
	OldWndProc = lpWndClass->lpfnWndProc;
	lpWndClass->lpfnWndProc = UOWndProc;

	return RegisterClassA(lpWndClass);
}

ATOM WINAPI hook_RegisterClassW(WNDCLASSW *lpWndClass)
{
	OldWndProc = lpWndClass->lpfnWndProc;
	lpWndClass->lpfnWndProc = UOWndProc;

	return RegisterClassW(lpWndClass);
}
		
BOOL WINAPI hook_PeekMessage(LPMSG lpMsg, HWND hWnd, UINT wMsgFilterMin, UINT wMsgFilterMax, UINT wRemoveMsg)
{
	int ret = PeekMessage(lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);

	/* RTD: should this be considered idle time? */
	if(!ret)
		Sleep(1);

	return ret;
}

unsigned char* GetUOScreenData(BOOL ShowTitleBar, BITMAPINFO *bmInfo)
{
	HWND WndUse = NULL;
	RECT Rect;
	HDC ScreenDC = NULL, PicDC = NULL;
	HBITMAP PicBM = NULL;
	int ScreenWidth = 0, ScreenHeight = 0;
	static unsigned char *PicBuf = NULL;

	/* WndUse = GetDesktopWindow(); /* printscreen */
	WndUse = GetUOWindow(); /* UO's screen */

	GetClientRect(WndUse, &Rect);
	ScreenWidth = Rect.right - Rect.left;
	ScreenHeight = Rect.bottom - Rect.top;

	if(!ShowTitleBar)
		ScreenDC = GetDC(WndUse); /* does not display title bar */
	else
	{
		ScreenDC = GetWindowDC(WndUse); /* displays title bar */
		ScreenWidth += 2*GetSystemMetrics(SM_CXFRAME);
		ScreenHeight += 2*GetSystemMetrics(SM_CYFRAME) + GetSystemMetrics(SM_CYCAPTION);
	}

	/* setup the bitmap info */
	bmInfo->bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bmInfo->bmiHeader.biWidth = ScreenWidth;
	bmInfo->bmiHeader.biHeight = ScreenHeight;
	bmInfo->bmiHeader.biPlanes = 1;
	bmInfo->bmiHeader.biBitCount = 24; /* R-G-B, 3 bytes */
	bmInfo->bmiHeader.biCompression = 0;
	bmInfo->bmiHeader.biSizeImage = 0;
	bmInfo->bmiHeader.biXPelsPerMeter = 0;
	bmInfo->bmiHeader.biYPelsPerMeter = 0;
	bmInfo->bmiHeader.biClrUsed = 0;
	bmInfo->bmiHeader.biClrImportant = 0;

	/* get the screen data */
	PicBM = CreateCompatibleBitmap(ScreenDC, ScreenWidth, ScreenHeight);
	PicDC = CreateCompatibleDC(ScreenDC);

	SelectObject(PicDC, PicBM);
	BitBlt(PicDC, 0, 0, ScreenWidth, ScreenHeight, ScreenDC, 0, 0, SRCCOPY);

	/* copy bitmap data to memory */
	if(PicBuf == NULL)
		PicBuf = (unsigned char*)malloc(ScreenWidth * ScreenHeight * (24/8));
	GetDIBits(PicDC, PicBM, 0, ScreenHeight, PicBuf, bmInfo, DIB_RGB_COLORS);

	DeleteObject(PicBM);
	DeleteDC(PicDC);
	ReleaseDC(WndUse, ScreenDC);

	return PicBuf;
}
