package Beholder

Beholder: {
	Name:                 "a beholder"
	CorpseNameOverride:   "corpse of a beholder"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  160
	Int:                  600
	Dex:                  10
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          377
	Body:                 22
	CreatureType:         "Beholder"
	VirtualArmor:         20
	FightMode:            "Closest"
	HitsMax:              160
	Hue:                  33877
	LootItemChance:       75
	LootItemLevel:        2
	LootTable:            "57"
	ManaMaxSeed:          220
	ProvokeSkillOverride: 75
	StamMaxSeed:          65
	PreferredSpells: ["Paralyze", "Fireball", "Lightning", "MassCurse"]
	Skills: {
		MagicResist: 100
		Tactics:     100
		Wrestling:   100
		Magery:      125
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
