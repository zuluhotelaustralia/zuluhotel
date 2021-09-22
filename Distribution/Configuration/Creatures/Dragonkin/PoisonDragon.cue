package Dragonkin

PoisonDragon: {
	Name:                 "a Poison Dragon"
	CorpseNameOverride:   "corpse of a Poison Dragon"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  600
	Int:                  400
	Dex:                  340
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         45
	FightMode:            "Aggressor"
	HideType:             "Dragon"
	Hides:                5
	HitPoison:            "Deadly"
	HitsMax:              600
	Hue:                  264
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          200
	MinTameSkill:         140
	ProvokeSkillOverride: 140
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse"]
	Resistances: MagicImmunity: 3
	Skills: {
		Parry:        80
		MagicResist:  95
		Tactics:      120
		Wrestling:    140
		Poisoning:    140
		DetectHidden: 130
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound:  364
		HasBreath: true
	}
}
