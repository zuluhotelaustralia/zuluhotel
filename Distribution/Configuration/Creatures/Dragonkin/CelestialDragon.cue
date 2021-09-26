package Dragonkin

CelestialDragon: {
	Name:                 "a Celestial Dragon"
	CorpseNameOverride:   "corpse of a Celestial Dragon"
  Str:                  850
	Int:                  400
	Dex:                  150
	PassiveSpeed:         0.2
	BaseSoundID:          362
	Body:                 103
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         50
	FightMode:            "Closest"
	HideType:             "Wyrm"
	Hides:                5
	HitsMax:              850
	Hue:                  1301
	InitialInnocent:      true
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "35"
	ManaMaxSeed:          200
	MinTameSkill:         145
	ProvokeSkillOverride: 140
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse"]
	Resistances: {
		Poison:        6
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Parry:        80
		MagicResist:  110
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
		HitSound:      364
		Ability:       "BlackrockStrike"
		AbilityChance: 1
		HasBreath:     true
	}
}
