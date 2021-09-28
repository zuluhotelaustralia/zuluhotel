package Elemental

EarthElemental: {
	Name:               "an earth elemental"
	CorpseNameOverride: "corpse of an earth elemental"
	Str:                230
	Int:                45
	Dex:                50
	AlwaysMurderer:     true
	BaseSoundID:        268
	Body:               14
	CreatureType:       "Elemental"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMaxSeed:            230
	Hue:                33784
	LootItemChance:     25
	LootItemLevel:      3
	LootTable:          "21"
	ManaMaxSeed:        35
	StamMaxSeed:        40
	Resistances: {
		Earth: 100
		Air:   -50
	}
	Skills: {
		Parry:       75
		MagicResist: 40
		Tactics:     90
		Wrestling:   120
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 270
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR20"
		ArmorRating: 20
	}]
}
