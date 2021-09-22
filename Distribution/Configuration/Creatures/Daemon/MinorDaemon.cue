package Daemon

MinorDaemon: {
	Name:                 "a minor daemon"
	CorpseNameOverride:   "corpse of a minor daemon"

	Str:                  275
	Int:                  85
	Dex:                  180
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CreatureType:         "Daemon"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HitsMax:              275
	Hue:                  35
	LootItemChance:       10
	LootItemLevel:        3
	LootTable:            "23"
	ManaMaxSeed:          75
	ProvokeSkillOverride: 115
	StamMaxSeed:          50
	Skills: {
		MagicResist: 90
		Tactics:     100
		Wrestling:   135
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 374
	}
}
