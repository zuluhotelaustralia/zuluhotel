package Undead

Revenant: {
	Name:               "a Revenant"
	CorpseNameOverride: "corpse of a Revenant"
	Str:                135
	Int:                15
	Dex:                120
	AlwaysMurderer:     true
	Body:               3
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:        135
	Hue:                1285
	LootTable:          "14"
	ManaMaxSeed:        5
	StamMaxSeed:        30
	Resistances: {
		Poison: 6
		Necro:  100
	}
	Skills: {
		MagicResist: 60
		Tactics:     90
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 8
			Max: 64
		}
		Skill:    "Swords"
		HitSound: 473
		Ability: {
			SpellType: "Darkness"
		}
		AbilityChance: 1
	}
}
