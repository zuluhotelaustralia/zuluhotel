package Dragonkin

FrostDragon: {
	Name:                 "a Frost Dragon"
	CorpseNameOverride:   "corpse of a Frost Dragon"
	Str:                  800
	Int:                  400
	Dex:                  60
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "IceCrystal"
	Hides:                5
	HitsMaxSeed:          800
	Hue:                  1176
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
		Water:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  75
		Tactics:      120
		Wrestling:    130
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
			SpellType: "IceStrike"
		}
		AbilityChance: 0.8
	}
}
