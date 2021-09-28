package Undead

Vampire: {
	Name:               "Vampire"
	CorpseNameOverride: "corpse of Vampire"
	Str:                350
	Int:                350
	Dex:                350
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	CreatureType:       "Undead"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:        350
	LootItemChance:     75
	LootItemLevel:      3
	LootTable:          "71"
	ManaMaxSeed:        350
	StamMaxSeed:        350
	PreferredSpells: ["Poison", "Lightning", "Paralyze", "ManaDrain", "Explosion"]
	Resistances: {
		Poison:        6
		MagicImmunity: 3
	}
	Skills: {
		MagicResist: 100
		Tactics:     100
		Wrestling:   110
		Magery:      100
	}
	Attack: {
		Ability:       "LifeDrainStrike"
		AbilityChance: 1.0
		Damage: {
			Min: 12
			Max: 40
		}
		HitSound: 363
	}
	Equipment: [
		{
			ItemType: "ShortHair"
			Hue:      1
		},
		{
			ItemType: "Cloak"
			Hue:      0x66d
			Lootable: true
		},
		{
			ItemType: "Robe"
			Hue:      0x66d
			Lootable: true
		},
	]
}
