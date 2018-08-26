/******************************************************************************\
* 
* 
*  Copyright (C) 2002 mamaich
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
* 	n0p3:
* 	Somewhere in 2004  -- got the code from UOInjection, did some small changes
* 	nice usage of Luevelsmeyer's code.
*   Jan 04, 2005 -- the image base must now be declared by the caller
* 
\******************************************************************************/


#ifndef _IMPORTHOOK_H_INCLUDED
#define _IMPORTHOOK_H_INCLUDED

#define BASE_IMGADDR (void*)GetModuleHandle(NULL)

void* HookImportedFunction(void *ImageBase, const char *Dll, const char *FuncName, int Ordinal, void *Function);

#endif
