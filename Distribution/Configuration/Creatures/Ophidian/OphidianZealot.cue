package Ophidian

OphidianZealot: {
	Name:                 "an Ophidian Zealot"
	CorpseNameOverride:   "corpse of an Ophidian Zealot"

	Str:                  375
	Int:                  400
	Dex:                  510
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          639
	Body:                 85
	CreatureType:         "Ophidian"
	VirtualArmor:         25
	FightMode:            "Closest"
	HideType:             "Serpent"
	Hides:                5
	HitsMax:              375
	LootTable:            "66"
	ManaMaxSeed:          400
	ProvokeSkillOverride: 120
	StamMaxSeed:          70
	PreferredSpells: ["Lightning", "EnergyBolt", "Fireball", "Explosion"]
	Skills: {
		Parry:       70
		Magery:      110
		Wrestling:   70
		Tactics:     70
		MagicResist: 50
		EvalInt:     90
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 44
		}
		MissSound: 360
	}
}
