package Elemental

WaterElementalLord: {
	Name:               "a water elemental lord"
	CorpseNameOverride: "corpse of a water elemental lord"
	Str:                350
	Int:                410
	Dex:                300
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        278
	Body:               16
	CanSwim:            true
	CreatureType:       "Elemental"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMaxSeed:            350
	Hue:                102
	LootItemChance:     60
	LootItemLevel:      4
	LootTable:          "73"
	ManaMaxSeed:        900
	StamMaxSeed:        50
	PreferredSpells: ["EnergyBolt", "Lightning", "ArchCure"]
	Resistances: Water: 100
	Skills: {
		Tactics:     150
		Wrestling:   160
		Magery:      100
		MagicResist: 75
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 280
	}
}
