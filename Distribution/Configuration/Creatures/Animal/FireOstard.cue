package Animal

FireOstard: {
	BaseType:             "BaseMount"
	Name:                 "a fire ostard"
	CorpseNameOverride:   "corpse of a fire ostard"
	Str:                  160
	Int:                  110
	Dex:                  400
	PassiveSpeed:         0.2
	BaseSoundID:          624
	Body:                 219
	ItemID:								16037
	CreatureType:         "Animal"
	VirtualArmor:         30
	HideType:             "Ostard"
	Hides:                4
	HitsMaxSeed:          160
	Hue:                  1158
	ManaMaxSeed:          100
	MinTameSkill:         115
	ProvokeSkillOverride: 110
	StamMaxSeed:          150
	Tamable:              true
	Skills: {
		Parry:       100
		MagicResist: 70
		Tactics:     90
		Wrestling:   100
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 15
			Max: 64
		}
		HitSound:  595
		MissSound: 597
		HasBreath: true
	}
}
