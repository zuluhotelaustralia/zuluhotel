package Dragonkin

Warlock: {
	Name:                 "<random> the Warlock"
	CorpseNameOverride:   "corpse of <random> the Warlock"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  225
	Int:                  295
	Dex:                  180
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          418
	Body:                 33
	CreatureType:         "Dragonkin"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Lizard"
	Hides:                5
	HitsMax:              225
	Hue:                  1201
	LootItemChance:       50
	LootItemLevel:        2
	LootTable:            "55"
	ManaMaxSeed:          155
	ProvokeSkillOverride: 105
	StamMaxSeed:          80
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "Curse", "Paralyze"]
	Resistances: MagicImmunity: 2
	Skills: {
		Tactics:     50
		Wrestling:   50
		MagicResist: 90
		Magery:      110
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 420
	}
}
