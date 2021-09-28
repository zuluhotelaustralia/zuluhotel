package Undead

UndeadLizard: {
	Name:               "an Undead Lizard"
	CorpseNameOverride: "corpse of an Undead Lizard"
	Str:                290
	Int:                300
	Dex:                85
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        412
	Body:               33
	CreatureType:       "Undead"
	VirtualArmor:       30
	FightMode:          "Closest"
	HideType:           "Lizard"
	Hides:              5
	HitsMaxSeed:            290
	Hue:                17969
	LootItemChance:     75
	LootItemLevel:      3
	LootTable:          "46"
	ManaMaxSeed:        250
	StamMaxSeed:        80
	PreferredSpells: ["MagicArrow", "EnergyBolt", "Explosion", "MassCurse", "Fireball"]
	Resistances: {
		Poison:        6
		MagicImmunity: 3
	}
	Skills: {
		MagicResist: 60
		Tactics:     70
		Magery:      90
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 414
	}
}
