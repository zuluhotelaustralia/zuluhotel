package Elemental

MageHunter: {
	Name:               "a Mage Hunter"
	CorpseNameOverride: "corpse of a Mage Hunter"
	Str:                300
	Int:                25
	Dex:                150
	AlwaysMurderer:     true
	BaseSoundID:        570
	Body:               574
	CreatureType:       "Elemental"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:            300
	Hue:                1170
	ManaMaxSeed:        0
	StamMaxSeed:        50
	Resistances: {
		Poison:        6
		MagicImmunity: 8
	}
	Skills: {
		Poisoning:    100
		Tactics:      130
		Wrestling:    150
		MagicResist:  160
		DetectHidden: 130
	}
	Attack: {
		Speed: 32
		Damage: {
			Min: 1
			Max: 10
		}
		HitSound:      572
		Ability:       "BlackrockStrike"
		AbilityChance: 1
	}
}
