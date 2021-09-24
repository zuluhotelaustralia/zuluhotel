package Dragonkin

AirDrake: {
	Name:                 "an Air Drake"
	CorpseNameOverride:   "corpse of an Air Drake"
  Str:                  400
	Int:                  90
	Dex:                  400
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 61
	CreatureType:         "Dragonkin"
	VirtualArmor:         20
	FightMode:            "Aggressor"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              400
	Hue:                  1170
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         115
	ProvokeSkillOverride: 130
	StamMaxSeed:          130
	Tamable:              true
	Resistances: {
		Air:           100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        70
		MagicResist:  95
		Tactics:      110
		Wrestling:    110
		DetectHidden: 130
	}
	Attack: {
		Damage: {
			Min: 33
			Max: 73
		}
		Ability: {
			SpellType: "CallLightning"
		}
		AbilityChance: 0.5
		HasBreath:     true
	}
}
