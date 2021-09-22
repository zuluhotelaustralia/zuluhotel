package Undead

SkeletonArcher: {
	Name:               "a skeleton archer"
	CorpseNameOverride: "corpse of a skeleton archer"
	Str:                100
	Int:                25
	Dex:                150
	AiType:             "AI_Archer"
	AlwaysMurderer:     true
	Body:               50
	CreatureType:       "Undead"
	VirtualArmor:       5
	FightMode:          "Closest"
	HitsMax:            100
	Hue:                33784
	LootTable:          "49"
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Resistances: Poison: 6
	Skills: {
		MagicResist: 30
		Tactics:     50
		Wrestling:   95
		Archery:     60
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
}
