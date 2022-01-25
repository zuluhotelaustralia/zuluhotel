package Elemental

Juggernaught: {
	Name:               "a Juggernaught"
	CorpseNameOverride: "corpse of a Juggernaught"
	Str:                1000
	Int:                1000
	Dex:                10
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        268
	Body:               14
	CreatureType:       "Elemental"
	VirtualArmor:       100
	FightMode:          "Closest"
	FightRange:         6
	HitsMaxSeed:        2000
	LootItemChance:     50
	LootItemLevel:      6
	LootTable:          "9"
	ManaMaxSeed:        1000
	StamMaxSeed:        200
	PreferredSpells: ["WallOfStone", "BladeSpirits", "DispelField"]
	Resistances: {
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 4
	}
	Skills: {
		Tactics:      250
		Fencing:      200
		MagicResist:  200
		Magery:       200
		DetectHidden: 200
		Meditation:   200
	}
	Attack: {
		Speed: 99
		Damage: {
			Min: 7
			Max: 70
		}
		HitSound:  756
		MissSound: 536
		MaxRange:  2
		Ability: {
			SpellType: "Lightning"
		}
		AbilityChance: 1
	}
	Equipment: [{
		ItemType: "Spear"
		Name:     "Juggernaught Weapon"
	}, {
		ItemType:    "HeaterShield"
		Name:        "Shield AR40"
		ArmorRating: 40
	}]
}
