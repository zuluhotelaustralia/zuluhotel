package Dragonkin

ShadowDragon: {
	Name:               "a Shadow Dragon"
	CorpseNameOverride: "corpse of a Shadow Dragon"
	Str:                3000
	Int:                400
	Dex:                440
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        362
	Body:               12
	CanFly:             true
	CreatureType:       "Dragonkin"
	VirtualArmor:       20
	FightMode:          "Aggressor"
	HideType:           "Dragon"
	Hides:              5
	HitsMax:            3000
	Hue:                17969
	LootItemChance:     75
	LootItemLevel:      4
	LootTable:          "9"
	ManaMaxSeed:        200
	StamMaxSeed:        140
	Resistances: {
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        200
		MagicResist:  250
		Tactics:      100
		Wrestling:    140
		DetectHidden: 130
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 25
			Max: 75
		}
		HitSound: 364
		Ability: {
			SpellType: "DecayingRay"
		}
		AbilityChance: 1
		HasBreath:     true
	}
}
