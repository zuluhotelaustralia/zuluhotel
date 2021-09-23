package Human

DarkElfArcher: {
	Name:                 "a dark elf archer"
	CorpseNameOverride:   "corpse of a dark elf archer"
	Str:                  165
	Int:                  95
	Dex:                  130
	AiType:               "AI_Archer"
	AlwaysMurderer:       true
	Body:                 401
	CreatureType:         "Human"
	VirtualArmor:         25
	FightMode:            "Closest"
	HitsMax:              165
	Hue:                  1109
	LootTable:            "41"
	ManaMaxSeed:          85
	ProvokeSkillOverride: 60
	StamMaxSeed:          95
	Resistances: MagicImmunity: 6
	Skills: {
		Wrestling:   95
		Tactics:     150
		MagicResist: 110
		Archery:     150
		Hiding:      100
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 22
		}
	}
	Equipment: [
		{
			ItemType: "Bow"
			Lootable: true
		},
	]
}
