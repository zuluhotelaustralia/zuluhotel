package Human

CorruptCounselor: {
	Name:               "<random> the Corrupt Counselor"
	CorpseNameOverride: "corpse of <random> the Corrupt Counselor"
	Str:                1500
	Int:                1000
	Dex:                250
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        354
	Body:               970
	CanSwim:            true
	CreatureType:       "Human"
	FightMode:          "Aggressor"
	HitsMax:            4000
	Hue:                1157
	LootItemChance:     50
	LootItemLevel:      5
	LootTable:          "9"
	ManaMaxSeed:        1000
	StamMaxSeed:        500
	Resistances: {
		Fire:          100
		Air:           75
		Water:         75
		Poison:        1
		Necro:         75
		Earth:         75
		MagicImmunity: 6
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
