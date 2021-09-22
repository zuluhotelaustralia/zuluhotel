package Human

AdeptMage: {
	Name:                 "an adept mage"
	CorpseNameOverride:   "corpse of an adept mage"

	Str:                  200
	Int:                  295
	Dex:                  90
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	CreatureType:         "Human"
	VirtualArmor:         20
	FightMode:            "Closest"
	HitsMax:              200
	LootItemChance:       75
	LootItemLevel:        2
	LootTable:            "25"
	ManaMaxSeed:          95
	ProvokeSkillOverride: 94
	StamMaxSeed:          50
	PreferredSpells: ["Lightning", "Curse", "Weaken", "Clumsy", "Fireball", "MagicArrow", "EnergyBolt"]
	Skills: {
		MagicResist: 90
		Tactics:     85
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 40
		}
		HitSound: 315
	}
	Equipment: [{
		ItemType: "SLongHair"
		Hue:      1140
	}, {
		ItemType: "SGnarledStaff"
		Name:     "Evil Mage Weapon"
	}]
}
