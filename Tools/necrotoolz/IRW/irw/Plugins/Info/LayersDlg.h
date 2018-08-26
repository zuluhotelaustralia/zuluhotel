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

void TextBoxCat(HWND Dlg, int DlgItem, const char *Text, ...);
void ListLayers(int Layer);
LRESULT CALLBACK LayersDlgProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

#endif
