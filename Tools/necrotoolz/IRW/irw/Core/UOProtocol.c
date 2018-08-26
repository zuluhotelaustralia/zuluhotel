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
* 	Feb 29th, 2004 -- I started reversing OSI client, Krrios client,
* 	and RunUO one week ago, this file is the compilation of everything
* 	I could find about *all* UO packets.
* 
* 	Jun 07th, 2004 -- Minor changes
* 
* 	TODO: write a packetguide for future devs.
* 
\******************************************************************************/

#include <windows.h>
#include <stdio.h>
#include "UOProtocol.h"
#include "UONetwork.h"

static const char *PacketNames[256] =
{
	"Create character", /* 0x00 */
    "Disconnect notification", /* 0x01 */
    "Move request", /* 0x02 */
    "Talk request", /* 0x03 */
	"GOD: God mode on/off request", /* 0x04 */
    "Attack request", /* 0x05 */
    "Double click on object", /* 0x06 */
    "Pick up object request", /* 0x07 */
    "Drop item request", /* 0x08 */
    "Single click on object|Request look", /* 0x09 */
    "Edit dynamics and statics", /* 0x0a */
    "Edit area", /* 0x0b */
    "Alter tiledata", /* 0x0c */
    "Send new NPC data to the server", /* 0x0d */
    "Edit template data", /* 0x0e */
    "Paperdoll", /* 0x0f */
    "Modify hue data", /* 0x10 */
    "Status window info", /* 0x11 */
    "Request skill/magic/action usage", /* 0x12 */
    "Equip/Unequip item", /* 0x13 */
    "Change item's Z value", /* 0x14 */
    "Follow character", /* 0x15 */
    "Request scripts list", /* 0x16 */
    "Script modifcation commands", /* 0x17 */
    "Add new script to server", /* 0x18 */
    "Modify NPC speech data", /* 0x19 */
    "Object information", /* 0x1a */
    "Login Confirm|Character location and body type", /* 0x1b */
    "Server/player speech", /* 0x1c */
    "Delete object", /* 0x1d */
    "Control animation", /* 0x1e */
    "Cause explosion", /* 0x1f */
    "Draw game player", /* 0x20 */
    "Character move reject", /* 0x21 */
    "Character move ok|Resync request", /* 0x22 */
    "Drag item", /* 0x23 */
    "Draw container", /* 0x24 */
    "Add item to container", /* 0x25 */
    "Kick player", /* 0x26 */
    "Unable to pickup object", /* 0x27 */
    "Unable to drop object|Clear square", /* 0x28 */
    "Object dropped ok", /* 0x29 */
    "Blood mode", /* 0x2a */
    "GOD: response to on/off request", /* 0x2b */
    "Resurrection menu choice", /* 0x2c */
    "Health", /* 0x2d */
    "Character worn item", /* 0x2e */
    "Fight occurring", /* 0x2f */
    "Attack granted ", /* 0x30 */
    "Attack ended", /* 0x31 */
    "GOD: Admin command", /* 0x32 */
    "Pause/resume client", /* 0x33 */
    "Get player status", /* 0x34 */
    "Get resource type", /* 0x35 */
    "Resource type data", /* 0x36 */
    "Move object", /* 0x37 */
    "Follow move", /* 0x38 */
    "Groups", /* 0x39 */
    "Update skills", /* 0x3a */
    "Buy items", /* 0x3b */
    "Items in container", /* 0x3c */
    "Ship", /* 0x3d */
    "Version retrieval", /* 0x3e */
    "Update object chunk", /* 0x3f */
    "Update terrain chunk", /* 0x40 */
    "Update tile data", /* 0x41 */
    "Update art", /* 0x42 */
    "Update animations", /* 0x43 */
    "Update hues", /* 0x44 */
    "Version ok", /* 0x45 */
    "New art work", /* 0x46 */
    "New terrain", /* 0x47 */
    "New animation", /* 0x48 */
    "New hues", /* 0x49 */
    "Destroy art", /* 0x4a */
    "Check client version", /* 0x4b */
    "Modify script names", /* 0x4c */
    "edit script file", /* 0x4d */
    "Personal light level", /* 0x4e */
    "Global light level", /* 0x4f */
    "Bulletin board header", /* 0x50 */
    "Bulleting board message", /* 0x51 */
    "Post bulleting board message", /* 0x52 */
    "Login rejected|Idle warning", /* 0x53 */
    "Play sound effect", /* 0x54 */
    "Login complete", /* 0x55 */
    "Plot course for ships", /* 0x56 */
    "Update regions", /* 0x57 */
    "Create new region", /* 0x58 */
    "Create new effect", /* 0x59 */
    "Update effect", /* 0x5a */
    "Set time of day", /* 0x5b */
    "Restart version", /* 0x5c */
    "Select character", /* 0x5d */
    "Server list", /* 0x5e */
    "Add server", /* 0x5f */
    "Remove server", /* 0x60 */
    "Delete static", /* 0x61 */
    "Move static", /* 0x62 */
    "Load area", /* 0x63 */
    "Attempt to load area request", /* 0x64 */
    "Set weather", /* 0x65 */
    "Show book page", /* 0x66 */
    "Simped", /* 0x67 */
    "Add LS script", /* 0x68 */
    "Friends", /* 0x69 */
    "Notify friend", /* 0x6a */
    "Use key", /* 0x6b */
    "Targeting cursor", /* 0x6c */
    "Play midi music", /* 0x6d */
    "Show animation", /* 0x6e */
    "Secure trading window", /* 0x6f */
    "Play graphical effect", /* 0x70 */
    "Bulleting board commands", /* 0x71 */
    "Set war/peace mode request", /* 0x72 */
    "Ping", /* 0x73 */
    "Open buy window", /* 0x74 */
    "Rename mobile", /* 0x75 */
    "New subserver", /* 0x76 */
    "Update mobile", /* 0x77 */
    "Draw object", /* 0x78 */
    "Get resource", /* 0x79 */
    "Resource data", /* 0x7a */
    "Sequence", /* 0x7b */
    "Open dialog box|Pick object", /* 0x7c */
    "Response to dialog box|Picked object", /* 0x7d */
    "GOD: Get god view data", /* 0x7e */
    "GOD: God view data", /* 0x7f */
    "Account login request", /* 0x80 */
    "Account login ok", /* 0x81 */
    "Account login failed", /* 0x82 */
    "Account delete character", /* 0x83 */
    "Change account password", /* 0x84 */
    "Change character response", /* 0x85 */
    "Characters list", /* 0x86 */
    "Send resources", /* 0x87 */
    "Open paperdoll", /* 0x88 */
    "Corpse clothing", /* 0x89 */
    "Edit trigger", /* 0x8a */
    "Show sign", /* 0x8b */
    "Relay to game server", /* 0x8c */
    "UNUSED PACKET 3", /* 0x8d */
    "Move character", /* 0x8e */
    "UNUSED PACKET 4", /* 0x8f */
    "Open map plot", /* 0x90 */
    "Login to game server request", /* 0x91 */
    "Update multi", /* 0x92 */
    "Open book", /* 0x93 */
    "Alter skill", /* 0x94 */
    "Dye window", /* 0x95 */
    "GOD: Monitor game", /* 0x96 */
    "Player move", /* 0x97 */
    "Alter mobile name", /* 0x98 */
    "Targeting cursor for multi", /* 0x99 */
    "Console entry prompt|Text entry", /* 0x9a */
    "Request GM assistance|Page a GM", /* 0x9b */
    "Assitance response", /* 0x9c */
    "GM Single", /* 0x9d */
    "Sell list", /* 0x9e */
    "Sell reply", /* 0x9f */
    "Select server", /* 0xa0 */
    "Update current health", /* 0xa1 */
    "Update current mana", /* 0xa2 */
    "Update current stamina", /* 0xa3 */
    "Spy on client|Hardware info", /* 0xa4 */
    "Open URL", /* 0xa5 */
    "Tips/notices window", /* 0xa6 */
    "Request tips/notices", /* 0xa7 */
    "Game servers list", /* 0xa8 */
    "List characters and starting cities", /* 0xa9 */
    "OK/Not ok to attack", /* 0xaa */
    "Gump text entry dialog", /* 0xab */
    "Gump text entry response", /* 0xac */
    "Unicode speech (eeew 12 bit!)", /* 0xad */
    "Unicode server speech", /* 0xae */
    "Death animation", /* 0xaf */
    "Generic gump dialog", /* 0xb0 */
    "Generic gump choice", /* 0xb1 */
    "Chat message", /* 0xb2 */
    "Chat text", /* 0xb3 */
    "Target object list", /* 0xb4 */
    "Chat window", /* 0xb5 */
    "Request popup help", /* 0xb6 */
    "Display popup help", /* 0xb7 */
    "Request character profile", /* 0xb8 */
    "Enable T2A/LBR features", /* 0xb9 */
    "Quest arrow", /* 0xba */
    "Ultima messenger", /* 0xbb */
    "Season change", /* 0xbc */
    "Client version message", /* 0xbd */
    "Assist version", /* 0xbe */
    "General information", /* 0xbf */
    "Play hued graphical effect", /* 0xc0 */
    "Predefined client messages", /* 0xc1 */
    "Unicode text entry", /* 0xc2 */
    "GQ request (whatever that is)", /* 0xc3 */
    "Semi visible", /* 0xc4 */
    "Invalid map", /* 0xc5 */
    "Invalid map enable", /* 0xc6 */
    "3D particle effect", /* 0xc7 */
    "Update range change", /* 0xc8 */
    "Trip time", /* 0xc9 */
    "UTrip time", /* 0xca */
    "GQ count (eeps)", /* 0xcb */
    "Text ID and string", /* 0xcc */
    "Unused packet", /* 0xcd */
    "Unknown draw?", /* 0xce */
    "IGR Account login request", /* 0xcf */
    "Configuration File", /* 0xd0 */
    "Logout status", /* 0xd1 */
    "Extended Draw game player", /* 0xd2 */
    "Extended Draw object", /* 0xd3 */
    "Open book", /* 0xd4 */
    "Bogus packet", /* 0xd5 */
    "Property list content", /* 0xd6 */
    "Fight book/system", /* 0xd7 */
    "Custom house data", /* 0xd8 */
    "Improved system info", /* 0xd9 */
    "Mahjong board dialog", /* 0xda */
    "Character transfer data", /* 0xdb */
    "Equipment Description", /* 0xdc */
    "", /* 0xdd */
    "", /* 0xde */
    "", /* 0xdf */
    "", /* 0xe0 */
    "", /* 0xe1 */
    "", /* 0xe2 */
    "", /* 0xe3 */
    "", /* 0xe4 */
    "", /* 0xe5 */
    "", /* 0xe6 */
    "", /* 0xe7 */
    "", /* 0xe8 */
    "", /* 0xe9 */
    "", /* 0xea */
    "", /* 0xeb */
    "", /* 0xec */
    "", /* 0xed */
    "", /* 0xee */
    "", /* 0xef */
    "Custom client packet", /* 0xf0 */
    "", /* 0xf1 */
    "", /* 0xf2 */
    "", /* 0xf3 */
    "", /* 0xf4 */
    "", /* 0xf5 */
    "", /* 0xf6 */
    "", /* 0xf7 */
    "", /* 0xf8 */
    "", /* 0xf9 */
    "", /* 0xfa */
    "", /* 0xfb */
    "", /* 0xfc */
    "", /* 0xfd */
    "", /* 0xfe */
    "", /* 0xff */ /* this is what the buffer returns when the socket is closed, 4 bytes */
};

/* TODO: fill this up :P */
static const char PacketDirection[256];

static const int PacketLengths[256] =
{
	/* 0x00 */ 0x0068, 0x0005, 0x0007, 0x8000, 0x0002, 0x0005, 0x0005, 0x0007, 0x000E, 0x0005, 0x000B, 0x010A, 0x8000, 0x0003, 0x8000, 0x003D,
	/* 0x10 */ 0x00D7, 0x8000, 0x8000, 0x000A, 0x0006, 0x0009, 0x0001, 0x8000, 0x8000, 0x8000, 0x8000, 0x0025, 0x8000, 0x0005, 0x0004, 0x0008,
	/* 0x20 */ 0x0013, 0x0008, 0x0003, 0x001A, 0x0007, 0x0014, 0x0005, 0x0002, 0x0005, 0x0001, 0x0005, 0x0002, 0x0002, 0x0011, 0x000F, 0x000A,
	/* 0x30 */ 0x0005, 0x0001, 0x0002, 0x0002, 0x000A, 0x028D, 0x8000, 0x0008, 0x0007, 0x0009, 0x8000, 0x8000, 0x8000, 0x0002, 0x0025, 0x8000,
	/* 0x40 */ 0x00C9, 0x8000, 0x8000, 0x0229, 0x02C9, 0x0005, 0x8000, 0x000B, 0x0049, 0x005D, 0x0005, 0x0009, 0x8000, 0x8000, 0x0006, 0x0002,
	/* 0x50 */ 0x8000, 0x8000, 0x8000, 0x0002, 0x000C, 0x0001, 0x000B, 0x006E, 0x006A, 0x8000, 0x8000, 0x0004, 0x0002, 0x0049, 0x8000, 0x0031,
	/* 0x60 */ 0x0005, 0x0009, 0x000F, 0x000D, 0x0001, 0x0004, 0x8000, 0x0015, 0x8000, 0x8000, 0x0003, 0x0009, 0x0013, 0x0003, 0x000E, 0x8000,
	/* 0x70 */ 0x001C, 0x8000, 0x0005, 0x0002, 0x8000, 0x0023, 0x0010, 0x0011, 0x8000, 0x0009, 0x8000, 0x0002, 0x8000, 0x000D, 0x0002, 0x8000,
	/* 0x80 */ 0x003E, 0x8000, 0x0002, 0x0027, 0x0045, 0x0002, 0x8000, 0x8000, 0x0042, 0x8000, 0x8000, 0x8000, 0x000B, 0x8000, 0x8000, 0x8000,
	/* 0x90 */ 0x0013, 0x0041, 0x8000, 0x0063, 0x8000, 0x0009, 0x8000, 0x0002, 0x8000, 0x001A, 0x8000, 0x0102, 0x0135, 0x0033, 0x8000, 0x8000,
	/* 0xa0 */ 0x0003, 0x0009, 0x0009, 0x0009, 0x0095, 0x8000, 0x8000, 0x0004, 0x8000, 0x8000, 0x0005, 0x8000, 0x8000, 0x8000, 0x8000, 0x000D,
	/* 0xb0 */ 0x8000, 0x8000, 0x8000, 0x8000, 0x8000, 0x0040, 0x0009, 0x8000, 0x8000, 0x0003, 0x0006, 0x0009, 0x0003, 0x8000, 0x8000, 0x8000,
	/* 0xc0 */ 0x0024, 0x8000, 0x8000, 0x8000, 0x0006, 0x00CB, 0x0001, 0x0031, 0x0002, 0x0006, 0x0006, 0x0007, 0x8000, 0x0001, 0x8000, 0x004E,
	/* 0xd0 */ 0x8000, 0x0002, 0x0019, 0x8000, 0x8000, 0x8000, 0x8000, 0x8000, 0x8000, 0x010C, 0x8000, 0x8000, 0x0009, 0x0000, 0x0000, 0x0000,
	/* 0xe0 */ 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,
	/* 0xf0 */ 0x8000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0004
};

const char *GetPacketName(unsigned char *Buf)
{
	return PacketNames[Buf[0]&0xff];
}

unsigned int GetPacketID(unsigned char *Buf)
{
	return Buf[0]&0xff;
}

int GetPacketLen(unsigned char *Buf, unsigned int Size)
{
	unsigned int PacketLen = PacketLengths[Buf[0]&0xff];

	/* if it has a variable len, get it from the buffer */
	if(PacketLen >= 0x8000) /* '>=' just like the uo client */
	{
		/* if the buffer isnt big enough to get the pkt len, return a huge len =P */
		/* BYTE PKTID, SHORT PKTLEN (3 bytes necessary to retrieve len) */
		if(Size < 3)
			return PACKET_TOOSMALL;
		else /* the buffer has the necessary 3 bytes or more */
			return ((unsigned int)Buf[1] << 8 | (unsigned int)Buf[2]);
	}

	return PacketLen;
}

int CheckPacketIntegrity(unsigned char *Buf, unsigned int Size)
{
	int ret = 0;
	unsigned int PacketID = 0, PacketLen = 0;

	PacketID = Buf[0] & 0xFF;
	PacketLen = GetPacketLen(Buf, Size);

	/* don't bother checking the rest of the packet if there is no rest :P */
    if(PacketLen == PACKET_TOOSMALL)
		return PACKET_TOOSMALL;

	/* check if the packet len and the buffer are of same size */
	if(PacketLen != Size)
	{
		ret |= PACKET_BADSIZE;

		/* avoid bad mem access, the packet len is used to make packet checks */
		if(PacketLen > Size)
			PacketLen = Size;
	}

	/* UO defines the packet len on a maximum of 0x7FFF bytes */
	if(PacketLen >= 0x8000)
		ret |= PACKET_BADLEN;

	switch(PacketID)
	{
		case 0x00:
		{
			/* pattern check */
			if(memcmp(Buf + 1, "\xED\xED\xED\xED", 4))
				ret |= PACKET_MALFORMED;

			/* pattern check */
			if(memcmp(Buf + 5, "\xFF\xFF\xFF\xFF", 4))
				ret |= PACKET_MALFORMED;
		}break;
		case 0x01:
		{
			/* pattern check */
			if(memcmp(Buf + 1, "\xFF\xFF\xFF\xFF", 4))
				ret |= PACKET_MALFORMED;
		}break;
		case 0x03:
		{
			/* the client ALWAYS null-terminates the strings */
			if(Buf[PacketLen - 1] != 0x00)
				ret |= PACKET_MALFORMED;
		}break;
		case 0x04:
		{
			/* we only know 2 types */
			if(Buf[1] != 0 && Buf[1] != 1)
				ret |= PACKET_MALFORMED | PACKET_INTERESTING;
		}break;
		case 0x11:
		{
			/* we only know 3 types */
			if(Buf[42] != 0 && Buf[42] != 1 && Buf[42] != 3 && Buf[42] != 4)
				ret |= PACKET_MALFORMED | PACKET_INTERESTING;
		}break;
		case 0x12:
		{
			/* we only know 4 types */
			if(Buf[3] != 0x24 && Buf[3] != 0x56 && Buf[3] != 0x58 && Buf[3] != 0xC7)
				ret |= PACKET_MALFORMED | PACKET_INTERESTING;
		}break;
	}

	return ret;
}

void ClientPrintRaw(unsigned short Color, unsigned short Font, const char *Text, const char *Tag, va_list Args)
{
	char Final[4096];
	unsigned char *SpeechPacket = NULL;
	int Len = 0;

	vsprintf(Final, Text, Args);
	Len = (int)(44 + strlen(Final) + 1);
	SpeechPacket = (unsigned char *)malloc(Len);
    
	SpeechPacket[0] = 0x1c;
    PackUInt16(SpeechPacket + 1, Len);
	memset(SpeechPacket + 3, 0, 6); /* itemid = model = 0 */
	SpeechPacket[9] = 0x01; /* broadcast */
	PackUInt16(SpeechPacket + 10, Color);
	PackUInt16(SpeechPacket + 12, Font);
	if(Tag != NULL) strncpy((char*)(SpeechPacket + 14), Tag, 30);
	else memset(SpeechPacket + 14, 0, 30);
	strcpy((char*)(SpeechPacket + 44), Final);

	SendToClient(SpeechPacket, Len);
	free(SpeechPacket);

	return;
}

void ClientPrintColor(unsigned short Color, unsigned short Font, const char *Text, ...)
{
    va_list Args;

	va_start(Args, Text);
    ClientPrintRaw(Color, Font, Text, "[IRW]", Args);
	va_end(Args);

	return;
}

void ClientPrint(const char *Text, ...)
{
	va_list Args;

	va_start(Args, Text);
    ClientPrintRaw(0x33, 3, Text, "[IRW]", Args);
	va_end(Args);

	return;
}

void ClientPrintWarning(const char *Text, ...)
{
    va_list Args;

	va_start(Args, Text);
    ClientPrintRaw(0x26, 3, Text, "[IRW]", Args);
	va_end(Args);

	return;
}

void ClientPrintAboveRaw(unsigned int Serial, unsigned short Color, unsigned short Font, const char *Text, va_list Args)
{
	char Final[4096];
	unsigned char *SpeechPacket = NULL;
	int Len = 0;

	vsprintf(Final, Text, Args);
	Len = (int)(44 + strlen(Final) + 1);
	SpeechPacket = (unsigned char *)malloc(Len);
    
	SpeechPacket[0] = 0x1c;
    PackUInt16(SpeechPacket + 1, Len);
	PackUInt32(SpeechPacket + 3, Serial);
	SpeechPacket[9] = 0x06; /* "You see:" */
	PackUInt16(SpeechPacket + 10, Color);
	PackUInt16(SpeechPacket + 12, Font);
	strncpy((char*)(SpeechPacket + 14), "", 30);
	strcpy((char*)(SpeechPacket + 44), Final);

	SendToClient(SpeechPacket, Len);
	free(SpeechPacket);

	return;
}

void ClientPrintAboveColor(unsigned int Serial, unsigned short Color, unsigned short Font, const char *Text, ...)
{
    va_list Args;

	va_start(Args, Text);
    ClientPrintAboveRaw(Serial, Color, Font, Text, Args);
	va_end(Args);

	return;
}

void ClientPrintAbove(unsigned int Serial, const char *Text, ...)
{
    va_list Args;

	va_start(Args, Text);
    ClientPrintAboveRaw(Serial, 0x33, 3, Text, Args);
	va_end(Args);

	return;
}

unsigned int UnpackUInt32(const unsigned char *Buf)
{
    return (Buf[0] << 24) | (Buf[1] << 16) | (Buf[2] << 8) | Buf[3];
}

signed int UnpackSInt32(const unsigned char *Buf)
{
    return (Buf[0] << 24) | (Buf[1] << 16) | (Buf[2] << 8) | Buf[3];
}

unsigned short UnpackUInt16(const unsigned char *Buf)
{
    return (Buf[0] << 8) | Buf[1];
}

void PackUInt32(unsigned char *Buf, unsigned int x)
{
    Buf[0] = (unsigned char)(x >> 24);
    Buf[1] = (unsigned char)((x >> 16) & 0xff);
    Buf[2] = (unsigned char)((x >> 8) & 0xff);
    Buf[3] = (unsigned char)(x & 0xff);

	return;
}

void PackUInt16(unsigned char *Buf, unsigned short x)
{
	Buf[0] = x >> 8;
	Buf[1] = x & 0xff;

	return;
}

/* converts unicode to ascii and returns the amount of characters that couldnt be converted properly */
int UnicodeToAscii(const char *UnicodeText, int Len, char *AsciiText)
{
	int i = 0, NonAsciiCompatible = 0;

	for(i = 0; i < Len; i+=2)
	{
		if(UnicodeText[i] != 0x00)
			NonAsciiCompatible++;

        AsciiText[i/2] = UnicodeText[i+1];
	}

	AsciiText[i++/2] = '\0'; /* null terminate it */
    
	return NonAsciiCompatible;
}
