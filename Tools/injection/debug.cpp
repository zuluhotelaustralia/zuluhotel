////////////////////////////////////////////////////////////////////////////////
//
// debug.cpp
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

#include <windows.h>
#include "common.h"
#include "crash.h"

#define PEEKB(addr) (*(char*)(addr))

void DumpErrorInformation(CONTEXT* CR)
{
	error_printf("injection.dll base: %08X\n",GetModuleHandle("injection.dll"));
	error_printf("script.dll base: %08X\n",GetModuleHandle("script.dll"));

	error_printf("EAX=%08X, EBX=%08X, ECX=%08X, EDX=%08X\n",
		CR->Eax, CR->Ebx, CR->Ecx, CR->Edx);
	error_printf("ESI=%08X, EDI=%08X, ESP=%08X, EBP=%08X\n",
		CR->Esi, CR->Edi, CR->Esp, CR->Ebp);
	error_printf("EIP=%08X, EFLAGS=%08X\n\n",
		CR->Eip, CR->EFlags);

	error_printf("Bytes at EIP:\n");
	log_dump((unsigned char*)CR->Eip, 16);
	error_printf("Bytes at ESP:\n");
	log_dump((unsigned char*)CR->Esp, 64);
	log_flush();
}

void disasm(BYTE* iptr0, DWORD* osizeptr);

//
// This function is a hack. It tries to ignore an unhandled exception (i.e. crash),
// but if there are too many crashes it will force client to terminate...
//
LONG __stdcall MyUnhandledExceptionFilter(
  struct _EXCEPTION_POINTERS *ExceptionInfo)
{
	static int TotalErrorCount=0;
	static int ErrorCount=0;
	static DWORD LastErrorTime=GetTickCount();

	ErrorCount++; TotalErrorCount++;
	if(ExceptionInfo->ExceptionRecord)
	{
	error_printf("Unhandled exception%3d: 0x%08X at %08X\n",TotalErrorCount,
		ExceptionInfo->ExceptionRecord->ExceptionCode,
		ExceptionInfo->ExceptionRecord->ExceptionAddress);

	if(ErrorCount>100 && GetTickCount()-LastErrorTime<5000)
	// crash when too many errors happen too often
	{
		DumpErrorInformation(ExceptionInfo->ContextRecord);
		//MessageBox(0,"UO client performed an unrecoverable invalid operation.\nMore information can be found in injection_log.txt",0,MB_ICONSTOP);
		if(MessageBox(0,"UO client performed an unrecoverable invalid operation.\nContinue?",0,MB_ICONSTOP|MB_YESNO)==IDYES)
		 ExitProcess(1);
	}

	if(GetTickCount()-LastErrorTime>5000) // reset error count every 5 sec
	{
		ErrorCount=0;
		LastErrorTime=GetTickCount();
	}

	int B=255&PEEKB(ExceptionInfo->ContextRecord->Eip);	// this can cause another crash...

    if(B>=0xD9 && B<=0xDF)	// For some hell float exceptions are unmasked
    {	// - ignore all floating point exceptions and clear coprocessor err state
		// these constants are coprocessor opcodes
		ExceptionInfo->ContextRecord->FloatSave.ControlWord=0x27f;
        ExceptionInfo->ContextRecord->FloatSave.StatusWord&=~0xff;
        return EXCEPTION_CONTINUE_EXECUTION;
    }

	if(TotalErrorCount<3)
	{
		// Write error information to the log file
		// I think that only 2 first errors can be interesting

		DumpErrorInformation(ExceptionInfo->ContextRecord);
	}

// Determine the length of a faulting instruction and step over it
	DWORD Len=0;
	disasm((BYTE*)ExceptionInfo->ContextRecord->Eip,&Len);

	ExceptionInfo->ContextRecord->Eip+=Len;

	if(Len==0)
	    return EXCEPTION_CONTINUE_SEARCH;

	}
    return EXCEPTION_CONTINUE_EXECUTION;
}


// This function implements a watchdog thread which records necessary
// information about the hangup
DWORD WINAPI MyWatchForHangClientThread(LPVOID)
{
	CONTEXT ctx;
	ctx.ContextFlags = CONTEXT_FULL;
	Sleep(30000);

	LastMessageTime=GetTickCount();

	while(1)
	{
		Sleep(2500);
		PostMessage(g_ClientWindow,WM_NULL,0,0);

		SuspendThread(g_MainClientThread);
		GetThreadContext(g_MainClientThread,&ctx);

		if(GetTickCount()-LastMessageTime>5000)
		{	// If my MessageProc had not updated LastMessageTime for
			// 5 seconds - assume that client is hang
			error_printf("UO Client is hung...\n");
			DumpErrorInformation(&ctx);

			int t=IDNO;
			if(!NoHungMessage) t=MessageBox(0,"UO client appears to be hung. More information can be found in injection_log.txt\n"
//				"Press YES to try to 'resurrect' client, NO to terminate it or CANCEL to wait for 5 seconds.",0,MB_ICONSTOP|MB_YESNOCANCEL);
				"Press YES to terminate client or NO to wait for 5 seconds.",0,MB_ICONSTOP|MB_YESNO);
			if(t==IDYES)
				ExitProcess(1);
//			else if(t==IDYES)
//			{
//				ctx.Eip=(DWORD)MyTryToResurrectClient;
//				SetThreadContext(g_MainClientThread,&ctx);
//			}

			LastMessageTime=GetTickCount();
		}

		ResumeThread(g_MainClientThread);
	}
}

