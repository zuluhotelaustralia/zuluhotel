package Elemental

Wisp: {
	Name:                 "a wisp"
	CorpseNameOverride:   "corpse of a wisp"
  Str:                  225
	Int:                  500
	Dex:                  185
	AiType:               "AI_Mage"
	BaseSoundID:          466
	Body:                 58
	CanFly:               true
	CanSwim:              true
	CreatureType:         "Elemental"
	VirtualArmor:         30
	HitsMax:              225
	InitialInnocent:      true
	LootItemChance:       25
	LootItemLevel:        3
	LootTable:            "125"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 160
	StamMaxSeed:          50
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "Curse"]
	Resistances: {
		Poison: 6
		Earth:  100
	}
	Skills: {
		Parry:       80
		Tactics:     100
		MagicResist: 90
		Magery:      120
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 21
			Max: 45
		}
		Skill:    "Swords"
		HitSound: 468
	}
}
