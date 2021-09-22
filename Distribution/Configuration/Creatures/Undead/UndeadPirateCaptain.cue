package Undead

UndeadPirateCaptain: {
	Name:               "<random>, undead pirate captain"
	CorpseNameOverride: "corpse of <random>, undead pirate captain"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                250
	Int:                300
	Dex:                250
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMax:            250
	LootItemChance:     1
	LootItemLevel:      1
	LootTable:          "23"
	ManaMaxSeed:        400
	StamMaxSeed:        150
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "Explosion"]
	Resistances: Poison: 6
	Skills: {
		MagicResist: 60
		Tactics:     100
		Wrestling:   100
		Magery:      100
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 16
		}
	}
}
