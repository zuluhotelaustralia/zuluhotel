package Animated

GreaterEvilCodexDamnorum: {
	Name:               "a Greater Evil Codex Damnorum"
	CorpseNameOverride: "corpse of a Greater Evil Codex Damnorum"
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
	LootItemChance:     75
	LootItemLevel:      6
	LootTable:          "141"
	ManaMaxSeed:        1600
	StamMaxSeed:        600
	PreferredSpells: ["Darkness", "DecayingRay", "SpectresTouch", "SorcerersBane", "WyvernStrike", "WyvernStrike", "Plague"]
	Resistances: {
		Poison:        6
		Air:           75
		Water:         75
		Fire:          75
		Earth:         75
		Necro:         100
		MagicImmunity: 6
	}
	Skills: {
		Parry:       100
		MagicResist: 120
		Tactics:     100
		Magery:      180
		Healing:     100
	}
	Attack: {
		Damage: {
			Min: 25
			Max: 65
		}
		Skill:     "Swords"
		HitSound:  610
		MissSound: 611
	}
}
