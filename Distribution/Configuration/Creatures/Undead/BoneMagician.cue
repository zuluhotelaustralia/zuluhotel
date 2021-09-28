package Undead

BoneMagician: {
	Name:               "a bone magician"
	CorpseNameOverride: "corpse of a bone magician"
	Str:                200
	Int:                295
	Dex:                180
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1165
	Body:               56
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            200
	LootItemChance:     50
	LootItemLevel:      2
	LootTable:          "39"
	ManaMaxSeed:        175
	StamMaxSeed:        50
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "Paralyze", "MassCurse"]
	Resistances: Poison: 6
	Skills: {
		MagicResist: 70
		Tactics:     60
		Wrestling:   80
		Magery:      95
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 363
	}
}
