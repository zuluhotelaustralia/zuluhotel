package Animal

EliteOstard: {
	BaseType:             "BaseMount"
	Name:                 "Elite Ostard"
	CorpseNameOverride:   "corpse of Elite Ostard"
	Str:                  600
	Int:                  650
	Dex:                  300
	PassiveSpeed:         0.2
	AutoDispel:           true
	BaseSoundID:          629
	Body:                 218
	ItemID:								16037
	CreatureType:         "Animal"
	VirtualArmor:         50
	HitsMaxSeed:          600
	Hue:                  1298
	ManaMaxSeed:          150
	MinTameSkill:         115
	ProvokeSkillOverride: 150
	StamMaxSeed:          125
	Tamable:              true
	Resistances: {
		Poison:        6
		Air:           50
		Water:         50
		Fire:          50
		Necro:         50
		Earth:         50
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      100
		Wrestling:    130
		MagicResist:  100
		DetectHidden: 100
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound:  362
		HasBreath: true
	}
}
