package Daemon

BalronLord: {
	Name:               "a Balron Lord"
	CorpseNameOverride: "corpse of a Balron Lord"
	Str:                1100
	Int:                3000
	Dex:                300
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	AutoDispel:         true
	BardImmune:         true
	BaseSoundID:        1149
	Body:               784
	CanFly:             true
	CreatureType:       "Daemon"
	VirtualArmor:       75
	FightMode:          "Closest"
	FightRange:         3
	HitsMax:            2200
	Hue:                1108
	LootItemChance:     100
	LootItemLevel:      9
	LootTable:          "201"
	ManaMaxSeed:        2000
	StamMaxSeed:        70
	PreferredSpells: ["FlameStrike", "WyvernStrike", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "DispelField", "SpectresTouch", "Darkness", "GustOfAir", "MindBlast", "FireField", "IceStrike", "MeteorSwarm", "ShiftingEarth", "CallLightning"]
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
		MagicResist:  200
		Tactics:      200
		Wrestling:    150
		Magery:       250
		Hiding:       200
		EvalInt:      200
		DetectHidden: 200
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 24
			Max: 105
		}
		HitSound:  772
		MissSound: 770
		MaxRange:  3
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
