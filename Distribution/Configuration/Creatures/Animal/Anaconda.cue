package Animal

Anaconda: {
	Name:               "the Anaconda"
	CorpseNameOverride: "corpse of the Anaconda"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                2500
	Int:                0
	Dex:                500
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        219
	Body:               21
	CreatureType:       "Animal"
	VirtualArmor:       45
	FightMode:          "Aggressor"
	HitsMax:            2250
	Hue:                1157
	ManaMaxSeed:        0
	StamMaxSeed:        400
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		MagicResist:  200
		Tactics:      250
		Poisoning:    95
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
