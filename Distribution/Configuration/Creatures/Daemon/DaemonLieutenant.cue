package Daemon

DaemonLieutenant: {
	Name:               "a Daemon Lieutenant"
	CorpseNameOverride: "corpse of a Daemon Lieutenant"
	Str:                550
	Int:                350
	Dex:                350
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Daemon"
	VirtualArmor:       35
	FightMode:          "Closest"
	HideType:           "Liche"
	Hides:              3
	HitsMax:            550
	Hue:                1209
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "22"
	ManaMaxSeed:        200
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Explosion", "Paralyze", "MassCurse"]
	Resistances: Poison: 6
	Skills: {
		MagicResist: 90
		Tactics:     100
		Wrestling:   160
		Magery:      100
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 20
			Max: 65
		}
		HitSound: 357
	}
}
