package Elemental

BlackWisp: {
	Name:                 "a black wisp"
	CorpseNameOverride:   "corpse of a black wisp"
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
	HitsMaxSeed:              700
	Hue:                  1109
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "35"
	ManaMaxSeed:          1100
	ProvokeSkillOverride: 120
	StamMaxSeed:          175
	PreferredSpells: ["EnergyBolt", "Lightning", "Explosion", "MassCurse", "AbyssalFlame", "WyvernStrike", "SpectresTouch", "SorcerersBane", "WraithBreath", "DecayingRay"]
	Resistances: {
		Necro:         100
		Earth:         100
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
