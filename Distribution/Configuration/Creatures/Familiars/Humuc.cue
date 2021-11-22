package Familiars

Humuc: {
	Name:                 "totem"
	CorpseNameOverride:   "corpse of a totem"
	Str:                  200
	Int:                  75
	Dex:                  100
	PassiveSpeed:         0.5
	AiType:               "AI_Familiar"
	BaseSoundID:          422
	Body:                 39
	CanFly:               true
	CreatureType:         "Daemon"
	VirtualArmor:         100
	FightMode:            "Aggressor"
	HitsMaxSeed:          20
	Hue:                  746
	InitialInnocent:      true
	AlwaysAttackable:			false
	ManaMaxSeed:          75
	ProvokeSkillOverride: 150
	StamMaxSeed:          50
	Resistances: MagicImmunity: 8
	Skills: {
		Wrestling:   150
		Tactics:     50
		MagicResist: 160
		Magery:      75
	}
	Attack: {
		Speed: 100
		Damage: {
			Min: 2
			Max: 13
		}
		Skill:    "Macing"
		HitSound: 424
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}, {
		ItemType: "StrongBackpack"
	}]
}
