package Human

DarkMage: {
	Name:                 "<random> the Dark Mage"
	CorpseNameOverride:   "corpse of <random> the Dark Mage"

	Str:                  200
	Int:                  1000
	Dex:                  195
	PassiveSpeed:         0.2
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BardImmune:           true
	ClassLevel:           10
	ClassType:            "Mage"
	CreatureType:         "Human"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMax:              500
	Hue:                  1109
	LootItemChance:       60
	LootItemLevel:        4
	LootTable:            "138"
	ManaMaxSeed:          1000
	ProvokeSkillOverride: 120
	StamMaxSeed:          195
	PreferredSpells: ["FlameStrike", "WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "DispelField", "SpectresTouch", "Darkness", "GustOfAir", "RisingFire", "MindBlast", "FireField", "IceStrike", "MeteorSwarm", "ShiftingEarth", "CallLightning"]
	Resistances: MagicImmunity: 6
	Skills: {
		Macing:      95
		Tactics:     75
		MagicResist: 150
		Magery:      150
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 40
		}
		HitSound: 315
	}
	Equipment: [{
		ItemType: "SLongHair"
		Hue:      1
	}, {
		ItemType: "SGnarledStaff"
		Name:     "Evil Mage Weapon"
	}]
}
