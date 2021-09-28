package Elemental

FrostWisp: {
	Name:                 "a frost wisp"
	CorpseNameOverride:   "corpse of a frost wisp"
  Str:                  900
	Int:                  1600
	Dex:                  575
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          466
	Body:                 58
	CreatureType:         "Elemental"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMaxSeed:              650
	Hue:                  1154
	LootItemChance:       80
	LootItemLevel:        6
	LootTable:            "35"
	ManaMaxSeed:          100
	ProvokeSkillOverride: 130
	StamMaxSeed:          175
	PreferredSpells: ["WyvernStrike", "IceStrike", "CallLightning", "MassCurse", "Plague", "WyvernStrike", "SorcerersBane"]
	Resistances: {
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 6
	}
	Skills: {
		EvalInt:     200
		Tactics:     100
		Wrestling:   100
		MagicResist: 200
		Magery:      200
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
