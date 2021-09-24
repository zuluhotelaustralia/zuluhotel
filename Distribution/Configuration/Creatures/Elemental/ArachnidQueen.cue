package Elemental

ArachnidQueen: {
	Name:                 "an Arachnid Queen"
	CorpseNameOverride:   "corpse of an Arachnid Queen"
  Str:                  2250
	Int:                  55
	Dex:                  400
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          904
	Body:                 28
	CreatureType:         "Elemental"
	VirtualArmor:         45
	FightMode:            "Aggressor"
	HitsMax:              5250
	Hue:                  1175
	LootItemChance:       80
	LootItemLevel:        7
	LootTable:            "9"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 160
	StamMaxSeed:          200
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Poison:        1
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Poisoning:    70
		Tactics:      150
		Wrestling:    200
		MagicResist:  200
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound:  389
		MissSound: 390
		HitPoison: "Greater"
	}
}
