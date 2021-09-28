package Undead

EvisceratedCarcass: {
	Name:               "an Eviscerated Carcass"
	CorpseNameOverride: "corpse of an Eviscerated Carcass"
	Str:                300
	Int:                15
	Dex:                400
	AlwaysMurderer:     true
	Body:               3
	ClassLevel:         3
	ClassType:          "Warrior"
	CreatureType:       "Undead"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:            300
	Hue:                1290
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "131"
	ManaMaxSeed:        5
	StamMaxSeed:        30
	Resistances: Poison: 6
	Skills: {
		MagicResist: 80
		Tactics:     120
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 25
			Max: 55
		}
		Skill:    "Swords"
		HitSound: 473
	}
}
