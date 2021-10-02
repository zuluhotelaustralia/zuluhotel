package Ratkin

RatmanMarksman: {
	Name:                 "<random> the ratman marksman"
	CorpseNameOverride:   "corpse of <random> the ratman marksman"
	Str:                  160
	Int:                  35
	Dex:                  180
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          437
	Body:                 42
	CreatureType:         "Ratkin"
	VirtualArmor:         5
	FightMode:            "Closest"
	HideType:             "Rat"
	Hides:                5
	HitsMaxSeed:          160
	LootTable:            "51"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 55
	StamMaxSeed:          50
	Skills: {
		MagicResist: 40
		Tactics:     60
		Wrestling:   65
		Archery:     100
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
	Equipment: [
		{
			ItemType: "Bow"
			Lootable: true
		},
	]
}
