package Animal

Bat: {
	Name:                 "a bat"
	CorpseNameOverride:   "corpse of a bat"
  Str:                  30
	Int:                  60
	Dex:                  80
	AiType:               "AI_Mage"
	BaseSoundID:          27
	Body:                 6
	CreatureType:         "Animal"
	VirtualArmor:         10
	HitsMaxSeed:              30
	Hue:                  5555
	ManaMaxSeed:          50
	MinTameSkill:         35
	ProvokeSkillOverride: 10
	StamMaxSeed:          70
	Tamable:              true
	PreferredSpells: [
		"Poison",
		"ManaDrain",
	]
	Skills: {
		MagicResist: 20
		Tactics:     70
		Wrestling:   70
		Magery:      50
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 3
			Max: 9
		}
		HitSound: 570
		HitPoison: "Lesser"
	}
}
