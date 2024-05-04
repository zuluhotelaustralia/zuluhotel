package Human

SparringPartner: {
	Name:               "<random> the Sparring Partner"
	CorpseNameOverride: "corpse of <random> the Sparring Partner"
	BaseType:           "BaseSparringPartner"
	Str:                25
	Int:                10
	Dex:                10
	AlwaysMurderer:     false
	CreatureType:       "Human"
	VirtualArmor:       9999
	FightMode:          "Aggressor"
	HitsMaxSeed:        9999
	LootTable:          "59"
	ManaMaxSeed:        10
	StamMaxSeed:        10
	Resistances: {
			Poison:        6
			Water:         150
			Fire:          150
			Physical:      150
			Earth:         150
			Air:           150
			Necro:         150
			MagicImmunity: 8
	}
	Skills: {
		Parry:       130
		MagicResist: 130
	}
	Attack: {
		Damage: {
			Min: 1
			Max: 2
		}
	}
	Equipment: [
		{
			ItemType: "WoodenShield"
			Lootable: false
		},
		{
			ItemType: "ChainLegs"
			Lootable: false
		},
		{
			ItemType: "ChainChest"
			Lootable: false
		},
		{
			ItemType: "ChainCoif"
			Lootable: false
		},
	]
}
