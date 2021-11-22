package Animal

Horse: {
	BaseType:             "BaseMount"
	Name:                 "a horse"
	CorpseNameOverride:   "corpse of a horse"
	Str:                  95
	Int:                  10
	Dex:                  75
	AiType:               "AI_Animal"
	BaseSoundID:          168
	Body: 				  200
	ItemID:				  16031
	CreatureType:         "Animal"
	FightMode:            "Aggressor"
	VirtualArmor:         10
	Hides:                4
	HitsMaxSeed:          45
	ManaMaxSeed:          0
	MinTameSkill:         30
	ProvokeSkillOverride: 30
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		MagicResist: 30
		Tactics:     45
		Wrestling:   45
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 3
			Max: 4
		}
	}
}
