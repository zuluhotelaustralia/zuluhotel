package Ophidian

OphidianJusticar: {
	Name:                 "an Ophidian Justicar"
	CorpseNameOverride:   "corpse of an Ophidian Justicar"

	Str:                  500
	Int:                  1000
	Dex:                  210
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          639
	Body:                 85
	CreatureType:         "Ophidian"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Serpent"
	Hides:                5
	HitsMax:              500
	LootTable:            "66"
	ManaMaxSeed:          1000
	ProvokeSkillOverride: 110
	StamMaxSeed:          700
	PreferredSpells: ["Lightning", "EnergyBolt", "Fireball", "Explosion"]
	Skills: {
		Parry:       100
		Magery:      150
		Wrestling:   110
		Tactics:     100
		MagicResist: 130
		EvalInt:     80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 43
		}
		MissSound: 360
	}
}
