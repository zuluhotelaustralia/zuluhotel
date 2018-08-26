////////////////////////////////////////////////////////////////////////////////
//
// extdll.cpp
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


#include <stdlib.h>
#include <time.h>
#include <string.h>
#include "common.h"
#include "hooks.h"
#include "world.h"
#include "equipment.h"
#include "menus.h"
#include "vendor.h"
#include "target.h"
#include "spells.h"
#include "skills.h"
#include "runebook.h"
#include "hotkeyhook.h"

#include "gui.h"
#include "injection.h"
#include "extdll.h"

extern Injection * g_injection;
extern int g_Life, g_STR, g_Mana, g_INT, g_Stamina, g_DEX, 
    g_Armor, g_Weight, g_Gold,
    g_BM, g_BP, g_GA, g_GS, g_MR, g_NS, g_SA, g_SS, 
    g_VA, g_EN, g_WH, g_FD, g_BR,
    g_H, g_C, g_M, g_L, g_B,
    g_AR, g_BT;

void __cdecl DoCommand(const char *cmd)
{
    if(g_injection)
        g_injection->do_command(cmd);
}

void __cdecl ClientPrint(const char *str)
{
    if(g_injection)
        g_injection->client_print(str);
}

int __cdecl count_object_type(const char *str)
{
    if(g_injection)
        return g_injection->count_object_type(str);
    else
        return -1;
}


int __cdecl count_object_type_color(const char *str, const char *color)
{
    if(g_injection)
        return g_injection->count_object_type(str,color);
    else
        return -1;
}

// Internal function that converts script function parameters to string
string ConvertParamsToStr(LibraryFunctions *Table,
    ParserVariable *Result, ParserVariable *Params[], int ParamCount, ParserObject *Parser)
{
    string Parm="";
    for(int i=0; i<ParamCount-1; i++)   // the Params[last] is a class var
    {
        if(i)
            Parm+=" ";

        if(Table->GetType(Params[i])==T_Number) // convert numbers to string
        {
            char Buff[128];
            double t=Table->GetNumber(Params[i]);

            if(t!=(int)t)   // float number
            {
                _gcvt(t,8,Buff);
                if(Buff[strlen(Buff)-1]=='.')       // remove trailing dot
                    Buff[strlen(Buff)-1]=0;
            } else          // normal (hex) number
            {
                strcpy(Buff,"0x");
                _itoa(t,Buff+2,16);
            }
            Parm+=Buff;
        }
        else
        {
            const char *str=Table->GetString(Params[i]);    // quote strings with spaces
            if(strchr(str,' '))
                Parm+=string("'")+str+"'";
            else
                Parm+=str;
        }
    }
//  ClientPrint(Parm.c_str());
    return Parm;
}

const char * ScriptFunc_CountOnGround(LibraryFunctions *Table,                                      \
    ParserVariable *Result, ParserVariable *Params[], int ParamCount, ParserObject *Parser)
{
    string Parm1="";
    string Parm2="";
    int count=0;

    if(Table->GetType(Params[0])==T_Number) // convert numbers to string
    {
        char Buff[128];
        double t=Table->GetNumber(Params[0]);

        if(t!=(int)t)   // float number
        {
        	_gcvt(t,8,Buff);
            if(Buff[strlen(Buff)-1]=='.')       // remove trailing dot
            	Buff[strlen(Buff)-1]=0;
		} else          // normal (hex) number
        {
        	strcpy(Buff,"0x");
            _itoa(t,Buff+2,16);
        }
        Parm1=Buff;
	} else
		Parm1=Table->GetString(Params[0]);

    if(ParamCount>2)
    {
	    if(Table->GetType(Params[1])==T_Number) // convert numbers to string
    	{
        	char Buff[128];
	        double t=Table->GetNumber(Params[1]);

    	    if(t!=(int)t)   // float number
        	{
	        	_gcvt(t,8,Buff);
    	        if(Buff[strlen(Buff)-1]=='.')       // remove trailing dot
        	    	Buff[strlen(Buff)-1]=0;
			} else          // normal (hex) number
    	    {
        		strcpy(Buff,"0x");
            	_itoa(t,Buff+2,16);
	        }
    	    Parm2=Buff;
       	} else
			Parm2=Table->GetString(Params[1]);

        count=g_injection->count_on_ground(Parm1.c_str(),Parm2.c_str());
	} else
	    count=g_injection->count_on_ground(Parm1.c_str());

	Table->SetType(Result,T_Number);
	Table->SetNumber(Result,count);

    return 0;
}


// Stringification antics
#define STRINGIFY(x) STRINGIFY2(x)
#define STRINGIFY2(x) #x

#define MAKE_COMMAND_BODY(cmd)  \
const char * ScriptFunc_##cmd(LibraryFunctions *Table,                                      \
    ParserVariable *Result, ParserVariable *Params[], int ParamCount, ParserObject *Parser) \
{                                                                                           \
    DoCommand((string(STRINGIFY(cmd)) + " " + ConvertParamsToStr(Table,Result,Params,ParamCount,Parser)).c_str());                  \
    return 0;                                                                               \
}
#define DEFINE_COMMAND(cmd) {STRINGIFY(cmd),ScriptFunc_##cmd,-1},

//
// Each command should have MAKE_COMMAND_BODY and DEFINE_COMMAND to be accessible from scripts
//

MAKE_COMMAND_BODY(fixwalk)
MAKE_COMMAND_BODY(filterweather)
MAKE_COMMAND_BODY(fixtalk)
MAKE_COMMAND_BODY(dump)
MAKE_COMMAND_BODY(flush)
MAKE_COMMAND_BODY(usetype)
MAKE_COMMAND_BODY(useobject)
MAKE_COMMAND_BODY(waittargettype)
MAKE_COMMAND_BODY(waittargetobject)
MAKE_COMMAND_BODY(waittargetobjecttype)
MAKE_COMMAND_BODY(waittargetlast)
MAKE_COMMAND_BODY(waittargetself)
MAKE_COMMAND_BODY(canceltarget)
MAKE_COMMAND_BODY(setarm)
MAKE_COMMAND_BODY(unsetarm)
MAKE_COMMAND_BODY(arm)
MAKE_COMMAND_BODY(disarm)
MAKE_COMMAND_BODY(setdress)
MAKE_COMMAND_BODY(unsetdress)
MAKE_COMMAND_BODY(dress)
MAKE_COMMAND_BODY(undress)
MAKE_COMMAND_BODY(removehat)
MAKE_COMMAND_BODY(removeearrings)
MAKE_COMMAND_BODY(removeneckless)
MAKE_COMMAND_BODY(removering)
MAKE_COMMAND_BODY(dismount)
MAKE_COMMAND_BODY(mount)
MAKE_COMMAND_BODY(waitmenu)
MAKE_COMMAND_BODY(cancelmenu)
MAKE_COMMAND_BODY(buy)
MAKE_COMMAND_BODY(sell)
MAKE_COMMAND_BODY(shop)
MAKE_COMMAND_BODY(light)
MAKE_COMMAND_BODY(saveconfig)
MAKE_COMMAND_BODY(version)
MAKE_COMMAND_BODY(dye)
MAKE_COMMAND_BODY(snoop)
MAKE_COMMAND_BODY(info)
MAKE_COMMAND_BODY(hide)
MAKE_COMMAND_BODY(setreceivingcontainer)
MAKE_COMMAND_BODY(unsetreceivingcontainer)
MAKE_COMMAND_BODY(emptycontainer)
MAKE_COMMAND_BODY(grab)
MAKE_COMMAND_BODY(cast)
MAKE_COMMAND_BODY(setcatchbag)
MAKE_COMMAND_BODY(unsetcatchbag)
MAKE_COMMAND_BODY(bandageself)
MAKE_COMMAND_BODY(addrecall)
MAKE_COMMAND_BODY(addgate)
MAKE_COMMAND_BODY(setdefault)
MAKE_COMMAND_BODY(recall)
MAKE_COMMAND_BODY(gate)
MAKE_COMMAND_BODY(useskill)
MAKE_COMMAND_BODY(poison)
MAKE_COMMAND_BODY(usefromground)
MAKE_COMMAND_BODY(waittargetground)
MAKE_COMMAND_BODY(drop)
MAKE_COMMAND_BODY(drophere)
MAKE_COMMAND_BODY(setdressspeed)
MAKE_COMMAND_BODY(automenu)
MAKE_COMMAND_BODY(filterspeech)
MAKE_COMMAND_BODY(track)
MAKE_COMMAND_BODY(repbuy)
MAKE_COMMAND_BODY(addtype)
MAKE_COMMAND_BODY(addobject)
MAKE_COMMAND_BODY(boxhack)
MAKE_COMMAND_BODY(fontcolor)
MAKE_COMMAND_BODY(massmove)
MAKE_COMMAND_BODY(concolor)
MAKE_COMMAND_BODY(getstatus)
MAKE_COMMAND_BODY(getname)
MAKE_COMMAND_BODY(attack)
MAKE_COMMAND_BODY(waittargettile)
MAKE_COMMAND_BODY(infotile)
MAKE_COMMAND_BODY(easyobject)

CFuncTable FuncTable[]=
{
	{"CountGround",ScriptFunc_CountOnGround,-1},
	{"CountOnGround",ScriptFunc_CountOnGround,-1},

// This are the normal commands:
DEFINE_COMMAND(fixwalk)
DEFINE_COMMAND(filterweather)
DEFINE_COMMAND(fixtalk)
DEFINE_COMMAND(dump)
DEFINE_COMMAND(flush)
DEFINE_COMMAND(usetype)
DEFINE_COMMAND(useobject)
DEFINE_COMMAND(waittargettype)
DEFINE_COMMAND(waittargetobject)
DEFINE_COMMAND(waittargetobjecttype)
DEFINE_COMMAND(waittargetlast)
DEFINE_COMMAND(waittargetself)
DEFINE_COMMAND(canceltarget)
DEFINE_COMMAND(setarm)
DEFINE_COMMAND(unsetarm)
DEFINE_COMMAND(arm)
DEFINE_COMMAND(disarm)
DEFINE_COMMAND(setdress)
DEFINE_COMMAND(unsetdress)
DEFINE_COMMAND(dress)
DEFINE_COMMAND(undress)
DEFINE_COMMAND(removehat)
DEFINE_COMMAND(removeearrings)
DEFINE_COMMAND(removeneckless)
DEFINE_COMMAND(removering)
DEFINE_COMMAND(dismount)
DEFINE_COMMAND(mount)
DEFINE_COMMAND(waitmenu)
DEFINE_COMMAND(cancelmenu)
DEFINE_COMMAND(buy)
DEFINE_COMMAND(sell)
DEFINE_COMMAND(shop)
DEFINE_COMMAND(light)
DEFINE_COMMAND(saveconfig)
DEFINE_COMMAND(version)
DEFINE_COMMAND(dye)
DEFINE_COMMAND(snoop)
DEFINE_COMMAND(info)
DEFINE_COMMAND(hide)
DEFINE_COMMAND(setreceivingcontainer)
DEFINE_COMMAND(unsetreceivingcontainer)
DEFINE_COMMAND(emptycontainer)
DEFINE_COMMAND(grab)
DEFINE_COMMAND(cast)
DEFINE_COMMAND(setcatchbag)
DEFINE_COMMAND(unsetcatchbag)
DEFINE_COMMAND(bandageself)
DEFINE_COMMAND(addrecall)
DEFINE_COMMAND(addgate)
DEFINE_COMMAND(setdefault)
DEFINE_COMMAND(recall)
DEFINE_COMMAND(gate)
DEFINE_COMMAND(useskill)
DEFINE_COMMAND(poison)
DEFINE_COMMAND(usefromground)
DEFINE_COMMAND(waittargetground)
DEFINE_COMMAND(drop)
DEFINE_COMMAND(drophere)
DEFINE_COMMAND(setdressspeed)
DEFINE_COMMAND(automenu)
DEFINE_COMMAND(filterspeech)
DEFINE_COMMAND(track)
DEFINE_COMMAND(repbuy)
DEFINE_COMMAND(addtype)
DEFINE_COMMAND(addobject)
DEFINE_COMMAND(boxhack)
DEFINE_COMMAND(fontcolor)
DEFINE_COMMAND(concolor)
DEFINE_COMMAND(getstatus)
DEFINE_COMMAND(getname)
DEFINE_COMMAND(attack)
DEFINE_COMMAND(waittargettile)
DEFINE_COMMAND(infotile)
DEFINE_COMMAND(easyobject)
{0,0,0}
};

// Added this function so you don;t need to recompile script.dll every time
// you whant to add a new class/function accessible from script
void __cdecl AddClasses(ParserObject* Parser, const struct LibraryFunctions *Funcs)
{
    Funcs->SetClass(Parser,"InternalUoClass",FuncTable);
}


DllInterface Intrf={
    sizeof(DllInterface),
    DoCommand,ClientPrint, 
    count_object_type, count_object_type_color, AddClasses,
    0,
    &g_Life, &g_STR, &g_Mana, &g_INT, &g_Stamina, &g_DEX, 
    &g_Armor, &g_Weight, &g_Gold,
    &g_BM, &g_BP, &g_GA, &g_GS, &g_MR, &g_NS, &g_SA, &g_SS, 
    &g_VA, &g_EN, &g_WH, &g_FD, &g_BR,
    &g_H, &g_C, &g_M, &g_L, &g_B,
    &g_AR, &g_BT 
};

void InitExternalDll(HWND Tab)
{
	char Buff[1024]=".\blablabla";
	GetModuleFileName(g_hinstance,Buff,1024);
	char *t=strrchr(Buff,'\\');
	if(t) *t=0;
	strcat(Buff,"\\script.dll");

    Intrf.Window=Tab;
    HINSTANCE DLL=LoadLibrary(Buff);
    typedef void __cdecl Func(DllInterface*);
    Func *F=(Func*)GetProcAddress(DLL,"_Init");
    if(F)
        F(&Intrf);
    else
        error_printf("Error %d loading %s",GetLastError(),Buff);
}

void UnloadExternalDll()
{
    HINSTANCE DLL=GetModuleHandle("script.dll");
    typedef void __cdecl Func();
    Func *F=(Func*)GetProcAddress(DLL,"_Cleanup");
    if(F)
        F();
    FreeLibrary(DLL);

}

typedef void __cdecl Func(const char *);
		void __cdecl RunFunction(const char *Name);
		void __cdecl TerminateFunction(const char *Name);

bool HandleCommandInDll(const char *cmd)
{
    while(*cmd==' ' || *cmd==',')
        cmd++;

    if(strnicmp(cmd,"EXEC ",5)==0)
	{
		HINSTANCE DLL=GetModuleHandle("script.dll");
		Func *F=(Func*)GetProcAddress(DLL,"_RunFunction");
		if(F)
		{
			cmd+=5; // strlen("EXEC ");
			while(*cmd==' ') cmd++;
			F(cmd);
		}
		return true;
	}
	else 
    if(strnicmp(cmd,"TERMINATE ",10)==0)
	{
		HINSTANCE DLL=GetModuleHandle("script.dll");
		Func *F=(Func*)GetProcAddress(DLL,"_TerminateFunction");
		if(F)
		{
			cmd+=10; // strlen
			while(*cmd==' ') cmd++;
			F(cmd);
		}
		return true;
	}
	return false;
}
