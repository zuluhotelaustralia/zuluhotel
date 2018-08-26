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
* 	Sept 24th, 2004 -- world handler module
* 
\******************************************************************************/

/*
* Characters are containers as well
* they carry items on different layers (including backpack)
* TODO:
* - make the lists completly dynamic so they can DECREASE ITS SIZE
* - I wonder if I should improve the list handling... its too slow atm
* - should items be deleted after they are in a certain range?
* - add all the packet handlers
* - improve world dumping
* - add skill handling (own module)
* - add walking/player position handling (own module)
*/

#include <windows.h>
#include <math.h>
#include "UOWorld.h"
#include "Logger.h"
#include "UONetwork.h"
#include "UOProtocol.h"
#include "UOTarget.h"
#include "UOPacketHandler.h"
#include "UOJournal.h"

#define FALSE 0
#define TRUE 1

/* list of all objects in the world */
static GameObject *ObjectList = NULL;
static int ObjectCount = 0; /* size of buffer */

static int PlayerIdx = INVALID_IDX;

/* Walk requests sequences */
static char WalkReq[256];



/*******************************************************************************
* 
*   General info
* 
*******************************************************************************/

int GetPlayerIdx(void) { return PlayerIdx; }
unsigned int GetPlayerSerial(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("self", &Value);
	return Value;
}

unsigned int GetPlayerBackpack(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("backpack", &Value);
	return Value;
}

unsigned int GetLastAttack(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("lastattack", &Value);
	return Value;
}

unsigned int GetLastObject(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("lastobject", &Value);
	return Value;
}

unsigned int GetCatchBag(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("catchbag", &Value);
	return Value;
}

unsigned int GetLastContainer(void)
{
	unsigned int Value = INVALID_SERIAL;
	GetAlias("lastcontainer", &Value);
	return Value;
}

void SetLastContainer(unsigned int Serial) { SetAlias("lastcontainer", Serial); return; }
void SetLastAttack(unsigned int Serial){ SetAlias("lastattack", Serial); return; }
void SetLastObject(unsigned int Serial){ SetAlias("lastobject", Serial); return; }
void SetCatchBag(unsigned int Serial){ SetAlias("catchbag", Serial); return; }

/*******************************************************************************
* 
*   World stuff (world clean up, objects management and info)
* 
*******************************************************************************/

void CleanWorld(void)
{
    if(ObjectList != NULL)
	{
		free(ObjectList);
		ObjectList = NULL;
	}

	ObjectCount = 0;
	memset(WalkReq, 0, sizeof(WalkReq));

	PlayerIdx = INVALID_IDX;
	SetAlias("self", INVALID_SERIAL);
	SetAlias("backpack", INVALID_SERIAL);
	SetAlias("lastcontainer", INVALID_SERIAL);
	SetAlias("lastattack", INVALID_SERIAL);
	SetAlias("lastobject", INVALID_SERIAL);
	SetAlias("catchbag", INVALID_SERIAL);

	CleanTarget();
	CleanJournal();

	return;
}

int AddObject(unsigned int Serial)
{
	int i = 0, ObjIdx = 0;

	/* check for duplicate objects */
	ObjIdx = GetObjectIndex(Serial & 0x7fffffff);
	if(ObjIdx != OBJECT_NOTFOUND)
		return OBJECT_EXISTS;

	/* browse the list trying to find a deleted slot */
	for(i = 0; i < ObjectCount; i++)
	{
		/* always privillege the start of the list */
        if(ObjectList[i].IsEnabled == FALSE)
		{
			LogPrint(NOFILTER_LOG, "using deleted item\r\n");
			ObjIdx = i;
			break;
		}
	}

	/* add an object to the list if there isn't any space left */
	if(ObjIdx == OBJECT_NOTFOUND)
	{
		ObjectCount++;
		ObjIdx = ObjectCount - 1;
		ObjectList = (GameObject*)realloc(ObjectList, ObjectCount * sizeof(GameObject));
	}

	ObjectList[ObjIdx].Character = NULL;
	ObjectList[ObjIdx].Flags = 0;
	ObjectList[ObjIdx].IsEnabled = TRUE;
	ObjectList[ObjIdx].Serial = Serial & 0x7fffffff;
	ObjectList[ObjIdx].Container = INVALID_SERIAL;
	ObjectList[ObjIdx].IsContainer = FALSE;
	ObjectList[ObjIdx].Color = 0;
	ObjectList[ObjIdx].Layer = LAYER_NONE;
	ObjectList[ObjIdx].Quantity = 0;
	ObjectList[ObjIdx].Graphic = 0;
	ObjectList[ObjIdx].X = INVALID_XY;
	ObjectList[ObjIdx].Y = INVALID_XY;
	ObjectList[ObjIdx].Z = 0;

	return ObjIdx;
}

int RemoveObject(unsigned int Serial, int Idx)
{
	int ObjIdx = 0;

	/* the player can never be deleted */
	if((Serial & 0x7fffffff) == GetPlayerSerial())
		return OBJECT_INVALID;

	if(Idx == INVALID_IDX)
	{
		if((ObjIdx = GetObjectIndex(Serial & 0x7fffffff)) == OBJECT_NOTFOUND)
			return OBJECT_NOTFOUND;
	}
	else
	{
		if(Idx >= ObjectCount || Idx < 0)
			return INVALID_IDX;

		ObjIdx = Idx;
	}

	/* disable this object */
	ObjectList[ObjIdx].IsEnabled = FALSE;
	FREECHAR(ObjectList[ObjIdx]);
	
	return ObjIdx;
}

int AddCharacter(unsigned int Serial)
{
	unsigned char GetInfo[10];
	int ObjIdx = 0;

	ObjIdx = GetObjectIndex(Serial & 0x7fffffff);
	/* create the character object if it doesnt exist */
	if(ObjIdx == OBJECT_NOTFOUND)
		ObjIdx = AddObject(Serial);

	/* only reset it if it doesnt exist */
	if(ObjectList[ObjIdx].Character == NULL)
	{
		/* helps to keep the memory usage to a minimum */
		ALLOCCHAR(ObjectList[ObjIdx]);

		ObjectList[ObjIdx].Character->MaxHitPoints = 0;
		ObjectList[ObjIdx].Character->HitPoints = 0;
		strncpy(ObjectList[ObjIdx].Character->Name, "<not initialized>", 30);

		ObjectList[ObjIdx].Character->Direction = 0;
		ObjectList[ObjIdx].Character->STR = 0;
		ObjectList[ObjIdx].Character->DEX = 0;
		ObjectList[ObjIdx].Character->INT = 0;
		ObjectList[ObjIdx].Character->MaxStamina = 0;
		ObjectList[ObjIdx].Character->Stamina = 0;
		ObjectList[ObjIdx].Character->MaxMana = 0;
		ObjectList[ObjIdx].Character->Mana = 0;

		ObjectList[ObjIdx].Character->Gold = 0;
		ObjectList[ObjIdx].Character->Armor = 0;
		ObjectList[ObjIdx].Character->Weight = 0;

		/* send an object info request to the server */
		GetInfo[0] = 0x34;
		PackUInt32(GetInfo + 1, 0xedededed);
		GetInfo[5] = 0x04; /* type: get basic stats */
		PackUInt32(GetInfo + 6, ObjectList[ObjIdx].Serial);

		LogPrint(NOFILTER_LOG, "Requesting object info: 0x%08x\r\n", ObjectList[ObjIdx].Serial);
		SendToServer(GetInfo, 10);

		return ObjIdx;
	}

	return OBJECT_EXISTS;
}

int GetObjectIndex(unsigned int Serial)
{
	int i = 0;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		if(ObjectList[i].Serial == (Serial & 0x7fffffff))
			return i;
	}

	return OBJECT_NOTFOUND;
}

unsigned int GetObjectSerial(int Idx)
{
	int i = 0;

	if(Idx >= ObjectCount || Idx < 0)
		return INVALID_IDX;

	if(ObjectList[i].IsEnabled)
		return ObjectList[i].Serial;
	else
		return INVALID_SERIAL;
}

int GetObjectInfo(unsigned int Serial, int Idx, GameObject *Model)
{
	int ObjIdx = 0;
	CharacterObject *Ptr = NULL;

	if(Idx == INVALID_IDX)
	{
		if((ObjIdx = GetObjectIndex(Serial & 0x7fffffff)) == OBJECT_NOTFOUND)
			return OBJECT_NOTFOUND;
	}
	else
	{
		if(Idx >= ObjectCount || Idx < 0)
			return INVALID_IDX;

		ObjIdx = Idx;
	}

	/* copy the object info */
	memcpy(Model, &ObjectList[ObjIdx], sizeof(GameObject));

	/*
	* if the object is not a player set the pointer to null
	* otherwise alloc space and copy the player data
	* the caller is responsable for freeing the player data
	*/
	if(ObjectList[ObjIdx].Character == NULL)
		Model->Character = NULL;
	else
	{
		ALLOCCHAR((*Model));
		memcpy(Model->Character, ObjectList[ObjIdx].Character, sizeof(CharacterObject));
	}

	return ObjIdx;
}

int SetObjectInfo(unsigned int Serial, int Idx, GameObject Model)
{
	int ObjIdx = 0;
	CharacterObject *Ptr = NULL;

	if(Idx == INVALID_IDX)
	{
		if((ObjIdx = GetObjectIndex(Serial & 0x7fffffff)) == OBJECT_NOTFOUND)
			return OBJECT_NOTFOUND;
	}
	else
	{
		if(Idx >= ObjectCount || Idx < 0)
			return INVALID_IDX;

		ObjIdx = Idx;
	}

	ObjectList[ObjIdx].Serial &= 0x7fffffff;

	Ptr = ObjectList[ObjIdx].Character;
	ObjectList[ObjIdx] = Model;
	ObjectList[ObjIdx].Character = Ptr;
	if(ObjectList[ObjIdx].Character != NULL && Model.Character != NULL)
		memcpy(ObjectList[ObjIdx].Character, Model.Character, sizeof(CharacterObject));

	return ObjIdx;
}

int SetItemAsContainer(unsigned int Serial, int Idx)
{
	int ObjIdx = 0;

	if(Idx == INVALID_IDX)
	{
		if((ObjIdx = GetObjectIndex(Serial & 0x7fffffff)) == OBJECT_NOTFOUND)
			return OBJECT_NOTFOUND;
	}
	else
	{
		if(Idx >= ObjectCount || Idx < 0)
			return INVALID_IDX;

		ObjIdx = Idx;
	}
	
	ObjectList[ObjIdx].IsContainer = TRUE;
	return ObjIdx;
}

void WorldDump(void)
{
	int i = 0, UsedCount = 0, CharCount = 0;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == TRUE)
			UsedCount++;

		if(ObjectList[i].IsEnabled == TRUE && ObjectList[i].Character != NULL)
			CharCount++;
	}

	LogPrint(NOFILTER_LOG, ":WORLD: Exiting with %d objects open. %d used and %d characters\r\n",
			ObjectCount, UsedCount, CharCount);

	LogPrint(NOFILTER_LOG, ":WORLD: Size of world buffer: %d bytes GameObject: %d CharacterObject: %d\r\n",
			ObjectCount*sizeof(GameObject), sizeof(GameObject), sizeof(CharacterObject));

	return;
}

void WorldCount(int *Objects, int *Characters, int *Total)
{
	int i = 0, ObjCount = 0, CharCount = 0;

	*Objects = 0;
	*Characters = 0;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == TRUE)
			ObjCount++;

		if(ObjectList[i].IsEnabled == TRUE && ObjectList[i].Character != NULL)
			CharCount++;
	}
    
	*Objects = ObjCount;
	*Characters = CharCount;
	*Total = ObjectCount;

	return;
}

int CountItemInContainer(unsigned short Graphic, unsigned int Serial)
{
	int i = 0, Count = 0;

    for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		if(ObjectList[i].Container == (Serial & 0x7fffffff) && ObjectList[i].Graphic == Graphic)
		{
			/* if this item is a container, add to the count the items in it */
			if(ObjectList[i].IsContainer)
				Count += CountItemInContainer(Graphic, ObjectList[i].Serial);

			/* add this item to the count */
			Count += ObjectList[i].Quantity;
		}
	}
	
	return Count;
}

/* CountObjectAtLocation was not added cause... well... its useless :P */

int* ListObjectsAtLocation(unsigned short X, unsigned short Y, int Distance)
{
	return NULL;
}

/* returns an index list of all items in the container */
int* ListItemsInContainer(unsigned int Serial)
{
	int *IdxList = NULL, *ContainerList = NULL;
	int i = 0, IdxCount = 0, ContainerCount = 0;

	/* skip the first item in the list, its reserved for the list's size */
	IdxCount++;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		if(ObjectList[i].Container == (Serial & 0x7fffffff))
		{
			IdxCount++;
			IdxList = (int*)realloc(IdxList, IdxCount * sizeof(int));
			IdxList[IdxCount - 1] = i;

			/* if the object is a container, list the items in it, copy to our list and free the obj's list */
			if(ObjectList[i].IsContainer)
			{
				ContainerList = ListItemsInContainer(ObjectList[i].Serial);
				ContainerCount = ContainerList[0] - 1;
				/* if the list doesn't have any items, delete it */
				if(ContainerCount == 0)
					free(ContainerList);
				else /* copy the list then delete it */
				{
					IdxCount += ContainerCount;
					IdxList = (int*)realloc(IdxList, IdxCount * sizeof(int));
					memcpy(IdxList + IdxCount - ContainerCount, ContainerList, ContainerCount * sizeof(int));
					free(ContainerList);
				}
			}
		}
	}

	/* set the size of the list, if there are no items in it, destroy the list */
	if(IdxCount > 1)
		IdxList[0] = IdxCount;
	else
	{
		free(IdxList);
		IdxList = NULL;
	}

	return IdxList;
}

unsigned int FindObjectAtLocation(unsigned short Graphic, unsigned short Color, unsigned short X, unsigned short Y, int Distance)
{
	int i = 0;
	unsigned int ObjSerial = INVALID_SERIAL;

	RECT Box = { X - Distance, Y - Distance, X + Distance + 1, Y + Distance +1};
	POINT p;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		/* ignore items in backpacks and too far away */
		p.x = ObjectList[i].X; p.y = ObjectList[i].Y;
		if(!PtInRect(&Box, p)) continue;

		/* by graphic only */
		if(Color == INVALID_COLOR && Graphic != INVALID_GRAPHIC && ObjectList[i].Graphic == Graphic)
				return ObjSerial;

		/* by color only */
		if(Graphic == INVALID_GRAPHIC && Color != INVALID_COLOR && ObjectList[i].Color == Color)
			return ObjSerial;

		/* by graphic and color */
		if( Graphic != INVALID_GRAPHIC && Color != INVALID_COLOR &&
			ObjectList[i].Color == Color && ObjectList[i].Graphic == Graphic )
			return ObjSerial;
	}

	/* if the search was for graphic&color but only a match by graphic was found */
	if(ObjSerial != INVALID_SERIAL)
		return ObjSerial;


    return INVALID_SERIAL;
}

unsigned int FindItemInContainer(unsigned short Graphic, unsigned short Color, unsigned int ContainerSerial)
{
	int i = 0;
	unsigned int ObjSerial = INVALID_SERIAL;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		if(ObjectList[i].Container == (ContainerSerial & 0x7fffffff))
		{
			/* container inside container, search in it as well */
			if(ObjectList[i].IsContainer)
			{
				ObjSerial = FindItemInContainer(Graphic, Color, ObjectList[i].Serial);
				if(ObjSerial != INVALID_SERIAL)
					return ObjSerial;
			}

			/* by graphic only */
			if(Color == INVALID_COLOR && Graphic != INVALID_GRAPHIC && ObjectList[i].Graphic == Graphic)
					return ObjectList[i].Serial;

			/* by color only */
			if(Graphic == INVALID_GRAPHIC && Color != INVALID_COLOR && ObjectList[i].Color == Color)
				return ObjectList[i].Serial;

			/* by graphic and color */
			if( Graphic != INVALID_GRAPHIC && Color != INVALID_COLOR &&
				ObjectList[i].Color == Color && ObjectList[i].Graphic == Graphic )
				return ObjectList[i].Serial;
		}
	}

	return INVALID_SERIAL;
}

unsigned int GetItemInLayer(unsigned int ContainerSerial, int Layer)
{
	int i = 0;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled == FALSE) continue;

		if(ObjectList[i].Container == ContainerSerial && ObjectList[i].Layer == Layer)
			return ObjectList[i].Serial;
	}

	return INVALID_SERIAL;
}


/*******************************************************************************
* 
*   Packet handlers
* 
*******************************************************************************/

int IRWServer_EnterWorld(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	int Idx = 0;

	/* clean up the world first */
	LogPrint(NOFILTER_LOG, "Cleaning the world\r\n");
	CleanWorld();

	SetAlias("self", UnpackUInt32(Packet + 1));
	PlayerIdx = Idx = AddCharacter(GetPlayerSerial());
	GetObjectInfo(GetPlayerSerial(), Idx, &TmpObj); /* get the current info so we wont override it */
    
	/* update it */
	TmpObj.Graphic = UnpackUInt16(Packet + 9);
	TmpObj.X = UnpackUInt16(Packet + 9);
	TmpObj.Y = UnpackUInt16(Packet + 13);
	TmpObj.Z = UnpackUInt16(Packet + 15) & 0xff;
	TmpObj.Character->Direction = Packet[17];

	SetObjectInfo(GetPlayerSerial(), Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, GetPlayerSerial(), Idx, Packet, Len);
	FREECHAR(TmpObj); /* if we arent going to use it anymore, delete it */

	return SEND_PACKET;
}

int IRWServer_CharStatus(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 3);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);
	
	TmpObj.Character->MaxHitPoints = UnpackUInt16(Packet + 39);
	TmpObj.Character->HitPoints = UnpackUInt16(Packet + 37);
	strncpy(TmpObj.Character->Name, (const char*)(Packet + 7), 30);

	if(Len > 42 && Packet[42] != 0)
	{
		if(!Packet[43]) /* 0 == male */
		{
			SETOBJFLAG(TmpObj, OBJ_FLAG_MALE);
			CLEAROBJFLAG(TmpObj, OBJ_FLAG_FEMALE);
		}
		else /* 1 == female */
		{
			SETOBJFLAG(TmpObj, OBJ_FLAG_FEMALE);
			CLEAROBJFLAG(TmpObj, OBJ_FLAG_MALE);
		}

		TmpObj.Character->STR = UnpackUInt16(Packet + 44);
		TmpObj.Character->DEX = UnpackUInt16(Packet + 44 + 2);
		TmpObj.Character->INT = UnpackUInt16(Packet + 44 + 4);
		TmpObj.Character->Stamina = UnpackUInt16(Packet + 44 + 6);
		TmpObj.Character->MaxStamina = UnpackUInt16(Packet + 44 + 8);
		TmpObj.Character->Mana = UnpackUInt16(Packet + 44 + 10);
		TmpObj.Character->MaxMana = UnpackUInt16(Packet + 44 + 12);
		TmpObj.Character->Gold = UnpackUInt16(Packet + 44 + 14);
		TmpObj.Character->Armor = UnpackUInt16(Packet + 44 + 18);
		TmpObj.Character->Weight = UnpackUInt16(Packet + 44 + 20);
	}

	/* TODO: add support to flags 3 and 4 and check the client's asm for flag 2 */

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	return SEND_PACKET;
}

int IRWServer_UpdateItem(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 3);
	Idx = AddObject(Serial);
	TmpObj.Character = NULL; /* this is an object, not a player */
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.Graphic = UnpackUInt16(Packet + 7);

	if(Serial & 0x80000000)
	{
		/* if the item is a body, what should be the quantity is nothing but the graphic id */
		if(TmpObj.Graphic != 0x620)
			TmpObj.Quantity = UnpackUInt16(Packet + 9);

		Packet += 2;
	}

	if(TmpObj.Graphic & 0x8000)
		Packet++;

	TmpObj.X = UnpackUInt16(Packet + 9);
	TmpObj.Y = UnpackUInt16(Packet + 11);

	if(TmpObj.X & 0x8000)
		Packet++;

	TmpObj.Z = Packet[13] & 0xff;

	if(TmpObj.Y & 0x8000)
		TmpObj.Color = UnpackUInt16(Packet + 14);

	/* only these bits are used, the others are flags */
	TmpObj.X &= 0x7fff;
	TmpObj.Y &= 0x3fff;

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(ITEM_UPDATE, Serial, Idx, Packet, Len);

	return SEND_PACKET;
}

int IRWServer_DeleteItem(unsigned char *Packet, int Len)
{
	unsigned int Serial = UnpackUInt32(Packet + 1);

	RemoveObject(Serial, INVALID_IDX);
	return SEND_PACKET;
}

int IRWServer_UpdateCreature(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.Graphic = UnpackUInt16(Packet + 5);
	TmpObj.Color = UnpackUInt16(Packet + 8);

	if(Packet[10] & 0x04) SETOBJFLAG(TmpObj, OBJ_FLAG_POISONED);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_POISONED);

	if(Packet[10] & 0x40) SETOBJFLAG(TmpObj, OBJ_FLAG_WAR);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_WAR);

	if(Packet[10] & 0x80) SETOBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);

	TmpObj.X = UnpackUInt16(Packet + 11);
	TmpObj.Y = UnpackUInt16(Packet + 13);
	TmpObj.Character->Direction = Packet[17] & 0x0f;
	TmpObj.Z = Packet[18] & 0xff;

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	return SEND_PACKET;
}

int IRWServer_OpenContainer(unsigned char *Packet, int Len)
{
	unsigned int Serial = INVALID_SERIAL;

	Serial = UnpackUInt32(Packet + 1);
	SetLastContainer(Serial);
	SetItemAsContainer(Serial, AddObject(Serial));

	return SEND_PACKET;
}

int IRWClient_DropItem(unsigned char *Packet, int Len)
{
	unsigned int Serial = UnpackUInt32(Packet+10);

	/* if dropping an item to the backpack, move it to the catchbag */
	if(Serial == GetPlayerBackpack() && GetCatchBag() != INVALID_SERIAL)
		PackUInt32(Packet + 10, GetCatchBag());

	return SEND_PACKET; 
}


int IRWServer_AddToContainer(unsigned char *Packet, int Len)
{
	GameObject TmpItem;
	unsigned int ItemSerial = INVALID_SERIAL, ContainerSerial = INVALID_SERIAL;
	int ItemIdx = 0, ContainerIdx = 0;

	ItemSerial = UnpackUInt32(Packet + 1);
	ItemIdx = AddObject(ItemSerial);
	TmpItem.Character = NULL;
	GetObjectInfo(ItemSerial, ItemIdx, &TmpItem);

	ContainerSerial = UnpackUInt32(Packet + 14);
	ContainerIdx = AddObject(ContainerSerial);
	SetItemAsContainer(ContainerSerial, ContainerIdx);

	TmpItem.Container = ContainerSerial & 0x7fffffff;
	TmpItem.Graphic = UnpackUInt16(Packet + 5);
	TmpItem.Quantity = UnpackUInt16(Packet + 8);
	TmpItem.X = UnpackUInt16(Packet + 10);
	TmpItem.Y = UnpackUInt16(Packet + 12);
	TmpItem.Color = UnpackUInt16(Packet + 18);

	SetObjectInfo(ItemSerial, ItemIdx, TmpItem);
	HandleWorld(ITEM_UPDATE, ItemSerial, ItemIdx, Packet, Len);

	return SEND_PACKET;
}

int IRWServer_ClearSquare(unsigned char *Packet, int Len)
{
	return SEND_PACKET;
}

int IRWServer_EquipItem(unsigned char *Packet, int Len)
{
	/*
	* this message is received for items drawn on the chars
	* so place them to the player container
	*/
	GameObject TmpItem;
	unsigned int ItemSerial = INVALID_SERIAL, OwnerSerial = INVALID_SERIAL;
	int ItemIdx = 0, OwnerIdx = 0;

	ItemSerial = UnpackUInt32(Packet + 1);
	ItemIdx = AddObject(ItemSerial);
	TmpItem.Character = NULL;
	GetObjectInfo(ItemSerial, ItemIdx, &TmpItem);

	OwnerSerial = UnpackUInt32(Packet + 9);
	OwnerIdx = AddCharacter(OwnerSerial);
	SetItemAsContainer(OwnerSerial, OwnerIdx);

	TmpItem.Container = OwnerSerial & 0x7fffffff;
	TmpItem.Graphic = UnpackUInt16(Packet + 5);
	TmpItem.Layer = Packet[8] & 0xff;
	TmpItem.Color = UnpackUInt16(Packet + 13);

	SetObjectInfo(ItemSerial, ItemIdx, TmpItem);
	HandleWorld(ITEM_UPDATE, ItemSerial, ItemIdx, Packet, Len);

	return SEND_PACKET;
}

int IRWServer_UpdateSkills(unsigned char *Packet, int Len)
{

	return SEND_PACKET;
}

/* RTD: HANDLEWORLD HERE MIGHT SPICE THINGS UP A BIT! :P */
int IRWServer_UpdateContainer(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int ItemSerial = INVALID_SERIAL, ContainerSerial = INVALID_SERIAL;
	int i = 0, ItemIdx = 0, ContainerIdx = 0, ItemCount = 0;

	ItemCount = UnpackUInt16(Packet + 3);

	if(ItemCount <= 0)
	{
		LogPrint(NOFILTER_LOG | ERROR_LOG, ":ERROR: Update container packet is fucked up\r\n");
		return EAT_PACKET;
	}

	/* the first part of the packet is 5 bytes long. each item segment is 19 bytes long */
	for(i = 0; i < ItemCount; i++)
	{
        ItemSerial = UnpackUInt32(Packet + 5 + (19*i));
		ItemIdx = AddObject(ItemSerial);
		TmpObj.Character = NULL;
		GetObjectInfo(ItemSerial, ItemIdx, &TmpObj);

		ContainerSerial = UnpackUInt32(Packet + 5 + (19*i) + 13);
		ContainerIdx = AddObject(ContainerSerial);
		SetItemAsContainer(ContainerSerial, ContainerIdx);

		TmpObj.Container =  ContainerSerial & 0x7fffffff;
		TmpObj.Graphic = UnpackUInt16(Packet + 5 + (19*i) + 4);
		TmpObj.Quantity = UnpackUInt16(Packet + 5 + (19*i) + 7);
		TmpObj.X = UnpackUInt16(Packet + 5 + (19*i) + 9);
		TmpObj.Y = UnpackUInt16(Packet + 5 + (19*i) + 11);
		TmpObj.Color = UnpackUInt16(Packet + 5 + (19*i) + 17);

		SetObjectInfo(ItemSerial, ItemIdx, TmpObj);
		HandleWorld(ITEM_UPDATE, ItemSerial, ItemIdx, Packet, Len);
	}

	return SEND_PACKET;
}

int IRWServer_UpdatePlayerPos(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.X = UnpackUInt16(Packet + 7);
	TmpObj.Y = UnpackUInt16(Packet + 9);
	TmpObj.Z = Packet[11] & 0xff;
	TmpObj.Character->Direction = Packet[12] & 0xff;
	TmpObj.Color = UnpackUInt16(Packet + 13);
	
	if(Packet[15] & 0x04) SETOBJFLAG(TmpObj, OBJ_FLAG_POISONED);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_POISONED);

	if(Packet[15] & 0x40) SETOBJFLAG(TmpObj, OBJ_FLAG_WAR);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_WAR);

	if(Packet[15] & 0x80) SETOBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	return SEND_PACKET;
}

/* where's Cartman to shout "bitch!" when we need to hear it? */
int IRWServer_UpdatePlayer(unsigned char *Packet, int Len)
{
	GameObject TmpChar, TmpItem;
    unsigned int CharSerial = INVALID_SERIAL, ItemSerial = INVALID_SERIAL;
	int CharIdx = 0, ItemIdx = 0;

	/* handle the character data */
	CharSerial = UnpackUInt32(Packet + 3);
	CharIdx = AddCharacter(CharSerial);
	SetItemAsContainer(CharSerial, CharIdx);
	GetObjectInfo(CharSerial, CharIdx, &TmpChar);

	TmpChar.Graphic = UnpackUInt16(Packet + 7);

	if(CharSerial & 0x80000000)
	{
		TmpChar.Color = UnpackUInt16(Packet + 9);
		Packet += 2;
	}

	TmpChar.X = UnpackUInt16(Packet + 9);
	TmpChar.Y = UnpackUInt16(Packet + 11);

	if(TmpChar.X & 0x8000)
	{
		TmpChar.Character->Direction = Packet[13] & 0xff;
		Packet++;
	}

	TmpChar.Z = Packet[13] & 0xff;
	TmpChar.Color = UnpackUInt16(Packet + 15);

	if(Packet[17] & 0x04) SETOBJFLAG(TmpChar, OBJ_FLAG_POISONED);
	else CLEAROBJFLAG(TmpChar, OBJ_FLAG_POISONED);

	if(Packet[17] & 0x40) SETOBJFLAG(TmpChar, OBJ_FLAG_WAR);
	else CLEAROBJFLAG(TmpChar, OBJ_FLAG_WAR);

	if(Packet[17] & 0x80) SETOBJFLAG(TmpChar, OBJ_FLAG_HIDDEN);
	else CLEAROBJFLAG(TmpChar, OBJ_FLAG_HIDDEN);

	/* set the 3 bits of notoriety */
	CLEAROBJFLAG(TmpChar, OBJ_FLAG_NOTORIETY);
	SETOBJFLAG(TmpChar, Packet[18] << 29);

	TmpChar.X &= 0x7fff;
	TmpChar.Y &= 0x3fff;

	/* finally! I hope items are easier... */
	SetObjectInfo(CharSerial, CharIdx, TmpChar);
	HandleWorld(PLAYER_UPDATE, CharSerial, CharIdx, Packet, Len);
	FREECHAR(TmpChar);

	/* set the pointer to the start of the loop and handle the items data */
	Packet += 19;
	while(*((unsigned int*)Packet) != 0)
	{
		/* yes items are easier haha */
		ItemSerial = UnpackUInt32(Packet);
		ItemIdx = AddObject(ItemSerial);
		TmpItem.Character = NULL;
		GetObjectInfo(ItemSerial, ItemIdx, &TmpItem);

		TmpItem.Container = CharSerial & 0x7fffffff;
		TmpItem.Graphic = UnpackUInt16(Packet + 4);
		TmpItem.Layer = Packet[6] & 0xff;

		if(TmpItem.Graphic & 0x8000)
		{
			TmpItem.Color = UnpackUInt16(Packet + 7);
			Packet += 2;
		}

		if(CharSerial == GetPlayerSerial() && TmpItem.Layer == LAYER_BACKPACK)
		{
			/* the catchbag is by default the player's backpack */
			SetAlias("backpack", ItemSerial);
			if(GetCatchBag() == INVALID_SERIAL)
				SetCatchBag(ItemSerial);

			LogPrint(NOFILTER_LOG, "Player 0x%08x with backpack 0x%08x\r\n",
					GetPlayerSerial(), GetPlayerBackpack());
		}

        TmpItem.Graphic &= 0x7fff;
		SetObjectInfo(ItemSerial, ItemIdx, TmpItem);
		HandleWorld(ITEM_UPDATE, ItemSerial, ItemIdx, Packet, Len);

		Packet += 7;
	}

	return SEND_PACKET;
}

int IRWServer_UpdateHealth(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.Character->HitPoints = UnpackUInt16(Packet + 7);
	TmpObj.Character->MaxHitPoints = UnpackUInt16(Packet + 5);

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	return SEND_PACKET;
}

int IRWServer_UpdateMana(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.Character->Mana = UnpackUInt16(Packet + 7);
	TmpObj.Character->MaxMana = UnpackUInt16(Packet + 5);

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	if(Serial != GetPlayerSerial())
	{
		LogPrint(NOFILTER_LOG | INTEREST_LOG, "Server bug. Mana for NPC %08x Max: %d Cur: %d",
				Serial, TmpObj.Character->MaxMana, TmpObj.Character->Mana);
	}

	return SEND_PACKET;
}

int IRWServer_UpdateStamina(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	TmpObj.Character->Stamina = UnpackUInt16(Packet + 7);
	TmpObj.Character->MaxStamina = UnpackUInt16(Packet + 5);

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	if(Serial != GetPlayerSerial())
	{
		LogPrint(NOFILTER_LOG | INTEREST_LOG, "Server error. Stamina for NPC %08x Max: %d Cur: %d",
				Serial, TmpObj.Character->MaxStamina, TmpObj.Character->Stamina);
	}

	return SEND_PACKET;
}

int IRWServer_WarPeace(unsigned char *Packet, int Len)
{
	/* not needed since I already hook packet 0x77 with UpdatePlayerPos */
	return SEND_PACKET;
}

int IRWServer_Paperdoll(unsigned char *Packet, int Len)
{
	GameObject TmpObj;
	unsigned int Serial = INVALID_SERIAL;
	int Idx = 0;

	Serial = UnpackUInt32(Packet + 1);
	Idx = AddCharacter(Serial);
	GetObjectInfo(Serial, Idx, &TmpObj);

	if(Packet[65] & 0x04) SETOBJFLAG(TmpObj, OBJ_FLAG_POISONED);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_POISONED);

	if(Packet[65] & 0x40) SETOBJFLAG(TmpObj, OBJ_FLAG_WAR);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_WAR);

	if(Packet[65] & 0x80) SETOBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);
	else CLEAROBJFLAG(TmpObj, OBJ_FLAG_HIDDEN);

	SetObjectInfo(Serial, Idx, TmpObj);
	HandleWorld(PLAYER_UPDATE, Serial, Idx, Packet, Len);
	FREECHAR(TmpObj);

	return SEND_PACKET;
}

int IRWServer_Fight(unsigned char *Packet, int Len)
{
	int i = 0;

	for(i = 0; i < ObjectCount; i++)
	{
		if(ObjectList[i].IsEnabled && ObjectList[i].Serial == UnpackUInt32(Packet + 2))
		{
			ObjectList[i].Character->LastAttack = UnpackUInt32(Packet + 6);
			break;
		}
	}

	return SEND_PACKET;
}

int IRWClient_Attack(unsigned char *Packet, int Len)
{
    SetLastAttack(UnpackUInt32(Packet + 1));
	return SEND_PACKET;
}

int IRWClient_DoubleClick(unsigned char *Packet, int Len)
{
    SetLastObject(UnpackUInt32(Packet + 1));
	return SEND_PACKET;
}

int IRWClient_PlayerRequestWalk(unsigned char *Packet, int Len)
{
	/* assign the direction of this walk request */
    WalkReq[Packet[2]] = Packet[1] & 0x0F;
    return SEND_PACKET;
}

int IRWServer_PlayerConfirmWalk(unsigned char *Packet, int Len)
{
    char NewDirection = WalkReq[Packet[1]];

	/* if the direction changed, the player is just turning */
	if(ObjectList[PlayerIdx].Character->Direction != NewDirection)
		ObjectList[PlayerIdx].Character->Direction = NewDirection; 
    else /* if the direction is the same, we got movement */
    {
       switch (NewDirection)
       {
		   /* North */
           case 0: ObjectList[PlayerIdx].Y--;                                break;
		   /* Northeast */
           case 1: ObjectList[PlayerIdx].X++; ObjectList[PlayerIdx].Y--;     break;
		   /* East */
           case 2: ObjectList[PlayerIdx].X++;                                break;
		   /* Southeast */
           case 3: ObjectList[PlayerIdx].X++; ObjectList[PlayerIdx].Y++;     break;
		   /* South */
           case 4: ObjectList[PlayerIdx].Y++;                                break;
		   /* Southwest */
           case 5: ObjectList[PlayerIdx].X--; ObjectList[PlayerIdx].Y++;     break;
		   /* West */
           case 6: ObjectList[PlayerIdx].X--;                                break;
		   /* Northwest */
           case 7: ObjectList[PlayerIdx].X--; ObjectList[PlayerIdx].Y--;     break;
       }
    }

    return SEND_PACKET;
}


/*******************************************************************************
* 
*  World interactions
* 
*******************************************************************************/

/*
* RTD: when you pickup an item, it's not longer in your possession
* the item remains as a "ghost" item in the world. if you die while
* you're holding an item it will still be in your possession
* so if you drop it to your backpack. you have the item back.
* *cheat hint ^^* 
*/
void PickupItem(unsigned int ItemSerial, int Quantity)
{
	unsigned char PickupPacket[7];

	PickupPacket[0] = 0x07;
	PackUInt32(PickupPacket + 1, ItemSerial);
	PackUInt16(PickupPacket + 5, Quantity);

	SendToServer(PickupPacket, 7);

	return;
}

void DropItem(unsigned int ItemSerial, unsigned short X, unsigned short Y, int Z)
{
	unsigned char DropPacket[14];

	DropPacket[0] = 0x08;
	PackUInt32(DropPacket + 1, ItemSerial);
	PackUInt16(DropPacket + 5, X);
	PackUInt16(DropPacket + 7, Y);
	DropPacket[9] = Z & 0xff;
	PackUInt32(DropPacket + 10, INVALID_SERIAL);

	SendToServer(DropPacket, 14);

	return;
}

void DropItemInContainer(unsigned int ItemSerial, unsigned int ContainerSerial)
{
	unsigned char DropPacket[14];

	DropPacket[0] = 0x08;
	PackUInt32(DropPacket + 1, ItemSerial);
	PackUInt16(DropPacket + 5, INVALID_XY);
	PackUInt16(DropPacket + 7, INVALID_XY);
	DropPacket[9] = 0;
	PackUInt32(DropPacket + 10, ContainerSerial);

	SendToServer(DropPacket, 14);

	return;
}

void DropItemAtLocation(unsigned int ItemSerial, unsigned short X, unsigned short Y, int Z)
{
	unsigned char DropPacket[14];

	DropPacket[0] = 0x08;
	PackUInt32(DropPacket + 1, INVALID_SERIAL);
	PackUInt16(DropPacket + 5, X);
	PackUInt16(DropPacket + 7, Y);
	DropPacket[9] = Z & 0xff;
	PackUInt32(DropPacket + 10, INVALID_SERIAL);

	SendToServer(DropPacket, 14);

	return;
}

void MoveToContainer(unsigned int ItemSerial, int Quantity, unsigned int ContainerSerial)
{
	PickupItem(ItemSerial, Quantity);
	DropItemInContainer(ItemSerial, ContainerSerial);
	
	return;
}

void MoveToLocation(unsigned int ItemSerial, int Quantity, unsigned short X, unsigned short Y, int Z)
{
	PickupItem(ItemSerial, Quantity);
	DropItemAtLocation(ItemSerial, X, Y, Z);
	
	return;
}

void EquipItem(unsigned int ItemSerial, int Layer)
{
	unsigned char WearPacket[10];

	PickupItem(ItemSerial, 1);

	WearPacket[0] = 0x13;
	PackUInt32(WearPacket + 1, ItemSerial);
	WearPacket[5] = Layer & 0xff;
	PackUInt32(WearPacket + 6, GetPlayerSerial());

	SendToServer(WearPacket, 10);

	return;
}

void UnequipItem(int Layer)
{
	if(GetItemInLayer(GetPlayerSerial(), Layer) != INVALID_SERIAL)
		MoveToContainer(GetItemInLayer(GetPlayerSerial(), Layer), 1, GetCatchBag());

	return;
}

void UseObject(unsigned int Serial)
{
	unsigned char DoubleClick[5] = { 0x06, 0x00, 0x00, 0x00, 0x00 };

	SetLastObject(Serial);
	PackUInt32(DoubleClick + 1, Serial);
	SendToServer(DoubleClick, 5);

	return;
}

void ClickObject(unsigned int Serial)
{
	unsigned char SingleClick[5] = { 0x06, 0x00, 0x00, 0x00, 0x00 };

	PackUInt32(SingleClick + 1, Serial);
	SendToServer(SingleClick, 5);

	return;
}

int GetDistance(int Sx, int Sy, int Tx, int Ty)
{
	return sqrt((Sx - Tx)*(Sx - Tx) + (Sy - Ty)*(Sy - Ty));
}
