package Elemental

EnergyVortex: {
	Name:               "an energy vortex"
	CorpseNameOverride: "corpse of an energy vortex"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                350
	Int:                110
	Dex:                190
	AlwaysMurderer:     true
	BaseSoundID:        527
	Body:               573
	CanSwim:            true
	CreatureType:       "Elemental"
	VirtualArmor:       30
	FightMode:          "Aggressor"
	HitPoison:          "Regular"
	HitsMax:            350
	ManaMaxSeed:        125
	MinTameSkill:       130
	StamMaxSeed:        80
	Tamable:            true
	Skills: {
		Poisoning:   100
		MagicResist: 150
		Tactics:     125
		Wrestling:   200
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 5
			Max: 20
		}
		HitSound: 529
	}
}
