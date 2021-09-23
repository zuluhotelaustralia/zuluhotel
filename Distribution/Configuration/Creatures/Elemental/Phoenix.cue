package Elemental

Phoenix: {
	Name:                 "a Phoenix"
	CorpseNameOverride:   "corpse of a Phoenix"
  Str:                  450
	Int:                  500
	Dex:                  400
	AiType:               "AI_Mage"
	AutoDispel:           true
	BaseSoundID:          750
	Body:                 5
	CreatureType:         "Elemental"
	VirtualArmor:         40
	HideType:             "Lava"
	Hides:                2
	HitsMax:              450
	Hue:                  1645
	LootItemChance:       95
	LootItemLevel:        4
	ManaMaxSeed:          10
	MinTameSkill:         105
	ProvokeSkillOverride: 120
	RiseCreatureDelay:    "00:00:04"
	StamMaxSeed:          60
	Tamable:              true
	PreferredSpells: ["Lightning", "EnergyBolt", "Fireball", "Explosion"]
	Resistances: Fire: 100
	Skills: {
		MagicResist:  85
		Tactics:      110
		Magery:       130
		EvalInt:      120
		DetectHidden: 130
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 17
			Max: 52
		}
		Skill:    "Swords"
		HitSound: 143
	}
}
