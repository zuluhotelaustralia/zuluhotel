package Undead

SkeletalWarrior: {
	Name:               "a skeletal warrior"
	CorpseNameOverride: "corpse of a skeletal warrior"
	Str:                300
	Int:                35
	Dex:                350
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               57
	CreatureType:       "Undead"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMaxSeed:        300
	Hue:                1127
	LootItemChance:     5
	LootTable:          "59"
	ManaMaxSeed:        0
	StamMaxSeed:        175
	Resistances: Poison: 6
	Skills: {
		Tactics:     120
		MagicResist: 80
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 13
			Max: 45
		}
		Skill:    "Swords"
		HitSound: 572
	}
}
