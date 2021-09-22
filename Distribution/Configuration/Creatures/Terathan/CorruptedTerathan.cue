package Terathan

CorruptedTerathan: {
	Name:               "a Corrupted Terathan"
	CorpseNameOverride: "corpse of a Corrupted Terathan"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                400
	Int:                110
	Dex:                250
	AlwaysMurderer:     true
	BaseSoundID:        589
	Body:               70
	CreatureType:       "Terathan"
	VirtualArmor:       30
	FightMode:          "Aggressor"
	HitsMax:            400
	Hue:                1304
	LootItemChance:     90
	LootItemLevel:      3
	LootTable:          "127"
	ManaMaxSeed:        0
	StamMaxSeed:        70
	Skills: {
		Wrestling:   120
		Tactics:     100
		MagicResist: 100
		Parry:       100
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound:  588
		MissSound: 589
	}
	Equipment: [{
		ItemType:    "Server.Items.HeaterShield"
		Name:        "Shield AR20"
		ArmorRating: 20
	}]
}
