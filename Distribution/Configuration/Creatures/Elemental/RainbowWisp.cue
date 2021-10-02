package Elemental

RainbowWisp: {
	Name:                 "The Rainbow Wisp"
	CorpseNameOverride:   "corpse of The Rainbow Wisp"
	Str:                  1000
	Int:                  20000
	Dex:                  400
	AiType:               "AI_Mage"
	BaseSoundID:          466
	Body:                 58
	CanFly:               true
	CanSwim:              true
	ClassLevel:           4
	ClassType:            "Mage"
	CreatureType:         "Elemental"
	VirtualArmor:         100
	FightMode:            "Closest"
	FightRange:           12
	HitsMaxSeed:          3000
	Hue:                  1298
	InitialInnocent:      true
	LootItemChance:       60
	LootItemLevel:        6
	LootTable:            "9"
	ManaMaxSeed:          3000
	ProvokeSkillOverride: 160
	StamMaxSeed:          250
	PreferredSpells: ["CallLightning", "GustOfAir", "IceStrike", "ShiftingEarth"]
	Resistances: {
		Poison: 1
		Water:  100
		Air:    100
		Necro:  50
	}
	Skills: {
		Tactics:     150
		MagicResist: 100
		Magery:      200
		EvalInt:     150
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 36
			Max: 66
		}
		Skill:     "Swords"
		HitSound:  528
		MissSound: 529
		MaxRange:  12
	}
	Equipment: [{
		ItemType: "Bow"
	}]
}
