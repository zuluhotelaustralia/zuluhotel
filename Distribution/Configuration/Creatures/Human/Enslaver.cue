package Human

Enslaver: {
	Name:               "<random> the Enslaver"
	CorpseNameOverride: "corpse of <random> the Enslaver"
	Str:                2000
	Int:                55
	Dex:                400
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	ClassLevel:         3
	ClassType:          "Warrior"
	CreatureType:       "Human"
	VirtualArmor:       60
	FightMode:          "Aggressor"
	HitsMax:            2000
	Hue:                1645
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
		Fire:   75
		Air:    75
		Water:  75
		Poison: 6
		Necro:  75
		Earth:  75
	}
	Skills: {
		Tactics:      250
		Macing:       175
		MagicResist:  60
		DetectHidden: 200
	}
	Attack: {
		Speed: 51
		Damage: {
			Min: 10
			Max: 50
		}
		HitSound:  315
		MissSound: 563
	}
	Equipment: [{
		ItemType: "SWarHammer"
		Name:     "Enslavers Weapon"
		Hue:      1162
	}]
}
