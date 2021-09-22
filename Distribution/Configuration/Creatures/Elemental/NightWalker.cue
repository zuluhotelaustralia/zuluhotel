package Elemental

NightWalker: {
	Name:               "a Night Walker"
	CorpseNameOverride: "corpse of a Night Walker"
	Str:                4000
	Int:                1000
	Dex:                600
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        354
	Body:               311
	CanSwim:            true
	CreatureType:       "Elemental"
	FightMode:          "Aggressor"
	HitsMax:            4000
	Hue:                1157
	LootItemChance:     100
	LootItemLevel:      9
	LootTable:          "201"
	ManaMaxSeed:        1000
	StamMaxSeed:        500
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Poison:        1
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		MagicResist:  200
		Tactics:      100
		Wrestling:    150
		Parry:        100
		DetectHidden: 130
		Hiding:       200
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 16
			Max: 46
		}
		HitSound:      356
		MissSound:     355
		Ability:       "TriElementalStrike"
		AbilityChance: 1
	}
	Equipment: [{
		ItemType:    "SMetalShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
