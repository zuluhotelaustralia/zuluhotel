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
#include <stdio.h>
#include "LayersDlg.h"
#include "resource.h"
#include "irw.h"


static HWND CurWnd = NULL;
static unsigned int Target = INVALID_SERIAL;

void SetTarget(unsigned int Serial){ Target = Serial; return; }

/* concatinates Text with the text of DlgItem in DlgItem */
void TextBoxCat(HWND Dlg, int DlgItem, const char *Text, ...)
{
	char Final[4096];
	va_list List;

	char *NewText = NULL;
	unsigned int NewTextSize = 0;

	va_start(List, Text);
	vsprintf((char*)Final, Text, List);
	va_end(List);

	/* get the dlg text, make sure we have enough space for it */
	NewTextSize += 100;
	NewText = (char*)realloc(NewText, NewTextSize);
	while(GetDlgItemText(Dlg, DlgItem, NewText, NewTextSize) == (int)NewTextSize - 1)
	{
		NewTextSize += 100;
		NewText = (char*)realloc(NewText, NewTextSize);
	}

	NewTextSize += (int)(strlen(Final) + 1);
	NewText = (char*)realloc(NewText, NewTextSize);
	strcat(NewText, Final);
	SetDlgItemText(Dlg, DlgItem, NewText);

	free(NewText);
	return;
}

void ListLayers(int Layer)
{
	GameObject Obj;
	char Text[4096*2];
	char ItemText[1024];
	int i = 0, ListSize = 0, ItemCount = 0;
	int *List = NULL;
	
	List = ListItemsInContainer(Target);

	if(List == NULL)
	{
		sprintf(Text, "No items in layer (%d)", Layer);
		SetDlgItemText(CurWnd, EDIT_LAYERINFO, Text);
		free(List);
		return;
	}

	ListSize = List[0];
	ItemCount = ListSize - 1;
	sprintf(Text, "%d items in layer (%d):\r\n\r\n", ItemCount, Layer);

	for(i = 1; i < ListSize; i++)
	{
		GetObjectInfo(INVALID_SERIAL, List[i], &Obj);

		if(Obj.Layer == Layer || Layer == -1)
		{
			sprintf(ItemText, "Serial: 0x%08X Layer: %d Quantity: %d\r\nGraphic: 0x%04X Color:0x%04X\r\nIn container: 0x%08X IsContainer: %d\r\n", 
					Obj.Serial, Obj.Layer, Obj.Quantity, Obj.Graphic, Obj.Color, Obj.Container, Obj.IsContainer);
            
			strcat(Text, ItemText);
		}
	}

	TextBoxCat(CurWnd, EDIT_LAYERINFO, Text);
	free(List);
	return;
}

LRESULT CALLBACK LayersDlgProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	CurWnd = hWnd;

	switch(uMsg)
	{
		case WM_INITDIALOG:
		{
			SetDlgItemText(hWnd, EDIT_LAYERINFO, "Select the layers you wish to get information about");
			return TRUE;
		}break;
		case WM_COMMAND: /* big ass bitch */
		{
            if(LOWORD(wParam) == BUTTON_GETINFO)
			{
				char TargetText[4096];
				GameObject TmpObj;
				ALLOCCHAR(TmpObj);

				/* clear the text box */
				SetDlgItemText(hWnd, EDIT_LAYERINFO, "");
				
				/* get the target data. if no string given, use player serial */
				GetDlgItemText(CurWnd, EDIT_TARGET, TargetText, sizeof(TargetText));
				if(!strcmp(TargetText, ""))
					Target = GetPlayerSerial();
				else /* if we have user input, check it */
				{
					Target = ArgToInt(TargetText);
					if(GetObjectInfo(Target, INVALID_IDX, &TmpObj) == OBJECT_NOTFOUND)
					{
						SetDlgItemText(hWnd, EDIT_LAYERINFO, "Could not find that target in the object list");
						return TRUE;
					}

					if(TmpObj.Character == NULL)
					{
						SetDlgItemText(hWnd, EDIT_LAYERINFO, "The target is an item and not a character");
						return TRUE;
					}
				}
				FREECHAR(TmpObj);

                if(SendMessage(GetDlgItem(CurWnd, CHECK_HEAD), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_HELM);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_NECK), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_NECK);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_HAIR), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_HAIR);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_FACIALHAIR), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_FACIAL_HAIR);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_EARRINGS), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_EARRINGS);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_SHIRT), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_SHIRT);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_TORSO), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_TORSO);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_TUNIC), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_TUNIC);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_CLOAK), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_CLOAK);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_WAIST), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_WAIST);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_SKIRT), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_SKIRT);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_PANTS), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_PANTS);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_SHOES), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_SHOES);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_GLOVES), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_GLOVES);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_ARMS), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_ARMS);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_RHAND), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_ONE_HANDED);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_RING), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_RING);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_LHAND), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_TWO_HANDED);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_BRACELET), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_BRACELET);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_BACKPACK), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_BACKPACK);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_MOUNT), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_MOUNT);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_BANK), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_BANK);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_RESTOCK), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_VENDOR_BUY_RESTOCK);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_LHAND), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_VENDOR_BUY);

				if(SendMessage(GetDlgItem(CurWnd, CHECK_BRACELET), BM_GETCHECK, 0, 0) == BST_CHECKED)
					ListLayers(LAYER_VENDOR_SELL);

				return TRUE;
			}
		}break;
	}

	return FALSE;
}
