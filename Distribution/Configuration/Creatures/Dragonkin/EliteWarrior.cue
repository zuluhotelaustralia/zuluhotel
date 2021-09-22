package Dragonkin

EliteWarrior: {
	Name:                 "<random> the elite warrior"
	CorpseNameOverride:   "corpse of <random> the elite warrior"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  220
	Int:                  50
	Dex:                  103
	AlwaysMurderer:       true
	BaseSoundID:          417
	Body:                 36
	CreatureType:         "Dragonkin"
	VirtualArmor:         20
	FightMode:            "Aggressor"
	HideType:             "Lizard"
	Hides:                5
	HitsMax:              220
	LootTable:            "14"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          75
	Skills: {
		Tactics:     80
		MagicResist: 60
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 5
			Max: 30
		}
		HitSound: 315
	}
}
