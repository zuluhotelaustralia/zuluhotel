package Undead

Dracula: {
	Name:               "Dracula"
	CorpseNameOverride: "corpse of Dracula"
	Str:                600
	Int:                400
	Dex:                400
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMaxSeed:            600
	LootItemChance:     50
	LootItemLevel:      5
	LootTable:          "69"
	ManaMaxSeed:        400
	StamMaxSeed:        400
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     130
		Wrestling:   130
		MagicResist: 130
	}
	Attack: {
		Ability: "LifeDrainStrike"
		AbilityChance: 1.0
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
	Equipment: [
		{
			ItemType: "ShortHair"
			Hue:      1
		},
		{
			ItemType: "Cloak"
			Hue:      0x1
			Lootable: true
		},
		{
			ItemType: "FancyShirt"
			Lootable: true
		},
		{
			ItemType: "LongPants"
			Hue:      0x1
			Lootable: true
		},
		{
			ItemType: "SkullCap"
			Hue:      0x20
			Lootable: true
		},
	]
}
