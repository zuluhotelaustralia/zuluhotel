package Human

OrderOfIronFist: {
	Name:               "<random>, Order of the Iron Fist"
	CorpseNameOverride: "corpse of <random>, Order of the Iron Fist"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                300
	Int:                210
	Dex:                300
	AlwaysMurderer:     true
	CreatureType:       "Human"
	FightMode:          "Aggressor"
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
}
