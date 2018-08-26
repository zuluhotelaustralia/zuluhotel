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
#include "Registry.h"
#include "ProfileDlg.h"
#include "FoldersDlg.h"
#include "INIProfile.h"
#include "resource.h"

static HANDLE CurInstance = NULL;
static HWND CurWnd = NULL;


HANDLE GetCurrentInstance(void){ return CurInstance; }

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, INT nCmdShow)
{
	DWORD Disp = 0, Size = 0;
	char Choice[4096], CurPath[4096], ClientPath[4096];

	memset(CurPath, 0, sizeof(CurPath));
	memset(ClientPath, 0, sizeof(CurPath));
	
	/* if we had to create the registry key, irw isn't "installed" */
	GetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", NULL, &Disp, NULL, NULL);
	if(Disp == REG_CREATED_NEW_KEY)
	{
		/* get the current path. launcher.exe + irw.dll +  uoencryption.dll should be here */
		GetCurrentDirectory(4096, CurPath);
		if(!IRWFilesPresent(CurPath))
		{
			DeleteKey(HKEY_CURRENT_USER, "Software\\IRW");
			return FALSE;
		}

		sprintf(Choice, "IRW will be installed to: %s. Do you wish to continue?", CurPath);
		if(MessageBox(0, Choice, "Installing IRW", MB_YESNO | MB_ICONINFORMATION) != IDYES)
		{
			DeleteKey(HKEY_CURRENT_USER, "Software\\IRW");
			return FALSE;
		}

		/* set the values to the registry */
		SetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, CurPath);
	}

	/* pass IRW's install path so the profiles folder can be set */
	SetProfilesPath(GetIRWPath());

	/* init the dialog */
	CurInstance = hInstance;
	InitCommonControls();
	DialogBox(CurInstance, MAKEINTRESOURCE(DIALOG_MAIN), 0, (DLGPROC)MainDlgProc);

	return TRUE;
}

BOOL CALLBACK MainDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam)
{
	char FilePath[MAX_PATH];
	int Item = 0;

    CurWnd = hDlg;

	switch(wMsg)
	{
		case WM_INITDIALOG:
		{
			/* set the IRW icon */
			SendMessage(CurWnd, WM_SETICON, ICON_SMALL, (LPARAM)LoadIcon(CurInstance, MAKEINTRESOURCE(IRW_ICON)));

			/* add all the files from the \\IRWInstallDir\\Profiles folder to the list */
			UpdateList(CurWnd, LIST_PROFILES, GetProfilesPath(), "*.ini");
		}break;
		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
			{
				case BUTTON_LAUNCH:
				{
					char SelProfilePath[4096];
					char ClientPath[4096], ClientFolderPath[4096];
					char IRWDll[4096]; /* IRWInstallDir + "\\IRW.dll" */
					char UOCryptDll[4096]; /* IRWInstallDir + "\\UOEncryption.dll" */
					char **LoadInfo = NULL;
					char *Tmp = NULL;
					DWORD Size = 0;
					STARTUPINFO si;
					PROCESS_INFORMATION pi;

					if(!CheckCurSel()) return FALSE;

					memset(&si, 0, sizeof(si));
					memset(&pi, 0, sizeof(pi));
					si.cb = sizeof(si);
					
					/* first of all, check if IRW files are present in IRW's folder */
					if(!IRWFilesPresent(GetIRWPath())) return FALSE;

					/* write the current profile's path to the registry so IRW can use it */
					Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PROFILES), -1, LVNI_SELECTED);
					ListView_GetItemText(GetDlgItem(CurWnd, LIST_PROFILES), Item, 0, SelProfilePath, sizeof(SelProfilePath));
					SetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "UseProfile", NULL, SelProfilePath);

					/* get irw.dll path */
					strcpy(IRWDll, GetIRWPath());
					strcat(IRWDll, "\\IRW.dll");

					/* get UOEncryption.dll path */
					strcpy(UOCryptDll, GetIRWPath());
					strcat(UOCryptDll, "\\UOEncryption.dll");

					/* alloc the 4 strings pointer ourselves, otherwise the compiler won't build the pointer2pointer table */
					LoadInfo = (char**)malloc(4*sizeof(char**));
					LoadInfo[0] = malloc(strlen(UOCryptDll) + 1);
					strcpy(LoadInfo[0], UOCryptDll);
					LoadInfo[1] = NULL;
					LoadInfo[2] = malloc(strlen(IRWDll) + 1);
					strcpy(LoadInfo[2], IRWDll);
					LoadInfo[3] = malloc(7 + 1);
					strcpy(LoadInfo[3], "Install");

					/* get the client path from the profile */
					GetIRWProfileString(SelProfilePath, "IRW", "ClientPath", ClientPath, sizeof(ClientPath));
					GetIRWProfileString(SelProfilePath, "IRW", "ClientPath", ClientFolderPath, sizeof(ClientFolderPath));
					if((Tmp = strrchr(ClientFolderPath, '\\')) != NULL)
						*Tmp = '\0';

					/* launch the client using IRW's folder as base folder */
					if(!CreateProcess(ClientPath, NULL, NULL, NULL, FALSE, CREATE_SUSPENDED | CREATE_NEW_CONSOLE, NULL, ClientFolderPath, &si, &pi))
					{
						MBOut("ERROR", "Could not launch: %s", ClientPath);
						EndDialog(CurWnd, 0);
						return FALSE;
					}

					/*
					* load the encryption library first, for IRW
					* then inject IRW on the client process and we're done
					*/
					LoadLibraryOnProcess(pi.hProcess, pi.hThread, LoadInfo, 2);
					ResumeThread(pi.hThread);
					EndDialog(CurWnd, 0);

					return FALSE;
				}break;
				case BUTTON_NEW:
				{
					char ProfilesFolder[4096];

					/* create the IRWPath\\Profiles directory. if it exists this will have no effect */
					strcpy(ProfilesFolder, GetIRWPath());
					strcat(ProfilesFolder, "\\Profiles");
					CreateDirectory(ProfilesFolder, NULL);

					SetProfileIni(NULL);
					DialogBox(CurInstance, MAKEINTRESOURCE(DIALOG_PROFILE), CurWnd, (DLGPROC)ProfileDlgProc);

					/* update the list */
					UpdateList(CurWnd, LIST_PROFILES, GetProfilesPath(), "*.ini");

					return FALSE;
				}break;
				case BUTTON_EDIT:
				{
					if(!CheckCurSel()) return FALSE;

					/* get the selected profile name */
                    Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PROFILES), -1, LVNI_SELECTED);
					ListView_GetItemText(GetDlgItem(CurWnd, LIST_PROFILES), Item, 0, FilePath, sizeof(FilePath));
					SetProfileIni(FilePath);

					/* launch the profile dialog */
					DialogBox(CurInstance, MAKEINTRESOURCE(DIALOG_PROFILE), CurWnd, (DLGPROC)ProfileDlgProc);

					/* update the profile list */
					UpdateList(CurWnd, LIST_PROFILES, GetProfilesPath(), "*.ini");

					return FALSE;
				}break;
				case BUTTON_DELETE:
				{
					if(!CheckCurSel()) return FALSE;

					/* remove the selected file */
					strcpy(FilePath, GetProfilesPath());
					strcat(FilePath, "\\");
					Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PROFILES), -1, LVNI_SELECTED);
					ListView_GetItemText(GetDlgItem(CurWnd, LIST_PROFILES), Item, 0, FilePath + strlen(FilePath), (int)(sizeof(FilePath) - strlen(FilePath)));
					remove(FilePath);

					/* update the list */
					UpdateList(CurWnd, LIST_PROFILES, GetProfilesPath(), "*.ini");

					return FALSE;
				}break;
				case BUTTON_CHANGEPATHS:
				{
					DialogBox(CurInstance, MAKEINTRESOURCE(DIALOG_FOLDERS), CurWnd, (DLGPROC)FoldersDlgProc);

					/* we changed the path, so we gotta change the profiles path as well */
					SetProfilesPath(GetIRWPath());
					UpdateList(CurWnd, LIST_PROFILES, GetProfilesPath(), "*.ini");

					return FALSE;
				}break;
				case BUTTON_EXIT:
				{
					EndDialog(CurWnd, 0);
					ExitProcess(0);
					return FALSE;
				}break;
			}
		}break;
		case WM_NOTIFY:
		{
			/* RTD: dont mind checking if its a listview msg or not, too much trouble ;) */
			char Text[1024], IniOption[1024], IniName[MAX_PATH];

			Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PROFILES), -1, LVNI_SELECTED);
			ListView_GetItemText(GetDlgItem(CurWnd, LIST_PROFILES), Item, 0, IniName, sizeof(IniName));

			if(Item == -1)
				return FALSE;

			strcpy(Text, "Client: ");
			GetIRWProfileString(IniName, "IRW", "ClientPath", IniOption, sizeof(IniOption));
			strcat(Text, IniOption);
			strcat(Text, "\r\n");

			strcat(Text, "Use IRW: ");
			GetIRWProfileString(IniName, "IRW", "UseIRW", IniOption, sizeof(IniOption));
			strcat(Text, IniOption);
			strcat(Text, "\r\n");

			/* if the user is using IRW */
			if(!strcmp(IniOption, "yes"))
			{
				strcat(Text, "Use encryption: ");
				GetIRWProfileString(IniName, "IRW", "CryptEmulation", IniOption, sizeof(IniOption));
				strcat(Text, IniOption);
				strcat(Text, "\r\n");

				strcat(Text, "Shard address: ");
				GetIRWProfileString(IniName, "IRW", "ShardAddress", IniOption, sizeof(IniOption));
				strcat(Text, IniOption);
				strcat(Text, ":");
				GetIRWProfileString(IniName, "IRW", "ShardPort", IniOption, sizeof(IniOption));
				strcat(Text, IniOption);
				strcat(Text, "\r\n");
			}

			SetDlgItemText(CurWnd, TEXT_INFO, Text);

			return FALSE;
		}break;
		case WM_CLOSE:
		{
			EndDialog(CurWnd, 0);
			return FALSE;
		}break;
	}

	return FALSE;
}

HANDLE GetCurInstance(void)
{
	return CurInstance;
}

void UpdateList(HWND Wnd, int DlgItem, const char *SearchPath, const char *FType)
{
	WIN32_FIND_DATA FDInfo;
	HANDLE FDHandle = NULL;
	HWND ListWnd = NULL;
	LVITEM ListItem;
	char Path[MAX_PATH];
	int i = 0, b = 1;

	strcpy(Path, SearchPath);
	if(Path[strlen(Path) - 1] != '\\')
		strcat(Path, "\\");
	if(FType != NULL)
		strcat(Path, FType);

	ListWnd = GetDlgItem(Wnd, DlgItem);
	ListView_DeleteAllItems(ListWnd);

	FDHandle = FindFirstFile(Path, &FDInfo);

	while(b && FDHandle != NULL)
	{
		if(FType == NULL || (FType != NULL && strlen(FDInfo.cFileName) > 4 && strcmp(FDInfo.cFileName + strlen(FDInfo.cFileName) - 4, FType)))
		{
			memset(&ListItem, 0, sizeof(LVITEM));
			ListItem.mask = LVIF_TEXT | LVIF_PARAM;
			ListItem.iItem = i;
			ListItem.pszText = FDInfo.cFileName;
			ListItem.lParam = i;
			ListView_InsertItem(ListWnd, &ListItem);
			i++;
		}
		b = FindNextFile(FDHandle, &FDInfo);
	}

	FindClose(FDHandle);
    return;
}

/* checks the item selected in the list box to make sure its a .ini */
BOOL CheckCurSel(void)
{
	char FilePath[MAX_PATH];
	int Item = 0;

	Item = ListView_GetNextItem(GetDlgItem(CurWnd, LIST_PROFILES), -1, LVNI_SELECTED);
	ListView_GetItemText(GetDlgItem(CurWnd, LIST_PROFILES), Item, 0, FilePath, sizeof(FilePath));

	if(Item == -1 || FilePath[0] == '\0')
	{
		MBOut("Duh..mbass", "No .ini file selected");
		return FALSE;
	}

	return TRUE;
}

BOOL IRWFilesPresent(char *Path)
{
	char IRWPath[4096], CryptPath[4096];
	HANDLE IRWLib = NULL, CryptLib = NULL;

	if(Path != NULL)
	{
		strcpy(IRWPath, Path);
		strcat(IRWPath, "\\IRW.dll");
	}
	else
	{
		memset(IRWPath, 0, sizeof(IRWPath));
		strcat(IRWPath, "IRW.dll");
	}

	if(Path != NULL)
	{
		strcpy(CryptPath, Path);
		strcat(CryptPath, "\\UOEncryption.dll");
	}
	else
	{
		memset(CryptPath, 0, sizeof(CryptPath));
		strcat(CryptPath, "UOEncryption.dll");
	}


	/* IRW depends on the crypt library, so we must have it on the memory first */
	CryptLib = LoadLibrary(CryptPath);
	IRWLib = LoadLibrary(IRWPath);
	FreeLibrary(IRWLib);
	FreeLibrary(CryptLib);

	if(IRWLib == NULL)
	{
		MBOut("ERROR", "Could not find IRW.dll in the folder %s", Path);
		return FALSE;
	}

	if(CryptLib == NULL)
	{
		MBOut("ERROR", "Could not find UOEncryption.dll in the folder %s", Path);
		return FALSE;
	}

	return TRUE;
}

void MBOut(char *title, char *msg, ...)
{
	char final[4096];
	va_list list;
	va_start(list, msg);
	vsprintf((char*)final, msg, list);
	va_end(list);

	MessageBox(NULL, final, title, 0);
	
	return;
}

int LoadLibraryOnProcess(HANDLE hProcess, HANDLE hThread, char** LibData, int LibCount)
{
	/*
	* Client is started in a suspended state, so I patch it in memory
	* in such a way that it loads LoaderDLL.dll when resumed.
	* On NT4+ I allocate needed memory with VirtualAllocEx, and
	* on 9x I use free space in PE header, because VirtualAllocEx is
	* not implemented there. I can't use PE header space under all OSes
	* because XP does not allow writing there even after calling VirtualProtectEx.
	*/

	unsigned char SaveStack[2] =
	{
		0x9c,                           /* 00 pushfd */
		0x60                            /* 01 pushad */
	};

	unsigned char LoadLib[10] =
	{
		0x68, 0x00, 0x00, 0x00, 0x00,   /* 00 push "dll path" */
		0xe8, 0x00, 0x00, 0x00, 0x00    /* 05 call LoadLibraryA */
	};

	unsigned char CallFunc[13] =
	{
		0x68, 0x90, 0x90, 0x90, 0x90,   /* 00 push "function" */
		0x50,                           /* 05 push eax */
		0xe8, 0x90, 0x90, 0x90, 0x90,   /* 06 call GetProcAddress */
		0xff, 0xd0                      /* 11 call eax */
	};

	unsigned char PopStack[7] =
	{
		0x61,                           /* 00 popad */
		0x9d,                           /* 01 popfd */
		0xe9, 0x00, 0x00, 0x00, 0x00    /* 02 jmp oldeip */
	};

	CONTEXT ctx;
	HINSTANCE Lib = NULL;
	int AllOk = TRUE, i = 0;
	unsigned int Tmp = 0, PatchBase = 0, AsmSize = 0, TextSize = 0, AsmPos = 0, TextPos = 0;
	unsigned char *Patch = NULL;
	
	/* get the thread's info */
	AllOk = TRUE;
	ctx.ContextFlags = CONTEXT_FULL;
	AllOk &= GetThreadContext(hThread, &ctx);


	/* determine the patch size based on the opcodes and library text (names and functions) */
	AsmSize += sizeof(SaveStack);
	for(i = 0; i < LibCount*2; i+=2)
	{
		/* the even indices are the library name */
		/* the uneven indices are the function name */

		AsmSize += sizeof(LoadLib);
		TextSize += (unsigned int)(strlen(LibData[i]) & 0xFFFFFFFF) + 1;

		if(LibData[i + 1] != NULL)
		{
			AsmSize += sizeof(CallFunc);
			TextSize += (unsigned int)(strlen(LibData[i+1]) & 0xFFFFFFFF) + 1;
		}
	}
	AsmSize += sizeof(PopStack);

	/* get a patchbase. 0x400400 should always be alright */
	PatchBase = (unsigned int)(UINT_PTR)VirtualAllocEx(hProcess, 0, AsmSize + TextSize, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	if(!PatchBase) PatchBase = 0x400400;

	Patch = malloc(AsmSize + TextSize);
	if(Patch == NULL)
		return FALSE;

	/* build the patch data */
	memset(Patch, 0, AsmSize + TextSize);
	memcpy(Patch, SaveStack, sizeof(SaveStack));
	AsmPos += sizeof(SaveStack);
	for(i = 0; i < LibCount*2; i+=2)
	{
		memcpy(Patch + AsmPos, LoadLib, sizeof(LoadLib));

		/* "push" lib name, and lib name */
		Tmp = PatchBase + AsmSize + TextPos;
		memcpy(Patch + AsmPos + 1, &Tmp, sizeof(Tmp));
		memcpy(Patch + AsmSize + TextPos, LibData[i], strlen(LibData[i]));
		/* the loadlibrary call */
		Tmp = (unsigned int)(UINT_PTR)GetProcAddress(GetModuleHandle("kernel32"), "LoadLibraryA") - (PatchBase + AsmPos + 5) - 5;
		memcpy(Patch + AsmPos + 6, &Tmp, sizeof(Tmp));

		AsmPos += sizeof(LoadLib);
		TextPos += (unsigned int)(strlen(LibData[i]) & 0xFFFFFFFF) + 1;

		/* build GetProcAddress-Call */
		if(LibData[i + 1] != NULL)
		{
			memcpy(Patch + AsmPos, CallFunc, sizeof(CallFunc));

			/* "push" function name, and the function name */
			Tmp = PatchBase + AsmSize + TextPos;
			memcpy(Patch + AsmPos + 1, &Tmp, sizeof(Tmp));
			memcpy(Patch + AsmSize + TextPos, LibData[i+1], strlen(LibData[i+1]));
			/* the getprocaddress call */
			Tmp = (unsigned int)(UINT_PTR)GetProcAddress(GetModuleHandle("kernel32"), "GetProcAddress") - (PatchBase + AsmPos + 6) - 5;
			memcpy(Patch + AsmPos + 7, &Tmp, sizeof(Tmp));

			AsmPos += sizeof(CallFunc);
			TextPos += (unsigned int)(strlen(LibData[i+1]) & 0xFFFFFFFF) + 1;
		}
	}
	memcpy(Patch + AsmPos, PopStack, sizeof(PopStack));
	Tmp = ctx.Eip - (PatchBase + AsmPos + 2) - 5;
	memcpy(Patch + AsmPos + 3, &Tmp, sizeof(Tmp));

	/* write the patch */
	AllOk &= WriteProcessMemory(hProcess, (void *)(UINT_PTR)PatchBase, Patch, AsmSize + TextSize, 0);

	/* set the current eip to our patch */
	ctx.Eip = PatchBase;
	AllOk &= SetThreadContext(hThread, &ctx);

	free(Patch);
	return AllOk;
}
