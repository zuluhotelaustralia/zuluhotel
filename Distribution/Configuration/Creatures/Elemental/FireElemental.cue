package Elemental

FireElemental: {
	Name:               "a fire elemental"
	CorpseNameOverride: "corpse of a fire elemental"
	Str:                180
	Int:                205
	Dex:                80
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        838
	Body:               15
	CreatureType:       "Elemental"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            180
	Hue:                33784
	LootItemChance:     25
	LootItemLevel:      3
	LootTable:          "20"
	ManaMaxSeed:        100
	StamMaxSeed:        50
	PreferredSpells: ["MagicArrow", "Fireball", "MagicReflect", "Explosion", "FlameStrike"]
	Resistances: Fire: 100
	Skills: {
		Parry:       55
		Tactics:     100
		MagicResist: 70
		Magery:      100
		EvalInt:     65
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 21
			Max: 45
		}
		Skill:    "Swords"
		HitSound: 275
	}
}
