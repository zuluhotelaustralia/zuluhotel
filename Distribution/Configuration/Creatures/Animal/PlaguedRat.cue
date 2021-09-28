package Animal

PlaguedRat: {
	Name:                 "a plagued rat"
	CorpseNameOverride:   "corpse of a plagued rat"
	Str:                  30
	Int:                  110
	Dex:                  80
	BaseSoundID:          204
	Body:                 238
	CreatureType:         "Animal"
	VirtualArmor:         10
	HitsMaxSeed:          30
	Hue:                  33784
	ManaMaxSeed:          200
	MinTameSkill:         35
	ProvokeSkillOverride: 70
	StamMaxSeed:          70
	Tamable:              true
	Skills: {
		Tactics:   50
		Wrestling: 50
		Magery:    40
	}
	Attack: {
		Speed: 15
		Damage: {
			Min: 1
			Max: 10
		}
		HitSound:  206
		HitPoison: "Lesser"
	}
}
