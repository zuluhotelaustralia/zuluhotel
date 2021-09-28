package Undead

Shade: {
	Name:               "a shade"
	CorpseNameOverride: "corpse of a shade"
	Str:                300
	Int:                35
	Dex:                90
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            300
	Hue:                17969
	LootItemChance:     50
	LootItemLevel:      1
	LootTable:          "25"
	ManaMaxSeed:        25
	StamMaxSeed:        50
	Resistances: Poison: 6
	Skills: {
		MagicResist: 60
		Tactics:     80
		Wrestling:   105
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 44
		}
		HitSound: 384
	}
}
