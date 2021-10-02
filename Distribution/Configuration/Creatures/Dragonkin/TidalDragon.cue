package Dragonkin

TidalDragon: {
	Name:                 "a Tidal Dragon"
	CorpseNameOverride:   "corpse of a Tidal Dragon"
	Str:                  500
	Int:                  400
	Dex:                  340
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "IceCrystal"
	Hides:                5
	HitsMaxSeed:          500
	Hue:                  1165
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
		MagicResist:  130
		Tactics:      100
		Wrestling:    120
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
			SpellType: "SorcerersBane"
		}
		AbilityChance: 0.8
	}
}
