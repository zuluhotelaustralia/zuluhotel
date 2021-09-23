package Undead

AnUndeadPirate: {
	Name:               "<random>, an undead pirate"
	CorpseNameOverride: "corpse of <random>, an undead pirate"
	Str:                110
	Int:                20
	Dex:                110
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Aggressor"
	HitsMax:            110
	LootItemChance:     10
	LootItemLevel:      1
	LootTable:          "47"
	ManaMaxSeed:        100
	StamMaxSeed:        100
	Resistances: Poison: 6
	Skills: {
		Tactics:     100
		MagicResist: 30
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 16
		}
	}
	Equipment: [
		{
			ItemType: "Cutlass"
			Lootable: true
		},
		{
			ItemType: "ShortPants"
			Hue:      32
			Lootable: true
		},
		{
			ItemType: "Shirt"
			Hue:      72
			Lootable: true
		},
		{
			ItemType: "SkullCap"
			Hue:      32
			Lootable: true
		},
		{
			ItemType: "LeatherGloves"
			Lootable: true
		},
		{
			ItemType: "BodySash"
			Hue:      40
			Lootable: true
		},
	]
}
