package Elemental

EarthQuaker: {
	Name:               "an Earth Quaker"
	CorpseNameOverride: "corpse of an Earth Quaker"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                2250
	Int:                55
	Dex:                400
	PassiveSpeed:       0.2
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	AutoDispel:         true
	BardImmune:         true
	BaseSoundID:        268
	Body:               14
	ClassLevel:         4
	ClassType:          "Mage"
	CreatureType:       "Elemental"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMax:            2250
	Hue:                1000
	LootItemChance:     50
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        2550
	StamMaxSeed:        200
	PreferredSpells: ["ShiftingEarth", "ShiftingEarth", "ShiftingEarth", "ShiftingEarth"]
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Tactics:      250
		Wrestling:    150
		MagicResist:  60
		DetectHidden: 200
		Hiding:       200
		Magery:       300
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
