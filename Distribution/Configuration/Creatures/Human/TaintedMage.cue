package Human

TaintedMage: {
	Name:               "<random> the Tainted Mage"
	CorpseNameOverride: "corpse of <random> the Tainted Mage"
	Str:                200
	Int:                1000
	Dex:                150
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	ClassLevel:         3
	ClassType:          "Mage"
	CreatureType:       "Human"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:        200
	Hue:                1
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "131"
	ManaMaxSeed:        1000
	RiseCreatureDelay:  "00:00:02"
	StamMaxSeed:        50
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "DispelField", "MassDispel", "WraithBreath", "Darkness"]
	Skills: {
		Tactics:     100
		MagicResist: 120
		EvalInt:     120
		Magery:      140
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 15
		}
		Animation: "Bash2H"
		HitSound:  315
		MissSound: 563
	}
	Equipment: [{
		ItemType: "GnarledStaff"
		Name:     "an ebony staff"
		Hue:      1157
	}, {
		ItemType: "DeathShroud"
		Name:     "a tattered mage's robe"
		Hue:      1302
	}, {
		ItemType:    "LeatherGloves"
		Name:        "a pair of black leather gloves"
		Hue:         1
		ArmorRating: 1
	}, {
		ItemType: "Boots"
		Name:     "a pair of black leather boots"
		Hue:      1
	}]
}
