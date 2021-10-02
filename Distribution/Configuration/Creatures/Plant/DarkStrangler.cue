package Plant

DarkStrangler: {
	Name:               "a Dark Strangler"
	CorpseNameOverride: "corpse of a Dark Strangler"
	Str:                400
	Int:                45
	Dex:                200
	AiType:             "AI_Archer"
	AlwaysMurderer:     true
	BaseSoundID:        684
	Body:               8
	CreatureType:       "Plant"
	VirtualArmor:       40
	FightMode:          "Closest"
	FightRange:         7
	HitsMaxSeed:        400
	Hue:                1285
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "128"
	ManaMaxSeed:        35
	StamMaxSeed:        95
	Resistances: Earth: 100
	Skills: {
		Parry:       90
		Hiding:      125
		Tactics:     110
		MagicResist: 75
		Archery:     150
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 16
			Max: 46
		}
		HitSound:           458
		MissSound:          457
		HitPoison:          "Regular"
		ProjectileEffectId: 0x37C4
	}
	Equipment: [{
		ItemType: "Bow"
		Name:     "Dark Strangler Weapon"
		Hue:      1171
	}, {
		ItemType:    "HeaterShield"
		Name:        "Shield AR30"
		ArmorRating: 30
	}]
}
