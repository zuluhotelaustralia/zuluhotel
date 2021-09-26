package Undead

JujuZombie: {
	Name:               "a juju zombie"
	CorpseNameOverride: "corpse of a juju zombie"
	Str:                375
	Int:                15
	Dex:                400
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	Body:               3
	CreatureType:       "Undead"
	VirtualArmor:       50
	FightMode:          "Closest"
	FightRange:         4
	HitsMax:            375
	Hue:                1300
	LootTable:          "24"
	ManaMaxSeed:        5
	StamMaxSeed:        400
	Resistances: {
		Poison: 6
		Necro:  100
	}
	Skills: {
		MagicResist: 40
		Tactics:     80
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 8
			Max: 64
		}
		Skill:     "Swords"
		HitSound:  283
		MissSound: 284
		MaxRange:  4
	}
}
