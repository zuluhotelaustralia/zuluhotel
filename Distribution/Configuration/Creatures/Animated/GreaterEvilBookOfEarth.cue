package Animated

GreaterEvilBookOfEarth: {
	Name:               "a Greater Evil Book of the Earth"
	CorpseNameOverride: "corpse of a Greater Evil Book of the Earth"
	Str:                1600
	Int:                1910
	Dex:                1600
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        608
	Body:               985
	CreatureType:       "Animated"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:            1850
	Hue:                1645
	LootItemChance:     65
	LootItemLevel:      7
	LootTable:          "9"
	ManaMaxSeed:        1600
	StamMaxSeed:        800
	PreferredSpells: ["ShiftingEarth", "GustOfAir", "CallLightning", "IceStrike", "MassDispel", "Clumsy"]
	Resistances: {
		Poison:        6
		Air:           100
		Water:         25
		Fire:          100
		Earth:         100
		Necro:         25
		MagicImmunity: 8
	}
	Skills: {
		Parry:       150
		MagicResist: 150
		Tactics:     150
		Magery:      185
		Healing:     175
	}
	Attack: {
		Damage: {
			Min: 25
			Max: 70
		}
		Skill:     "Swords"
		HitSound:  610
		MissSound: 611
	}
}
