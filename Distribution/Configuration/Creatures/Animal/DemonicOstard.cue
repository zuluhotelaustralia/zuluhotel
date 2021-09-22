package Animal

DemonicOstard: {
	Name:                 "a Demonic Ostard"
	CorpseNameOverride:   "corpse of a Demonic Ostard"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  850
	Int:                  650
	Dex:                  475
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          629
	Body:                 218
	CreatureType:         "Animal"
	VirtualArmor:         50
	FightMode:            "Aggressor"
	HitsMax:              850
	Hue:                  1259
	ManaMaxSeed:          150
	MinTameSkill:         115
	ProvokeSkillOverride: 150
	StamMaxSeed:          175
	Tamable:              true
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           50
		Water:         50
		Necro:         50
		Earth:         50
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      100
		Wrestling:    150
		MagicResist:  110
		Magery:       150
		DetectHidden: 120
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
