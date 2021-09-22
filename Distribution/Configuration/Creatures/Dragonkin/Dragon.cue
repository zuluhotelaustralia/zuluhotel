package Dragonkin

Dragon: {
	Name:                 "a dragon"
	CorpseNameOverride:   "corpse of a dragon"

	Str:                  750
	Int:                  110
	Dex:                  160
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BaseSoundID:          362
	Body:                 59
	CreatureType:         "Dragonkin"
	VirtualArmor:         55
	FightMode:            "Aggressor"
	HideType:             "Dragon"
	Hides:                5
	HitsMax:              750
	LootItemChance:       60
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          100
	MinTameSkill:         130
	ProvokeSkillOverride: 130
	StamMaxSeed:          150
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse", "GreaterHeal"]
	Resistances: {
		Fire:          100
		MagicImmunity: 3
	}
	Skills: {
		Parry:        80
		MagicResist:  95
		Tactics:      130
		Wrestling:    140
		DetectHidden: 130
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound:  364
		HasBreath: true
	}
}
