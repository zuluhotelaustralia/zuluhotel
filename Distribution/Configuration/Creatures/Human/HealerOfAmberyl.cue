package Human

HealerOfAmberyl: {
	Name:               "<random>, Healer of Amberyl"
	CorpseNameOverride: "corpse of <random>, Healer of Amberyl"
	Str:                300
	Int:                300
	Dex:                200
	AiType:             "AI_Healer"
	Body:               401
	CreatureType:       "Human"
	VirtualArmor:       100
	Female:             true
	HitsMaxSeed:        300
	Hue:                33784
	InitialInnocent:    true
	AlwaysAttackable:		false
	ManaMaxSeed:        200
	StamMaxSeed:        100
	Skills: Magery: 200
	Attack: {
		Damage: {
			Min: 0
			Max: 0
		}
	}
	Equipment: [
		{
			ItemType: "LongHair"
			Hue:      36
		},
		{
			ItemType: "GnarledStaff"
			Lootable: true
		},
		{
			ItemType: "Robe"
			Lootable: true
			Hue:      52
		},
	]
}
