package Daemon

Balron: {
	Name:               "a Balron"
	CorpseNameOverride: "corpse of a Balron"
	Str:                1100
	Int:                2000
	Dex:                150
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	AutoDispel:         true
	BaseSoundID:        357
	Body:               40
	CanFly:             true
	ClassLevel:         3
	ClassType:          "Mage"
	CreatureType:       "Daemon"
	VirtualArmor:       75
	FightMode:          "Closest"
	HideType:           "Balron"
	Hides:              1
	HitsMaxSeed:            1100
	Hue:                16385
	LootItemChance:     80
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        2000
	StamMaxSeed:        70
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "MassDispel", "SpectresTouch", "WraithBreath", "Darkness"]
	Resistances: {
		Fire:   100
		Water:  75
		Air:    100
		Poison: 6
		Necro:  100
		Earth:  75
	}
	Skills: {
		MagicResist:  150
		Tactics:      200
		Wrestling:    150
		Magery:       200
		EvalInt:      200
		Parry:        130
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
		ItemType:    "HeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
