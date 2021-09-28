package Human

MasterOfWind: {
	Name:                 "a master of the wind"
	CorpseNameOverride:   "corpse of a master of the wind"
	Str:                  160
	Int:                  295
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMaxSeed:              160
	LootItemChance:       66
	LootItemLevel:        3
	LootTable:            "57"
	ManaMaxSeed:          95
	ProvokeSkillOverride: 94
	StamMaxSeed:          50
	PreferredSpells: [
		"Lightning",
	]
	Resistances: {
		Air:           100
		MagicImmunity: 4
	}
	Skills: {
		MagicResist: 120
		Tactics:     100
		Magery:      150
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 6
			Max: 60
		}
	}
	Equipment: [
		{
			ItemType: "LongHair"
		},
		{
			ItemType: "GnarledStaff"
			Lootable: true
		},
		{
			ItemType: "Robe"
			Hue:      0x04b9
		},
	]
}
