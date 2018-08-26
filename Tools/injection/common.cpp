////////////////////////////////////////////////////////////////////////////////
//
// common.cpp
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
//
// Originally based on part of Sniffy:
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
//  This contains code for logging and error reporting.
//
////////////////////////////////////////////////////////////////////////////////

#include <ctype.h>
#include <stdarg.h>
#include <errno.h>
#include <stdlib.h>
#include <assert.h>

#include <windows.h>

#include "common.h"


extern HINSTANCE g_hinstance;   // defined in gui.cpp

Logger * g_logger = 0;
HANDLE g_mutex = 0;
ECustomShard Shard=DefaultShard;
bool NoHungMessage = false;

////////////////////////////////////////////////////////////////////////////////
//
//  Constructor/Destructor
//
////////////////////////////////////////////////////////////////////////////////

Logger::Logger()
: m_verbose(true), m_flush(true)
{
    ASSERT(g_mutex == 0);
    g_mutex = CreateMutex(NULL, TRUE, "InjectionMutex");
    if(g_mutex == NULL)
        FATAL("Failed to create mutex");
    else if(GetLastError() == ERROR_ALREADY_EXISTS)
        FATAL("Injection is already running!");

    m_fp = fopen("injection_log.txt", "wt");
    if(m_fp == NULL)
    {
        OutputDebugString("ERROR: failed to create log file");
        OutputDebugString(strerror(errno));
    }
    else
        g_logger = this;
}

Logger::~Logger()
{
    g_logger = 0;
    if(m_fp != 0)
    {
        printf(true, "Log closed.\n");
        fclose(m_fp);
    }
    CloseHandle(g_mutex);
    g_mutex = 0;
}



////////////////////////////////////////////////////////////////////////////////
//
//  Dump functions
//
////////////////////////////////////////////////////////////////////////////////

void Logger::dump(bool verbose, unsigned char * buf, int length)
{
    if(verbose && !m_verbose)
        return;
    int num_lines = length / 16;
    if(length % 16 != 0)
        num_lines++;

    // Dump the buffer 16 bytes per line
    for(int line = 0; line < num_lines; line++)
    {
        int row;
        fprintf(m_fp, "%04x: ", line * 16);

        // Print the bytes of the line as hex
        for(row = 0; row < 16; row++)
        {
            if(line * 16 + row < length)
                fprintf(m_fp, "%02x ", buf[line * 16 + row]);
            else
                fprintf(m_fp, "-- ");
        }
        fprintf(m_fp, ": ");

        // Print the bytes as characters (if printable)
        for(row = 0; row < 16; row++)
        {
            if(line * 16 + row < length)
                fputc(isprint(buf[line * 16 + row])
                    ? buf[line * 16 + row] : '.', m_fp);
        }
        fputc('\n', m_fp);
    }
    if(m_flush)
        fflush(m_fp);
}

void Logger::printf(bool verbose, const char * format, ...)
{
    if(verbose && !m_verbose)
        return;
    va_list arg;
    va_start(arg, format);
    vfprintf(m_fp, format, arg);
    va_end(arg);
    if(m_flush)
        fflush(m_fp);
}

void Logger::vprintf(bool verbose, const char * format, va_list ap)
{
    if(verbose && !m_verbose)
        return;
    vfprintf(m_fp, format, ap);
    if(m_flush)
        fflush(m_fp);
}

void Logger::flush()
{
    fflush(m_fp);
}


////////////////////////////////////////////////////////////////////////////////
//
//  These functions are for error reporting
//
////////////////////////////////////////////////////////////////////////////////

static char error_buf[500];

void assert_failed_msg(const char * condition, const char * filename, int line,
    const char * message)
{
    OutputDebugString("### Assertion Failed ###");
    OutputDebugString(message);
    OutputDebugString(condition);
    sprintf(error_buf, "file: %s\n", filename);
    OutputDebugString(error_buf);
    sprintf(error_buf, "line: %d\n", line);
    OutputDebugString(error_buf);

    if(g_logger != 0)
    {
        g_logger->printf(false, "### Assertion Failed ###\n%s\n%s\nfile: %s\nline: %d\n",
            message, condition, filename, line);
        g_logger->flush();
    }
    else
        OutputDebugString("(log unavailable)");
    // g_hinstance is zero until global constructors have completed, and is
    // set to zero just before global destructors are executed.
    if(g_hinstance == 0)
        TerminateProcess(GetCurrentProcess(), 1);
    // Call the C runtime library failure handler.
#ifdef __GNUC__
    _assert(condition, filename, line);
#endif
}

void fatal_error(const char * filename, int line, const char * message)
{
    OutputDebugString("### Fatal Error ###");
    OutputDebugString(message);
    sprintf(error_buf, "file: %s\n", filename);
    OutputDebugString(error_buf);
    sprintf(error_buf, "line: %d\n", line);
    OutputDebugString(error_buf);

    if(g_logger != 0)
    {
        g_logger->printf(false, "### Fatal Error ###\n%s\nfile: %s\nline: %d\n",
            message, filename, line);
        g_logger->flush();
    }
    else
        OutputDebugString("(log unavailable)");
    // g_hinstance is zero until global constructors have completed, and is
    // set to zero just before global destructors are executed.
    if(g_hinstance == 0)
        TerminateProcess(GetCurrentProcess(), 1);
    // Call the C runtime library failure handler.
#ifdef __GNUC__
    _assert(message, filename, line);
#endif
}

void log_printf(const char * format, ...)
{
    if(g_logger != 0)
    {
        va_list arg;
        va_start(arg, format);
        g_logger->vprintf(false, format, arg);
        va_end(arg);
    }
    else
    {
        OutputDebugString("log_printf(): logger doesn't exist!");
        va_list arg;
        va_start(arg, format);
        vsprintf(error_buf, format, arg);
        va_end(arg);
        OutputDebugString(error_buf);
    }
}

void error_printf(const char * format, ...)
{
    if(g_logger != 0)
    {
        va_list arg;
        va_start(arg, format);
        g_logger->printf(false, "***Error: ");
        g_logger->vprintf(false, format, arg);
        va_end(arg);
    }
    else
    {
        OutputDebugString("error_printf(): logger doesn't exist!");
        va_list arg;
        va_start(arg, format);
        vsprintf(error_buf, format, arg);
        va_end(arg);
        OutputDebugString(error_buf);
    }
}

void warning_printf(const char * format, ...)
{
    if(g_logger != 0)
    {
        va_list arg;
        va_start(arg, format);
        g_logger->printf(false, "**Warning: ");
        g_logger->vprintf(false, format, arg);
        va_end(arg);
    }
    else
    {
        OutputDebugString("warning_printf(): logger doesn't exist!");
        va_list arg;
        va_start(arg, format);
        vsprintf(error_buf, format, arg);
        va_end(arg);
        OutputDebugString(error_buf);
    }
}

void trace_printf(const char * format, ...)
{
    if(g_logger != 0)
    {
        va_list arg;
        va_start(arg, format);
        g_logger->vprintf(true, format, arg);
        va_end(arg);
    }
    else
    {
        OutputDebugString("trace_printf(): logger doesn't exist!");
        va_list arg;
        va_start(arg, format);
        vsprintf(error_buf, format, arg);
        va_end(arg);
        OutputDebugString(error_buf);
    }
}

void log_dump(unsigned char * buf, int length)
{
    if(g_logger != 0)
        g_logger->dump(false, buf, length);
    else
        OutputDebugString("log_dump(): logger doesn't exist!");
}

void trace_dump(unsigned char * buf, int length)
{
    if(g_logger != 0)
        g_logger->dump(true, buf, length);
    else
        OutputDebugString("trace_dump(): logger doesn't exist!");
}

void log_flush()
{
    if(g_logger != 0)
        g_logger->flush();
    else
        OutputDebugString("log_flush(): logger doesn't exist!");
}

////////////////////////////////////////////////////////////////////////////////

bool string_to_bool(const char * s, bool & b)
{
    if(strcmp(s, "true") == 0)
        b = true;
    else if(strcmp(s, "false") == 0)
        b = false;
    else
        return false;
    return true;
}

bool string_to_serial(const char * s, uint32 & serial)
{
    char * end;
    serial = strtoul(s, &end, 16);
	if(end!=s+strlen(s)) serial=0xFFFFFFFF;
    return end == s + strlen(s);
}

bool string_to_int(const char * s, int & n)
{
    char * end;
    // If the string starts with "0x", treat it as hex.
    if(s[0] == '0' && s[1] != '\0' && s[1] == 'x')
        n = strtol(s + 2, &end, 16);
    else
        n = strtol(s, &end, 10);
    return end == s + strlen(s);
}


