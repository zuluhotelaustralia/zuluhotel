package Orc

OrcDragonDefender: {
	Name:                 "<random> the Orc Dragon Defender"
	CorpseNameOverride:   "corpse of <random> the Orc Dragon Defender"
	Str:                  305
	Int:                  85
	Dex:                  230
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 7
	CreatureType:         "Orc"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMaxSeed:          305
	Hue:                  1109
	LootItemChance:       1
	LootTable:            "13"
	ManaMaxSeed:          75
	ProvokeSkillOverride: 114
	StamMaxSeed:          50
	Skills: {
		MagicResist: 70
		Tactics:     100
		Wrestling:   130
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound:  434
		MissSound: 432
	}
}
