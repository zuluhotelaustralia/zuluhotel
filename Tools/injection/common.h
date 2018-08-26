////////////////////////////////////////////////////////////////////////////////
//
// common.h
//
// Copyright (C) 2001 Luke 'Infidel' Dunstan
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
//  Declarations for types and functions common to many parts of Injection
//
//  When compiling, the following names may be #defined:
//
//  NDEBUG - disable assertions
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _COMMON_H_
#define _COMMON_H_

#include <stdio.h>
#include <memory.h>

typedef unsigned char uint8;
typedef unsigned short uint16;
typedef unsigned long uint32;
typedef signed long sint32;

// Given a buffer of 4 bytes, extract a big endian 32 bit unsigned integer
inline uint32 unpack_big_uint32(uint8 * buf)
{
    return (buf[0] << 24) | (buf[1] << 16) | (buf[2] << 8) | buf[3];
}

// Given a buffer of 4 bytes, extract a big endian 32 bit signed integer
inline sint32 unpack_big_sint32(uint8 * buf)
{
    return (buf[0] << 24) | (buf[1] << 16) | (buf[2] << 8) | buf[3];
}

// Given a buffer of 2 bytes, extract a big endian 16 bit unsigned integer
inline uint16 unpack_big_uint16(uint8 * buf)
{
    return (buf[0] << 8) | buf[1];
}

// Given a buffer of 2 bytes, extract a little endian 16 bit unsigned integer
inline uint16 unpack_little_uint16(uint8 * buf)
{
    return *reinterpret_cast<uint16 *>(buf);
}

inline void pack_big_uint32(uint8 * buf, uint32 x)
{
    buf[0] = uint8(x >> 24);
    buf[1] = uint8((x >> 16) & 0xff);
    buf[2] = uint8((x >> 8) & 0xff);
    buf[3] = uint8(x & 0xff);
}

inline void pack_big_uint16(uint8 * buf, uint16 x)
{
    buf[0] = x >> 8;
    buf[1] = x & 0xff;
}

inline void pack_little_uint16(uint8 * buf, uint16 x)
{
    buf[0] = x & 0xff;
    buf[1] = x >> 8;
}

// GCC_NORETURN means the function never returns
// GCC_PRINTF means the function has printf-style arguments
#ifdef __GNUC__
#   define GCC_NORETURN __attribute__((noreturn))
#   define GCC_PRINTF(n,m) __attribute__((format (printf, n, m)))
#else
#   define GCC_NORETURN
#   define GCC_PRINTF(n,m)
#endif

////////////////////////////////////////////////////////////////////////////////

//// Common constants:

const uint32 INVALID_SERIAL = 0xffffffff;

// Message codes used when creating new messages:
const int CODE_CLIENT_TALK = 0x03;
const int CODE_SINGLE_CLICK_ITEM = 0x09;
const int CODE_DOUBLE_CLICK_ITEM = 0x06;
const int CODE_PICK_UP_ITEM = 0x07;
const int CODE_DROP_ITEM = 0x08;
const int CODE_CAST_SPELL_USE_SKILL = 0x12;
const int CODE_CLIENT_EQUIP_ITEM = 0x13;
const int CODE_SERVER_TALK = 0x1c;
const int CODE_PAUSE_CONTROL = 0x33;
const int CODE_VENDOR_BUY_REPLY = 0x3b;
const int CODE_GLOBAL_LIGHT_LEVEL = 0x4f;
const int CODE_TARGETING_CURSOR = 0x6c;
const int CODE_MENU_CHOICE = 0x7d;
////////////////////////////////////////////////////////////////////////////////

void log_printf(const char * format, ...) GCC_PRINTF(1,2);
void error_printf(const char * format, ...) GCC_PRINTF(1,2);
void warning_printf(const char * format, ...) GCC_PRINTF(1,2);
void log_dump(unsigned char * buf, int length);
void log_flush();
void trace_printf(const char * format, ...) GCC_PRINTF(1,2);
void trace_dump(unsigned char * buf, int length);

class Logger
{
protected:
    FILE * m_fp;
    bool m_verbose, m_flush;

public:
    // Constructor/Destructor
    Logger();
    ~Logger();

public:
    // dump functions
    void dump(bool verbose, unsigned char * buf, int length);
    void printf(bool verbose, const char * format, ...) GCC_PRINTF(3,4);
    void vprintf(bool verbose, const char * format, va_list ap);
    void flush();
    bool get_flush() const { return m_flush; }
    void set_flush(bool flush) { m_flush = flush; }
    bool get_verbose() const { return m_verbose; }
    void set_verbose(bool verbose) { m_verbose = verbose; }
    bool dmenus,fsnd;
};

extern Logger * g_logger;

////////////////////////////////////////////////////////////////////////////////

/*
    Assertion macros

    These macros do nothing in release mode:
    ASSERT(condition) : aborts if the condition is false
    ASSERTMSG(condition,message) : aborts if the condition is false, and
        outputs the additional error message

    These macros still work in release mode:
    FATAL(message) : outputs the error then terminates
*/
#ifdef __cplusplus
#   define VOIDEXPRESSION (static_cast<void>(0))
#else
#   define VOIDEXPRESSION ((void)0)
#endif

#ifdef NDEBUG
#   define ASSERT(cond) VOIDEXPRESSION
#   define ASSERTMSG(cond,msg) VOIDEXPRESSION
#else
#   define ASSERT(cond) ((cond)? VOIDEXPRESSION : \
        assert_failed_msg(#cond, __FILE__, __LINE__, ""))
#   define ASSERTMSG(cond,msg) ((cond)? VOIDEXPRESSION : \
        assert_failed_msg(#cond, __FILE__, __LINE__, msg))
#endif
#define FATAL(msg) fatal_error(__FILE__, __LINE__, msg)

/****
    The below functions are defined in logger.cpp

    Do not call these functions directly: use the above macros !
*****/

/*
    See ASSERT() and ASSERTMSG()
    Called when an assertion fails
*/
void assert_failed_msg(const char * condition, const char * filename,
    int line, const char * message) GCC_NORETURN;

void fatal_error(const char * filename, int line, const char * message)
    GCC_NORETURN;

////////////////////////////////////////////////////////////////////////////////

bool string_to_bool(const char * s, bool & b);
bool string_to_serial(const char * s, uint32 & serial);
bool string_to_int(const char * s, int & n);

extern bool g_FixUnicodeCaption;
extern int CharStat;

enum ECustomShard
{
	DefaultShard=0,
	AoP,
	OSI,
	Sphere,
	POL,
	ECS_End
};

enum TWState{ NORMAL, WAITING, TARGETING};
extern ECustomShard Shard;
extern bool NoHungMessage;

#endif

