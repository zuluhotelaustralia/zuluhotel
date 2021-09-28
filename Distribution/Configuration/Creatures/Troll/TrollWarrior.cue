package Troll

TrollWarrior: {
	Name:                 "a troll warrior"
	CorpseNameOverride:   "corpse of a troll warrior"
	Str:                  225
	Int:                  40
	Dex:                  80
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 54
	CreatureType:         "Troll"
	VirtualArmor:         10
	FightMode:            "Closest"
	HideType:             "Troll"
	Hides:                4
	HitsMaxSeed:          225
	Hue:                  33784
	LootTable:            "14"
	ManaMaxSeed:          30
	ProvokeSkillOverride: 80
	StamMaxSeed:          60
	Skills: {
		MagicResist: 80
		Tactics:     115
		Wrestling:   115
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 8
			Max: 20
		}
		HitSound:  324
		MissSound: 570
	}
}
