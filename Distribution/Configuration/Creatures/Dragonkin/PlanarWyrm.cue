package Dragonkin

PlanarWyrm: {
	Name:               "a Planar Wyrm"
	CorpseNameOverride: "corpse of a Planar Wyrm"
	Str:                1800
	Int:                650
	Dex:                1475
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        362
	Body:               59
	CreatureType:       "Dragonkin"
	VirtualArmor:       50
	FightMode:          "Closest"
	FightRange:         7
	HitsMax:            3000
	Hue:                1301
	LootItemChance:     80
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        150
	MinTameSkill:       150
	StamMaxSeed:        175
	Tamable:            true
	Resistances: {
		Poison:        6
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      200
		Wrestling:    150
		MagicResist:  200
		Magery:       150
		DetectHidden: 200
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
