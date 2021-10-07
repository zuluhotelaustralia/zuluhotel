package Dragonkin

PoisonWyrm: {
	Name:                 "a Poison Wyrm"
	CorpseNameOverride:   "corpse of a Poison Wyrm"
	Str:                  1900
	Int:                  400
	Dex:                  340
	ActiveSpeed:          0.05
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BardImmune:           true
	BaseSoundID:          362
	Body:                 59
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         60
	FightMode:            "Closest"
	HitsMaxSeed:          800
	Hue:                  264
	LootItemChance:       90
	LootItemLevel:        6
	LootTable:            "37"
	ManaMaxSeed:          200
	MinTameSkill:         140
	ProvokeSkillOverride: 140
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse"]
	Resistances: {
		Poison:        6
		MagicImmunity: 5
	}
	Skills: {
		Parry:        80
		MagicResist:  165
		Tactics:      120
		Wrestling:    140
		Poisoning:    140
		DetectHidden: 130
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 20
			Max: 155
		}
		HitSound:  362
		HasBreath: true
		HitPoison: "Deadly"
	}
}
