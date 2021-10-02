package Terathan

TerathanStriker: {
	Name:                 "a Terathan Striker"
	CorpseNameOverride:   "corpse of a Terathan Striker"
	Str:                  370
	Int:                  55
	Dex:                  635
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          594
	Body:                 71
	CreatureType:         "Terathan"
	VirtualArmor:         10
	FightMode:            "Closest"
	HitsMaxSeed:          370
	Hue:                  11
	LootTable:            "64"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          635
	Resistances: {
		Fire: 75
		Air:  75
	}
	Skills: {
		Tactics:     100
		MagicResist: 100
		Parry:       100
	}
	Attack: {
		Speed: 43
		Damage: {
			Min: 8
			Max: 44
		}
		Skill:     "Swords"
		HitSound:  593
		MissSound: 594
	}
}
