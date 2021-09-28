package Elemental

UmbralFlicker: {
	Name:               "an Umbral Flicker"
	CorpseNameOverride: "corpse of an Umbral Flicker"
	Str:                450
	Int:                2200
	Dex:                250
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        466
	Body:               58
	CanFly:             true
	CanSwim:            true
	ClassLevel:         4
	ClassType:          "Mage"
	CreatureType:       "Elemental"
	VirtualArmor:       40
	HitsMaxSeed:            450
	Hue:                25125
	LootItemChance:     75
	LootItemLevel:      5
	LootTable:          "130"
	ManaMaxSeed:        2200
	StamMaxSeed:        50
	PreferredSpells: ["Explosion", "GreaterHeal", "CallLightning", "GustOfAir", "IceStrike", "ShiftingEarth", "RisingFire", "WyvernStrike", "AbyssalFlame", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "MassDispel", "WraithBreath", "Darkness"]
	Resistances: {
		Poison: 6
		Fire:   100
		Necro:  100
		Earth:  100
	}
	Skills: {
		Tactics:     120
		MagicResist: 130
		Magery:      150
		EvalInt:     150
	}
	Attack: {
		Speed:    35
		Skill:    "Swords"
		HitSound: 468
	}
}
