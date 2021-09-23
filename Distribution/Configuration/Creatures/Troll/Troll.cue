package Troll

Troll: {
	Name:                 "a troll"
	CorpseNameOverride:   "corpse of a troll"
  Str:                  185
	Int:                  35
	Dex:                  70
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 55
	CreatureType:         "Troll"
	VirtualArmor:         20
	FightMode:            "Aggressor"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              185
	Hue:                  33784
	LootTable:            "14"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 90
	StamMaxSeed:          50
	Skills: {
		Parry:       60
		Wrestling:   90
		Tactics:     90
		MagicResist: 60
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 10
			Max: 35
		}
		Skill:    "Swords"
		HitSound: 463
	}
}
