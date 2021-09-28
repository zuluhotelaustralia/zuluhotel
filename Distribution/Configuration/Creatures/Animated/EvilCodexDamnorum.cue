package Animated

EvilCodexDamnorum: {
	Name:               "an Evil Codex Damnorum"
	CorpseNameOverride: "corpse of an Evil Codex Damnorum"
	Str:                600
	Int:                910
	Dex:                600
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        608
	Body:               985
	CreatureType:       "Animated"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:        850
	Hue:                1645
	LootItemChance:     75
	LootItemLevel:      6
	LootTable:          "141"
	ManaMaxSeed:        1600
	StamMaxSeed:        600
	PreferredSpells: ["Darkness", "DecayingRay", "SpectresTouch", "AbyssalFlame", "WraithBreath", "SorcerersBane", "WyvernStrike", "WyvernStrike", "Plague"]
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
