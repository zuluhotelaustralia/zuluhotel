package Undead

Banshee: {
	Name:               "a banshee"
	CorpseNameOverride: "corpse of a banshee"
	Str:                925
	Int:                1025
	Dex:                380
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        1154
	Body:               310
	ClassLevel:         6
	ClassType:          "Mage"
	CreatureType:       "Undead"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMax:            925
	Hue:                1176
	LootItemChance:     80
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        1025
	StamMaxSeed:        80
	PreferredSpells: [
		"WyvernStrike",
		"WyvernStrike",
	]
	Resistances: Poison: 6
	Skills: {
		EvalInt:     100
		Magery:      150
		Parry:       80
		MagicResist: 105
		Tactics:     120
		Wrestling:   120
		Hiding:      130
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 15
			Max: 40
		}
		HitSound:      642
		MissSound:     641
		Ability:       "TriElementalStrike"
		AbilityChance: 1
	}
}
