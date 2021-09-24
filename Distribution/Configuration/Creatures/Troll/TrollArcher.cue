package Troll

TrollArcher: {
	Name:                 "a troll archer"
	CorpseNameOverride:   "corpse of a troll archer"
  Str:                  190
	Int:                  50
	Dex:                  105
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          461
	Body:                 54
	CreatureType:         "Troll"
	VirtualArmor:         20
	FightMode:            "Closest"
	HideType:             "Troll"
	Hides:                4
	HitsMax:              190
	Hue:                  33784
	LootTable:            "41"
	ManaMaxSeed:          40
	ProvokeSkillOverride: 80
	StamMaxSeed:          60
	Skills: {
		MagicResist: 60
		Tactics:     95
		Wrestling:   100
		Archery:     95
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	},
	Equipment: [
		{
			ItemType: "Bow"
			Lootable: true
		},
	]
}
