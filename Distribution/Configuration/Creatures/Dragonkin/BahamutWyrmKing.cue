package Dragonkin

BahamutWyrmKing: {
	Name:               "Bahamut, the Wyrm King"
	CorpseNameOverride: "corpse of Bahamut, the Wyrm King"
	Str:                1900
	Int:                650
	Dex:                2475
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        362
	Body:               59
	ClassLevel:         6
	ClassType:          "Mage"
	CreatureType:       "Dragonkin"
	VirtualArmor:       50
	FightMode:          "Closest"
	FightRange:         7
	HitsMaxSeed:        1900
	Hue:                1297
	LootItemChance:     80
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        150
	MinTameSkill:       150
	StamMaxSeed:        2475
	Tamable:            true
	PreferredSpells: ["Paralyze", "GustOfAir", "IceStrike"]
	Resistances: {
		Poison:        6
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      150
		Wrestling:    150
		MagicResist:  110
		Magery:       300
		DetectHidden: 150
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 25
			Max: 75
		}
		Skill:    "Swords"
		HitSound: 362
		MaxRange: 7
	}
}
