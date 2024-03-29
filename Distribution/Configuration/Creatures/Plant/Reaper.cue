package Plant

Reaper: {
	Name:               "a reaper"
	CorpseNameOverride: "corpse of a reaper"
	Str:                210
	Int:                35
	Dex:                110
	AlwaysMurderer:     true
	BaseSoundID:        442
	Body:               47
	CreatureType:       "Plant"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:        210
	LootTable:          "34"
	ManaMaxSeed:        25
	StamMaxSeed:        100
	Resistances: {
		Poison: 6
		Water:  100
		Earth:  100
	}
	Skills: {
		Parry:       60
		Magery:      100
		Tactics:     100
		Wrestling:   150
		MagicResist: 75
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 5
			Max: 50
		}
		HitSound: 444
	}
}
