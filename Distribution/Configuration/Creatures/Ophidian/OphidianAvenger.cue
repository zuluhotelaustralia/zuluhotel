package Ophidian

OphidianAvenger: {
	Name:                 "an Ophidian Avenger"
	CorpseNameOverride:   "corpse of an Ophidian Avenger"
  Str:                  350
	Int:                  35
	Dex:                  210
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          634
	Body:                 86
	CreatureType:         "Ophidian"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Serpent"
	Hides:                5
	HitsMaxSeed:              350
	LootItemChance:       20
	LootItemLevel:        2
	LootTable:            "71"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 110
	StamMaxSeed:          70
	Resistances: {
		Fire:  75
		Earth: 100
	}
	Skills: {
		Parry:       120
		Magery:      80
		Wrestling:   130
		Tactics:     130
		MagicResist: 130
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 8
			Max: 43
		}
		MissSound: 360
		HitPoison: "Regular"
	}
}
