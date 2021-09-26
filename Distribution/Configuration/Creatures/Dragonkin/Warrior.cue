package Dragonkin

Warrior: {
	Name:                 "<random> the warrior"
	CorpseNameOverride:   "corpse of <random> the warrior"
  Str:                  205
	Int:                  60
	Dex:                  103
	AlwaysMurderer:       true
	BaseSoundID:          417
	Body:                 36
	CreatureType:         "Dragonkin"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Lizard"
	Hides:                5
	HitsMax:              205
	LootTable:            "11"
	ManaMaxSeed:          40
	ProvokeSkillOverride: 70
	StamMaxSeed:          75
	Skills: {
		MagicResist: 60
		Tactics:     80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 5
			Max: 30
		}
		HitSound: 315
	}
}
