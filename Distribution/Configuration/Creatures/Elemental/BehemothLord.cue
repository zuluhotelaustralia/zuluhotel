package Elemental

BehemothLord: {
	Name:               "a Behemoth Lord"
	CorpseNameOverride: "corpse of a Behemoth Lord"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                2950
	Int:                55
	Dex:                400
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        268
	Body:               14
	CreatureType:       "Elemental"
	VirtualArmor:       45
	FightMode:          "Aggressor"
	HitsMax:            2250
	Hue:                54
	LootItemChance:     80
	LootItemLevel:      7
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Tactics:      250
		Wrestling:    150
		MagicResist:  60
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
