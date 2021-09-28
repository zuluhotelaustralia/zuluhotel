package Terathan

TerathanChampion: {
	Name:                 "a Terathan Champion"
	CorpseNameOverride:   "corpse of a Terathan Champion"
	Str:                  650
	Int:                  35
	Dex:                  105
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          589
	Body:                 70
	ClassLevel:           4
	ClassType:            "Warrior"
	CreatureType:         "Terathan"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMaxSeed:          650
	Hue:                  1127
	LootTable:            "63"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          70
	Resistances: {
		Physical:      100
		Fire:          100
		Earth:         100
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     130
		MagicResist: 130
		Parry:       135
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 8
			Max: 43
		}
		Skill:     "Swords"
		HitSound:  588
		MissSound: 589
	}
}
