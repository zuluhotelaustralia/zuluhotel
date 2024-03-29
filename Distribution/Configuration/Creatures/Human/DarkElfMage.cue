package Human

DarkElfMage: {
	Name:                 "a dark elf mage"
	CorpseNameOverride:   "corpse of a dark elf mage"
	Str:                  160
	Int:                  300
	Dex:                  195
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          313
	Body:                 401
	CreatureType:         "Human"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMaxSeed:          160
	Hue:                  1109
	LootItemChance:       60
	LootItemLevel:        3
	LootTable:            "57"
	ManaMaxSeed:          85
	ProvokeSkillOverride: 50
	StamMaxSeed:          95
	PreferredSpells: ["Fireball", "Lightning", "MagicArrow", "Paralyze", "EnergyBolt"]
	Resistances: MagicImmunity: 6
	Skills: {
		Macing:      95
		Tactics:     75
		MagicResist: 120
		Hiding:      100
		Magery:      110
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 315
	}
	Equipment: [{
		ItemType: "Longsword"
		Lootable: true
	}]
}
