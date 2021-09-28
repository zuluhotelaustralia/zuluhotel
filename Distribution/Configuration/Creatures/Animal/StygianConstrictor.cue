package Animal

StygianConstrictor: {
	Name:               "a Stygian Constrictor"
	CorpseNameOverride: "corpse of a Stygian Constrictor"
	Str:                275
	Int:                175
	Dex:                110
	AlwaysMurderer:     true
	BaseSoundID:        219
	Body:               21
	CreatureType:       "Animal"
	VirtualArmor:       40
	FightMode:          "Closest"
	HideType:           "Serpent"
	Hides:              5
	HitsMaxSeed:            275
	Hue:                1157
	ManaMaxSeed:        70
	StamMaxSeed:        75
	Resistances: Poison: 3
	Skills: {
		MagicResist: 70
		Tactics:     90
		Poisoning:   90
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 13
			Max: 69
		}
		Skill:     "Swords"
		HitSound:  220
		MissSound: 219
	}
}
