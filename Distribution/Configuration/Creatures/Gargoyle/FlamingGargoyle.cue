package Gargoyle

FlamingGargoyle: {
	Name:                 "a flaming gargoyle"
	CorpseNameOverride:   "corpse of a flaming gargoyle"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  250
	Int:                  335
	Dex:                  95
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CreatureType:         "Gargoyle"
	VirtualArmor:         35
	FightMode:            "Closest"
	HitsMax:              250
	Hue:                  232
	LootItemChance:       60
	LootItemLevel:        2
	LootTable:            "38"
	ManaMaxSeed:          100
	ProvokeSkillOverride: 105
	StamMaxSeed:          75
	PreferredSpells: [
		"Fireball",
		"MagicArrow",
	]
	Resistances: {
		Fire:          100
		MagicImmunity: 3
	}
	Skills: {
		MagicResist: 80
		Tactics:     90
		Wrestling:   150
		Magery:      120
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
