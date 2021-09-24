package Dragonkin

RockDragon: {
	Name:                 "a Rock Dragon"
	CorpseNameOverride:   "corpse of a Rock Dragon"
  Str:                  800
	Int:                  400
	Dex:                  60
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         50
	FightMode:            "Aggressor"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              800
	Hue:                  1160
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
		Earth:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  80
		Tactics:      130
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
			SpellType: "ShiftingEarth"
		}
		AbilityChance: 0.5
		HasBreath:     true
	}
}
