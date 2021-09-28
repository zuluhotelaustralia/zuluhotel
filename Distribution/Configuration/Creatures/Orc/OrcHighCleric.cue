package Orc

OrcHighCleric: {
	Name:               "Orc High Cleric"
	CorpseNameOverride: "corpse of Orc High Cleric"
	Str:                1200
	Int:                2750
	Dex:                310
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1114
	Body:               17
	CreatureType:       "Orc"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMaxSeed:            1200
	Hue:                1170
	LootItemChance:     100
	LootItemLevel:      6
	LootTable:          "35"
	ManaMaxSeed:        2750
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Explosion", "WyvernStrike", "Plague", "DecayingRay", "Darkness", "WyvernStrike", "AbyssalFlame", "GreaterHeal"]
	Resistances: {
		Poison:        6
		Earth:         100
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
