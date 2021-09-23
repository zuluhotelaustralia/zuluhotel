package Misc

Lolth: {
	Name:                 "Lolth"
	CorpseNameOverride:   "corpse of Lolth"
  Str:                  1000
	Int:                  20000
	Dex:                  400
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          599
	Body:                 72
	ClassLevel:           4
	ClassType:            "Mage"
	VirtualArmor:         100
	Female:               true
	FightMode:            "Closest"
	FightRange:           12
	HitsMax:              3000
	Hue:                  1109
	LootItemChance:       60
	LootItemLevel:        6
	LootTable:            "9"
	ManaMaxSeed:          3000
	ProvokeSkillOverride: 160
	StamMaxSeed:          250
	PreferredSpells: ["SorcerersBane", "WyvernStrike", "DecayingRay"]
	Resistances: {
		Air:    50
		Fire:   100
		Poison: 1
		Necro:  100
	}
	Skills: {
		Parry:       150
		Tactics:     150
		Magery:      200
		MagicResist: 100
	}
	Attack: {
		Speed: 20
		Damage: {
			Min: 36
			Max: 66
		}
		Skill:     "Swords"
		HitSound:  458
		MissSound: 457
		MaxRange:  12
		HitPoison: "Greater"
	}
	Equipment: [{
		ItemType: "Bow"
	}]
}
