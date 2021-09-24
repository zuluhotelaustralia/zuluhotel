package Undead

LicheLord: {
	Name:               "a Liche Lord"
	CorpseNameOverride: "corpse of a Liche Lord"
	Str:                450
	Int:                600
	Dex:                85
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Undead"
	VirtualArmor:       30
	FightMode:          "Closest"
	HideType:           "Liche"
	Hides:              3
	HitsMax:            450
	Hue:                17969
	LootItemChance:     40
	LootItemLevel:      3
	LootTable:          "75"
	ManaMaxSeed:        250
	StamMaxSeed:        80
	PreferredSpells: ["EnergyBolt", "Explosion", "MassCurse", "Lightning"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 6
	}
	Skills: {
		MagicResist: 100
		Tactics:     70
		Wrestling:   125
		Magery:      120
	}
	Attack: {
		Damage: {
			Min: 19
			Max: 55
		}
		HitSound: 414
	}
}
