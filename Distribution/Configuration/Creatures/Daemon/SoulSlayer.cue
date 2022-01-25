package Daemon

SoulSlayer: {
	Name:               "a Soul Slayer"
	CorpseNameOverride: "corpse of a Soul Slayer"
	Str:                1100
	Int:                3000
	Dex:                300
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        1001
	Body:               792
	CanFly:             true
	CreatureType:       "Daemon"
	VirtualArmor:       60
	FightMode:          "Closest"
	FightRange:         5
	HitsMaxSeed:        1000
	Hue:                1302
	LootItemChance:     100
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        2000
	StamMaxSeed:        70
	PreferredSpells: ["Plague", "DispelField", "SpectresTouch", "MindBlast", "Curse"]
	Resistances: {
		Fire:          100
		Water:         100
		Air:           100
		Poison:        6
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Parry:        200
		MagicResist:  200
		Tactics:      200
		Wrestling:    150
		Magery:       125
		EvalInt:      200
		DetectHidden: 200
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 57
			Max: 99
		}
		HitSound:  606
		MissSound: 360
		MaxRange:  2
	}
}
