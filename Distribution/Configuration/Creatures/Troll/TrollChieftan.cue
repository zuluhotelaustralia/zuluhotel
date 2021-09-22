package Troll

TrollChieftan: {
	Name:                 "a Troll Chieftan"
	CorpseNameOverride:   "corpse of a Troll Chieftan"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  350
	Int:                  90
	Dex:                  220
	AlwaysMurderer:       true
	BaseSoundID:          560
	Body:                 53
	CreatureType:         "Troll"
	VirtualArmor:         35
	FightMode:            "Aggressor"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              350
	Hue:                  1125
	LootItemChance:       30
	LootItemLevel:        3
	LootTable:            "60"
	ManaMaxSeed:          80
	ProvokeSkillOverride: 120
	StamMaxSeed:          90
	Skills: {
		Tactics:     140
		MagicResist: 80
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 21
			Max: 45
		}
		Skill:     "Swords"
		HitSound:  562
		MissSound: 570
	}
}
