package Daemon

MulticolouredHoardeDemon: {
	Name:               "a Multicoloured Hoarde Demon"
	CorpseNameOverride: "corpse of a Multicoloured Hoarde Demon"
	Str:                2000
	Int:                200
	Dex:                200
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        362
	Body:               999
	CreatureType:       "Daemon"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMax:            2250
	LootItemChance:     70
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		Water:         100
		Earth:         100
		Necro:         100
		MagicImmunity: 8
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
