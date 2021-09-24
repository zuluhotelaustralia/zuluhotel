package Dragonkin

FireDrake: {
	Name:                 "a Fire Drake"
	CorpseNameOverride:   "corpse of a Fire Drake"
  Str:                  350
	Int:                  90
	Dex:                  110
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 61
	CreatureType:         "Dragonkin"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HideType:             "Lava"
	Hides:                5
	HitsMax:              350
	Hue:                  1158
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         115
	ProvokeSkillOverride: 130
	StamMaxSeed:          130
	Tamable:              true
	Resistances: {
		Fire:          100
		MagicImmunity: 3
	}
	Skills: {
		Parry:        70
		MagicResist:  110
		Tactics:      100
		Wrestling:    130
		DetectHidden: 130
	}
	Attack: {
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
		Ability: {
			SpellType: "Explosion"
		}
		AbilityChance: 0.5
		HasBreath:     true
	}
}
