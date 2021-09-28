package Ratkin

Ratlord: {
	Name:                 "<random> the Ratlord"
	CorpseNameOverride:   "corpse of <random> the Ratlord"
	Str:                  300
	Int:                  65
	Dex:                  450
	AlwaysMurderer:       true
	BaseSoundID:          437
	Body:                 45
	CreatureType:         "Ratkin"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Rat"
	Hides:                5
	HitsMaxSeed:          300
	LootItemChance:       50
	LootItemLevel:        1
	LootTable:            "27"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 95
	StamMaxSeed:          50
	Skills: {
		Tactics:     100
		MagicResist: 75
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 8
			Max: 64
		}
		Skill:    "Swords"
		HitSound: 439
	}
}
