package Elemental

PoisonElemental: {
	Name:                 "a poison elemental"
	CorpseNameOverride:   "corpse of a poison elemental"
	Str:                  350
	Int:                  350
	Dex:                  160
	AlwaysMurderer:       true
	BaseSoundID:          655
	Body:                 13
	CreatureType:         "Elemental"
	VirtualArmor:         40
	FightMode:            "Closest"
	HitsMaxSeed:          350
	Hue:                  2006
	LootItemChance:       50
	LootItemLevel:        4
	LootTable:            "46"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 120
	StamMaxSeed:          50
	Resistances: Poison: 1
	Skills: {
		Tactics:     100
		Wrestling:   200
		MagicResist: 70
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 25
			Max: 50
		}
		HitSound:  468
		HitPoison: "Deadly"
	}
}
