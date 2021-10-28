package Animal

FireFrenziedOstard: {
	BaseType:             "BaseMount"
	Name:                 "a fire frenzied ostard"
	CorpseNameOverride:   "corpse of a fire frenzied ostard"
	Str:                  225
	Int:                  135
	Dex:                  400
	PassiveSpeed:         0.2
	BaseSoundID:          629
	Body:                 218
	ItemID:								16036
	CreatureType:         "Animal"
	VirtualArmor:         35
	HideType:             "Ostard"
	Hides:                4
	HitsMaxSeed:          225
	Hue:                  1158
	ManaMaxSeed:          125
	MinTameSkill:         115
	ProvokeSkillOverride: 130
	StamMaxSeed:          175
	Tamable:              true
	Skills: {
		Parry:       110
		MagicResist: 100
		Tactics:     110
		Wrestling:   160
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 13
			Max: 48
		}
		HitSound:  595
		MissSound: 597
		HasBreath: true
	}
}
