package Animal

GoldenOstard: {
	BaseType:             "BaseMount"
	Name:                 "a golden ostard"
	CorpseNameOverride:   "corpse of a golden ostard"
	Str:                  120
	Int:                  35
	Dex:                  240
	AiType:               "AI_Animal"
	BaseSoundID:          624
	Body:                 219
	ItemID:				  16037
	CreatureType:         "Animal"
	FightMode:            "Aggressor"
	VirtualArmor:         10
	HideType:             "Ostard"
	Hides:                4
	HitsMaxSeed:          120
	Hue:                  48
	ManaMaxSeed:          0
	MinTameSkill:         50
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
