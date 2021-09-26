package Daemon

SkitteringHopper: {
	Name:               "a Skittering Hopper"
	CorpseNameOverride: "corpse of a Skittering Hopper"
	Str:                200
	Int:                200
	Dex:                200
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BaseSoundID:        959
	Body:               302
	CreatureType:       "Daemon"
	FightMode:          "Closest"
	HitsMax:            300
	Hue:                33784
	LootItemChance:     70
	LootItemLevel:      3
	LootTable:          "69"
	ManaMaxSeed:        400
	StamMaxSeed:        400
	Resistances: {
		Poison:        6
		Earth:         75
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     110
		Wrestling:   110
		MagicResist: 110
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
}
