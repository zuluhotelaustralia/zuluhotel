package Undead

ArchVampire: {
	Name:               "Arch Vampire"
	CorpseNameOverride: "corpse of Arch Vampire"
	Str:                550
	Int:                700
	Dex:                550
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	ClassLevel:         4
	ClassType:          "Mage"
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMax:            350
	LootItemChance:     75
	LootItemLevel:      5
	LootTable:          "71"
	ManaMaxSeed:        700
	StamMaxSeed:        550
	PreferredSpells: ["WyvernStrike", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "DispelField", "SpectresTouch", "Darkness", "GustOfAir", "MindBlast", "IceStrike", "ShiftingEarth"]
	Resistances: {
		Poison:        6
		Necro:         75
		MagicImmunity: 3
	}
	Skills: {
		MagicResist: 100
		Tactics:     200
		Wrestling:   200
		Magery:      200
	}
	Attack: {
		Damage: {
			Min: 12
			Max: 40
		}
		HitSound: 363
	}
	Equipment: [{
		ItemType: "SShortHair"
		Hue:      1
	}]
}
