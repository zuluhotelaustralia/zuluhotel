package Animal

HeavenlyOstard: {
	BaseType:             "BaseMount"
	Name:                 "a heavenly ostard"
	CorpseNameOverride:   "corpse of a heavenly ostard"
	Str:                  250
	Int:                  160
	Dex:                  400
	AiType:               "AI_Healer"
	BaseSoundID:          624
	Body:                 219
	ItemID:				  16037
	CreatureType:         "Animal"
	FightMode:            "Aggressor"
	VirtualArmor:         30
	HideType:             "Ostard"
	Hides:                4
	HitsMaxSeed:          250
	Hue:                  1181
	ManaMaxSeed:          150
	MinTameSkill:         105
	ProvokeSkillOverride: 110
	StamMaxSeed:          100
	Tamable:              true
	Skills: {
		Parry:       100
		MagicResist: 70
		Tactics:     90
		Wrestling:   100
		Magery:      200
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 15
			Max: 64
		}
		HitSound:  595
		MissSound: 597
	}
}
