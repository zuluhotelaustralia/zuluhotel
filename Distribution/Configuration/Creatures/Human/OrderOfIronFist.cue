package Human

OrderOfIronFist: {
	Name:               "<random>, Order of the Iron Fist"
	CorpseNameOverride: "corpse of <random>, Order of the Iron Fist"
	Str:                300
	Int:                210
	Dex:                300
	AlwaysMurderer:     true
	CreatureType:       "Human"
	FightMode:          "Closest"
	HitsMax:            300
	LootTable:          "59"
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Skills: {
		Tactics:     120
		MagicResist: 80
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
