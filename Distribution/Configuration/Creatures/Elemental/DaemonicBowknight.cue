package Elemental

DaemonicBowknight: {
	Name:                 "a Daemonic Bowknight"
	CorpseNameOverride:   "corpse of a Daemonic Bowknight"
	Str:                  2250
	Int:                  55
	Dex:                  400
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          357
	Body:                 318
	CreatureType:         "Elemental"
	VirtualArmor:         45
	FightMode:            "Closest"
	FightRange:           7
	HitsMaxSeed:          2250
	Hue:                  16385
	LootItemChance:       50
	LootItemLevel:        5
	LootTable:            "9"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 160
	StamMaxSeed:          500
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Tactics:      150
		Archery:      130
		Macing:       175
		MagicResist:  60
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 17
			Max: 57
		}
		HitSound: 252
	}
	Equipment: [{
		ItemType: "Bow"
		Hue:      1171
	}]
}
