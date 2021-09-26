package Orc

GoblinTribeMaster: {
	Name:               "<random> the Goblin Tribe Master"
	CorpseNameOverride: "corpse of <random> the Goblin Tribe Master"
	Str:                2250
	Int:                55
	Dex:                400
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	ClassLevel:         5
	ClassType:          "Warrior"
	CreatureType:       "Orc"
	VirtualArmor:       50
	FightMode:          "Closest"
	HitsMax:            2250
	Hue:                34186
	LootItemChance:     50
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
		Fire:   75
		Air:    75
		Water:  75
		Poison: 6
		Earth:  75
		Necro:  75
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
		ItemType:    "OrcHelm"
		Name:        "Goblin Helmet"
		Hue:         1418
		ArmorRating: 18
	}, {
		ItemType: "WarHammer"
		Name:     "Goblin Tribe Master Weapon"
	}]
}
