package Beholder

Gazer: {
	Name:                 "a gazer"
	CorpseNameOverride:   "corpse of a gazer"
  Str:                  150
	Int:                  205
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          377
	Body:                 22
	CanFly:               true
	CreatureType:         "Beholder"
	VirtualArmor:         10
	FightMode:            "Closest"
	HitsMax:              150
	LootItemChance:       100
	LootItemLevel:        2
	LootTable:            "30"
	ManaMaxSeed:          195
	ProvokeSkillOverride: 95
	StamMaxSeed:          80
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Fireball", "Weaken"]
	Resistances: MagicImmunity: 2
	Skills: {
		Parry:       65
		MagicResist: 65
		Tactics:     50
		Magery:      90
		EvalInt:     70
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		Skill:    "Swords"
		HitSound: 379
	}
}
