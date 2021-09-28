package Terathan

TerathanMatriarch: {
	Name:               "a Terathan Matriarch"
	CorpseNameOverride: "corpse of a Terathan Matriarch"
	Str:                350
	Int:                650
	Dex:                70
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        599
	Body:               72
	CreatureType:       "Terathan"
	VirtualArmor:       35
	FightMode:          "Closest"
	HitsMaxSeed:            350
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "65"
	ManaMaxSeed:        200
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Explosion"]
	Resistances: MagicImmunity: 6
	Skills: {
		Parry:       65
		Tactics:     90
		Wrestling:   110
		Magery:      175
		MagicResist: 150
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
