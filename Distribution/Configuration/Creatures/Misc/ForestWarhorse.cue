package Misc

ForestWarhorse: {
	Name:                 "a forest warhorse"
	CorpseNameOverride:   "corpse of a forest warhorse"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  500
	Int:                  500
	Dex:                  500
	BaseSoundID:          168
	Body:                 121
	VirtualArmor:         40
	Fame:                 1
	FightMode:            "Aggressor"
	HitsMax:              500
	Karma:                1
	LootItemChance:       100
	LootTable:            "26"
	ManaMaxSeed:          500
	MinTameSkill:         120
	ProvokeSkillOverride: 120
	StamMaxSeed:          500
	Tamable:              true
	Skills: {
		MagicResist: 120
		EvalInt:     100
		Tactics:     120
		Wrestling:   110
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 2
			Max: 16
		}
	}
}
