package Dragonkin

FireLizard: {
	Name:                 "a fire lizard"
	CorpseNameOverride:   "corpse of a fire lizard"
  Str:                  140
	Int:                  30
	Dex:                  80
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          660
	Body:                 202
	CanSwim:              true
	CreatureType:         "Dragonkin"
	VirtualArmor:         10
	HideType:             "Lizard"
	Hides:                5
	HitsMaxSeed:              140
	Hue:                  1882
	ManaMaxSeed:          20
	MinTameSkill:         90
	ProvokeSkillOverride: 110
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		MagicResist: 100
		Tactics:     100
		Wrestling:   130
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 28
		}
		HitSound:  651
		MissSound: 649
		HasBreath: true
	}
}
