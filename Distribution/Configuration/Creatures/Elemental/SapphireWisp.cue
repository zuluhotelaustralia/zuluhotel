package Elemental

SapphireWisp: {
	Name:                 "a sapphire wisp"
	CorpseNameOverride:   "corpse of a sapphire wisp"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  700
	Int:                  1100
	Dex:                  575
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          466
	Body:                 58
	CreatureType:         "Elemental"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              700
	Hue:                  1171
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "205"
	ManaMaxSeed:          1100
	ProvokeSkillOverride: 120
	StamMaxSeed:          175
	PreferredSpells: ["EnergyBolt", "Lightning", "MassCurse", "WyvernStrike", "SpectresTouch", "SorcerersBane", "IceStrike", "GustOfAir"]
	Resistances: {
		Water:         100
		Earth:         100
		Necro:         50
		MagicImmunity: 6
	}
	Skills: {
		Tactics:     100
		Wrestling:   100
		MagicResist: 200
		Magery:      200
		EvalInt:     200
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 468
	}
}
