package Ophidian

OphidianApprenticeMage: {
	Name:                 "an Ophidian Apprentice Mage"
	CorpseNameOverride:   "corpse of an Ophidian Apprentice Mage"
  Str:                  150
	Int:                  400
	Dex:                  210
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          639
	Body:                 85
	CreatureType:         "Ophidian"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Serpent"
	Hides:                5
	HitsMaxSeed:              550
	LootTable:            "76"
	ManaMaxSeed:          400
	ProvokeSkillOverride: 110
	StamMaxSeed:          70
	PreferredSpells: ["Lightning", "EnergyBolt", "Fireball", "Explosion", "DispelField"]
	Skills: {
		Parry:       70
		Magery:      80
		Wrestling:   70
		Tactics:     70
		MagicResist: 80
		EvalInt:     80
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 26
		}
		MissSound: 360
	}
}
