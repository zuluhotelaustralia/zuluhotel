package Animal

GiantRockScorpion: {
	Name:                 "a giant rock scorpion"
	CorpseNameOverride:   "corpse of a giant rock scorpion"
  Str:                  230
	Int:                  40
	Dex:                  80
	AlwaysMurderer:       true
	BaseSoundID:          397
	Body:                 48
	CreatureType:         "Animal"
	VirtualArmor:         35
	HitsMaxSeed:              230
	Hue:                  1118
	ManaMaxSeed:          0
	MinTameSkill:         80
	ProvokeSkillOverride: 90
	StamMaxSeed:          70
	Tamable:              true
	Resistances: Poison: 3
	Skills: {
		Tactics:     70
		Wrestling:   130
		MagicResist: 50
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 11
			Max: 35
		}
		HitSound: 399
		HitPoison: "Greater"
	}
}
