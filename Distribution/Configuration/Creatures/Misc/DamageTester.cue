package Misc

DamageTester: {
	Name:               "<random> the damage tester"
	CorpseNameOverride: "corpse of <random> the damage tester"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                60000
	Int:                200
	Dex:                200
	AiType:             "AI_Animal"
	AlwaysMurderer:     true
	HitsMax:            100000
	Hue:                33784
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Skills: {
		Parry:     200
		Mining:    1
		Tactics:   1
		Wrestling: 1
	}
	Attack: {
		Speed: 1
		Damage: {
			Min: 0
			Max: 0
		}
	}
}
