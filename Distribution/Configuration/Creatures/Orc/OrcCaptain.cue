package Orc

OrcCaptain: {
	Name:                 "<random> the Orc Captain"
	CorpseNameOverride:   "corpse of <random> the Orc Captain"

	Str:                  285
	Int:                  40
	Dex:                  190
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 7
	CreatureType:         "Orc"
	VirtualArmor:         30
	HitsMax:              285
	Hue:                  33784
	LootTable:            "42"
	ManaMaxSeed:          30
	ProvokeSkillOverride: 90
	StamMaxSeed:          90
	Skills: {
		Tactics:     100
		MagicResist: 60
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 572
	}
}
