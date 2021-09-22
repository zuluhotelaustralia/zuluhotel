package Human

KingOfNujelm: {
	Name:               "King of Nujelm"
	CorpseNameOverride: "corpse of King of Nujelm"
	Str:                800
	Int:                60
	Dex:                250
	AiType:             "AI_Archer"
	AlwaysMurderer:     true
	BaseSoundID:        250
	Body:               991
	ClassLevel:         4
	ClassType:          "Ranger"
	CreatureType:       "Human"
	VirtualArmor:       40
	FightMode:          "Closest"
	FightRange:         7
	HitsMax:            800
	Hue:                1300
	LootItemChance:     50
	LootItemLevel:      6
	LootTable:          "131"
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Skills: {
		Tactics:      100
		Archery:      130
		MagicResist:  90
		Hiding:       120
		DetectHidden: 100
	}
	Attack: {
		Damage: {
			Min: 17
			Max: 57
		}
		HitSound: 252
	}
	Equipment: [{
		ItemType: "SBow"
		Name:     "Tainted Ranger Weapon"
		Hue:      1171
	}, {
		ItemType:    "SLeatherGloves"
		Name:        "a pair of dark leather gloves"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType: "SBoots"
		Name:     "a pair of black leather boots"
		Hue:      1
	}, {
		ItemType: "SLongPants"
		Name:     "a pair of black leather pants"
		Hue:      1157
	}, {
		ItemType:    "SLeatherChest"
		Name:        "a Tunic of Woven Shadows"
		Hue:         1302
		ArmorRating: 1
	}, {
		ItemType:    "SBoneArms"
		Name:        "a Bracer of Woven Shadows"
		Hue:         1302
		ArmorRating: 1
	}]
}
