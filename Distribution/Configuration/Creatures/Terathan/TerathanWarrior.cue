package Terathan

TerathanWarrior: {
	Name:                 "a Terathan Warrior"
	CorpseNameOverride:   "corpse of a Terathan Warrior"
	Str:                  550
	Int:                  35
	Dex:                  105
	AlwaysMurderer:       true
	BaseSoundID:          589
	Body:                 70
	CreatureType:         "Terathan"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMaxSeed:          550
	LootTable:            "63"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          70
	Resistances: MagicImmunity: 5
	Skills: {
		Tactics:     90
		MagicResist: 90
		Parry:       75
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
