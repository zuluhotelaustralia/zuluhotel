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


#ifndef _LAYERSDLG_H_INCLUDED
#define _LAYERSDLG_H_INCLUDED

LRESULT CALLBACK PlayersDlgProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

void AddColumn(HWND List, int Index, int Width, char *Name);
int AddColumnItem(HWND List, char *Name);
int FindItemInList(HWND List, int ColIndex, char *Text);
void CheckPlayerUpdates(int Type, unsigned int Serial, int Idx, unsigned char *Packet, int Len);

#endif
