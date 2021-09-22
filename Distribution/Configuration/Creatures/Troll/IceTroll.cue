package Troll

IceTroll: {
	Name:                 "an ice troll"
	CorpseNameOverride:   "corpse of an ice troll"

	Str:                  250
	Int:                  70
	Dex:                  185
	AlwaysMurderer:       true
	BaseSoundID:          560
	Body:                 53
	CreatureType:         "Troll"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              250
	Hue:                  1154
	LootTable:            "14"
	ManaMaxSeed:          60
	ProvokeSkillOverride: 100
	StamMaxSeed:          150
	Skills: {
		Tactics:     130
		MagicResist: 80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 21
			Max: 45
		}
		HitSound:  562
		MissSound: 570
	}
}
