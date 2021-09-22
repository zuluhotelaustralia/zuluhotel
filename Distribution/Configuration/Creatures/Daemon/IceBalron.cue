package Daemon

IceBalron: {
	Name:               "an Ice Balron"
	CorpseNameOverride: "corpse of an Ice Balron"
	Str:                1100
	Int:                2200
	Dex:                150
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        357
	Body:               40
	CanFly:             true
	CreatureType:       "Daemon"
	VirtualArmor:       75
	FightMode:          "Closest"
	HitsMax:            1100
	Hue:                1152
	LootItemChance:     95
	LootItemLevel:      7
	LootTable:          "9"
	ManaMaxSeed:        2000
	StamMaxSeed:        70
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "SorcerersBane", "Earthquake", "DispelField", "MassDispel", "SpectresTouch", "WraithBreath", "Darkness"]
	Resistances: {
		Fire:          100
		Water:         75
		Air:           100
		Poison:        6
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Parry:        200
		MagicResist:  150
		Tactics:      200
		Wrestling:    150
		Magery:       220
		EvalInt:      200
		DetectHidden: 200
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 25
			Max: 75
		}
	}
	Equipment: [{
		ItemType:    "SHeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
