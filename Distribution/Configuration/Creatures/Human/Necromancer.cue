package Human

Necromancer: {
	Name:                 "<random> the Necromancer"
	CorpseNameOverride:   "corpse of <random> the Necromancer"
	Str:                  130
	Int:                  300
	Dex:                  90
	AiType:               "AI_Mage"
	CreatureType:         "Human"
	VirtualArmor:         25
	HideType:             "Necromancer"
	Hides:                3
	HitsMaxSeed:              130
	LootItemChance:       50
	LootItemLevel:        3
	LootTable:            "46"
	ManaMaxSeed:          95
	ProvokeSkillOverride: 90
	RiseCreatureDelay:    "00:00:08"
	StamMaxSeed:          50
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "MassCurse", "Explosion", "WraithForm", "LicheForm", "Darkness", "DecayingRay"]
	Resistances: {
		Fire:  75
		Necro: 75
		Earth: 75
	}
	Skills: {
		MagicResist: 80
		Tactics:     100
		Macing:      100
		Magery:      150
	}
	Attack: {
		Speed: 50
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 330
	}
	Equipment: [
		{
			ItemType: "LongHair"
			Hue:      1136
		},
		{
			ItemType: "BlackStaff"
		},
		{
			ItemType: "Robe"
			Lootable: true
			Hue:      0x044e
		},
		{
			ItemType: "Spellbook"
		},
	]
}
