package Elemental

Djinn: {
	Name:               "a Djinn"
	CorpseNameOverride: "corpse of a Djinn"
	Str:                450
	Int:                255
	Dex:                100
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        609
	Body:               76
	CreatureType:       "Elemental"
	VirtualArmor:       45
	HideType:           "Lava"
	Hides:              4
	HitsMaxSeed:            450
	Hue:                1209
	LootItemChance:     60
	LootItemLevel:      5
	LootTable:          "46"
	ManaMaxSeed:        160
	StamMaxSeed:        90
	PreferredSpells: ["Fireball", "RisingFire", "AbyssalFlame"]
	Resistances: {
		Fire:          100
		MagicImmunity: 3
	}
	Skills: {
		Tactics:     125
		Wrestling:   175
		Magery:      100
		MagicResist: 130
	}
	Attack: {
		Damage: {
			Min: 6
			Max: 60
		}
		HitSound:  606
		MissSound: 360
	}
}
