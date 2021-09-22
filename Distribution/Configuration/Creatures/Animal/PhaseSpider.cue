package Animal

PhaseSpider: {
	Name:                 "a phase spider"
	CorpseNameOverride:   "corpse of a phase spider"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  700
	Int:                  350
	Dex:                  60
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          904
	Body:                 28
	CreatureType:         "Animal"
	VirtualArmor:         25
	FightMode:            "Aggressor"
	HitPoison:            "Regular"
	HitsMax:              250
	Hue:                  25125
	ManaMaxSeed:          0
	MinTameSkill:         150
	ProvokeSkillOverride: 90
	StamMaxSeed:          50
	Tamable:              true
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MagicArrow"]
	Resistances: Poison: 2
	Skills: {
		MagicResist: 90
		Tactics:     70
		Wrestling:   125
		Magery:      90
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 389
		HasWebs:  true
	}
}
