package Ophidian

OphidianKnightErrant: {
	Name:                 "an Ophidian Knight-Errant"
	CorpseNameOverride:   "corpse of an Ophidian Knight-Errant"
  Str:                  750
	Int:                  160
	Dex:                  310
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          634
	Body:                 86
	CreatureType:         "Ophidian"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HideType:             "Serpent"
	Hides:                5
	HitsMax:              1750
	LootTable:            "71"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 120
	StamMaxSeed:          1070
	Skills: {
		Parry:       130
		Magery:      100
		Wrestling:   100
		Tactics:     110
		MagicResist: 110
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
