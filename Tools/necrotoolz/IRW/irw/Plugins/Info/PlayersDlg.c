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
* 	Jan 08th, 2005 -- divided into General(main)/Layers/Players info
* 
\******************************************************************************/


#include <windows.h>
#include <commctrl.h>
#include <stdio.h>
#include "PlayersDlg.h"
#include "resource.h"
#include "irw.h"


#define NAME_COL_IDX 0
#define HEALTH_COL_IDX 1
#define DIST_COL_IDX 2
#define HIDDEN_COL_IDX 3
#define WAR_COL_IDX 4
#define POSX_COL_IDX 5
#define POSY_COL_IDX 6
#define POSZ_COL_IDX 7
#define SERIAL_COL_IDX 8


static HWND CurWnd = NULL;
static unsigned int LastLifeSerial = INVALID_SERIAL;
static unsigned int LastLife = 0;

LRESULT CALLBACK PlayersDlgProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	CurWnd = hWnd;

	switch(uMsg)
	{
		case WM_INITDIALOG:
		{
			int DisplayLifeChanges = 0;

			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), NAME_COL_IDX, 90, "Name");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), HEALTH_COL_IDX, 30, "Health");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), DIST_COL_IDX, 50, "Distance");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), HIDDEN_COL_IDX, 30, "Hidden");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), WAR_COL_IDX, 30, "War mode");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), POSX_COL_IDX, 50, "X");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), POSY_COL_IDX, 50, "Y");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), POSZ_COL_IDX, 50, "Z");
			AddColumn(GetDlgItem(CurWnd, LIST_PLAYERS), SERIAL_COL_IDX, 100, "Serial");

			/* if the user wants to display life changes... check the box */
			GetIRWProfileInt(NULL, "Info", "DisplayLifeChanges", &DisplayLifeChanges);
			if(DisplayLifeChanges) SendMessage(GetDlgItem(CurWnd, CHECK_PRINTLIFE), BM_SETCHECK, BST_CHECKED, 0);

			AddWorldCallback(&CheckPlayerUpdates);

			return TRUE;
		}break;
		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
			{
				case CHECK_PRINTLIFE:
				{
					/* set the user preference */
					if(SendMessage(GetDlgItem(CurWnd, CHECK_PRINTLIFE), BM_GETCHECK, 0, 0) == BST_CHECKED)
						SetIRWProfileInt(NULL, "Info", "DisplayLifeChanges", 1);
					else
						SetIRWProfileInt(NULL, "Info", "DisplayLifeChanges", 0);

					return TRUE;
				}break;
			}
		}break;
	}

	return FALSE;
}

void AddColumn(HWND List, int ColIndex, int Width, char *Name)
{
	LV_COLUMN Col;

	memset(&Col, 0, sizeof(LV_COLUMN));
	Col.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	Col.fmt = LVCFMT_LEFT;
	Col.cx = Width;
	Col.pszText = Name;
	Col.iSubItem = ColIndex;
	ListView_InsertColumn(List, ColIndex, &Col);

	return;
}

int AddColumnItem(HWND List, char *Name)
{
	LV_ITEM ListItem;
	int Index = ListView_GetItemCount(List);

	memset(&ListItem, 0, sizeof(LV_ITEM));
	ListItem.mask = LVIF_TEXT | LVIF_PARAM;
	ListItem.pszText = Name;
	ListItem.iItem = Index;
	ListItem.lParam = Index;
	ListItem.iSubItem = 0;
	return ListView_InsertItem(List, &ListItem);
}

int FindItemInList(HWND List, int ColIndex, char *Text)
{
	int i = 0;
	char ItemText[4096];

	for(i = 0; i < ListView_GetItemCount(List); i++)
	{
		ListView_GetItemText(List, i, ColIndex, ItemText, sizeof(ItemText));

		if(!strcmp(ItemText, Text))
			return i;
	}

	return -1;
}

void CheckPlayerUpdates(int Type, unsigned int Serial, int Idx, unsigned char *Packet, int Len)
{
	HWND ListWnd = NULL;
	GameObject TmpObj, TmpPlayer;
	char TmpTxt[1024];
	int i = 0, ItemIdx = -1;
	ListWnd = GetDlgItem(CurWnd, LIST_PLAYERS);

	if(Type != PLAYER_UPDATE)
		return;

	if(GetObjectInfo(Serial, Idx, &TmpObj) == OBJECT_NOTFOUND)
	{
		MBOut("WTF", "Bad error with the world handler");
		return;
	}

	if(TmpObj.Character == NULL || !strcmp(TmpObj.Character->Name, "<not initialized>"))
	{
		FREECHAR(TmpObj);
		return;
	}

	/* find the item's serial in the list, if present just return the idx, if not add the item to the list */
	sprintf(TmpTxt, "0x%08X", Serial);
	ItemIdx = FindItemInList(ListWnd, SERIAL_COL_IDX, TmpTxt);
	if(ItemIdx == -1)
		ItemIdx = AddColumnItem(ListWnd, "<not initialized>");

	/* set the item props to the list */
	ListView_SetItemText(ListWnd, ItemIdx, NAME_COL_IDX, TmpObj.Character->Name);

	/* Health percentage - avoid divide by zero */
	if(TmpObj.Character->MaxHitPoints) sprintf(TmpTxt, "%d%%", (TmpObj.Character->HitPoints*100)/TmpObj.Character->MaxHitPoints);
	else sprintf(TmpTxt, "0%%");
	ListView_SetItemText(ListWnd, ItemIdx, HEALTH_COL_IDX, TmpTxt);

	/* Distance */
	GetObjectInfo(INVALID_SERIAL, GetPlayerIdx(), &TmpPlayer);
	sprintf(TmpTxt, "%d", GetDistance(TmpPlayer.X, TmpPlayer.Y, TmpObj.X, TmpObj.Y));
	ListView_SetItemText(ListWnd, ItemIdx, DIST_COL_IDX, TmpTxt);

	/* Hidden (Yes/No) */
	if(GETOBJFLAG(TmpObj, OBJ_FLAG_HIDDEN)) strcpy(TmpTxt, "Yes");
	else strcpy(TmpTxt, "No");
	ListView_SetItemText(ListWnd, ItemIdx, HIDDEN_COL_IDX, TmpTxt);

	/* WarMode (On/Off) */
	if(GETOBJFLAG(TmpObj, OBJ_FLAG_WAR)) strcpy(TmpTxt, "On");
	else strcpy(TmpTxt, "Off");
	ListView_SetItemText(ListWnd, ItemIdx, WAR_COL_IDX, TmpTxt);

	/* XYZ */
	sprintf(TmpTxt, "%d", TmpObj.X);
	ListView_SetItemText(ListWnd, ItemIdx, POSX_COL_IDX, TmpTxt);
	sprintf(TmpTxt, "%d", TmpObj.Y);
	ListView_SetItemText(ListWnd, ItemIdx, POSY_COL_IDX, TmpTxt);
	sprintf(TmpTxt, "%d", TmpObj.Z);
	ListView_SetItemText(ListWnd, ItemIdx, POSZ_COL_IDX, TmpTxt);

	/* Serial */
	sprintf(TmpTxt, "0x%08X", TmpObj.Serial);
	ListView_SetItemText(ListWnd, ItemIdx, SERIAL_COL_IDX, TmpTxt);

	/* if its a life update, display it */
	if(Packet[0] == 0xa1)
	{
		if(SendMessage(GetDlgItem(CurWnd, CHECK_PRINTLIFE), BM_GETCHECK, 0, 0) == BST_CHECKED && TmpObj.Character != NULL)
		{
			/* some emulators send "bursts" of updates, ignore them */
			if(LastLifeSerial != TmpObj.Serial || LastLife != TmpObj.Character->HitPoints)
			{
				ClientPrintAbove(TmpObj.Serial, "%s %d%%", TmpObj.Character->Name,
							    (TmpObj.Character->HitPoints*100)/TmpObj.Character->MaxHitPoints);

				LastLifeSerial = TmpObj.Serial;
				LastLife = TmpObj.Character->HitPoints;
			}
		}
	}

    FREECHAR(TmpObj);
	return;
}
