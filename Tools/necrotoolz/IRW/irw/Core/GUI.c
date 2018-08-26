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
* 	Sept 18th, 2004 -- window stuff. this is the ONLY second thread
* 
\******************************************************************************/


#include <windows.h>
#include <commctrl.h>
#include "GUI.h"
#include "Main.h"
#include "Logger.h"


static HANDLE GUIThread = NULL;
static HWND GUIWnd = NULL;
static HFONT GUIFont = NULL;
static HWND MainTabWnd = NULL;
static int TabCount = 0;
static BOOL KeepAlive = TRUE;

static TabList Tabs[100];

HWND GetIRWWindow(void)
{
	return GUIWnd;
}

HWND GetMainTabWindow(void)
{
	return MainTabWnd;
}

void AddTab(HWND TabWnd, const char *Name, int Resizeable)
{
	RECT TabRect;
	TCITEM tci;

	GetClientRect(TabWnd, &TabRect);
	Tabs[TabCount].Wnd = TabWnd;
	Tabs[TabCount].Name = Name;
	Tabs[TabCount].Resizeable = Resizeable;
	Tabs[TabCount].Width = TabRect.right;
	Tabs[TabCount].Height = TabRect.bottom;

	ShowWindow(TabWnd, SW_HIDE);

	tci.mask = TCIF_TEXT;
	tci.iImage = -1;
	tci.pszText = (LPSTR)Name;
	TabCtrl_InsertItem(MainTabWnd, TabCount, &tci);
	TabCount++;

	/* activate the first tab */
	if(TabCount == 1)
		ActivateTab(0);

	return;
}

/* fix the GUI so the main dialog fits to the current tab */
int FixTabSize(void)
{
	RECT TabRect, GUIRect;
	int TabIdx = 0, Width = 0, Height = 0;

	TabIdx = TabCtrl_GetCurSel(MainTabWnd);

	if(Tabs[TabIdx].Resizeable == TRUE)
		return 0;

	if(MainTabWnd == NULL || TabCount == 0 || TabIdx < 0 || TabIdx+1 > TabCount)
		return -1;

	/* get the size of the tab + plugin wnd */
	TabRect.left = TabRect.top = 0;
	TabRect.bottom = Tabs[TabIdx].Height;
	TabRect.right = Tabs[TabIdx].Width;
	TabCtrl_AdjustRect(MainTabWnd, TRUE, &TabRect);

	/* stretch the GUI in a way that the plugin wnd will be shown */
	GetWindowRect(GUIWnd, &GUIRect);
	Width = (TabRect.right - TabRect.left) + 2*GetSystemMetrics(SM_CXFRAME);
	Height = (TabRect.bottom - TabRect.top) + 2*GetSystemMetrics(SM_CYFRAME) + GetSystemMetrics(SM_CYCAPTION);
	MoveWindow(GUIWnd, GUIRect.left, GUIRect.top, Width, Height, TRUE);

	return 1;
}

void ActivateTab(int TabIdx)
{
	RECT TabRect;
	HWND Wnd = NULL;
	int i = 0, Resizeable = 0, Width = 0, Height = 0;

	if(MainTabWnd == NULL || TabCount == 0 || TabIdx < 0)
		return;

	if(TabIdx+1 > TabCount)
	{
		LogPrint(ERROR_LOG, "GUI:ERROR: TabIdx %d exceeded %d\r\n", TabIdx, TabCount);
		return;
	}

	/* hide all other tab windows */
	for(i = 0; i < TabCount; i++)
		if(i == TabIdx) continue;
		else ShowWindow(Tabs[i].Wnd, SW_HIDE);
	
	Wnd = Tabs[TabIdx].Wnd;
	Resizeable = Tabs[TabIdx].Resizeable;
	Width = Tabs[TabIdx].Width;
	Height = Tabs[TabIdx].Height;

	if(Wnd == NULL)
	{
		LogPrint(WARNING_LOG, ":GUI:WARNING: Tab %s has a null wnd!\r\n", Tabs[TabIdx].Name);
		return;
	}

	/* fix the window in the tab  */
	GetClientRect(Wnd, &TabRect);
	TabCtrl_AdjustRect(MainTabWnd, FALSE, &TabRect);
	MoveWindow(Wnd, TabRect.left, TabRect.top, Width, Height, TRUE);

	FixTabSize();
	/* show the tab window and irw window */
	ShowWindow(Wnd, SW_SHOW);
	ShowWindow(GUIWnd, SW_SHOW);

	return;
}

void InitGUI(void)
{
	DWORD NoUse = 0;
	GUIThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)LoopGUI, NULL, 0, &NoUse);

	/* wait for the GUI to be initialized */
	while(MainTabWnd == NULL)
		Sleep(1);

	return;
}

void DestroyGUI(void)
{
	KeepAlive = FALSE;
	if(GUIFont != NULL)
	{
		DeleteObject(GUIFont);
		GUIFont = NULL;
	}

	if(GUIWnd != NULL)
	{
		DestroyWindow(GUIWnd);
		GUIWnd = NULL;
		MainTabWnd = NULL;
	}

	if(GUIThread != NULL)
	{
        CloseHandle(GUIThread);
		GUIThread = NULL;
	}

	return;
}

void LoopGUI(void *Bleh)
{
	WNDCLASSEX wc;
	INITCOMMONCONTROLSEX cc; /* RTD: _WIN32_IE_ >= 0x300 */
	MSG msg;
	RECT Rect;

	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.style = CS_HREDRAW | CS_VREDRAW | CS_NOCLOSE;
	wc.lpfnWndProc = WndProc;
	wc.hInstance = GetIRWInstance();
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH) GetStockObject(LTGRAY_BRUSH);
	wc.lpszClassName = "IRWClass";
	/*wc.hIcon = LoadIcon(GetIRWInstance(), MAKEINTRESOURCE(IDI_ICON1));*/
	RegisterClassEx(&wc);

	/* create the window */
	GUIWnd = CreateWindowEx(WS_EX_WINDOWEDGE | WS_EX_CONTROLPARENT,
							"IRWClass", "IRW",
							WS_VISIBLE | WS_OVERLAPPED | WS_THICKFRAME | WS_SYSMENU | WS_CAPTION | WS_MINIMIZEBOX | WS_CLIPCHILDREN,
							CW_USEDEFAULT, CW_USEDEFAULT, 50, 80, NULL, NULL, GetIRWInstance(), NULL);

	if(GUIWnd == NULL)
	{
		LogPrint(ERROR_LOG, ":GUI:ERROR: Could not create the window\r\n");
		MBOut(":GUI:ERROR:", "Could not create the window");
		ExitProcess(0);
		return;
	}

	/* init common controls for tabs */
	cc.dwSize = sizeof(INITCOMMONCONTROLSEX);
	cc.dwICC = ICC_TAB_CLASSES;
	InitCommonControlsEx(&cc);

	/* create the tab window */
	GetClientRect(GUIWnd, &Rect);
	MainTabWnd = CreateWindow(WC_TABCONTROL, "", WS_CHILD | WS_CLIPCHILDREN | WS_VISIBLE, 0, 0,
							  Rect.right, Rect.bottom, GUIWnd, NULL, GetIRWInstance(), NULL);

	if(MainTabWnd == NULL)
	{
		LogPrint(ERROR_LOG, ":GUI:ERROR: Could not create the main tab\r\n");
		MBOut(":GUI:ERROR:", "Could not create the main tab");
		ExitProcess(0);
		return;
	}

	/* set the font of the main window (GUI) and the main tab */
	GUIFont = CreateFont(8, 0, 0, 0, FW_DONTCARE, 0, 0, 0, ANSI_CHARSET, OUT_DEFAULT_PRECIS,
						 CLIP_DEFAULT_PRECIS, PROOF_QUALITY, FF_MODERN, "MS Sans Serif");
	SendMessage(GUIWnd, WM_SETFONT, (WPARAM)GUIFont, 1);
	SendMessage(MainTabWnd, WM_SETFONT, (WPARAM)GUIFont, 1);

	if(GUIFont == NULL)
	{
		LogPrint(ERROR_LOG, ":GUI:ERROR: Could not set the font\r\n");
		MBOut(":GUI:ERROR:", "Could not set the font");
		ExitProcess(0);
		return;
	}

	/* IRW withouth plugins is nothing but an empty window ;) */
	ShowWindow(GUIWnd, SW_HIDE);
	UpdateWindow(GUIWnd);

	while(KeepAlive)
	{
		if(PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
		else /* RTD: should this be considered the idle time? */
			Sleep(15);
	}

	return;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	NMHDR *nmptr = NULL;

	switch(uMsg)
	{
		case WM_SIZE:
		{
			MoveWindow(MainTabWnd, 0, 0, LOWORD(lParam), HIWORD(lParam), TRUE);

			if(FixTabSize() == 0)
				MoveWindow(Tabs[TabCtrl_GetCurSel(MainTabWnd)].Wnd, 0, 0, LOWORD(lParam), HIWORD(lParam), TRUE);
		}break;
		case WM_NOTIFY:
		{
			nmptr = (NMHDR*)lParam;
			if(nmptr->code == TCN_SELCHANGE)
				ActivateTab(TabCtrl_GetCurSel(MainTabWnd));
		}
		break;
	}

	return DefWindowProc(hWnd, uMsg, wParam, lParam);
}
