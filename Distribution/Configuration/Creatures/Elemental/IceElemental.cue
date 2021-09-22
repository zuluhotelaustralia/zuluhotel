package Elemental

IceElemental: {
	Name:               "an ice elemental"
	CorpseNameOverride: "corpse of an ice elemental"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                140
	Int:                110
	Dex:                70
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        278
	Body:               16
	CreatureType:       "Elemental"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMax:            140
	Hue:                1153
	LootItemChance:     50
	LootItemLevel:      3
	LootTable:          "20"
	ManaMaxSeed:        125
	StamMaxSeed:        65
	PreferredSpells: ["MassCurse", "Lightning", "MagicArrow", "Explosion"]
	Resistances: Water: 75
	Skills: {
		MagicResist: 170
		Tactics:     310
		Wrestling:   185
		Magery:      90
	}
	Attack: {
		Damage: {
			Min: 20
			Max: 35
		}
		HitSound: 280
	}
}
