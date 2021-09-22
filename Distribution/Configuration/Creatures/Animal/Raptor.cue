package Animal

Raptor: {
	Name:                 "a Raptor"
	CorpseNameOverride:   "corpse of a Raptor"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  110
	Int:                  90
	Dex:                  160
	BaseSoundID:          624
	Body:                 210
	CreatureType:         "Animal"
	VirtualArmor:         20
	HideType:             "Ostard"
	Hides:                4
	HitsMax:              110
	Hue:                  1109
	LootTable:            "48"
	ManaMaxSeed:          80
	MinTameSkill:         95
	ProvokeSkillOverride: 30
	StamMaxSeed:          90
	Tamable:              true
	Skills: {
		MagicResist: 60
		Tactics:     100
		Wrestling:   150
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 45
		}
		HitSound: 477
	}
}
