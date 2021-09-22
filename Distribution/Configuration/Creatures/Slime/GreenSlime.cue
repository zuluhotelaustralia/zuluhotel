package Slime

GreenSlime: {
	Name:                 "a green slime"
	CorpseNameOverride:   "corpse of a green slime"

	Str:                  140
	Int:                  15
	Dex:                  50
	BaseSoundID:          456
	Body:                 51
	CreatureType:         "Slime"
	VirtualArmor:         20
	HitsMax:              140
	Hue:                  2001
	ManaMaxSeed:          0
	MinTameSkill:         40
	ProvokeSkillOverride: 19
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		MagicResist: 80
		Tactics:     100
		Wrestling:   100
	}
	Attack: {
		Speed: 25
		Damage: {
			Min: 2
			Max: 30
		}
		HitSound: 458
	}
}
