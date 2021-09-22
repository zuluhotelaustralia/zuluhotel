package Ophidian

OphidianShaman: {
	Name:               "an Ophidian Shaman"
	CorpseNameOverride: "corpse of an Ophidian Shaman"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                500
	Int:                350
	Dex:                160
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        639
	Body:               85
	CreatureType:       "Ophidian"
	VirtualArmor:       20
	FightMode:          "Closest"
	HideType:           "Serpent"
	Hides:              5
	HitsMax:            500
	Hue:                88
	LootItemChance:     30
	LootItemLevel:      4
	LootTable:          "69"
	ManaMaxSeed:        200
	StamMaxSeed:        160
	PreferredSpells: ["Lightning", "EnergyBolt", "Fireball", "Explosion"]
	Resistances: {
		Air:   75
		Water: 75
	}
	Skills: {
		Parry:       120
		Magery:      120
		Wrestling:   120
		Tactics:     70
		MagicResist: 90
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 30
		}
		MissSound: 360
	}
}
