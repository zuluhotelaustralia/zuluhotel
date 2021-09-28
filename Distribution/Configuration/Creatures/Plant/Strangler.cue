package Plant

Strangler: {
	Name:               "a Strangler"
	CorpseNameOverride: "corpse of a Strangler"
	Str:                1500
	Int:                255
	Dex:                400
	ActiveSpeed:        0.15
	PassiveSpeed:       0.3
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        904
	Body:               28
	CreatureType:       "Plant"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:            2000
	Hue:                1175
	LootItemChance:     100
	LootItemLevel:      8
	LootTable:          "201"
	ManaMaxSeed:        0
	StamMaxSeed:        500
	PreferredSpells: ["ShiftingEarth", "GustOfAir", "WraithBreath", "MassDispel"]
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Poison:        1
		Earth:         100
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Poisoning:    90
		Tactics:      150
		Wrestling:    200
		MagicResist:  200
		DetectHidden: 200
		Hiding:       200
		Magery:       200
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 11
			Max: 41
		}
		MissSound: 360
	}
}
