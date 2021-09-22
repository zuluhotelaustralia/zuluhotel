package Beholder

EyesOfEradication: {
	Name:                 "Eyes of Eradication"
	CorpseNameOverride:   "corpse of Eyes of Eradication"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  500
	Int:                  600
	Dex:                  500
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          377
	Body:                 22
	CreatureType:         "Beholder"
	VirtualArmor:         20
	FightMode:            "Closest"
	HitsMax:              500
	Hue:                  1172
	LootItemChance:       75
	LootItemLevel:        4
	LootTable:            "57"
	ManaMaxSeed:          500
	ProvokeSkillOverride: 160
	StamMaxSeed:          500
	PreferredSpells: ["Paralyze", "Fireball", "Lightning", "MassCurse"]
	Skills: {
		MagicResist: 200
		Tactics:     200
		Wrestling:   150
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
