package Elemental

BladeSpirit: {
	Name:               "a blade spirit"
	CorpseNameOverride: "corpse of a blade spirit"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                200
	Int:                25
	Dex:                200
	AlwaysMurderer:     true
	BaseSoundID:        570
	Body:               574
	CreatureType:       "Elemental"
	VirtualArmor:       15
	FightMode:          "Aggressor"
	HitsMax:            200
	Hue:                33784
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Skills: {
		Poisoning:   100
		Tactics:     50
		Wrestling:   125
		MagicResist: 110
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 2
			Max: 8
		}
		HitSound: 572
	}
}
