package Undead

BoneWarlock: {
	Name:               "a bone warlock"
	CorpseNameOverride: "corpse of a bone warlock"
	Str:                270
	Int:                295
	Dex:                94
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1165
	Body:               56
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:        270
	LootItemChance:     50
	LootItemLevel:      2
	LootTable:          "23"
	ManaMaxSeed:        120
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "Lightning", "Fireball", "Paralyze", "EnergyBolt"]
	Resistances: Poison: 6
	Skills: {
		MagicResist: 76
		Tactics:     100
		Wrestling:   110
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 363
	}
}
