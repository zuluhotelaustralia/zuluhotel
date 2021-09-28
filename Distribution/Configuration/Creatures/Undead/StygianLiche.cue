package Undead

StygianLiche: {
	Name:               "a Stygian Liche"
	CorpseNameOverride: "corpse of a Stygian Liche"
	Str:                2000
	Int:                2800
	Dex:                300
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Undead"
	VirtualArmor:       65
	FightMode:          "Closest"
	HitsMaxSeed:            2400
	Hue:                1174
	LootItemChance:     90
	LootItemLevel:      8
	LootTable:          "201"
	ManaMaxSeed:        2000
	StamMaxSeed:        70
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Explosion", "WraithBreath", "WyvernStrike", "Plague", "DecayingRay", "Darkness", "WyvernStrike", "AbyssalFlame", "IceStrike"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Magery:       215
		MagicResist:  200
		Tactics:      190
		Wrestling:    135
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
