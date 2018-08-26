////////////////////////////////////////////////////////////////////////////////
//
// ignition.cpp
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
//
// Parts based on Ignition:
// Copyright (C) 2000 Bruno 'Beosil' Heidelberger
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
//  This module serves the same purpose as Ignition.dll, and is used instead
//  of Ignition when Injection is loaded using ILaunch
//
////////////////////////////////////////////////////////////////////////////////

#include "gui.h"    // for g_hinstance

#include "patch.h"

typedef int (*install_func_t)(unsigned int checksum, unsigned int length);
typedef const char * (*geterrortext_func_t)(int error);

// The following variable must be marked as shared, so that the instance of
// injection.dll loaded into the client address space can access data
// in the instance of injection.dll that is in ILaunch's address space.

#ifdef __GNUC__
#   define DLLEXPORT __attribute__((dllexport))
#   define DLLSHARED __attribute__((section (".shared"), shared))
#else
#   define DLLSHARED
#   define DLLEXPORT
#endif

#ifndef __GNUC__
#pragma data_seg("SHAREDAT")
#endif

HHOOK DLLSHARED g_patch_hhook = 0;
// Stupid VC does not allow uninitialized vars in SHARED sections
char DLLSHARED g_patch_buffer[sizeof(Patch)]="Garbage1";    
bool DLLSHARED g_use_injection = true;
char DLLSHARED g_message[300] = "Garbage";

#ifndef __GNUC__
#pragma data_seg()
#endif

Patch &g_patch=*((Patch*)g_patch_buffer);

static void api_error(const char * message)
{
    int err = GetLastError();
    LPVOID msg_buf;
    FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
        NULL, err, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        reinterpret_cast<LPTSTR>(&msg_buf), 0, NULL);
    wsprintf(g_message, "%s\nReason: (%d)\n%s", message, err, msg_buf);
    LocalFree(msg_buf);
}

static LRESULT WINAPI patch_GetMsgProc(int code, WPARAM wparam, LPARAM lparam)
{
    if(code == HC_ACTION)
    {
        DWORD error = 0;
        MSG * msg = reinterpret_cast<MSG *>(lparam);
        DWORD launching_thread = msg->wParam;

        // Apply the patch
        const PatchElement * elem = g_patch.get_elements();
        int num_elements = g_patch.get_num_elements();
        char Tmp[1024];
        wsprintf(Tmp,"num_elements1=%d\n",num_elements);
        OutputDebugString(Tmp);
        for(int i = 0; i < num_elements; i++)
        {
            DWORD oldProtect;
            if(VirtualProtect(reinterpret_cast<LPVOID>(elem->m_address),
                    elem->m_length, PAGE_READWRITE, &oldProtect) == 0)
            {
                api_error("Failed to patch (page protection)");
                error = 1;
                break;
            }
            CopyMemory(reinterpret_cast<unsigned char *>(elem->m_address),
                elem->m_buffer, elem->m_length);
            elem++;
        }

        // Load Injection DLL (unless disabled)
        if(error == 0 && g_use_injection)
        {
            HINSTANCE hmodule = LoadLibrary("injection.dll");
            if(hmodule == NULL)
                api_error("Failed to load injection.dll in client");
            else
            {
                // find entry points in injection.dll
                install_func_t install_func = reinterpret_cast<install_func_t>(
                    GetProcAddress(hmodule, "Install"));
                geterrortext_func_t geterrortext_func =
                    reinterpret_cast<geterrortext_func_t>(
                        GetProcAddress(hmodule, "GetErrorText"));
                if(install_func == NULL)
                {
                    api_error("Failed to find Install function in injection.dll");
                    error = 1;
                }
                else if(geterrortext_func == NULL)
                {
                    api_error("Failed to find GetErrorText function in injection.dll");
                    error = 1;
                }
                else
                {
                    // Attempt to install Injection
                    // NOTE: the checksum and length parameters are not needed
                    int err = install_func(0, 0);
                    if(err != 0)
                    {
                        strcpy(g_message, geterrortext_func(err));
                        error = 1;
                    }
                }
            }
        }
        // Post a message to the launching thread (param: success/failure)
        PostThreadMessage(launching_thread, WM_USER, error, GetLastError());
        UnhookWindowsHookEx(g_patch_hhook);
    }
    return CallNextHookEx(g_patch_hhook, code, wparam, lparam);
}

extern "C" bool patch_setup(unsigned long client_thread_id,
    const Patch & patch, bool use_injection);
extern "C" const char * patch_getmessage();

// returns false for failure
DLLEXPORT bool patch_setup(unsigned long client_thread_id,
    const Patch & patch, bool use_injection)
{
    g_use_injection = use_injection;
    g_patch = patch;
    g_message[0] = '\0';
    g_patch_hhook = SetWindowsHookEx(WH_GETMESSAGE, patch_GetMsgProc,
        g_hinstance, client_thread_id);

    if(g_patch_hhook == 0)
    {
        api_error("Failed to install hook in client");
        return false;
    }
    return true;
}

DLLEXPORT const char * patch_getmessage()
{
    return g_message;
}

