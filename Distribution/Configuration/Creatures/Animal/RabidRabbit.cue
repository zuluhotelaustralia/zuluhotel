package Animal

RabidRabbit: {
	Name:                 "a rabid rabbit"
	CorpseNameOverride:   "corpse of a rabid rabbit"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  90
	Int:                  25
	Dex:                  550
	BaseSoundID:          201
	Body:                 205
	CreatureType:         "Animal"
	VirtualArmor:         20
	HitsMax:              90
	Hue:                  1154
	ManaMaxSeed:          15
	MinTameSkill:         55
	ProvokeSkillOverride: 130
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		Tactics:   70
		Wrestling: 75
	}
	Attack: {
		Damage: {
			Min: 7
			Max: 12
		}
		HitSound: 458
	}
}
