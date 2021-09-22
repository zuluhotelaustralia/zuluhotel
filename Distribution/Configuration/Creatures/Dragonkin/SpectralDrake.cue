package Dragonkin

SpectralDrake: {
	Name:                 "a spectral drake"
	CorpseNameOverride:   "corpse of a spectral drake"

	Str:                  550
	Int:                  360
	Dex:                  105
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 60
	CreatureType:         "Dragonkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              550
	Hue:                  17969
	LootItemChance:       70
	LootItemLevel:        4
	LootTable:            "36"
	ManaMaxSeed:          150
	MinTameSkill:         120
	ProvokeSkillOverride: 130
	StamMaxSeed:          50
	Tamable:              true
	PreferredSpells: [
		"EnergyBolt",
		"Lightning",
	]
	Resistances: MagicImmunity: 4
	Skills: {
		Tactics:     120
		Wrestling:   140
		Magery:      125
		MagicResist: 90
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
	}
}
