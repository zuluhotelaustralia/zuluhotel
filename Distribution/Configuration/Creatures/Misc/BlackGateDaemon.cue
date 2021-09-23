package Misc

BlackGateDaemon: {
	Name:                 "a black gate daemon"
	CorpseNameOverride:   "corpse of a black gate daemon"
  Str:                  1500
	Int:                  290
	Dex:                  165
	AiType:               "AI_Mage"
	BaseSoundID:          168
	Body:                 228
	VirtualArmor:         20
	Fame:                 4
	HitsMax:              1500
	Karma:                5
	ManaMaxSeed:          465
	ProvokeSkillOverride: 150
	StamMaxSeed:          165
	PreferredSpells: [
		"SummonDaemon",
	]
	Skills: {
		MagicResist: 120
		Tactics:     90
		Wrestling:   90
		Magery:      100
		EvalInt:     90
		Anatomy:     25
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
