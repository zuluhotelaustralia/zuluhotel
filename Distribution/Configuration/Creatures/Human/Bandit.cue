package Human

Bandit: {
	Name:                 "a bandit"
	CorpseNameOverride:   "corpse of a bandit"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
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
	Equipment: [{
		ItemType: "Server.Items.Cutlass"
		Name:     "Brigand1 Weapon"
	}]
}
