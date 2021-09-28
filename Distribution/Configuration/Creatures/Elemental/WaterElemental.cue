package Elemental

WaterElemental: {
	Name:               "a water elemental"
	CorpseNameOverride: "corpse of a water elemental"
	Str:                210
	Int:                250
	Dex:                80
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        278
	Body:               16
	CanSwim:            true
	CreatureType:       "Elemental"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:            210
	Hue:                33784
	LootItemChance:     25
	LootItemLevel:      3
	LootTable:          "20"
	ManaMaxSeed:        200
	StamMaxSeed:        70
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow"]
	Resistances: Water: 100
	Skills: {
		Parry:       55
		MagicResist: 30
		Tactics:     100
		Magery:      90
		EvalInt:     65
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 21
			Max: 45
		}
		Skill:    "Swords"
		HitSound: 280
	}
}
