package Elemental

ShadowLord: {
	Name:               "a Shadow Lord"
	CorpseNameOverride: "corpse of a Shadow Lord"
	Str:                550
	Int:                200
	Dex:                300
	AlwaysMurderer:     true
	BaseSoundID:        640
	Body:               146
	CanSwim:            true
	CreatureType:       "Elemental"
	FightMode:          "Closest"
	HitsMax:            550
	Hue:                1
	LootItemChance:     15
	LootItemLevel:      5
	LootTable:          "79"
	ManaMaxSeed:        125
	StamMaxSeed:        80
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 6
	}
	Skills: {
		MagicResist:  150
		Tactics:      100
		Wrestling:    150
		DetectHidden: 130
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 16
			Max: 46
		}
		HitSound:      642
		MissSound:     641
		Ability:       "TriElementalStrike"
		AbilityChance: 1
	}
	Equipment: [{
		ItemType:    "BoneGloves"
		Name:        "Blue Bone Gloves AR10"
		Hue:         1170
		ArmorRating: 10
	}, {
		ItemType:    "BoneHelm"
		Name:        "Red Bone Helm AR45"
		Hue:         1172
		ArmorRating: 45
	}]
}
