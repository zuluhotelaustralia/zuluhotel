package Human

Bandit: {
	Name:                 "a bandit"
	CorpseNameOverride:   "corpse of a bandit"
	Str:                  30
	Int:                  20
	Dex:                  30
	CreatureType:         "Human"
	HitsMax:              30
	LootItemChance:       1
	LootTable:            "47"
	ManaMaxSeed:          10
	ProvokeSkillOverride: 50
	StamMaxSeed:          30
	Skills: {
		Tactics: 30
		Fencing: 30
	}
	Attack: {
		Damage: {
			Min: 5
			Max: 50
		}
	}
	Equipment: [
		{
			ItemType: "Cutlass"
			Lootable: true
		},
		{
			ItemType: "LongPants"
			Lootable: true
		},
		{
			ItemType: "FancyShirt"
			Lootable: true
		},
		{
			ItemType: "SkullCap"
			Lootable: true
		},
	]
}
