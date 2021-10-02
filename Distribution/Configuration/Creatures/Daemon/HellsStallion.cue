package Daemon

HellsStallion: {
	Name:                 "a Hells Stallion"
	CorpseNameOverride:   "corpse of a Hells Stallion"
	Str:                  1950
	Int:                  450
	Dex:                  650
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          451
	Body:                 793
	CreatureType:         "Daemon"
	VirtualArmor:         60
	FightMode:            "Closest"
	HitsMaxSeed:          1050
	Hue:                  1305
	LootItemChance:       90
	LootItemLevel:        5
	LootTable:            "9"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 160
	StamMaxSeed:          150
	Resistances: {
		Fire:          100
		Earth:         75
		MagicImmunity: 6
	}
	Skills: {
		MagicResist: 100
		EvalInt:     100
		Tactics:     130
		Wrestling:   150
		Magery:      150
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 15
			Max: 150
		}
		HitSound:  170
		HasBreath: true
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
