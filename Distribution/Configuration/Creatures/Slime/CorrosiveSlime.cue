package Slime

CorrosiveSlime: {
	Name:               "a corrosive slime"
	CorpseNameOverride: "corpse of a corrosive slime"
	Str:                600
	Int:                10
	Dex:                150
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BaseSoundID:        456
	Body:               51
	CreatureType:       "Slime"
	VirtualArmor:       50
	FightRange:         7
	HitsMax:            500
	Hue:                1296
	LootTable:          "125"
	ManaMaxSeed:        0
	StamMaxSeed:        150
	Resistances: {
		Poison: 6
		Fire:   100
	}
	Skills: {
		MagicResist: 90
		Poisoning:   40
		Tactics:     130
	}
	Attack: {
		Speed: 10
		Damage: {
			Min: 15
			Max: 60
		}
		Skill:    "Swords"
		HitSound: 78
		MaxRange: 7
	}
}
