package Undead

Wraith: {
	Name:               "a wraith"
	CorpseNameOverride: "corpse of a wraith"
	Str:                125
	Int:                1025
	Dex:                380
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1154
	Body:               26
	CreatureType:       "Undead"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMaxSeed:            125
	Hue:                16385
	LootItemChance:     80
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        1025
	StamMaxSeed:        80
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "MassDispel", "SpectresTouch", "WraithBreath", "IceStrike"]
	Resistances: {
		Poison:   6
		Necro:    100
		Physical: 4
	}
	Skills: {
		EvalInt:     100
		Magery:      150
		Parry:       80
		MagicResist: 105
		Tactics:     120
		Wrestling:   120
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 15
			Max: 40
		}
		HitSound:      642
		MissSound:     641
		Ability:       "TriElementalStrike"
		AbilityChance: 1
	}
}
