package Undead

BoneKnight: {
	Name:               "a bone knight"
	CorpseNameOverride: "corpse of a bone knight"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                160
	Int:                35
	Dex:                90
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               57
	CreatureType:       "Undead"
	VirtualArmor:       15
	FightMode:          "Aggressor"
	HitsMax:            160
	Hue:                33784
	LootItemChance:     2
	LootTable:          "16"
	ManaMaxSeed:        25
	StamMaxSeed:        80
	Resistances: Poison: 6
	Skills: {
		Tactics:     100
		MagicResist: 50
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 8
			Max: 64
		}
		Skill:    "Swords"
		HitSound: 453
	}
}
