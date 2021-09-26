package Human

BrigandKing: {
	Name:               "Brigand King"
	CorpseNameOverride: "corpse of Brigand King"
	Str:                600
	Int:                400
	Dex:                400
	AlwaysMurderer:     true
	CreatureType:       "Human"
	FightMode:          "Closest"
	HitsMax:            600
	LootItemChance:     100
	LootItemLevel:      5
	LootTable:          "111"
	ManaMaxSeed:        400
	StamMaxSeed:        400
	Resistances: {
		Poison:        6
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     130
		MagicResist: 130
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 19
			Max: 31
		}
	}
	Equipment: [
		{
			ItemType: "Cutlass"
			Hue:      1172
		},
		{
			ItemType: "LongPants"
			Hue:      1172
		},
		{
			ItemType: "FancyShirt"
			Hue:      1172
		},
		{
			ItemType: "SkullCap"
			Hue:      1172
		},
		{
			ItemType: "Cloak"
			Hue:      1172
		},
	]
}
