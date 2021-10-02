package Daemon

MediumHoardeDemon: {
	Name:               "a Medium Hoarde Demon"
	CorpseNameOverride: "corpse of a Medium Hoarde Demon"
	Str:                1000
	Int:                200
	Dex:                200
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        362
	Body:               795
	CreatureType:       "Daemon"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMaxSeed:        1200
	LootItemChance:     10
	LootItemLevel:      5
	LootTable:          "37"
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Resistances: {
		Poison:        3
		Fire:          75
		Air:           75
		Water:         75
		Necro:         75
		Earth:         75
		MagicImmunity: 3
	}
	Skills: {
		Tactics:      150
		Wrestling:    175
		MagicResist:  60
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
