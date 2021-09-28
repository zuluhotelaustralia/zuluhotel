package Dragonkin

WaterDrake: {
	Name:                 "a Water Drake"
	CorpseNameOverride:   "corpse of a Water Drake"
  Str:                  400
	Int:                  90
	Dex:                  400
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 61
	CreatureType:         "Dragonkin"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                5
	HitsMaxSeed:              400
	Hue:                  1165
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         115
	ProvokeSkillOverride: 120
	StamMaxSeed:          130
	Tamable:              true
	Resistances: {
		Water:         100
		MagicImmunity: 3
	}
	Skills: {
		Parry:        70
		MagicResist:  130
		Tactics:      100
		Wrestling:    100
		DetectHidden: 130
	}
	Attack: {
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
	}
}
