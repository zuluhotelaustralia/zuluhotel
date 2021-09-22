package Elemental

AirElementalLord: {
	Name:               "an air elemental lord"
	CorpseNameOverride: "corpse of an air elemental lord"
	BaseType:           "Server.Mobiles.BaseElementalLord"
	Str:                210
	Int:                600
	Dex:                500
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        655
	Body:               13
	CreatureType:       "Elemental"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMax:            210
	Hue:                1050
	LootItemChance:     60
	LootItemLevel:      4
	LootTable:          "73"
	ManaMaxSeed:        900
	StamMaxSeed:        200
	PreferredSpells: ["EnergyBolt", "Lightning", "BladeSpirits"]
	Resistances: Air: 100
	Skills: {
		Tactics:   150
		Wrestling: 170
		Magery:    100
	}
	Attack: {
		Damage: {
			Min: 21
			Max: 45
		}
		HitSound: 265
	}
}
