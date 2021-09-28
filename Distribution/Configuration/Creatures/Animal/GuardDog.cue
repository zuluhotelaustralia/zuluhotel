package Animal

GuardDog: {
	Name:                 "a guard dog"
	CorpseNameOverride:   "corpse of a guard dog"
  Str:                  50
	Int:                  35
	Dex:                  80
	AiType:               "AI_Animal"
	BaseSoundID:          229
	Body:                 225
	CreatureType:         "Animal"
	VirtualArmor:         15
	HitsMaxSeed:              50
	Hue:                  33784
	ManaMaxSeed:          0
	MinTameSkill:         10
	ProvokeSkillOverride: 70
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		Tactics:     60
		Wrestling:   75
		MagicResist: 40
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 2
			Max: 10
		}
		HitSound: 135
	}
}
