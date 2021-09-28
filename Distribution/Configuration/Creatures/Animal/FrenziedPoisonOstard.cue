package Animal

FrenziedPoisonOstard: {
	Name:                 "a frenzied poison ostard"
	CorpseNameOverride:   "corpse of a frenzied poison ostard"
  Str:                  225
	Int:                  185
	Dex:                  400
	AlwaysMurderer:       true
	BaseSoundID:          629
	Body:                 218
	CreatureType:         "Animal"
	VirtualArmor:         15
	FightMode:            "Closest"
	HitsMaxSeed:              225
	Hue:                  264
	ManaMaxSeed:          175
	MinTameSkill:         110
	ProvokeSkillOverride: 130
	StamMaxSeed:          125
	Tamable:              true
	Resistances: Poison: 3
	Skills: {
		Parry:       110
		MagicResist: 110
		Tactics:     110
		Wrestling:   160
		Magery:      225
		Poisoning:   110
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 24
		}
		Skill:    "Swords"
		HitSound: 399
		HitPoison: "Regular"
	}
}
