package Orc

Marksman: {
	Name:                 "<random> the marksman"
	CorpseNameOverride:   "corpse of <random> the marksman"

	Str:                  135
	Int:                  35
	Dex:                  190
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	VirtualArmor:         10
	FightMode:            "Closest"
	HitsMax:              135
	LootTable:            "52"
	ManaMaxSeed:          25
	ProvokeSkillOverride: 80
	StamMaxSeed:          90
	Skills: {
		MagicResist: 50
		Tactics:     100
		Wrestling:   65
		Archery:     100
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 3
			Max: 12
		}
		HitSound: 231
	}
}
