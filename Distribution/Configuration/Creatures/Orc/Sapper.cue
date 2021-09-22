package Orc

Sapper: {
	Name:               "<random> the Sapper"
	CorpseNameOverride: "corpse of <random> the Sapper"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                205
	Int:                30
	Dex:                90
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        1114
	Body:               17
	CreatureType:       "Orc"
	VirtualArmor:       45
	FightMode:          "Aggressor"
	HitsMax:            205
	Hue:                1295
	LootItemChance:     6
	LootItemLevel:      3
	LootTable:          "42"
	ManaMaxSeed:        0
	StamMaxSeed:        80
	Skills: {
		Tactics: 300
		Macing:  300
	}
	Attack: {
		Speed: 50
		Damage: {
			Min: 1
			Max: 8
		}
		HitSound: 364
	}
	Equipment: [{
		ItemType: "Server.Items.WarHammer"
		Name:     "Bomber weapon"
	}]
}
