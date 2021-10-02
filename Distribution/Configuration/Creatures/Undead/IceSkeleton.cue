package Undead

IceSkeleton: {
	Name:               "an ice skeleton"
	CorpseNameOverride: "corpse of an ice skeleton"
	Str:                200
	Int:                35
	Dex:                180
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               57
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:        200
	Hue:                1301
	LootItemChance:     2
	LootTable:          "14"
	ManaMaxSeed:        25
	StamMaxSeed:        50
	Resistances: {
		Poison: 6
		Necro:  75
	}
	Skills: {
		Tactics:     120
		MagicResist: 80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 44
		}
		Skill:    "Swords"
		HitSound: 452
	}
}
