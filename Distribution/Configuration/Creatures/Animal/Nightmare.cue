package Animal

Nightmare: {
	BaseType:             "BaseMount"
	Name:                 "a Nightmare"
	CorpseNameOverride:   "corpse of a Nightmare"
	Str:                  750
	Int:                  110
	Dex:                  160
	ActiveSpeed:          0.15
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body: 								284
	ItemID:								16018
	CreatureType:         "Animal"
	VirtualArmor:         55
	FightMode:            "Closest"
	Hides:                4
	HitsMaxSeed:          750
	LootItemChance:       60
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          100
	MinTameSkill:         130
	ProvokeSkillOverride: 130
	StamMaxSeed:          150
	Tamable:              true
	Skills: {
		Parry:        80
		MagicResist:  95
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
		HitSound:  364
		HasBreath: true
	}
}
