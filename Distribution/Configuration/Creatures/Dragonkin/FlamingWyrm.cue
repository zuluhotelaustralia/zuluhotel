package Dragonkin

FlamingWyrm: {
	Name:                 "a Flaming Wyrm"
	CorpseNameOverride:   "corpse of a Flaming Wyrm"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  2800
	Int:                  3500
	Dex:                  475
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 46
	CreatureType:         "Dragonkin"
	VirtualArmor:         100
	FightMode:            "Aggressor"
	HitsMax:              2000
	Hue:                  1305
	LootItemChance:       65
	LootItemLevel:        6
	LootTable:            "9"
	ManaMaxSeed:          1500
	MinTameSkill:         160
	ProvokeSkillOverride: 150
	StamMaxSeed:          175
	Tamable:              true
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "MassCurse", "GreaterHeal", "Earthquake", "ManaVampire", "Paralyze", "Fireball"]
	Resistances: {
		Poison:        6
		Air:           50
		Fire:          100
		Earth:         50
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      180
		Wrestling:    150
		MagicResist:  200
		Magery:       150
		DetectHidden: 180
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 20
			Max: 110
		}
		HitSound: 364
		Ability: {
			SpellType: "FlameStrike"
		}
		AbilityChance: 0.65
		HasBreath:     true
	}
}
