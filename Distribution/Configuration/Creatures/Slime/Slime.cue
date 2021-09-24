package Slime

Slime: {
	Name:                 "a slime"
	CorpseNameOverride:   "corpse of a slime"
  Str:                  40
	Int:                  15
	Dex:                  50
	AlwaysMurderer:       true
	BaseSoundID:          456
	Body:                 51
	CreatureType:         "Slime"
	VirtualArmor:         5
	HitsMax:              40
	LootTable:            "62"
	ManaMaxSeed:          0
	MinTameSkill:         20
	ProvokeSkillOverride: 40
	StamMaxSeed:          50
	Tamable:              true
	Resistances: Poison: 6
	Skills: {
		Parry:       18
		MagicResist: 20
		Poisoning:   40
		Tactics:     50
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 2
			Max: 10
		}
		Skill:    "Swords"
		HitSound: 458
	}
}
