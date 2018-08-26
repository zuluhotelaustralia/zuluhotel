////////////////////////////////////////////////////////////////////////////////
//
// extdll.h
//
// Copyright (C) 2001 mamaich
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////
//
//  Declarations for classes in hotkeyhook.cpp
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _EXTDLL_H_
#define _EXTDLL_H_

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "script\mycsubs.h"


void InitExternalDll(HWND);
void UnloadExternalDll();
bool HandleCommandInDll(const char *cmd);

typedef void __cdecl CharFunc(const char*);
typedef int __cdecl CharFuncInt(const char*);
typedef int __cdecl Char2FuncInt(const char*,const char*);
typedef void __cdecl AddClassesFun(ParserObject* , const struct LibraryFunctions *);

struct DllInterface // This struct should be equal in both DLLs
{
    int Size;       // do version check

    CharFunc *DoCommand;
    CharFunc *ClientPrint;

    CharFuncInt  *CountType; 
    Char2FuncInt *CountTypeColor;
    AddClassesFun *AddClasses;

    HWND Window;

#ifdef __GNUC__
    int *Params[29];    
#else   // | These should be equal in size
    int *Life, *STR, *Mana, *INT, *Stamina, *DEX, 
        *Armor, *Weight, *Gold,
        *BM, *BP, *GA, *GS, *MR, *NS, *SA, *SS, 
        *VA, *EN, *WH, *FD, *BR,
        *H, *C, *M, *L, *B,
        *AR, *BT;
#endif
};

#endif
