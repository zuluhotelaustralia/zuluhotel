package Elemental

BloodElemental: {
	Name:                 "a blood elemental"
	CorpseNameOverride:   "corpse of a blood elemental"

	Str:                  450
	Int:                  400
	Dex:                  185
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          278
	Body:                 16
	CreatureType:         "Elemental"
	VirtualArmor:         45
	FightMode:            "Closest"
	HitsMax:              450
	Hue:                  1209
	LootItemChance:       25
	LootItemLevel:        4
	LootTable:            "46"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 115
	StamMaxSeed:          50
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm"]
	Resistances: MagicImmunity: 3
	Skills: {
		Tactics:     100
		Wrestling:   200
		MagicResist: 80
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 21
			Max: 45
		}
		HitSound: 468
	}
}
