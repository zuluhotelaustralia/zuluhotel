package Undead

Zombie: {
	Name:               "a zombie"
	CorpseNameOverride: "corpse of a zombie"
	Str:                75
	Int:                15
	Dex:                40
	AlwaysMurderer:     true
	Body:               3
	CreatureType:       "Undead"
	VirtualArmor:       10
	FightMode:          "Closest"
	HitsMaxSeed:        75
	LootTable:          "24"
	ManaMaxSeed:        5
	StamMaxSeed:        30
	Resistances: {
		Poison: 6
		Necro:  75
	}
	Skills: {
		MagicResist: 40
		Tactics:     80
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 2
			Max: 16
		}
		Skill:    "Swords"
		HitSound: 473
	}
}
