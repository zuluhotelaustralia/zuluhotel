package Dragonkin

LizardmanKing: {
	Name:                 "<random> the Lizardman King"
	CorpseNameOverride:   "corpse of <random> the Lizardman King"
  Str:                  300
	Int:                  75
	Dex:                  200
	AlwaysMurderer:       true
	BaseSoundID:          417
	Body:                 33
	CreatureType:         "Dragonkin"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Lizard"
	Hides:                5
	HitsMax:              300
	Hue:                  1125
	LootItemChance:       50
	LootItemLevel:        3
	LootTable:            "56"
	ManaMaxSeed:          65
	ProvokeSkillOverride: 110
	StamMaxSeed:          100
	Resistances: MagicImmunity: 1
	Skills: {
		Tactics:     120
		Wrestling:   100
		MagicResist: 80
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound:  421
		MissSound: 418
	}
}
