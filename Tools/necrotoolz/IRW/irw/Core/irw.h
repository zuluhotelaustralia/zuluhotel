/*****************************************************************************\
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
* 	Oct 10th, 2004 -- irw is pretty much done. now its time for plugins!
* 
\*****************************************************************************/


#ifndef _IRW_H_INCLUDED
#define _IRW_H_INCLUDED

/*
* version == <type> a.b-c
* a == major changes, which release this is, first, second...
* b == smaller changes after the release a, but still release a
* c == YEARMONTHDAY
*/
/* this is the version of the SDK/Core release */
#define IRW_VERSION "IRW 0xB4DC0D3 1.4-20050312"


#ifdef __cplusplus
extern "C" {
#endif

/* returns the base address of the base image, which should be UO's */
#define BASE_IMGADDR (void*)GetModuleHandle(NULL)

#ifndef _PLUGINS_H_INCLUDED
typedef struct tagIRWPlugin
{
	char Name[1024];
	HMODULE BaseAddr;
}IRWPlugin;
#endif

/* returns the hInstance of IRW */
HINSTANCE GetIRWInstance(void);
/* returns a string that contains the version of the current IRW release */
char* GetIRWVersion(void);
/* sets IRW up */
void Install(void);
/* returns an internal list and it's size. the list is a pointer to pointers */
void ListPlugins(const IRWPlugin **List, unsigned int *Size);
/* returns the previous value of the hooked function. returns 0 if failed */
void* HookImportedFunction(void *ImageBase, const char *Dll, const char *FuncName, int Ordinal, void *Function);


/*
* CLIENTPRINT EXPORTS
*/

void ClientPrint(const char *Text, ...);
void ClientPrintWarning(const char *Text, ...);
void ClientPrintColor(unsigned short Color, unsigned short Font, const char *Text, ...);
void ClientPrintAbove(unsigned int Serial, const char *Text, ...);
void ClientPrintAboveColor(unsigned int Serial, unsigned short Color, unsigned short Font, char *Text, ...);


/*
* LOGGER EXPORTS
*/

#define NOFILTER_LOG 1
#define NETWORK_LOG 2
#define BUILDER_LOG 4
#define COMPRESSOR_LOG 8
#define API_LOG 16
#define WARNING_LOG 32
#define ERROR_LOG 64
#define CRYPT_LOG 128
#define INTEREST_LOG 256
#define CLIENTIRW_LOG 512 /* client->IRW packet */
#define IRWSERVER_LOG 1024 /* irw->server */
#define SERVERIRW_LOG 2048 /* server->IRW */
#define IRWCLIENT_LOG 4096 /* irw->client */


void MBOut(const char *title, const char *msg, ...);
void LogBlock(int Type);
void LogUnblock(int Type);
void LogPrint(int Type, const char *strFormat, ...);
void LogDump(int Type, unsigned char *pBuffer, int length);


/*
* NETWORK EXPORTS
*/

/* message directions (from) */
#define CLIENT_MESSAGE 0
#define SERVER_MESSAGE 1
#define CLIVER_MESSAGE 2

/* packet handlers return values */
#define EAT_PACKET 0
#define SEND_PACKET 1


BOOL GetDropBadPackets(void);
void SetDropBadPackets(BOOL drop);

void SendToClient(unsigned char *Buf, unsigned int Size);
void SendToServer(unsigned char *Buf, unsigned int Size);


/*
* PROTOCOL EXPORTS
*/

#define BUFFER_TOO_SMALL_FOR_LEN -1

/*
* returns the packet's size based on the (BYTE)packet id or (BYTE)packet id + (WORD)len
* if the packet id has a variable len and the buffer size is smaller than 3 bytes
* BUFFER_TOO_SMALL_FOR_LEN is returned
*/
int GetPacketLen(unsigned char *Buf, unsigned int Size);
/* returns the packet's packet id (the first byte) */
unsigned int GetPacketID(unsigned char *Buf);
/* returns the packet's name */
const char* GetPacketName(unsigned char *Buf);

/* extract/insert big endians from/to buffers */
unsigned int UnpackUInt32(const unsigned char *Buf);
signed int UnpackSInt32(const unsigned char *Buf);
unsigned short UnpackUInt16(const unsigned char *Buf);
void PackUInt32(unsigned char *Buf, unsigned int x);
void PackUInt16(unsigned char *Buf, unsigned short x);


/*
* WORLD EXPORTS
*/

#define ALLOCCHAR(Obj) Obj.Character = (CharacterObject*)malloc(sizeof(CharacterObject));
#define FREECHAR(Obj) { if(Obj.Character != NULL) free(Obj.Character); Obj.Character = NULL; }

/* object flags */
#define OBJ_FLAG_MALE 0x01
#define OBJ_FLAG_FEMALE 0x02
#define OBJ_FLAG_POISONED 0x04
#define OBJ_FLAG_WAR 0x40
#define OBJ_FLAG_HIDDEN 0x80
#define OBJ_FLAG_NOTORIETY (7 << 29) /* notoriety uses 3 bits */

#define GETOBJFLAG(Obj, Flag) (Obj.Flags & Flag)
#define SETOBJFLAG(Obj, Flag) (Obj.Flags |= Flag)
#define CLEAROBJFLAG(Obj, Flag) (Obj.Flags &= ~Flag)

typedef struct tagCharacterInfo
{
	/* all players (npcs and pcs) have these attributes */
	unsigned int LastAttack;
	unsigned short MaxHitPoints;
	unsigned short HitPoints;
	char Name[30 + 1];
	char Direction;

	/* character only attributes */
	unsigned short STR;
	unsigned short DEX;
	unsigned short INT;
	unsigned short MaxStamina;
	unsigned short Stamina;
	unsigned short MaxMana;
	unsigned short Mana;

	unsigned int Gold;
	unsigned short Armor;
	unsigned short Weight;
}CharacterObject;

typedef struct tagObjInfo
{
	unsigned int Container; /* serial of object which contains this item */
	BOOL IsContainer;
	unsigned int Serial;	
	unsigned short Graphic;
	unsigned short Color;
	unsigned short X, Y;
	char Z;
	int Flags;
	int Layer;
	int Quantity;
	BOOL IsEnabled;
	CharacterObject *Character; /* NULL if not a character (npc and pc) */
}GameObject;

#define ITEM_UPDATE 0
#define PLAYER_UPDATE 1

#define INVALID_SERIAL 0xFFFFFFFF
#define INVALID_XY 0xFFFF
#define INVALID_GRAPHIC 0xFFFF
#define INVALID_COLOR 0xFFFF

#define INVALID_IDX -1
#define OBJECT_EXISTS -1
#define OBJECT_NOTFOUND -2
#define OBJECT_INVALID -3

#define LAYER_NONE 0
#define LAYER_ONE_HANDED 1
#define LAYER_TWO_HANDED 2
#define LAYER_SHOES 3
#define LAYER_PANTS 4
#define LAYER_SHIRT 5
#define LAYER_HELM 6  /* hat */
#define LAYER_GLOVES 7
#define LAYER_RING 8
#define LAYER_9 9      /* unused */
#define LAYER_NECK 10
#define LAYER_HAIR 11
#define LAYER_WAIST 12 /* half apron */
#define LAYER_TORSO 13 /* chest armour */
#define LAYER_BRACELET 14
#define LAYER_15 15        /* unused */
#define LAYER_FACIAL_HAIR 16
#define LAYER_TUNIC 17 /* surcoat, tunic, full apron, sash */
#define LAYER_EARRINGS 18
#define LAYER_ARMS 19
#define LAYER_CLOAK 20
#define LAYER_BACKPACK 21
#define LAYER_ROBE 22
#define LAYER_SKIRT 23 /* skirt, kilt */
#define LAYER_LEGS 24  /* leg armour */
#define LAYER_MOUNT 25 /* horse, ostard, etc */
#define LAYER_VENDOR_BUY_RESTOCK 26
#define LAYER_VENDOR_BUY 27
#define LAYER_VENDOR_SELL 28
#define LAYER_BANK 29

/* returns the player's index in the ObjectsList buffer */
int GetPlayerIdx(void);
/* returns the current player serial. INVALID_SERIAL if not set */
unsigned int GetPlayerSerial(void);
/* returns the current player backpack serial. INVALID_SERIAL if not set (world not created) */
unsigned int GetPlayerBackpack(void);
/* returns the serial of the last attacked */
unsigned int GetLastAttack(void);
/* returns the last object used */
unsigned int GetLastObject(void);
/* returns the last container open */
unsigned int GetLastContainer(void);
/* returns the current catchbag */
unsigned int GetCatchBag(void);
/* sets the last attacked */
void SetLastAttack(unsigned int Serial);
/* sets the last object used */
void SetLastObject(unsigned int Serial);
/* sets the last container open */
void SetLastContainer(unsigned int Serial);
/* sets the catchbag */
void SetCatchBag(unsigned int Serial);

/* these functions are very sensitive so be careful when using */
/*
* these functions are either by serial OR by idx, set INVALID_SERIAL or INVALID_IDX
* when not using one of the parameters
*/
int AddObject(unsigned int Serial);
int AddCharacter(unsigned int Serial);
int RemoveObject(unsigned int Serial, int Idx);
int SetItemAsContainer(unsigned int Serial, int Idx);
int SetObjectInfo(unsigned int Serial, int Idx, GameObject Model);
int GetObjectInfo(unsigned int Serial, int Idx, GameObject *Model);
int GetObjectIndex(unsigned int Serial);
unsigned int GetObjectSerial(int Idx);

/* item/object functions */
int CountItemInContainer(unsigned short Graphic, unsigned int Serial);
/*
* these functions create lists. return is NULL for nothing
* else a pointer for the following structure: n elements, idx of each element
*/
int* ListObjectsAtLocation(unsigned short X, unsigned short Y, int Distance);
int* ListItemsInContainer(unsigned int Serial);
/* return is the serial of the object/item or OBJ_NOTFOUND */
unsigned int FindObjectAtLocation(unsigned short Graphic, unsigned short Color, unsigned short X, unsigned short Y, int Distance);
unsigned int FindItemInContainer(unsigned short Graphic, unsigned short Color, unsigned int ContainerSerial);
unsigned int GetItemInLayer(unsigned int ContainerSerial, int Layer);
void WorldCount(int *Objects, int *Characters, int *Total);

/* world interaction functions */
void PickupItem(unsigned int ItemSerial, int Quantity);
void DropItem(unsigned int ItemSerial, unsigned short X, unsigned short Y, int Z);
void DropItemInContainer(unsigned int ItemSerial, unsigned int ContainerSerial);
void MoveToContainer(unsigned int ItemSerial, int Quantity, unsigned int ContainerSerial);
void EquipItem(unsigned int ItemSerial, int Layer);
void UnequipItem(int Layer);
void UseObject(unsigned int Serial);
void ClickObject(unsigned int Serial);
/* calculates the distance between source and target */
int GetDistance(int Sx, int Sy, int Tx, int Ty);


/*
* TARGETTING EXPORTS
*/

#define USE_DISTANCE 3
#define TARGET_OBJECT 0
#define TARGET_TILE 1

typedef void (*IRWTarget) (unsigned int, unsigned short, unsigned short, int);


/* sets the last target */
void SetLastTarget(unsigned int Serial);
/* sets the xyz of the last tile */
void SetLastTile(unsigned short X, unsigned short Y, int Z);
/* returns the last target serial */
unsigned int GetLastTarget(void);
/* returns the xyz of the last tile */
unsigned short GetLastTileX(void);
unsigned short GetLastTileY(void);
int GetLastTileZ(void);

/* requests a target to the client and sets Handler as the callback */
void RequestTarget(int Type, void *Handler);
void WaitTarget(int Type, unsigned int Serial, unsigned short Graphic, unsigned short X, unsigned short Y, int Z);
BOOL ExpectTarget(void);

/* interaction functions */
void TargetRequest(int Type, int Sequence);
void TargetReplyObj(unsigned int Serial, unsigned short Graphic, int Sequence);
void TargetReplyTile(unsigned short Graphic, unsigned short X, unsigned short Y, int Z, int Sequence);
void CancelTargetRequest(int Sequence);


/*
* GUI EXPORTS
*/

/* returns IRW's main window HWND */
HWND GetIRWWindow(void);
/* returns IRW's main tab window HWND */
HWND GetMainTabWindow(void);
/* adds a tab to the main tab */
void AddTab(HWND TabWnd, const char *Name, int Resizeable);

/*
* UO's WINDOW EXPORTS
*/

/* returns the UO client's HWND. returns NULL if not set or destroyed */
HWND GetUOWindow(void);
/* returns a pointer to a buffer with RGB data and fills the bmInfo */
unsigned char* GetUOScreenData(BOOL ShowTitleBar, BITMAPINFO *bmInfo);


/*
* PACKET HANDLER EXPORTS
*/

#ifndef _UOPACKETHANDLER_H_INCLUDED
typedef void (*IRWWorldCallback) (int, unsigned int, int, unsigned char*, int);
typedef int (*IRWPacketHandler) (unsigned char*, int);
typedef void (*IRWPacketReader) (unsigned char*, int);
typedef void (*IRWCommandHandler) (char **, int);

typedef struct tagIRWCmd
{
	const char *Command;
	IRWCommandHandler Handler;
}IRWCmd;

typedef struct tagIRWAlias
{
	char Alias[1024];
	unsigned int Value;
}IRWAlias;
#endif

/* returns 1 if succesful. 0 if not */
int AddWorldCallback(void *Callback);
int AddPacketHandler(int From, int PacketID, void *Handler);
int AddPacketReader(int From, int PacketID, void *Handler);
/* calls the world callbacks */
void HandleWorld(int Type, unsigned int Serial, int Idx, unsigned char *Buf, int Len);
/* checks the readers, internal and external handlers. return is EAT_PACKET or SEND_PACKET */
int HandlePacket(unsigned char *Buf, int Len, int From);

/* returns 1 if succesful. 0 if command exists and -1 if the command buffer is full */
int AddCommand(const char *Name, void *Handler);
/* calls the command for "Text" */
void HandleCommand(char *Text);
/* the name pretty much explains it */
int ArgToInt(char *Arg);

/* returns 1 if the alias exists, 2 if NULL and 0 on success */
int AddAlias(const char *Name);
/* returns 1 if the alias does not exist */
int RemoveAlias(const char *Name);
/* returns 1 if the alias doesnt exist and 0 on success */
int SetAlias(const char *Name, unsigned int Value);
/* returns 1 if the alias doesnt exist and 0 on success */
int GetAlias(const char *Name, unsigned int *Value);

/* these functions will return direct access to the lists, be careful! */
void ListCommands(const IRWCmd **List, unsigned int *Size);
void ListAliases(const IRWAlias **List, unsigned int *Size);

/*
* JOURNAL
*/
/* cleans up the journal from all entries */
void CleanJournal(void);
/* adds text to the journal */
void JournalAdd(char *Text, int Type);
/* removes the line from the journal */
void JournalRemove(unsigned int Line);
/* returns a pointer with the text on line */
char* GetJournalLine(unsigned int Line);
/* returns the line that the text was found at. -1 if not found */
unsigned int IsInJournal(char *Text, int Type);


/*
* REGISTRY STUFF
*/

/* returns a pointer to irw's instalation path */
char* GetIRWPath(void);
/* returns a pointer to the profile in use */
char* GetCurrentProfile(void);

BOOL SetRegistryString(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, char *Buf);
BOOL GetRegistryString(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, char *Buf, DWORD *Size);

BOOL SetRegistryDword(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, DWORD *Value);
BOOL GetRegistryDword(HKEY Key, const char *SubKey, const char *Name, DWORD *dDisp, DWORD *Value);


/*
* PLAYER PROFILE STUFF
*/

BOOL CleanIRWProfileSection(const char *ProfileName, const char *Section);

BOOL GetIRWProfileString(const char *ProfileName, const char *Section,  const char *Key, char *Out, DWORD Size);
BOOL GetIRWProfileInt(const char *ProfileName, const char *Section, const char *Key, int *Out);

BOOL SetIRWProfileString(const char *ProfileName, const char *Section,  const char *Key, const char *Text);
BOOL SetIRWProfileInt(const char *ProfileName, const char *Section, const char *Key, DWORD Value);


#ifdef __cplusplus
}
#endif

#endif
