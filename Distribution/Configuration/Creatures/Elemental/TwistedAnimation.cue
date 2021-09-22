package Elemental

TwistedAnimation: {
	Name:               "a Twisted Animation"
	CorpseNameOverride: "corpse of a Twisted Animation"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                700
	Int:                200
	Dex:                100
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        569
	Body:               581
	ClassLevel:         4
	ClassType:          "Warrior"
	CreatureType:       "Elemental"
	VirtualArmor:       50
	FightMode:          "Aggressor"
	HitsMax:            700
	Hue:                49408
	LootItemChance:     75
	LootItemLevel:      5
	LootTable:          "132"
	ManaMaxSeed:        100
	StamMaxSeed:        50
	Skills: {
		Tactics:     120
		MagicResist: 150
	}
	Attack: {
		Speed: 50
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound:  571
		MissSound: 569
	}
	Equipment: [{
		ItemType: "Server.Items.Longsword"
		Name:     "an animation's blade"
		Hue:      33870
	}, {
		ItemType: "Server.Items.GoldBracelet"
		Name:     "a animation's shield"
	}]
}
