package Terathan

TerathanDrone: {
	Name:                 "a Terathan Drone"
	CorpseNameOverride:   "corpse of a Terathan Drone"
  Str:                  170
	Int:                  55
	Dex:                  135
	AlwaysMurderer:       true
	BaseSoundID:          594
	Body:                 71
	CreatureType:         "Terathan"
	VirtualArmor:         10
	FightMode:            "Closest"
	HitsMaxSeed:              70
	LootTable:            "64"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          125
	Skills: {
		Tactics:     50
		MagicResist: 30
		Parry:       40
	}
	Attack: {
		Speed: 43
		Damage: {
			Min: 8
			Max: 44
		}
		Skill:     "Swords"
		HitSound:  593
		MissSound: 594
	}
}
