package Daemon

IceFiend: {
	Name:                 "<random> the Ice Fiend"
	CorpseNameOverride:   "corpse of <random> the Ice Fiend"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  500
	Int:                  500
	Dex:                  300
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          357
	Body:                 43
	CreatureType:         "Daemon"
	VirtualArmor:         35
	FightMode:            "Closest"
	HideType:             "IceCrystal"
	Hides:                5
	HitsMax:              500
	Hue:                  1170
	LootItemChance:       50
	LootItemLevel:        4
	LootTable:            "22"
	ManaMaxSeed:          500
	ProvokeSkillOverride: 130
	StamMaxSeed:          95
	PreferredSpells: ["Poison", "Lightning", "ManaDrain", "EnergyBolt"]
	Resistances: MagicImmunity: 5
	Skills: {
		MagicResist: 90
		Tactics:     125
		Wrestling:   175
		Magery:      110
		EvalInt:     110
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 10
			Max: 40
		}
	}
}
