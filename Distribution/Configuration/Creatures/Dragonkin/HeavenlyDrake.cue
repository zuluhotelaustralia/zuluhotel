package Dragonkin

HeavenlyDrake: {
	Name:                 "a Heavenly Drake"
	CorpseNameOverride:   "corpse of a Heavenly Drake"
  Str:                  400
	Int:                  90
	Dex:                  300
	PassiveSpeed:         0.2
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                5
	HitsMaxSeed:              400
	Hue:                  1181
	InitialInnocent:      true
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "37"
	ManaMaxSeed:          80
	MinTameSkill:         125
	ProvokeSkillOverride: 130
	StamMaxSeed:          100
	Tamable:              true
	Resistances: {
		Poison:        6
		MagicImmunity: 5
	}
	Skills: {
		Parry:        70
		MagicResist:  110
		Tactics:      100
		Wrestling:    150
		DetectHidden: 130
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound:      364
		Ability:       "TriElementalStrike"
		AbilityChance: 1
		HasBreath:     true
	}
}
