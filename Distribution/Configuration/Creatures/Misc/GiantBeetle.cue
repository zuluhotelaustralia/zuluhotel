package Misc

GiantBeetle: {
	Name:                 "a giant beetle"
	CorpseNameOverride:   "corpse of a giant beetle"
  Str:                  300
	Int:                  500
	Dex:                  100
	AiType:               "AI_Animal"
	BaseSoundID:          168
	Body:                 133
	VirtualArmor:         20
	Fame:                 3
	HitsMax:              200
	Karma:                3
	ManaMaxSeed:          500
	MinTameSkill:         115
	ProvokeSkillOverride: 120
	StamMaxSeed:          100
	Tamable:              true
	Skills: {
		MagicResist: 80
		Tactics:     100
		Wrestling:   100
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 10
		}
		HitSound: 170
	}
}
