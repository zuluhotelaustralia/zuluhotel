package Animal

DireWeaver: {
	Name:               "a Dire Weaver"
	CorpseNameOverride: "corpse of a Dire Weaver"
	Str:                450
	Int:                35
	Dex:                300
	AlwaysMurderer:     true
	BaseSoundID:        904
	Body:               28
	CreatureType:       "Animal"
	VirtualArmor:       40
	FightMode:          "Closest"
	HitsMaxSeed:            450
	Hue:                1409
	ManaMaxSeed:        25
	StamMaxSeed:        50
	Resistances: {
		Poison: 3	
	}
	Skills: {
		Parry:       85
		Poisoning:   70
		MagicResist: 80
		Tactics:     120
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 25
			Max: 75
		}
		Skill:     "Swords"
		HitSound:  389
		MissSound: 390
		HitPoison: "Greater"
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR20"
		ArmorRating: 20
	}]
}
