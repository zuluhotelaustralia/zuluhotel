package Daemon

ScorpionKing: {
	Name:                 "a Scorpion King"
	CorpseNameOverride:   "corpse of a Scorpion King"

	Str:                  1500
	Int:                  2000
	Dex:                  200
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          397
	Body:                 48
	CanFly:               true
	CreatureType:         "Daemon"
	VirtualArmor:         75
	FightMode:            "Closest"
	HitPoison:            "Lethal"
	HitsMax:              5000
	Hue:                  1172
	LootItemChance:       90
	LootItemLevel:        8
	LootTable:            "150"
	ManaMaxSeed:          2000
	ProvokeSkillOverride: 160
	StamMaxSeed:          300
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "MassDispel", "SpectresTouch", "WraithBreath", "Darkness"]
	Resistances: {
		Fire:          75
		Water:         75
		Air:           100
		Poison:        6
		Earth:         100
		Necro:         100
		MagicImmunity: 7
	}
	Skills: {
		Parry:        200
		MagicResist:  300
		Tactics:      200
		Wrestling:    150
		Magery:       300
		EvalInt:      200
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 14
			Max: 140
		}
		HitSound: 399
	}
	Equipment: [{
		ItemType:    "SHeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
