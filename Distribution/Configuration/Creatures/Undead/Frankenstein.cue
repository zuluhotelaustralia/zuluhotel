package Undead

Frankenstein: {
	Name:               "Frankenstein"
	CorpseNameOverride: "corpse of Frankenstein"
	Str:                450
	Int:                450
	Dex:                450
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       40
	FightMode:          "Aggressor"
	HitsMax:            450
	Hue:                1433
	LootItemChance:     15
	LootItemLevel:      4
	LootTable:          "37"
	ManaMaxSeed:        450
	StamMaxSeed:        450
	Resistances: Poison: 6
	Skills: {
		Tactics:     130
		Wrestling:   130
		MagicResist: 90
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 25
			Max: 65
		}
		HitSound: 601
	}
	Equipment: [{
		ItemType: "ShortHair"
		Hue:      1
	}]
}
