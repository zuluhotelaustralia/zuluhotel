package Human

MasterOfWind: {
	Name:                 "a master of the wind"
	CorpseNameOverride:   "corpse of a master of the wind"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  160
	Int:                  295
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              160
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
	Equipment: [{
		ItemType: "Server.Items.LongHair"
		Hue:      1128
	}, {
		ItemType: "Server.Items.GnarledStaff"
		Name:     "Air Master Weapon"
	}]
}
