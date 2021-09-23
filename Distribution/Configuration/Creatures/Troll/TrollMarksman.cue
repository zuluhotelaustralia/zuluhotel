package Troll

TrollMarksman: {
	Name:                 "a troll marksman"
	CorpseNameOverride:   "corpse of a troll marksman"
  Str:                  185
	Int:                  55
	Dex:                  120
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 54
	CreatureType:         "Troll"
	VirtualArmor:         30
	FightMode:            "Closest"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              185
	Hue:                  33784
	LootTable:            "41"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 80
	StamMaxSeed:          60
	Skills: {
		MagicResist: 60
		Tactics:     105
		Wrestling:   115
		Archery:     130
	}
	Attack: {
		Damage: {
			Min: 12
			Max: 27
		}
	},
	Equipment: [
		{
			ItemType: "Bow"
			Lootable: true
		},
	]
}
