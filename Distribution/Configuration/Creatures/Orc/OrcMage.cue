package Orc

OrcMage: {
	Name:                 "<random> the Orcmage"
	CorpseNameOverride:   "corpse of <random> the Orcmage"

	Str:                  195
	Int:                  300
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	VirtualArmor:         15
	FightMode:            "Closest"
	HitsMax:              195
	Hue:                  201
	LootItemChance:       75
	LootItemLevel:        2
	LootTable:            "31"
	ManaMaxSeed:          90
	ProvokeSkillOverride: 105
	StamMaxSeed:          80
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "Poison", "MagicArrow", "Fireball", "Paralyze", "Curse"]
	Skills: {
		MagicResist: 70
		Tactics:     50
		Wrestling:   75
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 2
			Max: 8
		}
		HitSound: 434
	}
}
