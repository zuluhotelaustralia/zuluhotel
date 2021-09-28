package Human

BrigandMarksmen: {
	Name:                 "a brigand Marksmen"
	CorpseNameOverride:   "corpse of a brigand Marksmen"
	Str:                  95
	Int:                  45
	Dex:                  105
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMaxSeed:          95
	LootItemChance:       1
	LootTable:            "52"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          50
	Skills: {
		MagicResist: 150
		Tactics:     75
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
			ItemType: "Cutlass"
			Lootable: true
		},
		{
			ItemType: "LongPants"
			Hue:      1401
			Lootable: true
		},
		{
			ItemType: "FancyShirt"
			Hue:      71
			Lootable: true
		},
		{
			ItemType: "SkullCap"
			Hue:      0x215
			Lootable: true
		},
		{
			ItemType: "Cloak"
			Hue:      1401
			Lootable: true
		},
		{
			ItemType: "Bow"
			Lootable: true
		},

		{
			ItemType: "ThighBoots"
			Lootable: true
		},
	]
}
