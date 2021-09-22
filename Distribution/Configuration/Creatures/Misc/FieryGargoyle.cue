package Misc

FieryGargoyle: {
	Name:                 "a fiery gargoyle"
	CorpseNameOverride:   "corpse of a fiery gargoyle"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  600
	Int:                  250
	Dex:                  145
	AiType:               "AI_Mage"
	BaseSoundID:          372
	Body:                 4
	VirtualArmor:         40
	Fame:                 3
	FightMode:            "Closest"
	HitsMax:              600
	Karma:                3
	LootItemChance:       100
	LootTable:            "42"
	ManaMaxSeed:          250
	ProvokeSkillOverride: 120
	StamMaxSeed:          145
	PreferredSpells: ["MagicArrow", "Harm", "Fireball", "Poison", "Lightning", "MindBlast", "Paralyze", "EnergyBolt", "Explosion", "FlameStrike"]
	Skills: {
		MagicResist: 100
		Tactics:     100
		Wrestling:   100
	}
	Attack: {
		Damage: {
			Min: 6
			Max: 36
		}
	}
}
