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
* 	Sept 18th, 2004 -- world handler module
* 
\******************************************************************************/


#ifndef _UOWORLD_H_INCLUDED
#define _UOWORLD_H_INCLUDED

#define ALLOCCHAR(Obj) Obj.Character = (CharacterObject*)malloc(sizeof(CharacterObject));
#define FREECHAR(Obj) { if(Obj.Character != NULL) free(Obj.Character); Obj.Character = NULL; }

/* object flags */
#define OBJ_FLAG_MALE (1) /* 0x01 */
#define OBJ_FLAG_FEMALE (1 << 1) /* 0x02 */
#define OBJ_FLAG_POISONED (1 << 2) /* 0x04 */
#define OBJ_FLAG_WAR (1 << 6) /* 0x40 */
#define OBJ_FLAG_HIDDEN (1 << 7) /* 0x80 */
#define OBJ_FLAG_WAR (1 << 6) /* 0x40 */
#define OBJ_FLAG_HIDDEN (1 << 7) /* 0x80 */
#define OBJ_FLAG_NOTORIETY (7 << 29) /* notoriety uses 3 bits */

#define GETOBJFLAG(Obj, Flag) (Obj.Flags & Flag)
#define SETOBJFLAG(Obj, Flag) (Obj.Flags |= Flag)
#define CLEAROBJFLAG(Obj, Flag) (Obj.Flags = !(Obj.Flags & Flag))

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

/* INTERNALS */
void CleanWorld(void);
void WorldDump(void);

/* world handlers */
int IRWServer_EnterWorld(unsigned char *Packet, int Len);
int IRWServer_CharStatus(unsigned char *Packet, int Len);
int IRWServer_UpdateItem(unsigned char *Packet, int Len);
int IRWServer_DeleteItem(unsigned char *Packet, int Len);
int IRWServer_UpdateCreature(unsigned char *Packet, int Len);
int IRWServer_OpenContainer(unsigned char *Packet, int Len);
int IRWClient_DropItem(unsigned char *Packet, int Len);
int IRWServer_AddToContainer(unsigned char *Packet, int Len);
int IRWServer_ClearSquare(unsigned char *Packet, int Len);
int IRWServer_EquipItem(unsigned char *Packet, int Len);
int IRWServer_UpdateSkills(unsigned char *Packet, int Len);
int IRWServer_UpdateContainer(unsigned char *Packet, int Len);
int IRWServer_UpdatePlayerPos(unsigned char *Packet, int Len);
int IRWServer_UpdatePlayer(unsigned char *Packet, int Len);
int IRWServer_UpdateHealth(unsigned char *Packet, int Len);
int IRWServer_UpdateMana(unsigned char *Packet, int Len);
int IRWServer_UpdateStamina(unsigned char *Packet, int Len);
int IRWServer_WarPeace(unsigned char *Packet, int Len);
int IRWServer_Paperdoll(unsigned char *Packet, int Len);
int IRWServer_Fight(unsigned char *Packet, int Len);
int IRWClient_Attack(unsigned char *Packet, int Len);
int IRWClient_DoubleClick(unsigned char *Packet, int Len);
int IRWClient_PlayerRequestWalk(unsigned char *Packet, int Len);
int IRWServer_PlayerConfirmWalk(unsigned char *Packet, int Len);


/* EXPORTS */
/* serial storages */
int GetPlayerIdx(void);
unsigned int GetPlayerSerial(void);
unsigned int GetPlayerBackpack(void);
unsigned int GetLastAttack(void);
unsigned int GetLastObject(void);
unsigned int GetCatchBag(void);
unsigned int GetLastContainer(void);
void SetLastContainer(unsigned int Serial);
void SetLastAttack(unsigned int Serial);
void SetLastObject(unsigned int Serial);
void SetCatchBag(unsigned int Serial);

/* these functions are very sensitive so be careful when using */
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
int* ListObjectsAtLocation(unsigned short X, unsigned short Y, int Distance);
int* ListItemsInContainer(unsigned int Serial);
unsigned int FindObjectAtLocation(unsigned short Graphic, unsigned short Color, unsigned short X, unsigned short Y, int Distance);
unsigned int FindItemInContainer(unsigned short Graphic, unsigned short Color, unsigned int ContainerSerial);
unsigned int GetItemInLayer(unsigned int ContainerSerial, int Layer);
void WorldCount(int *Objects, int *Characters, int *Total);

/* world interaction functions */
void PickupItem(unsigned int ItemSerial, int Quantity);
void DropItem(unsigned int ItemSerial, unsigned short X, unsigned short Y, int Z);
void DropItemInContainer(unsigned int ItemSerial, unsigned int ContainerSerial);
void DropItemAtLocation(unsigned int ItemSerial, unsigned short X, unsigned short Y, int Z);
void MoveToContainer(unsigned int ItemSerial, int Quantity, unsigned int ContainerSerial);
void MoveToLocation(unsigned int ItemSerial, int Quantity, unsigned short X, unsigned short Y, int Z);
void EquipItem(unsigned int ItemSerial, int Layer);
void UnequipItem(int Layer);
void UseObject(unsigned int Serial);
void ClickObject(unsigned int Serial);
int GetDistance(int Sx, int Sy, int Tx, int Ty);


#endif
