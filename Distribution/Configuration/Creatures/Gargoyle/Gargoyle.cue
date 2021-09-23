package Gargoyle

Gargoyle: {
	Name:                 "a gargoyle"
	CorpseNameOverride:   "corpse of a gargoyle"
  Str:                  130
	Int:                  210
	Dex:                  80
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CreatureType:         "Gargoyle"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMax:              130
	LootItemChance:       10
	LootItemLevel:        2
	LootTable:            "38"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 85
	StamMaxSeed:          70
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Fireball", "Weaken", "MassCurse"]
	Resistances: {
		Physical:      25
		MagicImmunity: 2
	}
	Skills: {
		MagicResist: 80
		Tactics:     80
		Wrestling:   135
		Magery:      110
		EvalInt:     65
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 44
		}
		HitSound:  374
		MissSound: 562
	}
}
