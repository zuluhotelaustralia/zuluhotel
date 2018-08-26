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
* 	Dec 28th, 2004 -- UOCH was released 2 years ago, coded in VB. This is the
*   improved version, written in C
*   Jan 16th, 2005 -- added loginkeys searching to Developer's Dump
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "resource.h"
#include "Main.h"
#include "CrackTools.h"
#include "Crc.h"


HINSTANCE CurInstance = NULL;
HWND CurWnd = NULL;

unsigned char *ClientBuf = NULL;
unsigned int ClientSize = 0;
unsigned int ImageBase = 0;
unsigned int EntryPoint = 0;
unsigned int TimeDateStamp = 0;


int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, INT nCmdShow)
{
	CurInstance = hInstance;

	DialogBox(CurInstance, MAKEINTRESOURCE(DIALOG_MAIN), 0, (DLGPROC)MainDlgProc);

	return 1;
}

BOOL CALLBACK MainDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam)
{
	CurWnd = hDlg;

	switch(wMsg)
	{
		case WM_INITDIALOG:
		{
			char ClientPath[4096];
			HKEY Key = NULL;
			BOOL RegOK = TRUE;
			DWORD Size = sizeof(ClientPath);

			/* set the UOCH icon :) */
			SendMessage(CurWnd, WM_SETICON, ICON_SMALL, (LPARAM)LoadIcon(CurInstance, MAKEINTRESOURCE(NECRO_ICON)));

			/* you only deserve a client withouth the necro logo if you can patch this :P */
			SendMessage(GetDlgItem(CurWnd, CHECK_LOGO), BM_SETCHECK, BST_CHECKED, 0);

			/* see if we can open uo's registry key */
			if(RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\\Origin Worlds Online\\Ultima Online\\1.0", 0, KEY_READ, &Key) != ERROR_SUCCESS)
				if(RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\\Origin Worlds Online\\Ultima Online Third Dawn\\1.0", 0, KEY_READ, &Key) != ERROR_SUCCESS)
					RegOK = FALSE;

			/* if we can, get the client path and set it to the text box */
			if(RegOK)
			{
				RegQueryValueEx(Key, "ExePath", NULL, NULL/*type*/, (LPBYTE)ClientPath, &Size);
				RegCloseKey(Key);

				SetDlgItemText(CurWnd, EDIT_PATH, ClientPath);
			}

			return TRUE;
		}break;
		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
			{
				case BUTTON_PATH:
				{
					OPENFILENAME ofn;
					char ClientPath[4096];

					/* ask for the client's path */
					memset(ClientPath, 0, sizeof(ClientPath));
					memset(&ofn, 0, sizeof(ofn));
					ofn.lStructSize = sizeof(OPENFILENAME);
					ofn.hwndOwner = CurWnd;
					ofn.lpstrFile = ClientPath;
					ofn.nMaxFile = sizeof(ClientPath);
					ofn.lpstrFilter = "Exe\0*.Exe\0All\0*.*\0";
					ofn.nFilterIndex = 1;
					ofn.lpstrTitle = "Choose the UO client to be patched";
					ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
					GetOpenFileName(&ofn);

					/* set it */
					SetDlgItemText(CurWnd, EDIT_PATH, ClientPath);

					return TRUE;
				}break;
				case BUTTON_PATCH:
				{
					IMAGE_DOS_HEADER *idh = NULL;
					IMAGE_FILE_HEADER *ifh = NULL;
					IMAGE_OPTIONAL_HEADER *ioh = NULL;

					HANDLE ClientHandle = NULL;
					DWORD BytesRead = 0;
					char ClientPath[4096], NewClientPath[4096];
					char *Tmp = NULL;
					int i = 0;

					DWORD TickCount = 0;

					memset(NewClientPath, 0, sizeof(NewClientPath));
					GetDlgItemText(CurWnd, EDIT_PATH, ClientPath, sizeof(ClientPath));

					/* copy the client to a buffer */
					ClientHandle = CreateFile(ClientPath, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_FLAG_RANDOM_ACCESS, 0);
					if(ClientHandle == INVALID_HANDLE_VALUE)
					{
						MBOut("Yet another uneducated soul uses me...", "Could not open the file %s. Check if the path is right and the file is not being used", ClientPath);
						EndDialog(CurWnd, 0);
						return TRUE;
					}

					ClientSize = GetFileSize(ClientHandle, NULL);
					ClientBuf = (unsigned char *)malloc(ClientSize);
					if(ClientBuf == NULL)
					{
						MBOut("Shice", "Couldn't allocate enough memory (%d bytes) for the client's image (handle: %X).. weird.", ClientSize, ClientHandle);
						CloseHandle(ClientHandle);
						PostQuitMessage(0);
						return TRUE;
					}

					ReadFile(ClientHandle, ClientBuf, ClientSize, &BytesRead, NULL);
					CloseHandle(ClientHandle);

					/*
					* find the imagebase (virtual address of the module in memory when loaded)
					* and the entrypoint
					*/
					idh = (IMAGE_DOS_HEADER *)ClientBuf;
					ifh = (IMAGE_FILE_HEADER *)(ClientBuf + idh->e_lfanew + sizeof(IMAGE_NT_SIGNATURE));
					ioh = (IMAGE_OPTIONAL_HEADER *)((DWORD)(ifh) + sizeof(IMAGE_FILE_HEADER));

					if((*(DWORD*)(ClientBuf + idh->e_lfanew)) != IMAGE_NT_SIGNATURE)
					{
						MBOut("Ti Durak", "The client is not a valid PE");
						PostQuitMessage(0);
						return TRUE;
					}

					TimeDateStamp = ifh->TimeDateStamp;
					ImageBase = ioh->ImageBase;
					EntryPoint = ioh->AddressOfEntryPoint;

					TickCount = GetTickCount();

					TextBoxCat(CurWnd, TEXT_DUMP, "Patching...\r\n");
					Sleep(25);

					/* developer mode just prints debug info about the client */
					if(SendMessage(GetDlgItem(CurWnd, CHECK_DEV), BM_GETCHECK, 0, 0) == BST_CHECKED)
					{
						DeveloperDump();
						free(ClientBuf);
						ClientBuf = NULL;
						TextBoxCat(CurWnd, TEXT_DUMP, "Time elapsed during analysis: %dms\r\n\r\n", GetTickCount() - TickCount);
						return TRUE;
					}

					/* now check which patches need to be done and execute them */
					if(SendMessage(GetDlgItem(CurWnd, CHECK_EVERMAN), BM_GETCHECK, 0, 0) == BST_CHECKED)
						SetEvermanProtection();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_MULTI), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveMultiProtection();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_NIGHTHACK), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveNightCheck();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_NAMES), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveNameProtection();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_CRYPT), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveEncryption();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_MACRO), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveMacroDelay();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_STAMINA), BM_GETCHECK, 0, 0) == BST_CHECKED)
						RemoveStaminaCheck();

					if(SendMessage(GetDlgItem(CurWnd, CHECK_LOGO), BM_GETCHECK, 0, 0) == BST_CHECKED)
						AddNecroLogo();

					/* create a new client */
					/* remove the exe name from the path */
					if((Tmp = strrchr(ClientPath, '\\')) != NULL)
						*Tmp = '\0';

					/* try to find an unexisting name for the new exe */
					for(i = 0; i < 0xFFFF; i++)
					{
						ClientHandle = CreateFile(NewClientPath, GENERIC_WRITE, FILE_SHARE_WRITE, NULL, CREATE_NEW, FILE_ATTRIBUTE_NORMAL | FILE_FLAG_RANDOM_ACCESS, 0);
						if(ClientHandle != INVALID_HANDLE_VALUE)
							break;

						sprintf(NewClientPath, "%s\\UOCH_n0p3_Client%02d.exe", ClientPath, i);
					}

					WriteFile(ClientHandle, ClientBuf, ClientSize, &BytesRead, NULL);
					CloseHandle(ClientHandle);
					free(ClientBuf);
					ClientBuf = NULL;

					TextBoxCat(CurWnd, TEXT_DUMP, "New UOCH client: %s\r\n", NewClientPath);
					TextBoxCat(CurWnd, TEXT_DUMP, "Time elapsed during patching: %dms\r\n\r\n", GetTickCount() - TickCount);

					return TRUE;
				}break;
			}
		}break;
		case WM_DROPFILES:
		{
			char Path[4096];

			/* a text box will only pass the WM_DROPFILES to the owner if it's set as multiline and no accept files flag */
			DragQueryFile((HDROP)wParam, 0, Path, sizeof(Path));
			DragFinish((HDROP)wParam);

			SetDlgItemText(CurWnd, EDIT_PATH, Path);
			return TRUE;
		}break;
		case WM_NOTIFY:
			{
				MBOut("", "noitify");
			}break;
		case WM_CLOSE:
		{
			EndDialog(CurWnd, 0);
			return TRUE;
		}break;
	}

	return FALSE;
}


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

void DeveloperDump(void)
{
	/* to find the client's version */
	int FirstID = 0, MiddleID = 0, ThirdID = 0;
	unsigned int StringIDPos = 0;
	unsigned int VersionPos = 0, VersionpushPos = 0;
	char VersionSig[10] = "%d.%d.%d%s";
	char ClientVersion[1024];

	/* for the encryption function */
	unsigned char CryptSig[8] = { 0x81, 0xF9, 0x00, 0x00, 0x01, 0x00, 0x0F, 0x8F };
    unsigned int CryptPos = 0, CryptfuncPos = 0;

	/* for the login keys */
	unsigned char KeysSig[14] = { 0x81, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0x4D, 0x89, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0x89 };
	unsigned int KeysPos = 0, LKey1 = 0, LKey2 = 0;

	/* for the packet handler */
	unsigned char PackethandlerSig[12] = { 0x8A, 0x06, 0x83, 0xC0, 0xCC, 0x3D, 0xCC, 0x00, 0x00, 0x00, 0x0F, 0x87 };
	unsigned int PackethandlerPos = 0, PackethandlerfuncPos = 0;

	/* for the packet len dump */
	/* old clients use PACKET ID, LEN as the packet structure */
	unsigned char OldlenSig[8 * 2] = { 0x00, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00 };
	/* newer clients use PACKET ID, SOME ADDRESS, LEN as the packet structure */
	unsigned char NewlenSig[12 * 2] = { 0x00, 0x00, 0x00, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x68, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x05, 0x00, 0x00, 0x00 };
	unsigned int PackettablePos = 0;
	int Skip = 8; /* skip <Skip> bytes when reading the table */
	int i = 0, LastTablePkt = 0, PktLen = 0, PktID = 0, LastPktID = -1;
	BOOL TableFinished = FALSE;

	TextBoxCat(CurWnd, TEXT_DUMP, "\r\n==========================================\r\n");
	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client executable CRC32 %08X\r\n", CalculateCRC32(ClientBuf, ClientSize));
	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client executable size %X\r\n", ClientSize);
	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client time date stamp %X\r\n", TimeDateStamp);
	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client image base %X\r\n", ImageBase);
	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client entry point @%X\r\n", EntryPoint);

	/*
	* find the client version
	* all clients follow the format a.b.cs
	* s is a string, not a char
	*/
	strcpy(ClientVersion, "Unknown");

	VersionPos = FleXSearch("1.25.", ClientBuf, 5, ClientSize, 0, 0x100, 1);
	if(VersionPos == -1)
		VersionPos = FleXSearch("1.26.", ClientBuf, 5, ClientSize, 0, 0x100, 1);
	if(VersionPos == -1)
		VersionPos = FleXSearch("2.0.", ClientBuf, 4, ClientSize, 0, 0x100, 1);

	if(VersionPos != -1)
		strcpy(ClientVersion, (const char*)(ClientBuf + VersionPos));
	else
	{
		VersionpushPos = GetTextRef(ClientBuf, ClientSize, ImageBase, VersionSig, TRUE, 1);

		if((int)VersionpushPos < 0)
			TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Could not a reference to \"%s\" return was %d \r\n", VersionSig, VersionpushPos);
		else
		{
			/* copy the a.b.c */
			memcpy(&FirstID, (void*)(ClientBuf + VersionpushPos - 1), 1);
			memcpy(&MiddleID, (void*)(ClientBuf + VersionpushPos - 3), 1);
			memcpy(&ThirdID, (void*)(ClientBuf + VersionpushPos - 5), 1);
			/* copy the string's offset	*/
			memcpy(&StringIDPos, (void*)(ClientBuf + VersionpushPos - 10), 4);
			StringIDPos -= ImageBase; /* remove the base as from being "in memory" */

			sprintf(ClientVersion, "%d.%d.%d%s", FirstID, MiddleID, ThirdID, ClientBuf + StringIDPos);
		}
	}

	TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Client version is %s\r\n", ClientVersion);


	/* get the login encrypt info */
	CryptPos = FleXSearch(CryptSig, ClientBuf, sizeof(CryptSig), ClientSize, 0, 0x100, 1);

	if(CryptPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Could not find the crypt signature\r\n");
	else
	{
		CryptfuncPos = GetFunctionByRef(ClientBuf, ClientSize, CryptPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Login encryption function @%X\r\n", CryptfuncPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Login encryption signature @%X\r\n", CryptPos);
	}

	/*
	* get the login keys
	* client 4.0.4b
	* 00418FB3  |. 35 BD85F32E    |XOR EAX,2EF385BD
	* 00418FB8  |. 81F1 7F126DA2  |XOR ECX,A26D127F
	* 00418FBE  |. 4D             |DEC EBP
	* 00418FBF  |. 8983 F8000A00  |MOV DWORD PTR DS:[EBX+A00F8],EAX
	* 00418FC5  |. 898B F4000A00  |MOV DWORD PTR DS:[EBX+A00F4],ECX
	*
	* the above is found after the crypt sig
	* find the 2nd login key cause the first one has a different opcode in different client versions
	* the 4 bytes before are lkey1, the 4 bytes after (KeyPos + 1) are lkey2
	*/
	if(CryptPos != -1)
		KeysPos = FleXSearch(KeysSig, ClientBuf, sizeof(KeysSig), ClientSize, CryptPos, 0xCC, 1);

	if(KeysPos == -1 || CryptPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Could not find the loginkeys signature\r\n");
	else
	{
		memcpy(&LKey1, ClientBuf + (KeysPos - 4), sizeof(unsigned int));
		memcpy(&LKey2, ClientBuf + (KeysPos + 2), sizeof(unsigned int));

		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Login key signature @%X\r\n", KeysPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Login key1: %X\r\n", LKey1);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Login key2: %X\r\n", LKey2);
	}

	/* get the packet handler info */
	PackethandlerPos = FleXSearch(PackethandlerSig, ClientBuf, sizeof(PackethandlerSig), ClientSize, 0, 0xCC, 1);

	if(PackethandlerPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Could not find the packet handler signature\r\n");
	else
	{
		PackethandlerfuncPos = GetFunctionByRef(ClientBuf, ClientSize, PackethandlerPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Packet handler function @%X\r\n", PackethandlerfuncPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Packet handler signature @%X\r\n", PackethandlerPos);
	}

	/* get the packet table and dump it (this is a real... suka :P) */
	PackettablePos = FleXSearch(OldlenSig, ClientBuf, sizeof(OldlenSig), ClientSize, 0, 0x100, 1);
	if(PackettablePos == -1)
	{
		PackettablePos = FleXSearch(NewlenSig, ClientBuf, sizeof(NewlenSig), ClientSize, 0, 0xCC, 1);
		Skip = 12;
	}

	if(PackettablePos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Could not find the packet tables\r\n");
	else
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Packet table found @%X\r\n", PackettablePos);
		TextBoxCat(CurWnd, TEXT_DUMP, "static int PktLenTable[] = \r\n{");

		for(i = 0; i < 0x100; i++)
		{
			PktID = i; PktLen = 0;

			/* copy the packet id and len from the image */
			if(!TableFinished)
			{
				memcpy(&PktID, ClientBuf + PackettablePos + (i * Skip), sizeof(int));
				memcpy(&PktLen, ClientBuf + PackettablePos + (i * Skip) + (Skip - 4), sizeof(int));
			}

			/* if the pktid we just read is not in sequence, the table is finished */
			if(PktID != i && !TableFinished)
			{
				TableFinished = TRUE;
				LastTablePkt = i - 1;
				PktID = i; PktLen = 0;
			}

			/* 0x00, 0x10, 0x20, 0x30, 0x40... 0xf0 */
			if(i == 0x00 || i % 0x10 == 0)
				TextBoxCat(CurWnd, TEXT_DUMP, "\r\n\t/* 0x%02x */ ", i);

			/* dump the packet's len */
			TextBoxCat(CurWnd, TEXT_DUMP, "0x%04X", PktLen);
			/* avoid writting the comma at the last packet */
			if(i != 0xff)
				TextBoxCat(CurWnd, TEXT_DUMP, ", ", PktLen);
		}

		TextBoxCat(CurWnd, TEXT_DUMP, "\r\n};\r\n", i);
		TextBoxCat(CurWnd, TEXT_DUMP, "Dev dump: Last packet in client's table %X\r\n", LastTablePkt);
	}

	TextBoxCat(CurWnd, TEXT_DUMP, "==========================================\r\n");

	return;
}

void SetEvermanProtection(void)
{
	/*
	* client 4.0.4b
	* 0044DE18  |. 68 5CEF5800    PUSH client40.0058EF5C ;  ASCII "Permanently delete %s?"
	* 
	* search for PUSH <offset of "Permanently delete %s?">
	* and write a INT3-RETN to the beginning of the function
	* if the start of the function wasnt found, write a INT3 and 50 NOPs
	*/

	unsigned char BreaknRet[2] = { 0xCC, 0xC3 };
	unsigned int DeletePos = 0, FunctionPos = 0;

	DeletePos = GetTextRef(ClientBuf, ClientSize, ImageBase, "Permanently delete %s?", TRUE, 1);
	if((int)DeletePos < 0)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Everman's way: Could not find \"Permanently delete %%s\" return was %d\r\n", DeletePos);
		return;
	}

	/* find the start of the function that contains the push */
    FunctionPos = GetFunctionByRef(ClientBuf, ClientSize, DeletePos);

	/* if we couldn't find it, write 1 INT3 and 50 NOPs*/
	if(FunctionPos == ImageBase - 1)
	{
		memset(ClientBuf + DeletePos, 0xCC, 1);
		memset(ClientBuf + DeletePos + 1, 0x90, 50);
		TextBoxCat(CurWnd, TEXT_DUMP, "Everman's way: Patched using old method %X\r\n", GetTickCount());
		return;
	}

	/* this will still cause a crash, but more effectively ;) */
    memcpy(ClientBuf + FunctionPos, BreaknRet, sizeof(BreaknRet));
	TextBoxCat(CurWnd, TEXT_DUMP, "Everman's way: Patched with bpx and ret @%X\r\n", FunctionPos);

	return;
}

void RemoveMultiProtection(void)
{
	/*
	* client 4.0.4b
	* 004D463D  |. 53             PUSH EBX                                 ; /Title => NULL
	* 004D463E  |. 50             PUSH EAX                                 ; |Class => "Ultima Online"
	* 004D463F  |. FF15 48024F00  CALL NEAR DWORD PTR DS:[<&USER32.FindWin>; \FindWindowA
	* 004D4645  |. 85C0           TEST EAX,EAX
	* 004D4647  |. 74 15          JE SHORT Client.004D465E
	* 004D4649  |. 6A 40          PUSH 40                                  ; /Style = MB_OK|MB_ICONASTERISK|MB_APPLMODAL
	* 004D464B  |. 53             PUSH EBX                                 ; |Title => NULL
	* 004D464C  |. 68 FC215400    PUSH Client.005421FC                     ; |Text = "Another copy of UO is already running!"
	* 004D4651  |. 53             PUSH EBX                                 ; |hOwner => NULL
	* 004D4652  |. FF15 44024F00  CALL NEAR DWORD PTR DS:[<&USER32.Message>; \MessageBoxA
	* 
	* find a reference to "Another copy of UO is already running!"
	* and patch the first TEST EAX,EAX-JE XXX above it to CMP EAX,EAX (85 -> 3B)
    * 
	* and:
	* 004D48B5  |. 3D B7000000    CMP EAX,0B7
	* 004D48BA  |. 75 1B          JNZ SHORT Client.004D48D7
	* 004D48BC  |. 8B0D 60CA5400  MOV ECX,DWORD PTR DS:[54CA60]
	* 004D48C2  |. 51             PUSH ECX                                 ; /hObject => NULL
	* 004D48C3  |. FFD5           CALL NEAR EBP                            ; \CloseHandle
	* 004D48C5  |. 53             PUSH EBX                                 ; /Style
	* 004D48C6  |. 68 A8205400    PUSH Client.005420A8                     ; |Title = "ERROR"
	* 004D48CB  |. 68 3CCC5400    PUSH Client.0054CC3C                     ; |Text = "Another instance of UO is already running."
	* 004D48D0  |. 53             PUSH EBX                                 ; |hOwner
	* 004D48D1  |. FFD6           CALL NEAR ESI                            ; \MessageBoxA
	* 004D48D3  |. 6A 01          PUSH 1                                   ; /ExitCode = 1
	* 004D48D5  |. FFD7           CALL NEAR EDI                            ; \ExitProcess
	* 
	* find a reference to "Another instance of UO is already running."
	* and patch the first JNZ XXX above it to JMP (75 -> EB)
	*/

	unsigned char CmpeaxSig[3] = { 0x85, 0xC0, 0x74 };
	unsigned int AnothercopyPos = 0, CmpeaxPos = 0;
	unsigned char CmpJNZ[6] = { 0x3D, 0xB7, 0x00 ,0x00, 0x00, 0x75 };
	unsigned int AnotherinstPos = 0, CmpJNZPos = 0;

	/* find the CmpEaxSig in a range of 16 bytes before the PUSH */
	AnothercopyPos = GetTextRef(ClientBuf, ClientSize, ImageBase, "Another copy of UO is already running!", TRUE, 1);
	if((int)AnothercopyPos < 0)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Could not find \"Another copy of UO is already running!\" return was %d\r\n", AnothercopyPos);
		return;
	}

    CmpeaxPos = FleXSearch(CmpeaxSig, ClientBuf, sizeof(CmpeaxSig), ClientSize, AnothercopyPos - 0x10, 0xCC, 1);
	if(CmpeaxPos == -1)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Could not find a TEST EAX,EAX-JE %X\r\n", AnothercopyPos);
		return;
	}

	/* find the CMP EAX,B7-JNZ in a range of 32 bytes before the PUSH */
	AnotherinstPos = GetTextRef(ClientBuf, ClientSize, ImageBase, "Another instance of UO is already running.", 1, 1);
	if((int)AnotherinstPos < 0)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Could not find \"Another instance of UO is already running.\" return was %d\r\n", AnotherinstPos);
		return;
	}

    CmpJNZPos = FleXSearch(CmpJNZ, ClientBuf, sizeof(CmpJNZ), ClientSize, AnotherinstPos - 0x20, 0xCC, 1);
	if(CmpJNZPos == -1)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Could not find a CMP EAX,37-JNZ %X\r\n", AnotherinstPos);
		return;
	}

	/* change the TEST (0x80) to CMP (0x3B) */
    ClientBuf[CmpeaxPos] = 0x3B;
	TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Patched with cmp @%X\r\n", CmpeaxPos);

	/* change the JNZ (0x75) to JMP (0xEB) */
	ClientBuf[CmpJNZPos + 5] = 0xEB;
	TextBoxCat(CurWnd, TEXT_DUMP, "Multi: Patched with jmp @%X\r\n", CmpJNZPos + 5);

	return;
}

void RemoveNightCheck(void)
{
	/*
	* clients >= 3.0.4n
	* client 4.0.4b
	* 0041CA97     25 FF000000    AND EAX,0FF
	* 0041CA9C   . 83C4 0C        ADD ESP,0C
	* 0041CA9F   . 3BC8           CMP ECX,EAX
	* 0041CAA1   . 74 2F          JE SHORT Client40.0041CAD2
	* 
	* clients <= 3.0.3a
	* client 2.0.0g
	* 0048FEC1     25 FF000000    AND EAX,0FF
	* 0048FEC6   . 3BC8           CMP ECX,EAX
	* 0048FEC8   . 74 2C          JE SHORT Client.0048FEF6
	* 
	* The client gets the light value from the 0x4f packet (Packet[1] & 0xff)
	* and compares it to the current value (ECX) and changes only if
	* the two values are different (JE)
	* change the AND EAX,FF to AND EAX,0 (25 FF -> 25 00)
	* this will set the value to 0
	*/

	unsigned char LightSig[11] = { 0x25, 0xFF, 0x00, 0x00, 0x00, 0x83, 0xC4, 0x0C, 0x3B, 0xC8, 0x74 };
	unsigned char LightSig2[8] = { 0x25, 0xFF, 0x00, 0x00, 0x00, 0x3B, 0xC8, 0x74 };
	unsigned int LightPos = 0;

	/* search for both sigs */
	LightPos = FleXSearch(LightSig, ClientBuf, sizeof(LightSig), ClientSize, 0, 0x100, 1);
	if(LightPos == -1)
		LightPos = FleXSearch(LightSig2, ClientBuf, sizeof(LightSig2), ClientSize, 0, 0x100, 1);

	if(LightPos == -1)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Night hack: Could not find the light signature\r\n");
		return;
	}

	ClientBuf[LightPos + 1] = 0x00;
	TextBoxCat(CurWnd, TEXT_DUMP, "Night hack: Patched @%08X\r\n", LightPos);

	return;
}

void RemoveNameProtection(void)
{
	/*
	* client 4.0.4b
	* 0041F362   . 83F9 02        CMP ECX,2
	* 0041F365   . 73 26          JNB SHORT Client.0041F38D
	* 0041F367   . 50             PUSH EAX
	* 0041F368   . 50             PUSH EAX
	* 0041F369   . 6A 01          PUSH 1
	* 0041F36B   . 53             PUSH EBX
	* 0041F36C   . 68 D0865000    PUSH Client.005086D0                     ;  ASCII "Your character name is too short."
	* 0041F371   . 8943 6C        MOV DWORD PTR DS:[EBX+6C],EAX
	* 0041F374   . E8 57830300    CALL Client.004576D0
	* 0041F379   . 83C4 14        ADD ESP,14
	* 0041F37C   . 5F             POP EDI
	* 0041F37D   . 5B             POP EBX
	* 0041F37E   . 8B4C24 04      MOV ECX,DWORD PTR SS:[ESP+4]
	* 0041F382   . 64:890D 000000>MOV DWORD PTR FS:[0],ECX
	* 0041F389   . 83C4 10        ADD ESP,10
	* 0041F38C   . C3             RETN
	* 0041F38D   > 52             PUSH EDX
	* 0041F38E   . E8 7DF50500    CALL Client.0047E910
	* 0041F393   . 83C4 04        ADD ESP,4
	* 0041F396   . 85C0           TEST EAX,EAX
	* 0041F398   . 74 2C          JE SHORT Client.0041F3C6
	* 0041F39A   . 6A 00          PUSH 0
	* 0041F39C   . 6A 00          PUSH 0
	* 0041F39E   . 6A 01          PUSH 1
	* 0041F3A0   . 53             PUSH EBX
	* 0041F3A1   . 68 BC865000    PUSH Client.005086BC                     ;  ASCII "Unacceptable name."
	* 
	* There are 2 cool name protections in the client. only the second,
	* located just bellow "Your character name is too short." is used
	* 
	* We could search for the strings, but this method is easier
	* To remove the short names protection:
	* 83 f9 02 73 XX 50 50
	* To remove the cool names protection:
	* 85 c0 74 bellow short names protection
	*/
	unsigned char ShortSig[7] = { 0x83, 0xF9, 0x02, 0x73, 0xCC, 0x50, 0x50 };
	unsigned char CoolSig[3] = { 0x85, 0xC0, 0x74 };
	unsigned int ShortPos = 0, CoolPos = 0;

    ShortPos = FleXSearch(ShortSig, ClientBuf, sizeof(ShortSig), ClientSize, 0, 0xCC, 1);
	CoolPos = FleXSearch(CoolSig, ClientBuf, sizeof(CoolSig), ClientSize, ShortPos, 0x100, 1);

	if(ShortPos == -1 || CoolPos == -1)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Special names: Could not find the special names signatures %X %X\r\n", ShortPos, CoolPos);
		return;
	}

	/* change the JNB (0x73) to JMP (0xEB) */
	ClientBuf[ShortPos + 3] = 0xEB;
	TextBoxCat(CurWnd, TEXT_DUMP, "Special names: Patched with jmp @%X\r\n", ShortPos);

	/* change the TEST (0x80) to CMP (0x3B) */
	ClientBuf[CoolPos] = 0x3B;
	TextBoxCat(CurWnd, TEXT_DUMP, "Special names: Patched with cmp @%X\r\n", CoolPos);

	return;
}

void RemoveEncryption(void)
{
	/* for login encryption */
	unsigned char CryptSig[8] = { 0x81, 0xF9, 0x00, 0x00, 0x01, 0x00, 0x0F, 0x8F };
	unsigned char JNZSig[2] = { 0x0F, 0x85 };
	unsigned char JNESig[2] = { 0x0F, 0x84 };
	unsigned int CryptPos = 0, JNZPos = 0, JEPos = 0;
	

	/* for game encryption */
	unsigned char BFGamecryptSig[5] = { 0x2C, 0x52, 0x00, 0x00, 0x76 }; /* CMP XXX, 522c - JBE */
	unsigned char CmpSig[4] = { 0x3B, 0xC3, 0x0F, 0x84 }; /* CMP EAX,EBX - JE */
	unsigned int BFGamecryptPos = 0, CmpPos = 0;
	/* RTD: make sure the JE 0x10 and JE 0x9X000000 stays like that, if not... I have to find a new way */
	unsigned char TFGamecryptSig[14] = { 0x8B, 0x8B, 0xCC, 0xCC, 0xCC, 0xCC, 0x81, 0xF9, 0x00, 0x01, 0x00, 0x00, 0x74, 0x10 }; /* MOV EBX, XXX - CMP ECX, 0x100 - JE 0x10 */
	unsigned char JELong[7] = { 0x0F, 0x84, 0xCC, 0x00, 0x00, 0x00, 0x55 }; /* JE XX000000 -  */
	unsigned int TFGamecryptPos = 0, GJEPos = 0;

	/* for game decryption */
	unsigned char DecryptSig[8] = { 0x4A, 0x83, 0xCA, 0xF0, 0x42, 0x8A, 0x94, 0x32 };
	unsigned char DectestSig[10] = { 0x85, 0xCC, 0x74, 0xCC, 0x33, 0xCC, 0x85, 0xCC, 0x7E, 0xCC };
	unsigned int DecryptPos = 0, DectestPos = 0;

	/* LOGINENCRYPTION START */

	/*
	* magic x90 encryption signature: 81 f9 00 00 01 00 0f 8f
	* patching : find the first 0f 84 bellow and change it to 0f 85
	* or first 0f 85 and change it to 0f 84
	*/
	CryptPos = FleXSearch(CryptSig, ClientBuf, sizeof(CryptSig), ClientSize, 0, 0x100, 1);

	/* search for the first JNZ or JNE from crypt_addr */
	JNZPos = FleXSearch(JNZSig, ClientBuf, sizeof(JNZSig), ClientSize, CryptPos, 0x100, 1);
	JEPos = FleXSearch(JNESig, ClientBuf, sizeof(JNESig), ClientSize, CryptPos, 0x100, 1);

	/* if this is an OSI UO client, shouldnt happen AT ALL */
	if(CryptPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "LCrypt: could not find the sig... weird\r\n");
	else
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "LCrypt: Crypt sig @%X\r\n", CryptPos);

		if(!JEPos || JEPos > JNZPos)
		{
			ClientBuf[JNZPos + 1] = 0x84;
			TextBoxCat(CurWnd, TEXT_DUMP, "LCrypt: Patched with je @%X\r\n", JNZPos);
		}
		else if(!JNZPos || JNZPos > JEPos)
		{
			ClientBuf[JEPos + 1] = 0x85;
			TextBoxCat(CurWnd, TEXT_DUMP, "LCrypt: Patched with jnz @%X\r\n", JEPos);
		}
	}

	/* LOGINENCRYPTION END */

	/* GAMEENCRYPTION START */

	/*
	* BLOWFISH
	* this is simply "lost" inside the "send" function, just above it
	* it looks like:
	* if(GameMode != LOGIN_SOCKET) ;game socket
	* {
	*     BlowfishEncrypt() ;starts with a (while > 0x522c)
	*     ;a whole bunch of other stuff
	*     TwofishEncrypt() ;if present
	*     send()
	* }
	* else ;login socket
	*     send() ;yay, a send that bypasses all the encryption crap
	*
	* find the beginning of the loop while(Obj->stream_pos + len > CRYPT_GAMETABLE_TRIGGER)
	* CRYPT_GAMETABLE_TRIGGER is 0x522c
	* find the first CMP EAX,EBX-JE above and patch it to CMP EAX,EAX
	*/
	BFGamecryptPos = FleXSearch(BFGamecryptSig, ClientBuf, sizeof(BFGamecryptSig), ClientSize, 0, 0x100, 1);
	CmpPos = FleXSearch(CmpSig, ClientBuf, sizeof(CmpSig), ClientSize, BFGamecryptPos - 0x20, 0x100, 1);

	if(BFGamecryptPos == -1 || CmpPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Could not find blowfish encryption %X %X\r\n", BFGamecryptPos, CmpPos);
	else
	{
		ClientBuf[CmpPos + 1] = 0xC0;
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Found blowfish @%X\r\n", BFGamecryptPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Patched with cmp @%X\r\n", CmpPos);
	}

	
	/* 
	* TWOFISH
	* patch the encryption function to skip encryption
	* the function is always called before send
	*
	* find the beginning of the loop and the first JE above it
	* patch the JE (0x84) to JNE (0x85)
	*/

	TFGamecryptPos = FleXSearch(TFGamecryptSig, ClientBuf, sizeof(TFGamecryptSig), ClientSize, 0, 0xCC, 1);
	GJEPos = FleXSearch(JELong, ClientBuf, sizeof(JEPos), ClientSize, TFGamecryptPos - 0x20, 0xCC, 1);

    if(TFGamecryptPos == -1 || GJEPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Could not find twofish encryption %X %X\r\n", TFGamecryptPos, GJEPos);
	else
	{
		ClientBuf[GJEPos + 1] = 0x85;
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Found twofish @%X\r\n", TFGamecryptPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Patched with jnz @%X\r\n", GJEPos);
	}
	

	/* GAMEENCRYPTION END */

	/* GAMEDECRYPTION START (now for the easy part) */

	/*
	* search for 4A 83 CA F0 42 8A 94 32
	* and above it, 85 xx 74 xx 33 xx 85 xx 7E xx
	* the first TEST (85 xx) must be cracked to CMP EAX, EAX (3B C0)
	* if I want to do it like LB does in UORice, I'd crack
	* the first CMP xx JMP xx (85 xx 74 xx) to CMP EAX, EAX (3B C0)
	* which is bellow the one I crack
	*/

	/* find 4A 83 CA F0 42 8A 94 32 */
	/* find the TEST above it (not the one right above that is) */
	DecryptPos = FleXSearch(DecryptSig, ClientBuf, sizeof(DecryptSig), ClientSize, 0, 0x100, 1);
	DectestPos = FleXSearch(DectestSig, ClientBuf, sizeof(DectestSig), ClientSize, DecryptPos - 0x100, 0xCC, 1);

	if(DecryptPos == -1 || DectestPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "GDecrypt: Could not find MD5 decryption %X %X \r\n", DecryptPos, DecryptPos, DectestPos);
	else
	{
		DectestPos = FleXSearch(DectestSig, ClientBuf, sizeof(DectestSig), ClientSize, DecryptPos - 0x100, 0xCC, 1);
		ClientBuf[DectestPos] = 0x3B;
		TextBoxCat(CurWnd, TEXT_DUMP, "GCrypt: Found MD5 @%X\r\n", DecryptPos);
		TextBoxCat(CurWnd, TEXT_DUMP, "GDecrypt: Sig @%X patched with cmp @%X\r\n", DecryptPos, DectestPos);
	}

	/* GAMEDECRYPTION END */

	return;
}

void RemoveMacroDelay(void)
{
	/*
	* 4.0.4b
	* 004DF3BD  |. 3BC3           CMP EAX,EBX
	* 004DF3BF  |. 57             PUSH EDI
	* 004DF3C0  |. 74 18          JE SHORT client40.004DF3DA
	* 004DF3C2  |. 68 6C635900    PUSH client40.0059636C                   ;  ASCII "You must wait to perform another action."
	*
	* find the reference to "You must wait to perform another action."
	* and patch the JE (0x74) to JMP (0xEB)
	*/

	unsigned int WaitRef = 0;

	WaitRef = GetTextRef(ClientBuf, ClientSize, ImageBase, "You must wait to perform another action.", TRUE, 1);

	if((int)WaitRef < 0)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Macro delay: Could not find \"You must wait to perform another action.\" return was %d\r\n", WaitRef);
		return;
	}

	if(ClientBuf[WaitRef - 2] != 0x74)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Macro delay: The opcode (%c) before the ref @%X is not a JE!\r\n", ClientBuf[WaitRef - 2], WaitRef);
		return;
	}

	ClientBuf[WaitRef - 2] = 0xEB;
	TextBoxCat(CurWnd, TEXT_DUMP, "Macro delay: Patched with jmp @%X\r\n", WaitRef - 2);
	return;
}

void RemoveStaminaCheck(void)
{
	/*
	* client 4.0.4b
	* 004EA7A6  |. 85C0           |TEST EAX,EAX
	* 004EA7A8  |. 74 09          |JE SHORT client40.004EA7B3
	* 004EA7AA  |. 7E 36          |JLE SHORT client40.004EA7E2
	* 004EA7AC  |. 83F8 02        |CMP EAX,2
	* 004EA7AF  |. 7F 31          |JG SHORT client40.004EA7E2
	* 004EA7B1  |. EB 5D          |JMP SHORT client40.004EA810
	* 004EA7B3  |> 8B91 FC010000  |MOV EDX,DWORD PTR DS:[ECX+1FC]
	* 004EA7B9  |. 8B81 00020000  |MOV EAX,DWORD PTR DS:[ECX+200]
	* 004EA7BF  |. 3BD0           |CMP EDX,EAX
	* 004EA7C1  |. 74 4D          |JE SHORT client40.004EA810
	*
	* if I remember it right, at the end EDX has the current stamina, EAX the full
	* it checks if they are equal and if they are it allows you to walk
	* through other players
	* patch the CMP EDX,EAX (3B D0) to CMP EAX,EAX (3B C0)
	* hooligan mode :P
	* thanks to Bloodbob (bloob) for this one
	*/

	unsigned char StaminacheckSig[11] = { 0x74, 0xCC, 0x7E, 0xCC, 0x83, 0xF8, 0x02, 0x7F, 0xCC, 0xEB, 0xCC };
	unsigned char CmpSig[3] = { 0x3B, 0xCC, 0x74 };
	unsigned int StaminacheckPos = 0, CmpPos = 0;

	StaminacheckPos = FleXSearch(StaminacheckSig, ClientBuf, sizeof(StaminacheckSig), ClientSize, 0, 0xCC, 1);
	CmpPos = FleXSearch(CmpSig, ClientBuf, sizeof(CmpSig), ClientSize, StaminacheckPos, 0xCC, 1);

    if(StaminacheckPos == -1 || CmpPos == -1)
	{
		TextBoxCat(CurWnd, TEXT_DUMP, "Hooligan: Could not find the stamina check %X %X\r\n", StaminacheckPos, CmpPos);
		return;
	}

	ClientBuf[CmpPos + 1] = 0xC0;
	TextBoxCat(CurWnd, TEXT_DUMP, "Hooligan: patched with cmp @%X\r\n", CmpPos);


	return;
}

void AddNecroLogo(void)
{
	/*
	* find "UO Version %s" and write "Necr0Potenc3" on top of it ;)
	*/

	unsigned int VersionPos = 0;

	VersionPos = FleXSearch("UO Version %s", ClientBuf, (DWORD)strlen("UO Version %s"), ClientSize, 0, 0xCC, 1); 

	if(VersionPos == -1)
		TextBoxCat(CurWnd, TEXT_DUMP, "Logo: Couldn't find \"UO Version %%s\"... the logo can't be added :(\r\n");
	else
		strcpy(ClientBuf + VersionPos, "Necr0Potenc3");

	return;
}
