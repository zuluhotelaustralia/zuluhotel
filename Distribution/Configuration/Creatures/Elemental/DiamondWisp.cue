package Elemental

DiamondWisp: {
	Name:                 "a diamond wisp"
	CorpseNameOverride:   "corpse of a diamond wisp"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  1700
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
	HitsMax:              1400
	Hue:                  1176
	LootItemChance:       75
	LootItemLevel:        6
	LootTable:            "203"
	ManaMaxSeed:          1100
	ProvokeSkillOverride: 120
	StamMaxSeed:          175
	PreferredSpells: ["MassCurse", "AbyssalFlame", "WyvernStrike", "SpectresTouch", "SorcerersBane", "WraithBreath", "DecayingRay", "WyvernStrike"]
	Resistances: {
		Air:           100
		Necro:         100
		Earth:         100
		MagicImmunity: 6
	}
	Skills: {
		Tactics:     200
		Wrestling:   200
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
