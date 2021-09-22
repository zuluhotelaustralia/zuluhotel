package Elemental

MageMenacer: {
	Name:               "a Mage Menacer"
	CorpseNameOverride: "corpse of a Mage Menacer"
	Str:                2500
	Int:                25
	Dex:                600
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        362
	Body:               574
	CreatureType:       "Elemental"
	VirtualArmor:       60
	FightMode:          "Aggressor"
	HitPoison:          "Deadly"
	HitsMax:            3000
	Hue:                1306
	LootItemChance:     80
	LootItemLevel:      7
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        500
	Resistances: {
		Poison:        6
		Physical:      100
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Poisoning:    100
		Tactics:      150
		Wrestling:    150
		MagicResist:  160
		DetectHidden: 130
		Hiding:       130
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 80
		}
		HitSound:      364
		Ability:       "BlackrockStrike"
		AbilityChance: 1
	}
}
