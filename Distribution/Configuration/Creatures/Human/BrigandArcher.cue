package Human

BrigandArcher: {
	Name:                 "a brigand archer"
	CorpseNameOverride:   "corpse of a brigand archer"

	Str:                  150
	Int:                  60
	Dex:                  300
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMax:              150
	LootItemChance:       1
	LootTable:            "41"
	ManaMaxSeed:          350
	ProvokeSkillOverride: 70
	StamMaxSeed:          350
	Skills: {
		MagicResist: 65
		Tactics:     60
		Archery:     140
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
}
