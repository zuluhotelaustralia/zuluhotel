package Orc

OrcMasterMage: {
	Name:                 "<random> the Orc Master Mage"
	CorpseNameOverride:   "corpse of <random> the Orc Master Mage"
  Str:                  255
	Int:                  255
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	VirtualArmor:         40
	FightMode:            "Closest"
	HitsMaxSeed:              255
	Hue:                  1554
	LootItemChance:       75
	LootItemLevel:        3
	LootTable:            "31"
	ManaMaxSeed:          205
	ProvokeSkillOverride: 100
	StamMaxSeed:          80
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "Fireball", "Paralyze", "MassCurse", "Explosion"]
	Resistances: MagicImmunity: 3
	Skills: {
		MagicResist: 85
		Tactics:     80
		Wrestling:   100
		Magery:      120
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 434
	}
}
