package Undead

Spectre: {
	Name:               "a spectre"
	CorpseNameOverride: "corpse of a spectre"
	Str:                135
	Int:                35
	Dex:                90
	AlwaysMurderer:     true
	BaseSoundID:        1154
	Body:               26
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:        135
	Hue:                25125
	LootItemChance:     5
	LootItemLevel:      1
	LootTable:          "3"
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Resistances: {
		Poison: 6
		Necro:  75
	}
	Skills: {
		Parry:       55
		MagicResist: 60
		Tactics:     120
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 3
			Max: 15
		}
		Skill:    "Swords"
		HitSound: 384
	}
}
