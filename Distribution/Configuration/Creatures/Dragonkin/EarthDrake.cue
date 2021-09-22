package Dragonkin

EarthDrake: {
	Name:                 "an Earth Drake"
	CorpseNameOverride:   "corpse of an Earth Drake"

	Str:                  500
	Int:                  90
	Dex:                  60
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Aggressor"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              500
	Hue:                  1134
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         115
	ProvokeSkillOverride: 120
	StamMaxSeed:          100
	Tamable:              true
	Resistances: {
		Earth:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        70
		MagicResist:  80
		Tactics:      110
		Wrestling:    130
		DetectHidden: 130
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound:  364
		HasBreath: true
	}
}
