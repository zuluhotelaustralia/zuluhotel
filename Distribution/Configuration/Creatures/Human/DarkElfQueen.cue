package Human

DarkElfQueen: {
	Name:                 "a Dark-Elf Queen"
	CorpseNameOverride:   "corpse of a Dark-Elf Queen"

	Str:                  700
	Int:                  1500
	Dex:                  195
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          281
	Body:                 401
	ClassLevel:           2
	ClassType:            "Mage"
	CreatureType:         "Human"
	Female:               true
	FightMode:            "Closest"
	FightRange:           12
	HitsMax:              1500
	Hue:                  1109
	LootItemChance:       60
	LootItemLevel:        5
	LootTable:            "137"
	ManaMaxSeed:          1500
	ProvokeSkillOverride: 140
	StamMaxSeed:          195
	PreferredSpells: ["DecayingRay", "SorcerersBane", "WyvernStrike", "WyvernStrike"]
	Resistances: {
		Air:           50
		Water:         25
		Fire:          50
		Poison:        6
		Earth:         100
		MagicImmunity: 6
	}
	Skills: {
		Macing:       150
		Tactics:      75
		MagicResist:  150
		Magery:       200
		DetectHidden: 100
	}
	Attack: {
		Damage: {
			Min: 31
			Max: 61
		}
		Animation: "Bash2H"
		HitSound:  283
		MissSound: 284
		MaxRange:  12
	}
	Equipment: [{
		ItemType: "SGnarledStaff"
		Name:     "Staff of Fire"
		Hue:      1100
	}, {
		ItemType:    "SFemalePlateChest"
		Name:        "Elven Platemail"
		ArmorRating: 80
	}, {
		ItemType:    "SPlateLegs"
		Name:        "Long pants"
		Hue:         1172
		ArmorRating: 70
	}, {
		ItemType: "SLongHair"
		Hue:      1156
	}]
}
