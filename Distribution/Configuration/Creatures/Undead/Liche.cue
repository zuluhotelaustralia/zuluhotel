package Undead

Liche: {
	Name:               "a liche"
	CorpseNameOverride: "corpse of a liche"
	Str:                180
	Int:                300
	Dex:                80
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        1001
	Body:               24
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Closest"
	HideType:           "Liche"
	Hides:              3
	HitsMaxSeed:        180
	LootItemChance:     80
	LootItemLevel:      3
	LootTable:          "23"
	ManaMaxSeed:        200
	StamMaxSeed:        70
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Explosion", "Fireball", "MassCurse", "Paralyze", "Weaken"]
	Resistances: {
		Poison:        6
		Necro:         75
		MagicImmunity: 3
	}
	Skills: {
		Parry:       65
		Tactics:     50
		Wrestling:   100
		MagicResist: 80
		EvalInt:     80
		Magery:      150
	}
	Attack: {
		Damage: {
			Min: 4
			Max: 32
		}
		HitSound: 414
	}
}
