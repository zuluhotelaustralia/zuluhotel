package Elemental

RadiantWisp: {
	Name:                 "a Radiant Wisp"
	CorpseNameOverride:   "corpse of a Radiant Wisp"

	Str:                  350
	Int:                  550
	Dex:                  275
	AiType:               "AI_Mage"
	AutoDispel:           true
	BaseSoundID:          466
	Body:                 58
	CanFly:               true
	CanSwim:              true
	CreatureType:         "Elemental"
	VirtualArmor:         40
	HitsMax:              350
	Hue:                  1154
	InitialInnocent:      true
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "80"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 125
	StamMaxSeed:          50
	PreferredSpells: ["EnergyBolt", "Explosion", "GreaterHeal", "CallLightning", "GustOfAir", "IceStrike", "ShiftingEarth", "RisingFire", "Darkness", "WraithBreath"]
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
		Speed: 35
		Damage: {
			Min: 10
			Max: 80
		}
		Skill:    "Swords"
		HitSound: 468
	}
}
