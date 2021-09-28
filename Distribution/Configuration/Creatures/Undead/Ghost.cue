package Undead

Ghost: {
	Name:               "a ghost"
	CorpseNameOverride: "corpse of a ghost"
	Str:                126
	Int:                126
	Dex:                60
	AlwaysMurderer:     true
	BaseSoundID:        382
	Body:               970
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:            126
	LootItemChance:     1
	LootTable:          "47"
	ManaMaxSeed:        26
	StamMaxSeed:        50
	Resistances: Necro: 75
	Skills: {
		Wrestling:   50
		Parry:       50
		Macing:      50
		Tactics:     50
		MagicResist: 50
	}
	Attack: {
		Damage: {
			Min: 3
			Max: 30
		}
		HitSound: 570
	}
}
