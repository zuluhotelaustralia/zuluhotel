package Dragonkin

GreatWyrm: {
	Name:                 "a Great Wyrm"
	CorpseNameOverride:   "corpse of a Great Wyrm"
	Str:                  900
	Int:                  650
	Dex:                  475
	ActiveSpeed:          0.05
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          362
	Body:                 46
	CreatureType:         "Dragonkin"
	VirtualArmor:         50
	FightMode:            "Closest"
	HideType:             "Wyrm"
	Hides:                5
	HitsMaxSeed:          900
	Hue:                  1159
	LootItemChance:       80
	LootItemLevel:        5
	LootTable:            "35"
	ManaMaxSeed:          150
	MinTameSkill:         150
	ProvokeSkillOverride: 150
	StamMaxSeed:          175
	Tamable:              true
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "MassCurse", "GreaterHeal", "Earthquake", "ManaVampire", "Paralyze", "Fireball"]
	Resistances: {
		Poison:        6
		Fire:          100
		MagicImmunity: 5
	}
	Skills: {
		Tactics:      150
		Wrestling:    200
		MagicResist:  110
		Magery:       150
		DetectHidden: 150
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound:  362
		HasBreath: true
	}
}
