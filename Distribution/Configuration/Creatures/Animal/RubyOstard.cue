package Animal

RubyOstard: {
	Name:                 "a ruby ostard"
	CorpseNameOverride:   "corpse of a ruby ostard"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  175
	Int:                  135
	Dex:                  350
	AiType:               "AI_Animal"
	BaseSoundID:          624
	Body:                 219
	CreatureType:         "Animal"
	VirtualArmor:         20
	HideType:             "Ostard"
	Hides:                4
	HitsMax:              175
	Hue:                  1645
	ManaMaxSeed:          125
	MinTameSkill:         75
	ProvokeSkillOverride: 90
	StamMaxSeed:          125
	Tamable:              true
	Skills: {
		Parry:       70
		MagicResist: 70
		Tactics:     80
		Wrestling:   80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 13
			Max: 48
		}
		HitSound:  595
		MissSound: 597
	}
}
