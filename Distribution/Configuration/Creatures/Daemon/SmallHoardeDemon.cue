package Daemon

SmallHoardeDemon: {
	Name:               "a Small Hoarde Demon"
	CorpseNameOverride: "corpse of a Small Hoarde Demon"
	Str:                600
	Int:                100
	Dex:                500
	AlwaysMurderer:     true
	BaseSoundID:        925
	Body:               776
	CreatureType:       "Daemon"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMax:            600
	LootItemChance:     50
	LootItemLevel:      5
	LootTable:          "69"
	ManaMaxSeed:        100
	StamMaxSeed:        400
	Resistances: {
		Poison:        1
		MagicImmunity: 1
	}
	Skills: {
		Tactics:     130
		Wrestling:   130
		MagicResist: 130
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
	Equipment: [{
		ItemType: "ShortHair"
		Hue:      1
	}]
}
