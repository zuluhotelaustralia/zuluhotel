package Ophidian

OphidianWarrior: {
	Name:                 "an Ophidian Warrior"
	CorpseNameOverride:   "corpse of an Ophidian Warrior"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  300
	Int:                  160
	Dex:                  160
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          634
	Body:                 86
	CreatureType:         "Ophidian"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HideType:             "Serpent"
	Hides:                5
	HitsMax:              600
	LootTable:            "67"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 110
	StamMaxSeed:          320
	Skills: {
		Parry:       100
		Magery:      100
		Wrestling:   100
		Tactics:     100
		MagicResist: 100
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 11
			Max: 41
		}
		MissSound: 360
	}
}
