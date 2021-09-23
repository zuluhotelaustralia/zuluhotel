package Misc

HellbornePaladinsRevenant: {
	Name:                 "The Hellborne Paladin's Revenant"
	CorpseNameOverride:   "corpse of The Hellborne Paladin's Revenant"
  Str:                  1500
	Int:                  2000
	Dex:                  200
	PassiveSpeed:         0.2
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	Body:                 970
	CanFly:               true
	FightMode:            "Closest"
	HitsMax:              5000
	Hue:                  1157
	LootItemChance:       100
	LootItemLevel:        10
	LootTable:            "150"
	ManaMaxSeed:          6000
	ProvokeSkillOverride: 160
	StamMaxSeed:          300
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "MassDispel", "SpectresTouch", "WraithBreath", "Darkness"]
	Resistances: {
		Fire:          100
		Water:         75
		Air:           100
		Poison:        6
		Earth:         75
		Necro:         100
		MagicImmunity: 7
	}
	Skills: {
		Parry:        200
		MagicResist:  250
		Tactics:      200
		Wrestling:    300
		Magery:       300
		EvalInt:      200
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Damage: {
			Min: 0
			Max: 0
		}
	}
}
