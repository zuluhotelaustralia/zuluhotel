package Orc

OrcishBerserker: {
	Name:               "Orcish Berserker"
	CorpseNameOverride: "corpse of Orcish Berserker"
	Str:                2250
	Int:                55
	Dex:                400
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        1114
	Body:               7
	CreatureType:       "Orc"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMaxSeed:            2250
	Hue:                1172
	LootItemChance:     70
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
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
