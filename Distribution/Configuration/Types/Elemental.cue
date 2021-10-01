package Types

#ElementalTypeEnum: {
	None:            "None"
	Water:           "Water"
	Air:             "Air"
	Physical:        "Physical"
	Fire:            "Fire"
	Poison:          "Poison"
	Earth:           "Earth"
	Necro:           "Necro"
	Paralysis:       "Paralysis"
	HealingBonus:    "HealingBonus"
	MagicImmunity:   "MagicImmunity"
	MagicReflection: "MagicReflection"
}
#ElementalType: or([
		for _, v in #ElementalTypeEnum {
		v
	},
])
