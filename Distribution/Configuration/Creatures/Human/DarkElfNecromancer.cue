package Human

DarkElfNecromancer: {
	Name:                 "a Dark-Elf Necromancer"
	CorpseNameOverride:   "corpse of a Dark-Elf Necromancer"
	Str:                  200
	Int:                  1000
	Dex:                  195
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	ClassLevel:           2
	ClassType:            "Mage"
	CreatureType:         "Human"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMaxSeed:          500
	Hue:                  1109
	LootItemChance:       60
	LootItemLevel:        4
	LootTable:            "138"
	ManaMaxSeed:          1000
	ProvokeSkillOverride: 120
	StamMaxSeed:          195
	PreferredSpells: ["DecayingRay", "SorcerersBane", "WyvernStrike"]
	Resistances: MagicImmunity: 6
	Skills: {
		Macing:      95
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
	Equipment: [
		{
			ItemType: "LongHair"
			Hue:      1
			Lootable: true
		},
		{
			ItemType: "GnarledStaff"
			Lootable: true
		},
		{
			ItemType: "Robe"
			Hue:      1
			Lootable: true
		},
	]
}
