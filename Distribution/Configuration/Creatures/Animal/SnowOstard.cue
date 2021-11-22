package Animal

SnowOstard: {
	BaseType:             "BaseMount"
	Name:                 "a snow ostard"
	CorpseNameOverride:   "corpse of a snow ostard"
	Str:                  210
	Int:                  110
	Dex:                  400
	AiType:               "AI_Animal"
	BaseSoundID:          624
	Body:                 219
	ItemID:				  16037
	CreatureType:         "Animal"
	FightMode:            "Aggressor"
	VirtualArmor:         35
	HideType:             "Ostard"
	Hides:                4
	HitsMaxSeed:          210
	Hue:                  1156
	ManaMaxSeed:          100
	MinTameSkill:         95
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
	}
}
