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
#include <commctrl.h>
#include <stdio.h>
#include "Main.h"
#include "INIProfile.h"
#include "Registry.h"
#include "ProfileDlg.h"
#include "resource.h"

typedef void (*PluginInfoFunction) (char *, int);

static HWND CurWnd = NULL;
/* this value must ALWAYS be set when creating the dlgbox */
static char *ProfileIni = NULL;

static UOCryptInfo *EmulationList = NULL;
static int ListSize = 0;


BOOL CALLBACK ProfileDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam)
{
	CurWnd = hDlg;

	switch(wMsg)
	{
		case WM_INITDIALOG:
		{
			LV_FINDINFO ListItem;
			char Text[4096];

			/* set the IRW icon */
			SendMessage(CurWnd, WM_SETICON, ICON_SMALL, (LPARAM)LoadIcon(GetCurrentInstance(), MAKEINTRESOURCE(IRW_ICON)));

			/* update the encryption list based on UOKeys.cfg */
			UpdateCryptList(CurWnd, LIST_CRYPT);

			/* if we are creating a new profile */
			if(ProfileIni == NULL)
			{
				MBOut("", "Please type your new profile name");

				/* set it to use irw */
				SendMessage(GetDlgItem(CurWnd, CHECK_USEIRW), BM_SETCHECK, BST_CHECKED, 0);
				return FALSE;
			}

			/* if the profileini was passed, we are editing, set the current values to the text boxes */
			SetDlgItemText(CurWnd, EDIT_PROFILE, ProfileIni);

			GetIRWProfileString(ProfileIni, "IRW", "ClientPath", Text, sizeof(Text));
			SetDlgItemText(CurWnd, EDIT_CLIENT, Text);

			GetIRWProfileString(ProfileIni, "IRW", "UseIRW", Text, sizeof(Text));
			SendMessage(GetDlgItem(CurWnd, CHECK_USEIRW), BM_SETCHECK, !strcmp(Text, "yes") ? BST_CHECKED : BST_UNCHECKED, 0);
			SendMessage(CurWnd, WM_COMMAND, CHECK_USEIRW, 0);

			GetIRWProfileString(ProfileIni, "IRW", "CryptEmulation", Text, sizeof(Text));
			ListItem.flags = LVFI_STRING;
			ListItem.psz = Text;
			ListView_SetItemState(GetDlgItem(CurWnd, LIST_CRYPT), 
				                  ListView_FindItem(GetDlgItem(CurWnd, LIST_CRYPT), -1, &ListItem),
								  LVIS_SELECTED | LVIS_FOCUSED , LVIS_SELECTED | LVIS_FOCUSED);

			GetIRWProfileString(ProfileIni, "IRW", "ShardAddress", Text, sizeof(Text));
			SetDlgItemText(CurWnd, EDIT_SHARDADDRESS, Text);

			GetIRWProfileString(ProfileIni, "IRW", "ShardPort", Text, sizeof(Text));
			SetDlgItemText(CurWnd, EDIT_SHARDPORT, Text);

			GetIRWProfileString(ProfileIni, "IRW", "Username", Text, sizeof(Text));
			SetDlgItemText(CurWnd, EDIT_USERNAME, Text);

			GetIRWProfileString(ProfileIni, "IRW", "Password", Text, sizeof(Text));
			SetDlgItemText(CurWnd, EDIT_PWD, Text);

			/* update the list control  as well */
			UpdatePluginList(ProfileIni, CurWnd, LIST_PLUGINS);

			SetDlgItemText(CurWnd, EDIT_PLUGINDETAIL, "Plugin details...");

			return FALSE;
		}break;
		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
			{
				case CHECK_USEIRW:
				{
					/* enable the dlgitems if using irw */
					if(SendMessage(GetDlgItem(CurWnd, CHECK_USEIRW), BM_GETCHECK, 0, 0) == BST_CHECKED)
					{
						EnableWindow(GetDlgItem(CurWnd, COMBO_CRYPT), TRUE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_SHARDIP), TRUE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_SHARDPORT), TRUE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_USERNAME), TRUE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_PWD), TRUE);
						EnableWindow(GetDlgItem(CurWnd, BUTTON_ADD), TRUE);
						EnableWindow(GetDlgItem(CurWnd, BUTTON_REMOVE), TRUE);
					}
					else /* disable the dlgitems if not using irw */
					{
						EnableWindow(GetDlgItem(CurWnd, COMBO_CRYPT), FALSE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_SHARDIP), FALSE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_SHARDPORT), FALSE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_USERNAME), FALSE);
						EnableWindow(GetDlgItem(CurWnd, EDIT_PWD), FALSE);
						EnableWindow(GetDlgItem(CurWnd, BUTTON_ADD), FALSE);
						EnableWindow(GetDlgItem(CurWnd, BUTTON_REMOVE), FALSE);
					}

					return FALSE;
				}break;
				case BUTTON_CLIENT:
				{
					OPENFILENAME ofn;
					char ClientPath[MAX_PATH];

					/* ask for the client's path */
					memset(ClientPath, 0, sizeof(ClientPath));
					memset(&ofn, 0, sizeof(ofn));
					ofn.lStructSize = sizeof(OPENFILENAME);
					ofn.hwndOwner = CurWnd;
					ofn.lpstrFile = ClientPath;
					ofn.nMaxFile = sizeof(ClientPath);
					ofn.lpstrInitialDir = GetIRWPath();
					ofn.lpstrFilter = "Exe\0*.Exe\0All\0*.*\0";
					ofn.nFilterIndex = 1;
					ofn.lpstrTitle = "Choose the UO client to be launched";
					ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
					GetOpenFileName(&ofn);

					SetDlgItemText(CurWnd, EDIT_CLIENT, ClientPath);

					return FALSE;
				}break;
				case BUTTON_UPDATE:
				{
					char ProfileName[4096], Text[4096];
					int Item = 0;

					memset(ProfileName, 0, sizeof(ProfileName));
					memset(Text, 0, sizeof(Text));

					/* get the profile name */
					GetDlgItemText(CurWnd, EDIT_PROFILE, ProfileName, sizeof(ProfileName));

					/* check if its blank */
					if(!strcmp(ProfileName, ""))
					{
						MBOut("Profile error", "Please set a valid profile name");
						return FALSE;
					}

					/* check if we have to add .ini to it */
					if(strlen(ProfileName) <= 4)
						strcat(ProfileName, ".ini");

					if(strcmp(ProfileName + strlen(ProfileName) - 1 - 3, ".ini"))
						strcat(ProfileName, ".ini");

					/* if we are editing a profile and the user changed the name */
					if(ProfileIni != NULL && strcmp(ProfileIni, ProfileName))
					{
						MBOut("", "Changing profile name. Backing up old profile.");
						CopyFile(ProfileIni, ProfileName, FALSE);
					}

					/* get the client's path, check if it ends with .exe, the best we can do :P */
					GetDlgItemText(CurWnd, EDIT_CLIENT, Text, sizeof(Text));
					if(strlen(Text) < 4 && strcmp(Text + strlen(Text) - 4, ".exe"))
					{
						MBOut("Profile Error - Y0DA style", "A valid client that is not... uhm-hum!");
						return FALSE;
					}

					/*  set it to the profile */
					SetIRWProfileString(ProfileName, "IRW", "ClientPath", Text);

					/* get only the stuff we need */
					if(SendMessage(GetDlgItem(CurWnd, CHECK_USEIRW), BM_GETCHECK, 0, 0) != BST_CHECKED)
						SetIRWProfileString(ProfileName, "IRW", "UseIRW", "no");
					else
					{
						/* using irw, yeap */
						SetIRWProfileString(ProfileName, "IRW", "UseIRW", "yes");

						/* get the crypt info type */
						Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_CRYPT), -1, LVNI_SELECTED);
						if(Item == -1)
						{
							MBOut("", "Please select an encryption for emulation");
							return FALSE;
						}

						/* this is just so the user can see which crypt will be emulated */
						SetIRWProfileString(ProfileName, "IRW", "CryptEmulation", EmulationList[Item].Name);

						/* these are the ones used by IRW */
						SetIRWProfileInt(ProfileName, "IRW", "CryptType", EmulationList[Item].Type);
						SetIRWProfileInt(ProfileName, "IRW", "CryptKey1", EmulationList[Item].Key1);
						SetIRWProfileInt(ProfileName, "IRW", "CryptKey2", EmulationList[Item].Key2);

						/* shard's address (string or ip) */
						GetDlgItemText(CurWnd, EDIT_SHARDADDRESS, Text, sizeof(Text));
						SetIRWProfileString(ProfileName, "IRW", "ShardAddress", Text);

						/* shard's port */
						GetDlgItemText(CurWnd, EDIT_SHARDPORT, Text, sizeof(Text));
						SetIRWProfileString(ProfileName, "IRW", "ShardPort", Text);

						/* username, used by encryption detection */
						GetDlgItemText(CurWnd, EDIT_USERNAME, Text, sizeof(Text));
						SetIRWProfileString(ProfileName, "IRW", "Username", Text);

						/* password, used by encryption detection */
						GetDlgItemText(CurWnd, EDIT_PWD, Text, sizeof(Text));
						SetIRWProfileString(ProfileName, "IRW", "Password", Text);
					}

					/* gather all the plugin names in the list and write it to the profile ini */
					UpdateProfileFromList(ProfileName, CurWnd, LIST_PLUGINS);

					SendMessage(CurWnd, WM_CLOSE, 0, 0);
                    return FALSE;
				}break;
				case BUTTON_ADD:
				{
					LVITEM ListItem;
					int ItemCount = 0;
					OPENFILENAME ofn;
					char PluginPath[MAX_PATH];
					char *Ptr = NULL;

					/* ask for the client's path */
					memset(PluginPath, 0, sizeof(PluginPath));
					memset(&ofn, 0, sizeof(ofn));
					ofn.lStructSize = sizeof(OPENFILENAME);
					ofn.hwndOwner = CurWnd;
					ofn.lpstrFile = PluginPath;
					ofn.nMaxFile = sizeof(PluginPath);
					ofn.lpstrInitialDir = GetIRWPath();
					ofn.lpstrFilter = "Dll\0*.Dll\0All\0*.*\0";
					ofn.nFilterIndex = 1;
					ofn.lpstrTitle = "Choose the plugin to be loaded when IRW is launched";
					ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
					GetOpenFileName(&ofn);

					if(PluginPath[0] == '\0')
						return FALSE;

					/* remove the path name and leave only the plugin name */
					Ptr = strrchr(PluginPath, '\\') + 1;
					memmove(PluginPath, Ptr, strlen(Ptr) + 1);

					memset(&ListItem, 0, sizeof(LVITEM));
					ItemCount = ListView_GetItemCount(GetDlgItem(CurWnd, LIST_PLUGINS));
					if(!ItemCount) ItemCount++;
					ListItem.mask = LVIF_TEXT | LVIF_PARAM;
					ListItem.iItem = ItemCount - 1;
					ListItem.pszText = PluginPath;
					ListItem.lParam = ItemCount - 1;
					ListView_InsertItem(GetDlgItem(CurWnd, LIST_PLUGINS), &ListItem);

					return FALSE;
				}break;
				case BUTTON_REMOVE:
				{
					int Item = 0;

					Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PLUGINS), -1, LVNI_SELECTED);
					ListView_DeleteItem(GetDlgItem(CurWnd, LIST_PLUGINS), Item);

					return FALSE;
				}break;
				case BUTTON_CANCEL:
				{
					SendMessage(CurWnd, WM_CLOSE, 0, 0);
					return FALSE;
				}break;
			}
		}break;
		case WM_NOTIFY:
		{
			/* RTD: then again, no need to check if its a listview msg */
			HANDLE PluginHandle = NULL;
			PluginInfoFunction GetPluginInfo = NULL;
			char Text[1024], PluginPath[MAX_PATH];
			int Item = 0;
            DWORD Size = 0;

			memset(Text, 0, sizeof(Text));
			memset(PluginPath, 0, sizeof(PluginPath));

			Size = sizeof(PluginPath);;
			GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, PluginPath, &Size);
			strcat(PluginPath, "\\Plugins\\");

			Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PLUGINS), -1, LVNI_SELECTED);
			ListView_GetItemText(GetDlgItem(CurWnd, LIST_PLUGINS), Item, 0, PluginPath + strlen(PluginPath), (int)(sizeof(PluginPath) - strlen(PluginPath)));

			if(Item == -1)
			{
				SetDlgItemText(CurWnd, EDIT_PLUGINDETAIL, "No plugin selected");
				return FALSE;
			}

			PluginHandle = LoadLibrary(PluginPath);
			if(PluginHandle == NULL)
			{
				SetDlgItemText(CurWnd, EDIT_PLUGINDETAIL, "Could not load plugin for info");
				return FALSE;
			}

			GetPluginInfo = (PluginInfoFunction)GetProcAddress(PluginHandle, "GetPluginInfo");
			if(GetPluginInfo == NULL)
			{
				SetDlgItemText(CurWnd, EDIT_PLUGINDETAIL, "Could not find the GetPluginFunction in plugin");
				FreeLibrary(PluginHandle);
				return FALSE;
			}

            GetPluginInfo(Text, sizeof(Text));
			FreeLibrary(PluginHandle);

			SetDlgItemText(CurWnd, EDIT_PLUGINDETAIL, Text);

			return FALSE;
		}break;
		case WM_CLOSE:
		{
			if(EmulationList != NULL)
			{
				free((void*)EmulationList);
				EmulationList = NULL;
				ListSize = 0;
			}

            EndDialog(CurWnd, 0);
			return FALSE;
		}break;
	}

	return FALSE;
}

void UpdateCryptList(HWND Wnd, int DlgItem)
{
	HWND ListWnd = NULL;
	LVITEM ListItem;
	int i = 0, CryptType = 0;
	unsigned int Key1 = 0, Key2 = 0;
	char Name[1024], UOKeysPath[MAX_PATH];
	char *UOKeysBuf = NULL, *Ptr = NULL;
	DWORD Size = 0;
	FILE *UOKeysHandle = NULL;

	memset(UOKeysPath, 0, sizeof(UOKeysPath));

	ListWnd = GetDlgItem(Wnd, DlgItem);
	ListView_DeleteAllItems(ListWnd);

	Size = MAX_PATH;
    GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, UOKeysPath, &Size);
	strcat(UOKeysPath, "\\UOKeys.cfg");

	if((UOKeysHandle = fopen(UOKeysPath, "rb")) == NULL)
		return;
    
	fseek(UOKeysHandle, 0, SEEK_END);
	Size = ftell(UOKeysHandle);

	UOKeysBuf = (char*)malloc(Size);
	Ptr = UOKeysBuf;

	fseek(UOKeysHandle, 0, SEEK_SET);
	fread(UOKeysBuf, 1, Size, UOKeysHandle);
	fclose(UOKeysHandle);

	/* this shouldn't happen at all, it should only be called on initdlg */
	if(EmulationList != NULL)
	{
		free((void*)EmulationList);
		EmulationList = NULL;
		ListSize = 0;
	}

	while((DWORD)(Ptr - UOKeysBuf) < (Size - 1) && Ptr != NULL)
	{
		if(Ptr[0] == '#')
		{
			Ptr = strchr(Ptr, '\n') + 1;
			continue;
		}

		if(Ptr[0] == '\"')
		{
            memset(Name, 0, sizeof(Name));
            strncpy(Name, Ptr + 1, strchr(Ptr + 1, '\"') - 1 - Ptr);
			Ptr += strlen(Name) + 2;

			sscanf(Ptr, "%X %X %d", &Key1, &Key2, &CryptType);
			Ptr = strchr(Ptr, '\n') + 1;

			memset(&ListItem, 0, sizeof(LVITEM));
			ListItem.mask = LVIF_TEXT | LVIF_PARAM;
			ListItem.iItem = i;
			ListItem.pszText = Name;
			ListItem.lParam = i;
			ListView_InsertItem(ListWnd, &ListItem);
			i++;

            /* now insert the item in the struct */
			ListSize++;
			EmulationList = (UOCryptInfo*)realloc(EmulationList, sizeof(UOCryptInfo) * ListSize);
			strcpy(EmulationList[ListSize-1].Name, Name);
			EmulationList[ListSize-1].Key1 = Key1;
			EmulationList[ListSize-1].Key2 = Key2;
			EmulationList[ListSize-1].Type = CryptType;
			continue;
		}

		Ptr++;
	}

	free(UOKeysBuf);
	return;
}

void UpdatePluginList(const char *Profile, HWND Wnd, int DlgItem)
{
	HWND ListWnd = NULL;
	LVITEM ListItem;
	char Item[1024], PluginName[1024];
	int i = 0;

	ListWnd = GetDlgItem(Wnd, DlgItem);
	ListView_DeleteAllItems(ListWnd);
    
	sprintf(Item, "%d", i);
	while(GetIRWProfileString(Profile, "Plugins", Item, PluginName, sizeof(PluginName)))
	{
		memset(&ListItem, 0, sizeof(LVITEM));
		ListItem.mask = LVIF_TEXT | LVIF_PARAM;
		ListItem.iItem = i;
		ListItem.pszText = PluginName;
		ListItem.lParam = i;
		ListView_InsertItem(ListWnd, &ListItem);
		i++;
		sprintf(Item, "%d", i);
	}

    return;
}

void UpdateProfileFromList(const char *Profile, HWND Wnd, int DlgItem)
{
	HWND ListWnd = NULL;
	LVITEM ListItem;
	char Idx[1024], PluginName[1024];
	int i = 0, ItemCount = 0;

	ListWnd = GetDlgItem(Wnd, DlgItem);
	ItemCount = ListView_GetItemCount(ListWnd);

	CleanIRWProfileSection(Profile, "Plugins");
        
	for(i = 0; i < ItemCount; i++)
	{
		sprintf(Idx, "%d", i);
		ListItem.iSubItem = 0;
		ListItem.iItem = i;
		ListItem.mask = LVIF_TEXT;
		ListItem.cchTextMax = sizeof(PluginName);
		ListItem.pszText = PluginName;
		ListView_GetItem(ListWnd, &ListItem);
		SetIRWProfileString(Profile, "Plugins", Idx, PluginName);
	}

    return;
}

BOOL CheckItemInList(HWND Wnd, int DlgItem, const char *Name)
{
	HWND ListWnd = NULL;
	LVITEM ListItem;
	char ItemName[1024];
	int i = 0, ItemCount = 0;

	ListWnd = GetDlgItem(Wnd, DlgItem);
	ItemCount = ListView_GetItemCount(ListWnd);
        
	for(i = 0; i < ItemCount; i++)
	{
		ListItem.iSubItem = 0;
		ListItem.iItem = i;
		ListItem.mask = LVIF_TEXT;
		ListItem.cchTextMax = sizeof(ItemName);
		ListItem.pszText = ItemName;
		ListView_GetItem(ListWnd, &ListItem);
		if(!strcmp(ItemName, Name))
			return TRUE;
	}

    return FALSE;
}

void SetProfileIni(char *Ini)
{
	ProfileIni = Ini;
	return;
}
