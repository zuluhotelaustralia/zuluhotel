package Gargoyle

FrostGargoyle: {
	Name:                 "a frost gargoyle"
	CorpseNameOverride:   "corpse of a frost gargoyle"
  Str:                  250
	Int:                  285
	Dex:                  95
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CreatureType:         "Gargoyle"
	VirtualArmor:         35
	FightMode:            "Closest"
	HitsMaxSeed:              250
	Hue:                  1154
	LootItemChance:       25
	LootItemLevel:        2
	LootTable:            "38"
	ManaMaxSeed:          85
	ProvokeSkillOverride: 105
	StamMaxSeed:          75
	PreferredSpells: ["Paralyze", "Lightning", "Curse", "Weaken", "EnergyBolt"]
	Resistances: {
		Water:         75
		MagicImmunity: 3
	}
	Skills: {
		MagicResist: 80
		Tactics:     120
		Wrestling:   135
		Magery:      100
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 374
	}
}
