package Daemon

Harpy: {
	Name:                 "a harpy"
	CorpseNameOverride:   "corpse of a harpy"
  Str:                  85
	Int:                  75
	Dex:                  70
	AlwaysMurderer:       true
	BaseSoundID:          402
	Body:                 30
	CreatureType:         "Daemon"
	VirtualArmor:         10
	FightMode:            "Closest"
	HitsMaxSeed:              85
	LootTable:            "29"
	ManaMaxSeed:          95
	ProvokeSkillOverride: 75
	StamMaxSeed:          50
	Skills: {
		Parry:       80
		Tactics:     70
		MagicResist: 35
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 3
			Max: 18
		}
		Skill:    "Swords"
		HitSound: 404
	}
}
