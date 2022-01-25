package Undead

ShadeOfZanroth: {
	Name:               "a Shade of Zanroth"
	CorpseNameOverride: "corpse of a Shade of Zanroth"
	Str:                500
	Int:                500
	Dex:                500
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        588
	Body:               970
	CreatureType:       "Undead"
	VirtualArmor:       60
	FightMode:          "Closest"
	FightRange:         8
	HitsMaxSeed:        500
	Hue:                17969
	LootTable:          "142"
	ManaMaxSeed:        500
	StamMaxSeed:        500
	Resistances: {
		Poison: 6
		Necro:  100
	}
	Skills: {
		MagicResist: 120
		Tactics:     180
	}
	Attack: {
		Damage: {
			Min: 31
			Max: 61
		}
		Skill:     "Swords"
		HitSound:  590
		MissSound: 589
		MaxRange:  2
	}
}
