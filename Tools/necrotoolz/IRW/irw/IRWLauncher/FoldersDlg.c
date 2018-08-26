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
* 	Oct 4th, 2004  -- getting IRW ready for release, added this dlg
*   to choose the IRW "install" folder and the client to be used
* 
\******************************************************************************/


#include <windows.h>
#include <shlobj.h>
#include "Main.h"
#include "FoldersDlg.h"
#include "Registry.h"
#include "resource.h"

static HWND CurWnd = NULL;

BOOL CALLBACK FoldersDlgProc(HWND hDlg, UINT wMsg, WPARAM wParam, LPARAM lParam)
{
	char Tmp[4096];
	CurWnd = hDlg;

	switch(wMsg)
	{
		case WM_INITDIALOG:
		{
			/* set the IRW icon */
			SendMessage(CurWnd, WM_SETICON, ICON_SMALL, (LPARAM)LoadIcon(GetCurrentInstance(), MAKEINTRESOURCE(IRW_ICON)));

			/* get the current IRW folder and set it to the edit box */
			SetDlgItemText(CurWnd, TEXT_IRWFOLDER, GetIRWPath());

			return FALSE;
		}break;
		case WM_COMMAND:
		{
			switch(LOWORD(wParam))
			{
				case BUTTON_IRWFOLDER:
				{
					BROWSEINFO bi;
					ITEMIDLIST *idl;
					char NewIRWPath[4096];

					/* ask for irw's new path */
					bi.hwndOwner = CurWnd;
					bi.pidlRoot = NULL;
					bi.pszDisplayName = NewIRWPath;
					bi.lpszTitle = "Pick a folder for IRW";
					bi.ulFlags = BIF_RETURNONLYFSDIRS;
					bi.lpfn = NULL;
					bi.lParam = 0;
					idl = SHBrowseForFolder(&bi);

					if(idl == NULL)
						return FALSE;

					SHGetPathFromIDListA(idl, NewIRWPath);
					CoTaskMemFree(idl);

					if(NewIRWPath[0] == '\0')
						return FALSE;

					SetDlgItemText(CurWnd, TEXT_IRWFOLDER, NewIRWPath);

					return FALSE;
				}break;
				case BUTTON_OK:
				{
					/* check the values */
					GetDlgItemText(CurWnd, TEXT_IRWFOLDER, Tmp, sizeof(Tmp));
					if(!IRWFilesPresent(Tmp))
						return FALSE;

					GetDlgItemText(CurWnd, TEXT_IRWFOLDER, Tmp, sizeof(Tmp));
					SetRegistryString(HKEY_CURRENT_USER, "Software\\IRW", "IRWPath", NULL, Tmp);

					EndDialog(CurWnd, 0);
					return TRUE;
				}break;
				case BUTTON_CANCEL:
				{
					EndDialog(CurWnd, 0);
					return TRUE;
				}break;
			}
		}break;
		case WM_CLOSE:
		{
			EndDialog(CurWnd, 0);
			return TRUE;
		}break;
	}

	return FALSE;
}
