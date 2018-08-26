////////////////////////////////////////////////////////////////////////////////
//
// hotkeys.h
//
// Copyright (C) 2001 Wayne (Chiphead) Hogue
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
//  Declarations for classes in hotkeys.cpp
//
//  Handles hotkeys
//
////////////////////////////////////////////////////////////////////////////////


#ifndef _HOTKEYS_H_
#define _HOTKEYS_H_


const string KEY_TEXT[] = {     "0x00",         //0x00
                                "Left Mouse",   //0x01
                                "Right Mouse",  //0x02
                                "Ctrl-Break",   //0x03
                                "Middle Mouse", //0x04
                                "0x05",         //0x05
                                "0x06",         //0x06
                                "0x07",         //0x07
                                "BACKSPACE",    //0x08
                                "TAB",          //0x09
                                "0x0a",         //0x0a
                                "0x0b",         //0x0b
                                "CLEAR",        //0x0c
                                "ENTER",        //0x0d
                                "0x0e",         //0x0e
                                "0x0f",         //0x0f
                                "SHIFT",        //0x10
                                "CTRL",         //0x11
                                "ALT",          //0x12
                                "PAUSE",        //0x13
                                "CAPSLOCK",     //0x14
                                "0x15",         //0x15
                                "0x16",         //0x16
                                "0x17",         //0x17
                                "0x18",         //0x18
                                "0x19",         //0x19
                                "0x1a",         //0x1a
                                "ESC",          //0x1b
                                "0x1c",         //0x1c
                                "0x1d",         //0x1d
                                "0x1e",         //0x1e
                                "0x1f",         //0x1f
                                "SPACE",        //0x20
                                "PAGE UP",      //0x21
                                "PAGE DOWN",    //0x22
                                "END",          //0x23
                                "HOME",         //0x24
                                "LEFT Arrow",   //0x25
                                "UP Arrow",     //0x26
                                "RIGHT Arrow",  //0x27
                                "DOWN Arrow",   //0x28
                                "SELECT",       //0x29
                                "OEM 2A",       //0x2a
                                "EXECUTE",      //0x2b
                                "PRINT SCREEN", //0x2c
                                "INS",          //0x2d
                                "DEL",          //0x2e
                                "HELP",         //0x2f
                                "0",            //0x30
                                "1",            //0x31
                                "2",            //0x32
                                "3",            //0x33
                                "4",            //0x34
                                "5",            //0x35
                                "6",            //0x36
                                "7",            //0x37
                                "8",            //0x38
                                "9",            //0x39
                                "0x3a",         //0x3a
                                "0x3b",         //0x3b
                                "0x3c",         //0x3c
                                "0x3d",         //0x3d
                                "0x3e",         //0x3e
                                "0x3f",         //0x3f
                                "0x40",         //0x40
                                "A",            //0x41
                                "B",            //0x42
                                "C",            //0x43
                                "D",            //0x44
                                "E",            //0x45
                                "F",            //0x46
                                "G",            //0x47
                                "H",            //0x48
                                "I",            //0x49
                                "J",            //0x4a
                                "K",            //0x4b
                                "L",            //0x4c
                                "M",            //0x4d
                                "N",            //0x4e
                                "O",            //0x4f
                                "P",            //0x50
                                "Q",            //0x51
                                "R",            //0x52
                                "S",            //0x53
                                "T",            //0x54
                                "U",            //0x55
                                "V",            //0x56
                                "W",            //0x57
                                "X",            //0x58
                                "Y",            //0x59
                                "Z",            //0x5a
                                "LEFT WIN",     //0x5b
                                "RIGHT WIN",    //0x5c
                                "APPS",         //0x5d
                                "0x5e",         //0x5e
                                "0x5f",         //0x5f
                                "NUM-0",        //0x60
                                "NUM-1",        //0x61
                                "NUM-2",        //0x62
                                "NUM-3",        //0x63
                                "NUM-4",        //0x64
                                "NUM-5",        //0x65
                                "NUM-6",        //0x66
                                "NUM-7",        //0x67
                                "NUM-8",        //0x68
                                "NUM-9",        //0x69
                                "NUM-*",        //0x6a
                                "NUM-+",        //0x6b
                                "SEPARATOR",    //0x6c
                                "NUM--",        //0x6d
                                "NUM-.",        //0x6e
                                "NUM-/",        //0x6f
                                "F1",           //0x70
                                "F2",           //0x71
                                "F3",           //0x72
                                "F4",           //0x73
                                "F5",           //0x74
                                "F6",           //0x75
                                "F7",           //0x76
                                "F8",           //0x77
                                "F9",           //0x78
                                "F10",          //0x79
                                "F11",          //0x7a
                                "F12",          //0x7b
                                "F13",          //0x7c
                                "F14",          //0x7d
                                "F15",          //0x7e
                                "F16",          //0x7f
                                "F17",          //0x80
                                "F18",          //0x81
                                "F19",          //0x82
                                "F20",          //0x83
                                "F21",          //0x84
                                "F22",          //0x85
                                "F23",          //0x86
                                "F24",          //0x87
                                "0x88",         //0x88
                                "0x89",         //0x89
                                "0x8a",         //0x8a
                                "0x8b",         //0x8b
                                "0x8c",         //0x8c
                                "0x8d",         //0x8d
                                "0x8e",         //0x8e
                                "0x8f",         //0x8f
                                "NUMLOCK",      //0x90
                                "SCROLLLOCK",   //0x91
                                "0x92",         //0x92
                                "0x93",         //0x93
                                "0x94",         //0x94
                                "0x95",         //0x95
                                "0x96",         //0x96
                                "0x97",         //0x97
                                "0x98",         //0x98
                                "0x99",         //0x99
                                "0x9a",         //0x9a
                                "0x9b",         //0x9b
                                "0x9c",         //0x9c
                                "0x9d",         //0x9d
                                "0x9e",         //0x9e
                                "0x9f",         //0x9f
                                "0xa0",         //0xa0
                                "0xa1",         //0xa1
                                "0xa2",         //0xa2
                                "0xa3",         //0xa3
                                "0xa4",         //0xa4
                                "0xa5",         //0xa5
                                "0xa6",         //0xa6
                                "0xa7",         //0xa7
                                "0xa8",         //0xa8
                                "0xa9",         //0xa9
                                "0xaa",         //0xaa
                                "0xab",         //0xab
                                "0xac",         //0xac
                                "0xad",         //0xad
                                "0xae",         //0xae
                                "0xaf",         //0xaf
                                "0xb0",         //0xb0
                                "0xb1",         //0xb1
                                "0xb2",         //0xb2
                                "0xb3",         //0xb3
                                "0xb4",         //0xb4
                                "0xb5",         //0xb5
                                "0xb6",         //0xb6
                                "0xb7",         //0xb7
                                "0xb8",         //0xb8
                                "0xb9",         //0xb9
                                ";",        //0xba
                                "=",            //0xbb
                                ",",        //0xbc
                                "-",            //0xbd
                                ".",        //0xbe
                                "/",        //0xbf
                                "`",            //0xc0
                                "0xc1",         //0xc1
                                "0xc2",         //0xc2
                                "0xc3",         //0xc3
                                "0xc4",         //0xc4
                                "0xc5",         //0xc5
                                "0xc6",         //0xc6
                                "0xc7",         //0xc7
                                "0xc8",         //0xc8
                                "0xc9",         //0xc9
                                "0xca",         //0xca
                                "0xcb",         //0xcb
                                "0xcc",         //0xcc
                                "0xcd",         //0xcd
                                "0xce",         //0xce
                                "0xcf",         //0xcf
                                "0xd0",         //0xd0
                                "0xd1",         //0xd1
                                "0xd2",         //0xd2
                                "0xd3",         //0xd3
                                "0xd4",         //0xd4
                                "0xd5",         //0xd5
                                "0xd6",         //0xd6
                                "0xd7",         //0xd7
                                "0xd8",         //0xd8
                                "0xd9",         //0xd9
                                "0xda",         //0xda
                                "[",        //0xdb
                                "\\",       //0xdc
                                "]",            //0xdd
                                "'",        //0xde
                                "OEM DF",       //0xdf
                                "OEM E0",       //0xe0
                                "OEM E1",       //0xe1
                                "OEM E2",       //0x32
                                "OEM E3",       //0xe3
                                "OEM E4",       //0xe4
                                "0xe5",         //0xe5
                                "OEM E6",       //0xe6
                                "0xe7",         //0xe7
                                "0xe8",         //0xe8
                                "OEM E9",       //0xe9
                                "OEM EA",       //0xea
                                "OEM EB",       //0xeb
                                "OEM EC",       //0xec
                                "OEM ED",       //0xed
                                "OEM EE",       //0xee
                                "OEM EF",       //0xef
                                "OEM F0",       //0xf0
                                "OEM F1",       //0xf1
                                "OEM F2",       //0xf2
                                "OEM F3",       //0xf3
                                "OEM F4",       //0xf4
                                "OEM F5",       //0xf5
                                "ATTN",         //0xf6
                                "CRSEL",        //0xf7
                                "EXSEL",        //0xf8
                                "ERASE EOF",    //0xf9
                                "PLAY",         //0xfa
                                "ZOOM",         //0xfb
                                "0xfc",         //0xfc
                                "PA1",          //0xfd
                                "CLEAR",        //0xfe
                                "0xff"          //0xff
                            };

class Hotkeys
{
public:
    typedef std::hash_map<uint16, string> hotkey_map_t;

private:
    hotkey_map_t m_hotkeys;

    static inline uint8 get_key(uint16 key_hash) {return ((uint8)(key_hash & 0x00ff)); }
    static inline bool is_mod_ctrl(uint16 key_hash){ return 0!=(key_hash & 0x0100);}
    static inline bool is_mod_alt(uint16 key_hash){ return 0!=(key_hash & 0x0200);}
    static inline bool is_mod_shift(uint16 key_hash){ return 0!=(key_hash & 0x0400);}
    static inline bool is_mod_extended(uint16 key_hash){ return 0!=(key_hash & 0x0800);}

public:
    Hotkeys();
    ~Hotkeys();
    void add(uint16 key_hash, const string & command);
    void remove(uint16 key_hash);
    const string get_command(uint16 key_hash);
    bool exists(uint16 key_hash);
    void write_config(FILE * fp) const;
    static inline uint16 get_key_hash(uint8 key, bool extended, bool ctrl, bool alt, bool shift)
    {
        uint16 hash = key;
        if(ctrl) hash += 0x0100;
        if(alt) hash += 0x0200;
        if(shift) hash += 0x0400;
        if(extended) hash += 0x0800;    
        return hash;
    }
    static inline const string get_text(uint16 key_hash)
    {
        string text = "";
        if(is_mod_extended(key_hash)) text +="EXT ";
        if(is_mod_ctrl(key_hash)) text +="CTRL+";
        if(is_mod_alt(key_hash)) text += "ALT+";
        if(is_mod_shift(key_hash)) text += "SHIFT+";
        text += KEY_TEXT[get_key(key_hash)];
        return(text);
    }
    hotkey_map_t & get_hotkey_list() { return m_hotkeys; }
};

#endif
