package Undead

Bloodliche: {
	Name:               "a Bloodliche"
	CorpseNameOverride: "corpse of a Bloodliche"
	Str:                275
	Int:                900
	Dex:                210
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Undead"
	VirtualArmor:       30
	FightMode:          "Closest"
	HideType:           "Liche"
	Hides:              3
	HitsMaxSeed:        275
	Hue:                1209
	LootItemChance:     25
	LootItemLevel:      5
	LootTable:          "22"
	ManaMaxSeed:        200
	StamMaxSeed:        50
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Explosion", "Paralyze", "MassCurse"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 7
	}
	Skills: {
		Magery:      175
		MagicResist: 150
		Tactics:     50
		Wrestling:   160
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 9
			Max: 45
		}
		HitSound:  414
		MissSound: 412
	}
}
