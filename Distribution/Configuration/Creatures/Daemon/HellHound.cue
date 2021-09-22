package Daemon

HellHound: {
	Name:                 "a hell hound"
	CorpseNameOverride:   "corpse of a hell hound"

	Str:                  170
	Int:                  60
	Dex:                  185
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          229
	Body:                 225
	CreatureType:         "Daemon"
	VirtualArmor:         25
	FightMode:            "Closest"
	HideType:             "Wolf"
	Hides:                5
	HitsMax:              170
	Hue:                  232
	LootTable:            "61"
	ManaMaxSeed:          50
	MinTameSkill:         95
	ProvokeSkillOverride: 80
	StamMaxSeed:          70
	Tamable:              true
	PreferredSpells: [
		"Fireball",
	]
	Skills: {
		Tactics:     100
		Wrestling:   110
		Magery:      90
		MagicResist: 70
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 231
	}
}
