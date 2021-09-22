package Animal

BirdDog: {
	Name:                 "a bird dog"
	CorpseNameOverride:   "corpse of a bird dog"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  30
	Int:                  25
	Dex:                  70
	AiType:               "AI_Animal"
	BaseSoundID:          133
	Body:                 217
	CreatureType:         "Animal"
	VirtualArmor:         5
	HitsMax:              30
	Hue:                  33784
	ManaMaxSeed:          0
	MinTameSkill:         10
	ProvokeSkillOverride: 20
	StamMaxSeed:          50
	Tamable:              true
	Skills: {
		Tactics:     50
		Wrestling:   50
		MagicResist: 40
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 1
			Max: 6
		}
		HitSound: 135
	}
}
