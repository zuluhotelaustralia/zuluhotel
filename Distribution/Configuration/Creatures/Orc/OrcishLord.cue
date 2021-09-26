package Orc

OrcishLord: {
	Name:                 "<random> the Orcish Lord"
	CorpseNameOverride:   "corpse of <random> the Orcish Lord"
  Str:                  205
	Int:                  30
	Dex:                  90
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 7
	CreatureType:         "Orc"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMax:              205
	Hue:                  33784
	LootItemChance:       6
	LootItemLevel:        3
	LootTable:            "42"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 90
	StamMaxSeed:          80
	Resistances: {
		Fire:  75
		Earth: 75
	}
	Skills: {
		Tactics:     100
		MagicResist: 80
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 8
			Max: 64
		}
		Skill:    "Swords"
		HitSound: 572
	}
}
