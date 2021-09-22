package Elemental

AirElemental: {
	Name:               "an air elemental"
	CorpseNameOverride: "corpse of an air elemental"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                205
	Int:                205
	Dex:                150
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        655
	Body:               13
	CanFly:             true
	CreatureType:       "Elemental"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMax:            205
	Hue:                33784
	LootItemChance:     25
	LootItemLevel:      3
	LootTable:          "20"
	ManaMaxSeed:        195
	StamMaxSeed:        140
	PreferredSpells: [
		"EnergyBolt",
		"Lightning",
	]
	Resistances: Air: 100
	Skills: {
		Parry:       65
		MagicResist: 75
		Tactics:     100
		Magery:      90
		EvalInt:     75
	}
	Attack: {
		Damage: {
			Min: 5
			Max: 30
		}
		Skill:    "Swords"
		HitSound: 265
	}
}
