package Troll

EliteTrollArcher: {
	Name:                 "an elite troll archer"
	CorpseNameOverride:   "corpse of an elite troll archer"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  200
	Int:                  60
	Dex:                  120
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 54
	CreatureType:         "Troll"
	VirtualArmor:         25
	FightMode:            "Closest"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              200
	Hue:                  33784
	LootTable:            "41"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 80
	StamMaxSeed:          60
	Skills: {
		MagicResist: 60
		Tactics:     95
		Wrestling:   120
		Archery:     105
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
}
