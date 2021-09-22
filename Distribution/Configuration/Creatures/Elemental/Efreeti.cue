package Elemental

Efreeti: {
	Name:               "an efreeti"
	CorpseNameOverride: "corpse of an efreeti"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                350
	Int:                355
	Dex:                100
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        655
	Body:               13
	CreatureType:       "Elemental"
	VirtualArmor:       40
	HitsMax:            350
	Hue:                93
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "46"
	ManaMaxSeed:        160
	StamMaxSeed:        90
	PreferredSpells: ["Fireball", "Lightning", "EnergyBolt"]
	Resistances: {
		Air:           100
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     85
		Wrestling:   160
		Magery:      100
		MagicResist: 130
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 5
			Max: 50
		}
		HitSound: 265
	}
}
