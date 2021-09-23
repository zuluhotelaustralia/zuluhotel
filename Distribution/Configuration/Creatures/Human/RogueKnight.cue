package Human

RogueKnight: {
	Name:                 "a rogue knight"
	CorpseNameOverride:   "corpse of a rogue knight"
	Str:                  350
	Int:                  80
	Dex:                  140
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         40
	FightMode:            "Aggressor"
	HitsMax:              350
	Hue:                  33784
	LootItemChance:       1
	LootTable:            "59"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          125
	Skills: {
		Tactics:     130
		Wrestling:   130
		MagicResist: 65
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 35
		}
	}
	Equipment: [
		{
			ItemType: "Halberd"
			Lootable: true
		},
		{
			ItemType: "LongPants"
			Hue:      0x455
			Lootable: true
		},
		{
			ItemType: "PlateLegs"
			Lootable: true
		},
		{
			ItemType: "PlateArms"
			Lootable: true
		},
		{
			ItemType: "PlateChest"
			Lootable: true
		},
		{
			ItemType: "PlateGloves"
			Lootable: true
		},
		{
			ItemType: "PlateGorget"
			Lootable: true
		},
		{
			ItemType: "PlateHelm"
			Lootable: true
		},
	]
}
