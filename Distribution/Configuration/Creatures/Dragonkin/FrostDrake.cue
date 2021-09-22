package Dragonkin

FrostDrake: {
	Name:                 "a frost drake"
	CorpseNameOverride:   "corpse of a frost drake"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  350
	Int:                  385
	Dex:                  290
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "IceCrystal"
	Hides:                5
	HitsMax:              350
	Hue:                  1154
	LootItemChance:       60
	LootItemLevel:        3
	LootTable:            "36"
	ManaMaxSeed:          85
	MinTameSkill:         115
	ProvokeSkillOverride: 130
	StamMaxSeed:          50
	Tamable:              true
	PreferredSpells: [
		"EnergyBolt",
		"Lightning",
	]
	Resistances: Water: 75
	Skills: {
		MagicResist: 100
		Tactics:     100
		Wrestling:   120
		Magery:      110
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
	}
}
