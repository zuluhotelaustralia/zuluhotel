package Dragonkin

LizardShaman: {
	Name:                 "<random> the Lizard Shaman"
	CorpseNameOverride:   "corpse of <random> the Lizard Shaman"
  Str:                  170
	Int:                  280
	Dex:                  110
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          418
	Body:                 33
	CreatureType:         "Dragonkin"
	VirtualArmor:         10
	FightMode:            "Closest"
	HideType:             "Lizard"
	Hides:                5
	HitsMaxSeed:              170
	Hue:                  1218
	LootItemChance:       50
	LootItemLevel:        1
	LootTable:            "54"
	ManaMaxSeed:          100
	ProvokeSkillOverride: 105
	StamMaxSeed:          100
	PreferredSpells: [
		"EnergyBolt",
		"Curse",
	]
	Skills: {
		Tactics:     80
		Wrestling:   75
		MagicResist: 90
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 420
	}
}
