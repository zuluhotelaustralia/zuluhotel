package Daemon

ShadowWarrior: {
	Name:               "Shadow Warrior"
	CorpseNameOverride: "corpse of Shadow Warrior"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                1200
	Int:                400
	Dex:                1000
	AlwaysMurderer:     true
	CreatureType:       "Daemon"
	VirtualArmor:       35
	FightMode:          "Aggressor"
	HitsMax:            1000
	LootItemChance:     50
	LootItemLevel:      5
	LootTable:          "69"
	ManaMaxSeed:        400
	StamMaxSeed:        400
	Resistances: {
		Poison:        6
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     150
		Wrestling:   150
		MagicResist: 150
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
	Equipment: [{
		ItemType: "Server.Items.ShortHair"
		Hue:      1
	}]
}
