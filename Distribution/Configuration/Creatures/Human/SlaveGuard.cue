package Human

SlaveGuard: {
	Name:               "<random> the Slave Guard"
	CorpseNameOverride: "corpse of <random> the Slave Guard"
	Str:                1000
	Int:                55
	Dex:                400
	AlwaysMurderer:     true
	BardImmune:         true
	ClassLevel:         1
	ClassType:          "Warrior"
	CreatureType:       "Human"
	VirtualArmor:       60
	FightMode:          "Aggressor"
	HitsMax:            1000
	Hue:                1159
	LootItemChance:     20
	LootItemLevel:      1
	LootTable:          "9"
	ManaMaxSeed:        0
	StamMaxSeed:        200
	Resistances: {
		Fire:   25
		Air:    25
		Water:  25
		Poison: 3
		Necro:  25
		Earth:  25
	}
	Skills: {
		Tactics:      250
		Macing:       175
		MagicResist:  60
		DetectHidden: 200
	}
	Attack: {
		Speed: 51
		Damage: {
			Min: 10
			Max: 50
		}
		HitSound:  315
		MissSound: 563
	}
	Equipment: [{
		ItemType: "SWarHammer"
		Name:     "Enslavers Weapon"
		Hue:      1162
	}]
}
