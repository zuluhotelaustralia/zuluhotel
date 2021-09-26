package Dragonkin

GoldenDragon: {
	Name:                 "a Golden Dragon"
	CorpseNameOverride:   "corpse of a Golden Dragon"
  Str:                  1250
	Int:                  500
	Dex:                  450
	ActiveSpeed:          0.05
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          362
	Body:                 12
	CanFly:               true
	CreatureType:         "Dragonkin"
	VirtualArmor:         75
	FightMode:            "Closest"
	HideType:             "GoldenDragon"
	Hides:                5
	HitsMax:              1250
	Hue:                  48
	LootItemChance:       65
	LootItemLevel:        6
	LootTable:            "9"
	ManaMaxSeed:          500
	MinTameSkill:         170
	ProvokeSkillOverride: 160
	StamMaxSeed:          1000
	Tamable:              true
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		Water:         50
		Earth:         100
		MagicImmunity: 7
	}
	Skills: {
		Tactics:      150
		Wrestling:    190
		Magery:       150
		MagicResist:  100
		Parry:        100
		DetectHidden: 200
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 15
			Max: 150
		}
		HitSound:  362
		HasBreath: true
	}
}
