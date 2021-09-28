package Ophidian

OphidianQueen: {
	Name:               "an Ophidian Queen"
	CorpseNameOverride: "corpse of an Ophidian Queen"
	Str:                350
	Int:                700
	Dex:                70
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        644
	Body:               87
	ClassLevel:         5
	ClassType:          "Mage"
	CreatureType:       "Ophidian"
	VirtualArmor:       30
	FightMode:          "Closest"
	HideType:           "Serpent"
	Hides:              5
	HitsMaxSeed:            1950
	Hue:                1165
	LootItemChance:     60
	LootItemLevel:      6
	LootTable:          "37"
	ManaMaxSeed:        1000
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Explosion", "Paralyze", "MassCurse"]
	Resistances: MagicImmunity: 8
	Skills: {
		Parry:       160
		Magery:      160
		MagicResist: 95
		Tactics:     50
		Wrestling:   100
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 9
			Max: 45
		}
		MissSound: 360
	}
}
