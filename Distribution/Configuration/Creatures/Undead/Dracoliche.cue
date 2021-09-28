package Undead

Dracoliche: {
	Name:                 "a Dracoliche"
	CorpseNameOverride:   "corpse of a Dracoliche"
  Str:                  650
	Int:                  700
	Dex:                  150
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          1160
	Body:                 104
	CanFly:               true
	CreatureType:         "Undead"
	VirtualArmor:         40
	FightMode:            "Closest"
	HideType:             "Liche"
	Hides:                5
	HitsMaxSeed:              650
	Hue:                  1282
	LootItemChance:       75
	LootItemLevel:        5
	LootTable:            "35"
	ManaMaxSeed:          200
	MinTameSkill:         135
	ProvokeSkillOverride: 120
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DecayingRay", "SpectresTouch", "WraithBreath", "Darkness"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  110
		Tactics:      110
		Wrestling:    130
		Magery:       140
		EvalInt:      140
		DetectHidden: 130
	}
	Attack: {
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 364
		Ability: {
			SpellType: "WraithBreath"
		}
	}
}
