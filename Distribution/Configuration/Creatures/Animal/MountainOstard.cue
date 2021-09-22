package Animal

MountainOstard: {
	Name:                 "a mountain ostard"
	CorpseNameOverride:   "corpse of a mountain ostard"

	Str:                  120
	Int:                  35
	Dex:                  240
	AiType:               "AI_Animal"
	BaseSoundID:          624
	Body:                 219
	CreatureType:         "Animal"
	VirtualArmor:         10
	HideType:             "Ostard"
	Hides:                4
	HitsMax:              120
	Hue:                  1160
	ManaMaxSeed:          0
	MinTameSkill:         75
	ProvokeSkillOverride: 90
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		Parry:       40
		MagicResist: 40
		Tactics:     50
		Wrestling:   60
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 10
			Max: 45
		}
		HitSound:  595
		MissSound: 597
	}
}
