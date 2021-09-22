package Orc

OrcishKing: {
	Name:                 "<random> the Orcish King"
	CorpseNameOverride:   "corpse of <random> the Orcish King"

	Str:                  300
	Int:                  60
	Dex:                  300
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 7
	CreatureType:         "Orc"
	VirtualArmor:         35
	FightMode:            "Aggressor"
	HitsMax:              300
	Hue:                  1125
	LootItemChance:       25
	LootItemLevel:        3
	LootTable:            "13"
	ManaMaxSeed:          50
	ProvokeSkillOverride: 120
	StamMaxSeed:          150
	Skills: {
		Tactics:     100
		Wrestling:   150
		MagicResist: 85
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 432
	}
}
