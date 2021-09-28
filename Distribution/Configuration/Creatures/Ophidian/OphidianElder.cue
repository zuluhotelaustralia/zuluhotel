package Ophidian

OphidianElder: {
	Name:               "an Ophidian Elder"
	CorpseNameOverride: "corpse of an Ophidian Elder"
	Str:                160
	Int:                350
	Dex:                70
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        639
	Body:               85
	ClassLevel:         3
	ClassType:          "Mage"
	CreatureType:       "Ophidian"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:        960
	Hue:                1250
	LootItemChance:     50
	LootItemLevel:      3
	LootTable:          "66"
	ManaMaxSeed:        800
	StamMaxSeed:        500
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Explosion", "Fireball", "MassCurse", "Paralyze", "Weaken"]
	Resistances: MagicImmunity: 4
	Skills: {
		Parry:       130
		Magery:      150
		Tactics:     50
		Wrestling:   100
		MagicResist: 150
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 3
			Max: 30
		}
		MissSound: 360
	}
}
