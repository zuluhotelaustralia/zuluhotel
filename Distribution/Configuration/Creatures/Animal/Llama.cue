package Animal

Llama: {
	BaseType:             "BaseMount"
	Name:                 "a llama"
	CorpseNameOverride:   "corpse of a llama"
	Str:                  50
	Int:                  30
	Dex:                  55
	AiType:               "AI_Animal"
	BaseSoundID:          1011
	Body: 				  220
	ItemID:				  16038
	CreatureType:         "Animal"
	FightMode:            "Aggressor"
	VirtualArmor:         16
	Hides:                4
	HitsMaxSeed:          30
	ManaMaxSeed:          0
	MinTameSkill:         35
	ProvokeSkillOverride: 35
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		MagicResist: 20
		Tactics:     30
		Wrestling:   30
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 3
			Max: 5
		}
	}
}
