package Elemental

FireCyclops: {
	Name:               "a Fire Cyclops"
	CorpseNameOverride: "corpse of a Fire Cyclops"
	Str:                1000
	Int:                255
	Dex:                300
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        604
	Body:               75
	CreatureType:       "Elemental"
	VirtualArmor:       45
	HitsMax:            2000
	Hue:                1209
	LootItemChance:     100
	LootItemLevel:      5
	LootTable:          "9"
	ManaMaxSeed:        1600
	StamMaxSeed:        90
	PreferredSpells: ["Fireball", "RisingFire", "AbyssalFlame"]
	Resistances: {
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Tactics:     180
		Wrestling:   180
		Magery:      180
		MagicResist: 180
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
