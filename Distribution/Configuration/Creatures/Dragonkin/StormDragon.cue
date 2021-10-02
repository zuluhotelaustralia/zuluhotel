package Dragonkin

StormDragon: {
	Name:                 "a Storm Dragon"
	CorpseNameOverride:   "corpse of a Storm Dragon"
	Str:                  600
	Int:                  400
	Dex:                  340
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                5
	HitsMaxSeed:          600
	Hue:                  1170
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          200
	MinTameSkill:         140
	ProvokeSkillOverride: 140
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse"]
	Resistances: {
		Air:           100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  105
		Tactics:      110
		Wrestling:    150
		DetectHidden: 130
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound: 364
		Ability: {
			SpellType: "GustOfAir"
		}
		AbilityChance: 0.65
		HasBreath:     true
	}
}
