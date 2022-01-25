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
	HitsMaxSeed:          1500
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
		MaxRange:  2
	}
	Equipment: [
		{
			ItemType: "GnarledStaff"
			Name:     "Staff of Fire"
			Hue:      1100
		},
		{
			ItemType:    "FemalePlateChest"
			Name:        "Elven Platemail"
			Hue:         1172
			ArmorRating: 80
		},
		{
			ItemType:    "PlateLegs"
			Name:        "Elven Platemail"
			Hue:         1172
			ArmorRating: 70
		},
		{
			ItemType: "Cloak"
			Hue:      1
		},
		{
			ItemType: "LongHair"
			Hue:      1156
		},
	]
}
