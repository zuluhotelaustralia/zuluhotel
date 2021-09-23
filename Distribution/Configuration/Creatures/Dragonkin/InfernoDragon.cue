package Dragonkin

InfernoDragon: {
	Name:                 "an Inferno Dragon"
	CorpseNameOverride:   "corpse of an Inferno Dragon"
  Str:                  500
	Int:                  110
	Dex:                  160
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 59
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Aggressor"
	HideType:             "Lava"
	Hides:                5
	HitsMax:              500
	Hue:                  1172
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          100
	MinTameSkill:         140
	ProvokeSkillOverride: 140
	StamMaxSeed:          150
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse", "GreaterHeal"]
	Resistances: {
		Fire:          200
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  100
		Tactics:      120
		Wrestling:    140
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
			SpellType: "RisingFire"
		}
		AbilityChance: 0.5
		HasBreath:     true
	}
}
