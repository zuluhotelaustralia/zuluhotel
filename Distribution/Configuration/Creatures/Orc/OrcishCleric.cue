package Orc

OrcishCleric: {
	Name:                 "<random> the Orcish Cleric"
	CorpseNameOverride:   "corpse of <random> the Orcish Cleric"
  Str:                  215
	Int:                  290
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	VirtualArmor:         15
	FightMode:            "Closest"
	HitsMaxSeed:              215
	Hue:                  1401
	LootItemChance:       60
	LootItemLevel:        1
	LootTable:            "31"
	ManaMaxSeed:          90
	ProvokeSkillOverride: 115
	StamMaxSeed:          80
	PreferredSpells: [
		"Lightning",
	]
	Skills: {
		MagicResist: 80
		Tactics:     50
		Wrestling:   75
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 435
	}
}
