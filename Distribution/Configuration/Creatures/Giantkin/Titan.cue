package Giantkin

Titan: {
	Name:                 "a Titan"
	CorpseNameOverride:   "corpse of a Titan"

	Str:                  600
	Int:                  210
	Dex:                  180
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          609
	Body:                 76
	CreatureType:         "Giantkin"
	VirtualArmor:         40
	FightMode:            "Closest"
	HitsMax:              1000
	LootItemChance:       50
	LootItemLevel:        4
	LootTable:            "22"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 100
	StamMaxSeed:          300
	PreferredSpells: ["Poison", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Explosion", "Paralyze"]
	Resistances: {
		Earth:         75
		MagicImmunity: 3
	}
	Skills: {
		Parry:       50
		Magery:      120
		Tactics:     100
		Wrestling:   110
		MagicResist: 90
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound:  606
		MissSound: 360
	}
}
