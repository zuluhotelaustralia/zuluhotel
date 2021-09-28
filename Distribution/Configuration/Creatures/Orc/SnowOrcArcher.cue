package Orc

SnowOrcArcher: {
	Name:                 "<random> the snow orc archer"
	CorpseNameOverride:   "corpse of <random> the snow orc archer"
  Str:                  125
	Int:                  35
	Dex:                  250
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	BaseSoundID:          1114
	Body:                 17
	CreatureType:         "Orc"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMaxSeed:              125
	Hue:                  1154
	LootTable:            "72"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 95
	StamMaxSeed:          50
	Skills: {
		MagicResist: 60
		Tactics:     90
		Wrestling:   85
		Archery:     90
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
