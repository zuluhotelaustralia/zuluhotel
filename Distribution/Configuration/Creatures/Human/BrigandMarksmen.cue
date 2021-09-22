package Human

BrigandMarksmen: {
	Name:                 "a brigand Marksmen"
	CorpseNameOverride:   "corpse of a brigand Marksmen"

	Str:                  95
	Int:                  45
	Dex:                  105
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMax:              95
	LootItemChance:       1
	LootTable:            "52"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          50
	Skills: {
		MagicResist: 150
		Tactics:     75
		Archery:     100
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
}
