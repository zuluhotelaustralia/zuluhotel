package Gargoyle

StoneGargoyle: {
	Name:                 "a stone gargoyle"
	CorpseNameOverride:   "corpse of a stone gargoyle"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  495
	Int:                  90
	Dex:                  80
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CreatureType:         "Gargoyle"
	VirtualArmor:         20
	FightMode:            "Aggressor"
	HitsMax:              495
	Hue:                  1154
	LootItemChance:       15
	LootItemLevel:        2
	LootTable:            "40"
	ManaMaxSeed:          80
	ProvokeSkillOverride: 105
	StamMaxSeed:          65
	Resistances: MagicImmunity: 1
	Skills: {
		Tactics:     100
		Wrestling:   135
		MagicResist: 60
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound: 374
	}
}
