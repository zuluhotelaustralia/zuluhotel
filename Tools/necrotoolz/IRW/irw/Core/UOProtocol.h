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
* 	Jun 07th, 2004 -- Minor changes.
* 
* 	TODO: write a packetguide for future devs.
* 	The allignment of the table bellow may not seem correct on most IDEs
* 	but it looks fine in UE32 and the alignement should be done there
* 	PS: biggest bitch in the world to make the table bellow...
* 	but it especifies well the packet IDs. (old part of m2a's client protection)
* 
\******************************************************************************/

#ifndef _UOPROTOCOL_H_INCLUDED
#define _UOPROTOCOL_H_INCLUDED

#define CLIENT_MESSAGE 0
#define SERVER_MESSAGE 1
#define CLIVER_MESSAGE 2

#define PACKET_TOOSMALL -1
#define PACKET_INTERESTING 1
#define PACKET_BADSIZE 2
#define PACKET_BADLEN 4
#define PACKET_MALFORMED 8

#define EAT_PACKET 0
#define SEND_PACKET 1

/* size of login packets */
#define LOGIN1_SIZE 62 /* 0x80 - regular loginserver packet */
#define LOGIN3_SIZE 78 /* 0xCF - IGR loginserver packet, enabled by "noid" in registry */
#define LOGIN2_SIZE 65 /* 0x91 - gameserver packet */
#define SEED_SIZE 4

/* socket states */
#define SOCKET_NULL -1
#define SOCKET_LOGIN 0
#define SOCKET_GAME 1

/*
encryption states based on UOKeys.cfg
#	0 - no encryption
#	1 - the same encryption that client uses
#	2 - <=2.0.0 (BlowFish)
#	3 - 2.0.3 (BlowFish+TwoFish)
#	4 - >2.0.3 (TwoFish)
*/
#define CRYPT_NONE   0
#define CRYPT_SAME   1
#define CRYPT_BFISH  2
#define CRYPT_BTFISH 3
#define CRYPT_TFISH  4

/* packet ID's */
#define NEW_CHARACTER_ID              0x00
#define LOGOUT_ID                     0x01
#define REQUEST_MOVE_ID               0x02
#define SPEECH_OLD_ID                 0x03
#define GOD_MODE_ON_ID                0x04
#define ATTACK_ID                     0x05
#define REQUEST_OBJ_USE_ID            0x06
#define REQUEST_GET_OBJ_ID            0x07
#define REQUEST_DROP_OBJ_ID           0x08
#define REQUEST_LOOK_ID               0x09
#define EDIT_ID                       0x0A
#define EDIT_AREA_ID                  0x0B
#define TILE_DATA_ID                  0x0C
#define NPC_DATA_ID                   0x0D
#define TEMPLATE_DATA_ID              0x0E
#define PAPERDOLL_ID                  0x0F
#define HUE_DATA_ID                   0x10
#define MOBILE_STAT_ID                0x11
#define GOD_COMMAND_ID                0x12
#define REQUEST_OBJ_EQUIP_ID          0x13
#define ELEV_CHANGE_ID                0x14
#define FOLLOW_ID                     0x15
#define REQUEST_SCRIPT_NAMES_ID       0x16
#define SCRIPT_TREE_COMMAND_ID        0x17
#define SCRIPT_ATTACH_ID              0x18
#define NPC_CONVO_DATA_ID             0x19
#define OBJ_DATA_ID                   0x1A
#define LOGIN_CONFIRM_ID              0x1B
#define SERVER_SPEECH_ID              0x1C
#define DESTROY_OBJECT_ID             0x1D
#define ANIMATE_ID                    0x1E
#define EXPLODE_ID                    0x1F
#define DRAW_PLAYER_ID                0x20
#define BLOCKED_MOVE_ID               0x21
#define OK_MOVE_ID                    0x22
#define OBJ_MOVE_ID                   0x23
#define OPEN_GUMP_ID                  0x24
#define ADD_OBJ_TO_OBJ_ID             0x25
#define OLD_CLIENT_ID                 0x26
#define GET_OBJ_FAILED_ID             0x27
#define DROP_OBJ_FAILED_ID            0x28
#define DROP_OBJ_OK_ID                0x29
#define BLOOD_ID                      0x2A
#define GOD_MODE_ON_OFF_ID            0x2B
#define DEATH_ID                      0x2C
#define HEALTH_ID                     0x2D
#define EQUIP_ITEM_ID                 0x2E
#define FIGHT_SWING_ID                0x2F
#define ATTACK_OK_ID                  0x30
#define ATTACK_END_ID                 0x31
#define GOD_HACK_MOVER_ID             0x32
#define PAUSE_RESUME_ID               0x33 /* could be GROUP command, whatever that is */
#define GET_PLAYER_STATS_SKILLS_ID    0x34
#define GET_RESOURCE_TYPE_ID          0x35
#define RESOURCE_TILE_DATA_ID         0x36
#define MOVE_OBJECT_ID                0x37
#define FOLLOW_MOVE_ID                0x38
#define GROUPS_ID                     0x39
#define UPDATE_SKILLS_ID              0x3A
#define BUY_ACCEPT_ID                 0x3B
#define ADD_MULTI_OBJ_TO_OBJ_ID       0x3C
#define SHIP_ID                       0x3D
#define VERSIONS_ID                   0x3E
#define UPDATE_OBJ_CHUNK_ID           0x3F
#define UPDATE_TERRAIN_CHUNK_ID       0x40
#define UPDATE_TILE_DATA_ID           0x41
#define UPDATE_ART_ID                 0x42
#define UPDATE_ANIM_ID                0x43
#define UPDATE_HUES_ID                0x44
#define VERSION_OK_ID                 0x45
#define NEW_ART_ID                    0x46
#define NEW_TERRAIN_ID                0x47
#define NEW_ANIM_ID                   0x48
#define NEW_HUES_ID                   0x49
#define DESTROY_ART_ID                0x4A
#define CHECK_VERSION_ID              0x4B
#define SCRIPT_NAMES_ID               0x4C
#define SCRIPT_EDIT_ID                0x4D
#define LIGHT_CHANGE_PERSONAL_ID      0x4E
#define LIGHT_CHANGE_GLOBAL_ID        0x4F
#define BB_HEADER_ID                  0x50 /* BB == Bulletin Board */
#define BB_MESSAGE_ID                 0x51
#define BB_POST_MESSAGE_ID            0x52
#define LOGIN_REJECT_IDLE_ID          0x53
#define PLAY_SOUND_ID                 0x54
#define LOGIN_COMPLETE_ID             0x55
#define MAP_COMMAND_ID                0x56
#define UPDATE_REGIONS_ID             0x57
#define NEW_REGION_ID                 0x58
#define NEW_CONTEXT_FX_ID             0x59
#define UPDATE_CONTEXT_FX_ID          0x5A
#define GAME_TIME_ID                  0x5B
#define RESTART_VERSION_ID            0x5C
#define PRE_LOGIN_ID                  0x5D
#define SERVER_LIST_ID                0x5E
#define SERVER_ADD_ID                 0x5F
#define SERVER_REMOVE_ID              0x60
#define DESTROY_STATIC_ID             0x61
#define MOVE_STATIC_ID                0x62
#define AREA_LOAD_ID                  0x63
#define REQUEST_AREA_LOAD_ID          0x64
#define WEATHER_CHANGE_ID             0x65
#define BOOK_PAGE_ID                  0x66
#define SIMPED_ID                     0x67
#define SCRIPT_LS_ATTACH_ID           0x68
#define FRIENDS_ID                    0x69
#define FRIEND_NOTIFY_ID              0x6A
#define KEY_USE_ID                    0x6B
#define TARGET_ID                     0x6C
#define MUSIC_ID                      0x6D
#define ANIM_ID                       0x6E
#define TRADE_ID                      0x6F
#define PLAY_EFFECT_ID                0x70
#define BB_STUFF_ID                   0x71 /* 7 packets */
#define REQUEST_WAR_MODE_ID           0x72
#define PING_ID                       0x73
#define BUY_OPEN_WINDOW_ID            0x74
#define RENAME_CHARACTER_ID           0x75
#define NEW_SUBSERVER_ID              0x76
#define UPDATE_MOB_ID                 0x77 /* mobile */
#define DRAW_MOB_ID                   0x78
#define RESOURCE_QUERY_ID             0x79
#define RESOURCE_DATA_ID              0x7A
#define SEQUENCE_ID                   0x7B
#define PICK_OBJ_ID                   0x7C
#define PICKED_OBJ_ID                 0x7D
#define GOD_VIEW_QUERY_ID             0x7E
#define GOD_VIEW_DATA_ID              0x7F
#define LOGIN_REQUEST_ID              0x80
#define LOGIN_OK_ID                   0x81
#define LOGIN_FAIL_ID                 0x82
#define DELETE_CHARACTER_ID           0x83
#define CHANGE_ACCOUNT_PWD_ID         0x84
#define CHANGE_CHARACTER_RESULT_ID    0x85
#define ALL_CHARACTERS_ID             0x86
#define SEND_RESOURCES_ID             0x87
#define OPEN_PAPERDOLL_ID             0x88
#define CORPSE_CLOTHING_ID            0x89
#define EDIT_TRIGERS_ID               0x8A
#define SHOW_SIGN_ID                  0x8B
#define RELAY_TO_GAME_SERVER_ID       0x8C
#define UNUSED1_ID                    0x8D
#define MOVE_CHARACTER_ID             0x8E
#define UNUSED2_ID                    0x8F
#define OPEN_COURSE_GUMP_ID           0x90
#define LOGIN_GAME_ID                 0x91
#define UPDATE_MULTI_ID               0x92
#define BOOK_OPEN_ID                  0x93
#define UPDATE_SKILL_ID               0x94
#define DYE_WINDOW_ID                 0x95
#define GOD_MONITOR_GAME_ID           0x96
#define MOVE_PLAYER_ID                0x97
#define NAME_MOB_ID                   0x98
#define TARGET_MULTI_ID               0x99
#define TEXT_ENTRY_ID                 0x9A
#define ASSISTANCE_REQUEST_ID         0x9B /* gm page */
#define ASSISTANCE_RESPONSE_ID        0x9C
#define GM_SINGLE_ID                  0x9D
#define SELL_LIST_ID                  0x9E
#define SELL_REPLY_ID                 0x9F
#define SELECT_SERVER_ID              0xA0
#define UPDATE_HEALTH_ID              0xA1
#define UPDATE_MANA_ID                0xA2
#define UPDATE_STAMINA_ID             0xA3
#define SYSTEM_INFO_ID                0xA4 /* the infamous spy on client */
#define LAUNCH_URL_ID                 0xA5
#define SCROLL_MESSAGE_ID             0xA6
#define REQUEST_TOOLTIP_ID            0xA7
#define LIST_SERVERS_ID               0xA8
#define LIST_CHARACTERS_CITIES_ID     0xA9
#define CURRENT_TARGET_ID             0xAA
#define GUMP_TEXT_ENTRY_QUERY_ID      0xAB
#define GUMP_TEXT_ENTRY_RESPONSE_ID   0xAC
#define SPEECH_UNICODE_ID             0xAD
#define SERVER_SPEECH_UNICODE_ID      0xAE
#define DEATH_ANIM_ID                 0xAF
#define GENERIC_GUMP_SHOW_ID          0xB0
#define GENERIC_GUMP_CHOICE_ID        0xB1
#define CHAT_MESSAGE_ID               0xB2
#define CHAT_TEXT_ID                  0xB3
#define TARGET_OBJECT_LIST_ID         0xB4
#define CHAT_OPEN_ID                  0xB5
#define HELP_REQUEST_ID               0xB6
#define HELP_DATA_UNICODE_ID          0xB7
#define REQUEST_CHARACTER_PROFILE_ID  0xB8
#define ENABLE_FEATURE_ID             0xB9
#define QUEST_ARROW_ID                0xBA
#define ULTIMA_MESSENGER_ID           0xBB /* whatever this is lol */
#define SEASON_INFORMATION_ID         0xBC
#define CLIENT_VERSION_ID             0xBD
#define GENERAL_INFORMATION_ID        0xBF
#define PLAY_EFFECT_HUED_FX_ID        0xC0 /* another one */
#define PREDEFINED_MESSAGE_ID         0xC1
#define TEXT_ENTRY_UNICODE_ID         0xC2
#define GQ_REQUEST_ID                 0xC3
#define SEMIVISIBLE_ID                0xC4
#define INVALID_MAP_ID                0xC5
#define INVALID_MAP_ENABLE_ID         0xC6
#define PLAY_PARTICLE_EFFECT_3D_ID    0xC7
#define UPDATE_RANGE_CHANGE_ID        0xC8
#define TRIP_TIME_ID                  0xC9
#define UTRIP_TIME_ID                 0xCA
#define GQ_COUNT_ID                   0xCB
#define TEXT_ID_STRING_ID             0xCC
#define UNUSED_PACKET_ID              0xCD
#define DRAW_UNKNOWN_ID               0xCE
#define LOGIN_REQUEST_NEW_ID          0xCF
#define CONFIGURATION_FILE_ID         0xD0
#define LOGOUT_STATUS_ID              0xD1
#define DRAW_EXT_PLAYER_ID            0xD2
#define DRAW_EXT_MOB_ID               0xD3
#define OPEN_BOOK_NEW_ID              0xD4
#define BOGUS_PACKET_ID               0xD5 /* no packet handler... should be an OSI emu pkt */
#define LIST_PROPERTY_CONT_ID         0xD6
#define AOS_DATA_ID                   0xD7
#define CUSTOM_HOUSE_ID               0xD8
#define NEW_HARDWARE_INFO_ID          0xD9
#define MAHJONG_DIALOG_ID             0xDA
#define CHAR_TRANSFER_ID              0xDB
#define AOS_EQUIP_DESC_ID             0xDC /* takes two DWORDs and works as the 1st part of the subcmd 0x10 of pkt 0xBF */
#define CUSTOM_CLIENT_PACKET          0xF0


/*
* 0xBF subcmds, from RunUO (server side)
*
* 0x05  05 - ScreenSize (not ingame)
* 0x06  06 - PartyMessage
* 0x07  07 - QuestArrow
* 0x09  09 - DisarmRequest
* 0x0a  10 - StunRequest
* 0x0b  11 - Language (not ingame)
* 0x0c  12 - CloseStatus
* 0x0e  14 - Animate
* 0x0f  15 - Empty (not ingame)
* 0x10  16 - QueryProperties
* 0x13  19 - ContextMenuRequest
* 0x15  21 - ContextMenuResponse
* 0x19  25 - SetAbility
* 0x1a  26 - StatLockChange
* 0x1c  28 - CastSpell
* 0x24  36 - Unknown... ? (RTD: check it out)
* 0x28  40 - GuildGumpRequest
*
*
* 0xBF subcmds, from Krrios client (client side)
*
* 0x04  04 - close gumps
* 0x05  05 - unknown
* 0x06  06 - party system
* 0x07  07 - unknown
* 0x08  08 - set cursor hue/map
* 0x10  16 - equipment description  (everything beyond this point is about items)
* 0x14  20 - mobile popup
* 0x17  23 - open wisdom codex
* 0x18  24 - map patches
* 0x19  25 - extended status
* 0x1b  27 - spellbook content
* 0x1d  29 - custom house
* 0x20  32 - something to do with items... lol
* 0x21  33 - clear active ability... ability icon confirm... :PP so damn sleepy and its only 11pm
* 0x22  34 - damage
*/


/* EXPORTS */
int GetPacketLen(unsigned char *Buf, unsigned int Size);
unsigned int GetPacketID(unsigned char *Buf);
const char* GetPacketName(unsigned char *Buf);
int CheckPacketIntegrity(unsigned char *Buf, unsigned int Size);

unsigned int UnpackUInt32(const unsigned char *Buf);
signed int UnpackSInt32(const unsigned char *Buf);
unsigned short UnpackUInt16(const unsigned char *Buf);
void PackUInt32(unsigned char *Buf, unsigned int x);
void PackUInt16(unsigned char *Buf, unsigned short x);

void ClientPrint(const char *Text, ...);
void ClientPrintWarning(const char *Text, ...);
void ClientPrintColor(unsigned short Color, unsigned short Font, const char *Text, ...);
void ClientPrintRaw(unsigned short Color, unsigned short Font, const char *Text, const char *Tag, va_list Args);
void ClientPrintAbove(unsigned int Serial, const char *Text, ...);
void ClientPrintAboveColor(unsigned int Serial, unsigned short Color, unsigned short Font, const char *Text, ...);
void ClientPrintAboveRaw(unsigned int Serial, unsigned short Color, unsigned short Font, const char *Text, va_list Args);
int UnicodeToAscii(const char *UnicodeText, int Len, char *AsciiText);

#endif
