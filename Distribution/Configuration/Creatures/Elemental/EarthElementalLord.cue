package Elemental

EarthElementalLord: {
	Name:               "an earth elemental lord"
	CorpseNameOverride: "corpse of an earth elemental lord"
	BaseType:           "BaseElementalLord"
	Str:                450
	Int:                55
	Dex:                150
	AlwaysMurderer:     true
	BaseSoundID:        268
	Body:               14
	CreatureType:       "Elemental"
	VirtualArmor:       50
	FightMode:          "Closest"
	HitsMaxSeed:            450
	Hue:                1538
	LootItemChance:     60
	LootItemLevel:      4
	LootTable:          "74"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: Earth: 100
	Skills: {
		Tactics:     150
		Wrestling:   175
		MagicResist: 65
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 21
			Max: 45
		}
		HitSound: 270
	}
}
