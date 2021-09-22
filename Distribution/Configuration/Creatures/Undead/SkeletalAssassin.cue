package Undead

SkeletalAssassin: {
	Name:               "a skeletal assassin"
	CorpseNameOverride: "corpse of a skeletal assassin"
	Str:                130
	Int:                60
	Dex:                125
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               50
	CreatureType:       "Undead"
	VirtualArmor:       35
	FightMode:          "Aggressor"
	HitsMax:            130
	Hue:                17969
	LootItemChance:     4
	LootTable:          "10"
	ManaMaxSeed:        50
	StamMaxSeed:        115
	Resistances: {
		Poison: 6
		Necro:  75
	}
	Skills: {
		MagicResist: 50
		Tactics:     60
		Hiding:      90
		Stealth:     85
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 44
		}
		HitSound: 453
	}
}
