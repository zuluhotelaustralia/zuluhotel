package Undead

UndeadDrake: {
	Name:                 "an Undead Drake"
	CorpseNameOverride:   "corpse of an Undead Drake"
	Str:                  300
	Int:                  400
	Dex:                  110
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Undead"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Liche"
	Hides:                5
	HitsMaxSeed:          300
	Hue:                  1109
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "37"
	ManaMaxSeed:          80
	MinTameSkill:         115
	ProvokeSkillOverride: 120
	StamMaxSeed:          100
	Tamable:              true
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "SorcerersBane", "DecayingRay", "Darkness"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        70
		MagicResist:  100
		Tactics:      100
		Wrestling:    130
		EvalInt:      100
		DetectHidden: 130
	}
	Attack: {
		Ability:       "LifeDrainStrike"
		AbilityChance: 1.0
		Speed:         45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
	}
}
