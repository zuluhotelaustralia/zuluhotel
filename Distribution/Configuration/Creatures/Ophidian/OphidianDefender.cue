package Ophidian

OphidianDefender: {
	Name:                 "an Ophidian Defender"
	CorpseNameOverride:   "corpse of an Ophidian Defender"
  Str:                  2050
	Int:                  35
	Dex:                  210
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          634
	Body:                 86
	ClassLevel:           3
	ClassType:            "Warrior"
	CreatureType:         "Ophidian"
	VirtualArmor:         25
	FightMode:            "Closest"
	HideType:             "Serpent"
	Hides:                5
	HitsMax:              250
	LootTable:            "70"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 110
	StamMaxSeed:          70
	Resistances: {
		Fire:  75
		Earth: 100
	}
	Skills: {
		Parry:       200
		Magery:      80
		Wrestling:   100
		Tactics:     90
		MagicResist: 130
	}
	Attack: {
		Speed: 47
		Damage: {
			Min: 8
			Max: 44
		}
		MissSound: 360
	}
}
