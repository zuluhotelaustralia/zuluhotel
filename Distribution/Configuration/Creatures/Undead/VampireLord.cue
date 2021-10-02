package Undead

VampireLord: {
	Name:                 "a Vampire Lord"
	CorpseNameOverride:   "corpse of a Vampire Lord"
	Str:                  1500
	Int:                  1500
	Dex:                  500
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          599
	Body:                 401
	CreatureType:         "Undead"
	VirtualArmor:         40
	FightMode:            "Closest"
	HitsMaxSeed:          5000
	LootItemChance:       100
	LootItemLevel:        7
	LootTable:            "9"
	ManaMaxSeed:          350
	ProvokeSkillOverride: 160
	StamMaxSeed:          350
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		Water:         50
		MagicImmunity: 4
	}
	Skills: {
		Tactics:     800
		Wrestling:   150
		MagicResist: 250
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 33
			Max: 73
		}
		HitSound: 601
	}
	Equipment: [
		{
			ItemType: "ShortHair"
			Hue:      1
		},
		{
			ItemType: "ShortPants"
			Hue:      0x66d
			Lootable: true
		},
		{
			ItemType: "Shirt"
			Hue:      0x66d
			Lootable: true
		},
		{
			ItemType: "Cloak"
			Hue:      0x1
			Lootable: true
		},
	]
}
