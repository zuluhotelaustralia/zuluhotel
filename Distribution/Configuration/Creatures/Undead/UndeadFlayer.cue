package Undead

UndeadFlayer: {
	Name:               "an Undead Flayer"
	CorpseNameOverride: "corpse of an Undead Flayer"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                1200
	Int:                2750
	Dex:                310
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Undead"
	VirtualArmor:       45
	FightMode:          "Closest"
	HideType:           "Liche"
	Hides:              3
	HitsMax:            1200
	Hue:                1172
	LootItemChance:     100
	LootItemLevel:      6
	LootTable:          "35"
	ManaMaxSeed:        2750
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Explosion", "WraithBreath", "WyvernStrike", "Plague", "DecayingRay", "Darkness", "WyvernStrike", "AbyssalFlame", "GreaterHeal"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Magery:       175
		MagicResist:  150
		Tactics:      150
		Wrestling:    160
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
