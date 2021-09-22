package Animal

FireFrenziedOstard: {
	Name:                 "a fire frenzied ostard"
	CorpseNameOverride:   "corpse of a fire frenzied ostard"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  225
	Int:                  135
	Dex:                  400
	PassiveSpeed:         0.2
	BaseSoundID:          629
	Body:                 218
	CreatureType:         "Animal"
	VirtualArmor:         35
	HideType:             "Ostard"
	Hides:                4
	HitsMax:              225
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
