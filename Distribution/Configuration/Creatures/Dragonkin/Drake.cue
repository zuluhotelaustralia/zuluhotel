package Dragonkin

Drake: {
	Name:                 "a drake"
	CorpseNameOverride:   "corpse of a drake"
	Str:                  500
	Int:                  90
	Dex:                  110
	ActiveSpeed:          0.15
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 61
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                2
	HitsMaxSeed:          500
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          80
	MinTameSkill:         110
	ProvokeSkillOverride: 120
	StamMaxSeed:          130
	Tamable:              true
	Resistances: MagicImmunity: 3
	Skills: {
		Parry:       70
		MagicResist: 90
		Tactics:     100
		Wrestling:   120
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
