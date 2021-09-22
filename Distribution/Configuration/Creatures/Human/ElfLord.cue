package Human

ElfLord: {
	Name:                 "an Elf Lord"
	CorpseNameOverride:   "corpse of an Elf Lord"

	Str:                  700
	Int:                  1500
	Dex:                  195
	AiType:               "AI_Mage"
	ClassLevel:           2
	ClassType:            "Mage"
	CreatureType:         "Human"
	Female:               true
	FightMode:            "Closest"
	FightRange:           12
	HitsMax:              1500
	Hue:                  770
	InitialInnocent:      true
	LootItemChance:       60
	LootItemLevel:        5
	LootTable:            "136"
	ManaMaxSeed:          1500
	ProvokeSkillOverride: 140
	StamMaxSeed:          195
	PreferredSpells: ["ShiftingEarth", "GustOfAir", "CallLightning", "IceStrike"]
	Resistances: {
		Air:           50
		Water:         50
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
		HitSound:  38
		MissSound: 37
		MaxRange:  12
	}
	Equipment: [{
		ItemType: "SGnarledStaff"
		Name:     "Staff of Water"
		Hue:      1099
	}, {
		ItemType:    "SPlateChest"
		Name:        "Elven Breastplate"
		Hue:         1172
		ArmorRating: 80
	}, {
		ItemType:    "SPlateLegs"
		Name:        "Long pants"
		Hue:         1172
		ArmorRating: 70
	}]
}
