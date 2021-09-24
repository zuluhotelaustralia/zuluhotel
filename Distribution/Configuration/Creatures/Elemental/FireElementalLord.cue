package Elemental

FireElementalLord: {
	Name:               "a fire elemental lord"
	CorpseNameOverride: "corpse of a fire elemental lord"
	Str:                300
	Int:                400
	Dex:                300
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        838
	Body:               15
	CreatureType:       "Elemental"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMax:            300
	Hue:                137
	LootItemChance:     60
	LootItemLevel:      4
	LootTable:          "73"
	ManaMaxSeed:        900
	StamMaxSeed:        100
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "Explosion"]
	Resistances: Fire: 100
	Skills: {
		Tactics:     130
		Wrestling:   175
		Magery:      100
		MagicResist: 75
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 6
			Max: 36
		}
		HitSound: 275
	}
}
