package Misc

Hero: {
	Name:               "<random> the Hero"
	CorpseNameOverride: "corpse of <random> the Hero"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                300
	Int:                210
	Dex:                300
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	ClassLevel:         6
	ClassType:          "Warrior"
	VirtualArmor:       80
	FightMode:          "Aggressor"
	HitsMax:            2000
	Hue:                1002
	LootTable:          "59"
	ManaMaxSeed:        200
	StamMaxSeed:        200
	Skills: {
		Tactics:     200
		MagicResist: 200
	}
	Attack: {
		Speed: 67
		Damage: {
			Min: 33
			Max: 45
		}
		HitSound:  571
		MissSound: 569
	}
	Equipment: [{
		ItemType: "Server.Items.Longsword"
		Name:     "The Hero Longsword"
		Hue:      1182
	}]
}
