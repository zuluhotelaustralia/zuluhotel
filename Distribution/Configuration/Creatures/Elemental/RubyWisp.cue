package Elemental

RubyWisp: {
	Name:                 "a ruby wisp"
	CorpseNameOverride:   "corpse of a ruby wisp"
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
	Hue:                  1172
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "204"
	ManaMaxSeed:          1100
	ProvokeSkillOverride: 120
	StamMaxSeed:          175
	PreferredSpells: ["RisingFire", "Explosion", "MassCurse", "AbyssalFlame", "WyvernStrike", "WraithBreath", "DecayingRay"]
	Resistances: {
		Fire:          100
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
