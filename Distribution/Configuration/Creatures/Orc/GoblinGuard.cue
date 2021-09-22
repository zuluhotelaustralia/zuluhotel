package Orc

GoblinGuard: {
	Name:               "<random> the Goblin Guard"
	CorpseNameOverride: "corpse of <random> the Goblin Guard"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                500
	Int:                210
	Dex:                300
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	ClassLevel:         4
	ClassType:          "Warrior"
	CreatureType:       "Orc"
	VirtualArmor:       60
	FightMode:          "Aggressor"
	HitsMax:            500
	Hue:                34186
	LootTable:          "59"
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Resistances: {
		Physical:      25
		Poison:        1
		MagicImmunity: 1
	}
	Skills: {
		Tactics:     150
		MagicResist: 80
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound:  434
		MissSound: 432
	}
	Equipment: [{
		ItemType:    "Server.Items.OrcHelm"
		Name:        "Goblin Helmet"
		Hue:         1418
		ArmorRating: 18
	}]
}
