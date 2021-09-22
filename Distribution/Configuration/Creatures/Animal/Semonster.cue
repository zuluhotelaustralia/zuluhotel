package Animal

Semonster: {
	Name:                 "a sea monster"
	CorpseNameOverride:   "corpse of a sea monster"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  250
	Int:                  35
	Dex:                  160
	BaseSoundID:          444
	Body:                 95
	CanSwim:              true
	CreatureType:         "Animal"
	VirtualArmor:         30
	HitsMax:              250
	Hue:                  33784
	LootItemChance:       1
	LootTable:            "50"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 60
	StamMaxSeed:          50
	Skills: {
		Parry:       70
		Tactics:     100
		Wrestling:   120
		MagicResist: 40
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 9
			Max: 45
		}
		HitSound:  446
		MissSound: 447
	}
}
