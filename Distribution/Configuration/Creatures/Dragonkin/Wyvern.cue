package Dragonkin

Wyvern: {
	Name:                 "a Wyvern"
	CorpseNameOverride:   "corpse of a Wyvern"
	Str:                  450
	Int:                  90
	Dex:                  110
	ActiveSpeed:          0.1
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Wyrm"
	Hides:                5
	HitsMaxSeed:          450
	Hue:                  1304
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         120
	ProvokeSkillOverride: 120
	StamMaxSeed:          100
	Tamable:              true
	Resistances: {
		Poison:        6
		MagicImmunity: 3
	}
	Skills: {
		Parry:        70
		MagicResist:  90
		Tactics:      100
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
		HitPoison: "Greater"
	}
}
