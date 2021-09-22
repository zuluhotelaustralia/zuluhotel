package Human

InvisibleMan: {
	Name:               "an invisible man"
	CorpseNameOverride: "corpse of an invisible man"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                176
	Int:                126
	Dex:                60
	AlwaysMurderer:     true
	BaseSoundID:        569
	Body:               424
	CreatureType:       "Human"
	VirtualArmor:       20
	FightMode:          "Aggressor"
	HitsMax:            176
	LootItemChance:     1
	LootTable:          "47"
	ManaMaxSeed:        26
	StamMaxSeed:        50
	Skills: {
		Wrestling:   50
		Parry:       50
		Macing:      50
		Tactics:     50
		MagicResist: 75
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 30
		}
		HitSound: 570
	}
}
