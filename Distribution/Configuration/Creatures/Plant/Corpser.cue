package Plant

Corpser: {
	Name:               "a corpser"
	CorpseNameOverride: "corpse of a corpser"
	Str:                150
	Int:                45
	Dex:                105
	AlwaysMurderer:     true
	BaseSoundID:        684
	Body:               8
	CreatureType:       "Plant"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            150
	LootTable:          "32"
	ManaMaxSeed:        35
	StamMaxSeed:        95
	Resistances: Earth: 100
	Skills: {
		Parry:       20
		Hiding:      75
		Tactics:     90
		MagicResist: 20
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 5
			Max: 30
		}
		HitSound: 354
	}
}
