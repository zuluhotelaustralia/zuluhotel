package Dragonkin

PoisonDrake: {
	Name:                 "a Poison Drake"
	CorpseNameOverride:   "corpse of a Poison Drake"
  Str:                  350
	Int:                  90
	Dex:                  300
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              350
	Hue:                  264
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
		MagicImmunity: 4
	}
	Skills: {
		Parry:        70
		MagicResist:  70
		Tactics:      100
		Wrestling:    120
		DetectHidden: 130
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
		HitPoison: "Greater"
	}
}
