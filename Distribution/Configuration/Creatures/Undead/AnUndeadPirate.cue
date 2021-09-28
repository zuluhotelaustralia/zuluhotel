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
	FightMode:          "Closest"
	HitsMaxSeed:            110
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
			ItemType: "LongPants"
			Hue:      0x3E5
			Lootable: true
		},
		{
			ItemType: "Shirt"
			Hue:      0x192
			Lootable: true
		},
		{
			ItemType: "SkullCap"
			Hue:      0x215
			Lootable: true
		},
		{
			ItemType: "LeatherGloves"
			Lootable: true
		}
	]
}
