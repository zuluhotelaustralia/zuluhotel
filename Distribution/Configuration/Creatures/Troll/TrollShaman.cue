package Troll

TrollShaman: {
	Name:                 "a Troll Shaman"
	CorpseNameOverride:   "corpse of a Troll Shaman"
  Str:                  225
	Int:                  285
	Dex:                  170
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 54
	CreatureType:         "Troll"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Troll"
	Hides:                4
	HitsMaxSeed:              225
	Hue:                  544
	LootItemChance:       60
	LootItemLevel:        2
	LootTable:            "54"
	ManaMaxSeed:          85
	ProvokeSkillOverride: 100
	StamMaxSeed:          50
	PreferredSpells: [
		"Explosion",
	]
	Skills: {
		MagicResist: 60
		Tactics:     60
		Wrestling:   90
		Magery:      90
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 434
	}
}
