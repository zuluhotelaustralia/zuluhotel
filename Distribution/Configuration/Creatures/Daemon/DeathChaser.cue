package Daemon

DeathChaser: {
	Name:                 "a Death Chaser"
	CorpseNameOverride:   "corpse of a Death Chaser"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  950
	Int:                  650
	Dex:                  475
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          357
	Body:                 43
	CreatureType:         "Daemon"
	VirtualArmor:         50
	FightMode:            "Aggressor"
	HitsMax:              1100
	Hue:                  1489
	LootItemChance:       80
	LootItemLevel:        5
	LootTable:            "35"
	ManaMaxSeed:          150
	ProvokeSkillOverride: 150
	StamMaxSeed:          175
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "MassCurse", "GreaterHeal", "Earthquake", "ManaVampire", "Paralyze", "Fireball"]
	Resistances: {
		Poison:        6
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      150
		Wrestling:    200
		MagicResist:  110
		Magery:       150
		DetectHidden: 150
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound: 362
	}
}
