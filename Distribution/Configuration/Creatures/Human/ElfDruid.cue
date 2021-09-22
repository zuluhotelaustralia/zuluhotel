package Human

ElfDruid: {
	Name:                 "an Elf Druid"
	CorpseNameOverride:   "corpse of an Elf Druid"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  200
	Int:                  1000
	Dex:                  195
	AiType:               "AI_Mage"
	BaseSoundID:          313
	Body:                 401
	ClassLevel:           2
	ClassType:            "Mage"
	CreatureType:         "Human"
	VirtualArmor:         25
	Female:               true
	FightMode:            "Closest"
	HitsMax:              500
	Hue:                  770
	InitialInnocent:      true
	LootItemChance:       60
	LootItemLevel:        4
	LootTable:            "133"
	ManaMaxSeed:          1000
	ProvokeSkillOverride: 120
	StamMaxSeed:          195
	PreferredSpells: [
		"ShiftingEarth",
		"CallLightning",
	]
	Resistances: MagicImmunity: 6
	Skills: {
		Macing:      100
		Tactics:     75
		MagicResist: 150
		Magery:      150
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 40
		}
		HitSound: 315
	}
	Equipment: [{
		ItemType: "Server.Items.DeathShroud"
		Name:     "Druid's Robe"
		Hue:      1285
	}, {
		ItemType: "Server.Items.GnarledStaff"
		Name:     "Evil Mage Weapon"
	}]
}
