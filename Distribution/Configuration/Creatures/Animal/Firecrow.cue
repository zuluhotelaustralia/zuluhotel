package Animal

Firecrow: {
	Name:                 "a firecrow"
	CorpseNameOverride:   "corpse of a firecrow"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  18
	Int:                  15
	Dex:                  60
	PassiveSpeed:         0.2
	BaseSoundID:          27
	Body:                 6
	CreatureType:         "Animal"
	VirtualArmor:         10
	HitsMax:              18
	Hue:                  1109
	ManaMaxSeed:          0
	MinTameSkill:         25
	ProvokeSkillOverride: 10
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		MagicResist: 20
		Tactics:     50
		Wrestling:   10
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 1
			Max: 6
		}
		HitSound:  126
		MissSound: 125
		HasBreath: true
	}
}
