package Misc

GreaterBeholder: {
	Name:               "Greater Beholder"
	CorpseNameOverride: "corpse of Greater Beholder"
	Str:                1500
	Int:                1600
	Dex:                500
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        377
	Body:               22
	ClassLevel:         6
	ClassType:          "Mage"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            1500
	Hue:                1173
	LootItemChance:     75
	LootItemLevel:      4
	LootTable:          "57"
	ManaMaxSeed:        1500
	StamMaxSeed:        500
	PreferredSpells: ["Paralyze", "Fireball", "Lightning", "Curse", "WyvernStrike", "Poison", "MindBlast"]
	Skills: {
		MagicResist: 200
		Tactics:     200
		Wrestling:   200
		Magery:      300
	}
	Attack: {
		Speed: 33
		Damage: {
			Min: 14
			Max: 34
		}
		HitSound: 379
	}
}
