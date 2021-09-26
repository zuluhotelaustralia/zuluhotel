package Daemon

BalronSpawn: {
	Name:               "a Balron Spawn"
	CorpseNameOverride: "corpse of a Balron Spawn"
	Str:                600
	Int:                400
	Dex:                400
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BaseSoundID:        422
	Body:               74
	CreatureType:       "Daemon"
	FightMode:          "Closest"
	HitsMax:            600
	Hue:                17969
	LootItemChance:     80
	LootItemLevel:      4
	LootTable:          "69"
	ManaMaxSeed:        400
	StamMaxSeed:        400
	Resistances: {
		Poison:        6
		Earth:         100
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     130
		Wrestling:   130
		MagicResist: 130
	}
	Attack: {
		Ability: "LifeDrainStrike"
		AbilityChance: 1.0
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
}
