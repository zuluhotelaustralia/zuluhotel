package Human

EvilMage: {
	Name:                 "<random> the Evil Mage"
	CorpseNameOverride:   "corpse of <random> the Evil Mage"

	Str:                  180
	Int:                  385
	Dex:                  90
	AiType:               "AI_Mage"
	CreatureType:         "Human"
	VirtualArmor:         20
	HitsMax:              180
	LootItemChance:       2
	LootTable:            "57"
	ManaMaxSeed:          105
	ProvokeSkillOverride: 94
	StamMaxSeed:          80
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "Fireball", "Paralyze", "Explosion"]
	Skills: {
		MagicResist: 170
		Tactics:     380
		Macing:      95
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
		Hue:      1110
	}, {
		ItemType: "SGnarledStaff"
		Name:     "Evil Mage Weapon"
	}]
}
