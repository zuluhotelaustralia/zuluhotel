package Plant

TwistedBramble: {
	Name:               "a Twisted Bramble"
	CorpseNameOverride: "corpse of a Twisted Bramble"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                500
	Int:                500
	Dex:                150
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        442
	Body:               47
	ClassLevel:         3
	ClassType:          "Mage"
	CreatureType:       "Plant"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMax:            500
	Hue:                1282
	LootItemChance:     75
	LootItemLevel:      4
	LootTable:          "129"
	ManaMaxSeed:        500
	StamMaxSeed:        100
	PreferredSpells: ["EnergyBolt", "Lightning", "MindBlast", "Explosion", "Paralyze", "Weaken", "Curse"]
	Resistances: {
		Poison: 6
		Earth:  100
	}
	Skills: {
		Tactics:     100
		Wrestling:   150
		MagicResist: 75
		Magery:      120
		EvalInt:     100
	}
	Attack: {
		Damage: {
			Min: 15
			Max: 50
		}
		HitSound: 444
	}
}
