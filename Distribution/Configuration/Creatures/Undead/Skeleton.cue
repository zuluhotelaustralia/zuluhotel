package Undead

Skeleton: {
	Name:               "a skeleton"
	CorpseNameOverride: "corpse of a skeleton"
	Str:                60
	Int:                30
	Dex:                70
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               57
	CreatureType:       "Undead"
	VirtualArmor:       5
	FightMode:          "Closest"
	HitsMax:            60
	Hue:                33784
	LootTable:          "15"
	ManaMaxSeed:        0
	StamMaxSeed:        60
	Resistances: Poison: 6
	Skills: {
		Parry:       50
		Tactics:     60
		Wrestling:   50
		MagicResist: 20
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 16
		}
	}
}
