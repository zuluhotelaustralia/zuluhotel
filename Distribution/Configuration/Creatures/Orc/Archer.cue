package Orc

Archer: {
	Name:                 "<random> the archer"
	CorpseNameOverride:   "corpse of <random> the archer"
	Str:                  170
	Int:                  35
	Dex:                  180
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	FightMode:            "Closest"
	HitsMaxSeed:          170
	LootTable:            "52"
	ManaMaxSeed:          25
	ProvokeSkillOverride: 80
	StamMaxSeed:          80
	Skills: {
		MagicResist: 30
		Tactics:     80
		Wrestling:   85
		Archery:     85
	}
	Attack: {
		Damage: {
			Min: 0
			Max: 0
		}
	}
	Equipment: [
		{
			ItemType: "Bow"
			Lootable: true
		},
	]
}
