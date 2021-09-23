package Elemental

GreaterShadow: {
	Name:               "a Greater Shadow"
	CorpseNameOverride: "corpse of a Greater Shadow"
	Str:                350
	Int:                200
	Dex:                275
	AlwaysMurderer:     true
	BaseSoundID:        640
	Body:               573
	CanSwim:            true
	CreatureType:       "Elemental"
	FightMode:          "Aggressor"
	HitsMax:            350
	Hue:                1
	LootItemChance:     15
	LootItemLevel:      4
	LootTable:          "78"
	ManaMaxSeed:        125
	StamMaxSeed:        80
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 5
	}
	Skills: {
		MagicResist:  125
		Tactics:      125
		Wrestling:    150
		DetectHidden: 130
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 21
			Max: 45
		}
		HitSound:  642
		MissSound: 641
	}
	Equipment: [{
		ItemType:    "BoneGloves"
		Name:        "Green Bone Gloves AR10"
		Hue:         1169
		ArmorRating: 10
	}, {
		ItemType:    "BoneHelm"
		Name:        "Red Bone Helm AR45"
		Hue:         1172
		ArmorRating: 45
	}]
}
