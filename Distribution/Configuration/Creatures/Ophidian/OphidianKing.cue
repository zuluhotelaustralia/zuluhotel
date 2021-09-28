package Ophidian

OphidianKing: {
	Name:                 "The Ophidian King"
	CorpseNameOverride:   "corpse of The Ophidian King"
  Str:                  2050
	Dex:                  800
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          634
	Body:                 86
	ClassLevel:           6
	ClassType:            "Warrior"
	CreatureType:         "Ophidian"
	VirtualArmor:         50
	FightMode:            "Closest"
	HitsMaxSeed:              2050
	Hue:                  1209
	LootItemChance:       75
	LootItemLevel:        7
	LootTable:            "9"
	ProvokeSkillOverride: 150
	StamMaxSeed:          800
	Resistances: MagicImmunity: 10
	Skills: {
		Parry:       160
		Magery:      160
		Wrestling:   160
		Tactics:     160
		MagicResist: 160
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound: 362
	}
}
