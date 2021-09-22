package Terathan

TerathanQueen: {
	Name:               "a Terathan Queen"
	CorpseNameOverride: "corpse of a Terathan Queen"
	Str:                1350
	Int:                2000
	Dex:                70
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        599
	Body:               72
	ClassLevel:         4
	ClassType:          "Mage"
	CreatureType:       "Terathan"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMax:            2350
	Hue:                1177
	LootItemChance:     70
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        2000
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Explosion", "MindBlast", "DecayingRay", "WyvernStrike"]
	Resistances: MagicImmunity: 8
	Skills: {
		Parry:       100
		Tactics:     120
		Wrestling:   110
		Magery:      200
		MagicResist: 200
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 20
			Max: 60
		}
		HitSound:  598
		MissSound: 599
	}
}
