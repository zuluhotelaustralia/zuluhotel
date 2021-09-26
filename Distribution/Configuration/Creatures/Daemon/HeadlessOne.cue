package Daemon

HeadlessOne: {
	Name:                 "a headless one"
	CorpseNameOverride:   "corpse of a headless one"
  Str:                  60
	Int:                  25
	Dex:                  60
	AlwaysMurderer:       true
	BaseSoundID:          925
	Body:                 31
	CreatureType:         "Daemon"
	VirtualArmor:         5
	FightMode:            "Closest"
	HitsMax:              60
	LootTable:            "3"
	ManaMaxSeed:          15
	ProvokeSkillOverride: 40
	StamMaxSeed:          50
	Skills: {
		Parry:       40
		Tactics:     50
		MagicResist: 30
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 3
			Max: 21
		}
		Skill:     "Swords"
		HitSound:  409
		MissSound: 411
	}
}
