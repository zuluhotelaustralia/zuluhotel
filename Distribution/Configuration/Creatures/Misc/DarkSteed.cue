package Misc

DarkSteed: {
	Name:                 "a dark steed"
	CorpseNameOverride:   "corpse of a dark steed"
  Str:                  1200
	Int:                  600
	Dex:                  120
	AiType:               "AI_Mage"
	BaseSoundID:          168
	Body:                 179
	VirtualArmor:         40
	Fame:                 4
	FightMode:            "Closest"
	HitsMaxSeed:              1200
	Karma:                4
	LootItemChance:       100
	LootTable:            "26"
	ManaMaxSeed:          600
	MinTameSkill:         150
	ProvokeSkillOverride: 150
	StamMaxSeed:          120
	Tamable:              true
	PreferredSpells: ["MagicArrow", "Harm", "Fireball", "Poison", "Lightning", "ManaDrain", "MindBlast", "Paralyze", "EnergyBolt", "Explosion", "FlameStrike"]
	Skills: {
		Anatomy:     110
		MagicResist: 120
		EvalInt:     100
		Tactics:     120
		Wrestling:   110
		Magery:      100
	}
	Attack: {
		Speed: 55
		Damage: {
			Min: 8
			Max: 10
		}
		HitSound: 170
	}
}
