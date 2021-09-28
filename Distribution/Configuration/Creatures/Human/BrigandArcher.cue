package Human

BrigandArcher: {
	Name:                 "a brigand archer"
	CorpseNameOverride:   "corpse of a brigand archer"
	Str:                  150
	Int:                  60
	Dex:                  300
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMaxSeed:              150
	LootItemChance:       1
	LootTable:            "41"
	ManaMaxSeed:          350
	ProvokeSkillOverride: 70
	StamMaxSeed:          350
	Skills: {
		MagicResist: 65
		Tactics:     60
		Archery:     140
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
			ItemType: "LeatherGloves"
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
