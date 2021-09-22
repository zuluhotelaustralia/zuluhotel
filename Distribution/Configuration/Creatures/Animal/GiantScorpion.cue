package Animal

GiantScorpion: {
	Name:                 "a giant scorpion"
	CorpseNameOverride:   "corpse of a giant scorpion"

	Str:                  100
	Int:                  35
	Dex:                  90
	AlwaysMurderer:       true
	BaseSoundID:          397
	Body:                 48
	CreatureType:         "Animal"
	VirtualArmor:         15
	FightMode:            "Aggressor"
	HitPoison:            "Regular"
	HitsMax:              100
	ManaMaxSeed:          25
	MinTameSkill:         70
	ProvokeSkillOverride: 90
	StamMaxSeed:          80
	Tamable:              true
	Skills: {
		Parry:       65
		Poisoning:   90
		MagicResist: 40
		Tactics:     70
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 24
		}
		Skill:    "Swords"
		HitSound: 399
	}
}
