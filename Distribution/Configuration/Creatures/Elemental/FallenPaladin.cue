package Elemental

FallenPaladin: {
	Name:                 "a Fallen Paladin"
	CorpseNameOverride:   "corpse of a Fallen Paladin"
	BaseType:             "Server.Mobiles.BaseCreatureTemplate"
	Str:                  2500
	Int:                  250
	Dex:                  1000
	AlwaysMurderer:       true
	CreatureType:         "Elemental"
	FightMode:            "Aggressor"
	HitsMax:              6000
	Hue:                  33784
	ManaMaxSeed:          0
	MinTameSkill:         170
	ProvokeSkillOverride: 170
	RiseCreatureDelay:    "00:00:00"
	StamMaxSeed:          3000
	Tamable:              true
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		Water:         100
		Necro:         100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Tactics:      350
		Fencing:      150
		MagicResist:  250
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Speed: 50
		Damage: {
			Min: 55
			Max: 80
		}
		Animation: "Bash2H"
		HitSound:  567
		MissSound: 562
	}
	Equipment: [{
		ItemType:    "Server.Items.PlateChest"
		Name:        "Platemail of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType:    "Server.Items.PlateGloves"
		Name:        "Platemail Gloves of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType:    "Server.Items.PlateGorget"
		Name:        "Platemail Gorget of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType:    "Server.Items.PlateLegs"
		Name:        "Platemail Legs of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType:    "Server.Items.PlateArms"
		Name:        "Platemail Arms of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType:    "Server.Items.PlateHelm"
		Name:        "Platemail Helm of the Paladin"
		Hue:         1176
		ArmorRating: 70
	}, {
		ItemType: "Server.Items.Halberd"
		Name:     "Paladin's Halberd of Destruction"
		Hue:      1157
	}]
}