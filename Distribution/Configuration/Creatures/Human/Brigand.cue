package Human

Brigand: {
	Name:                 "a brigand"
	CorpseNameOverride:   "corpse of a brigand"
	Str:                  130
	Int:                  105
	Dex:                  300
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMax:              130
	LootItemChance:       1
	LootTable:            "47"
	ManaMaxSeed:          95
	ProvokeSkillOverride: 94
	StamMaxSeed:          50
	Skills: {
		Tactics:     85
		Fencing:     85
		MagicResist: 50
	}
	Attack: {
		Damage: {
			Min: 5
			Max: 50
		}
	}
	Equipment: [
		{
			ItemType: "Cutlass"
			Lootable: true
		},
		{
			ItemType: "LongPants"
			Hue:      71
			Lootable: true
		},
		{
			ItemType: "FancyShirt"
			Hue:      443
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
			Hue:      443
			Lootable: true
		},
	]
}
