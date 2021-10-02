package Daemon

Mongbat: {
	Name:                 "a mongbat"
	CorpseNameOverride:   "corpse of a mongbat"
	Str:                  20
	Int:                  35
	Dex:                  60
	AlwaysMurderer:       true
	BaseSoundID:          422
	Body:                 39
	CreatureType:         "Daemon"
	VirtualArmor:         5
	FightMode:            "Closest"
	HitsMaxSeed:          20
	LootTable:            "33"
	ManaMaxSeed:          0
	MinTameSkill:         25
	ProvokeSkillOverride: 55
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		Parry:       55
		Tactics:     50
		MagicResist: 10
	}
	Attack: {
		Damage: {
			Min: 2
			Max: 6
		}
		Skill:    "Swords"
		HitSound: 424
	}
}
