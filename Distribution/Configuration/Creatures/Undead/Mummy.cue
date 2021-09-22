package Undead

Mummy: {
	Name:               "a mummy"
	CorpseNameOverride: "corpse of a mummy"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                250
	Int:                25
	Dex:                50
	AlwaysMurderer:     true
	Body:               3
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Aggressor"
	HitsMax:            250
	Hue:                1109
	LootItemChance:     1
	LootItemLevel:      5
	LootTable:          "59"
	ManaMaxSeed:        15
	StamMaxSeed:        50
	Resistances: {
		Poison: 6
		Fire:   50
	}
	Skills: {
		MagicResist: 80
		Tactics:     100
		Wrestling:   150
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 471
	}
}
