package Slime

DarklingSlime: {
	Name:               "a darkling slime"
	CorpseNameOverride: "corpse of a darkling slime"
	Str:                200
	Int:                10
	Dex:                150
	AlwaysMurderer:     true
	BaseSoundID:        456
	Body:               51
	CreatureType:       "Slime"
	VirtualArmor:       30
	HitsMaxSeed:        200
	Hue:                25125
	LootTable:          "125"
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Resistances: Poison: 6
	Skills: {
		MagicResist: 90
		Poisoning:   40
		Tactics:     100
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 43
		}
		Skill:    "Swords"
		HitSound: 458
	}
}
