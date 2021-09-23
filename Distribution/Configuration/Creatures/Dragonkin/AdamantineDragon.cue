package Dragonkin

AdamantineDragon: {
	Name:                 "an Adamantine Dragon"
	CorpseNameOverride:   "corpse of an Adamantine Dragon"
  Str:                  600
	Int:                  600
	Dex:                  450
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          362
	Body:                 46
	CreatureType:         "Dragonkin"
	VirtualArmor:         100
	FightMode:            "Aggressor"
	HitsMax:              2000
	Hue:                  1006
	LootItemChance:       80
	LootItemLevel:        5
	LootTable:            "37"
	ManaMaxSeed:          2000
	MinTameSkill:         140
	ProvokeSkillOverride: 200
	StamMaxSeed:          140
	Tamable:              true
	PreferredSpells: ["Fireball", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Weaken", "MassCurse"]
	Resistances: {
		Fire:          50
		Water:         100
		Physical:      100
		Earth:         100
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		Parry:        80
		MagicResist:  140
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
		HitSound:      572
		Ability:       "BlackrockStrike"
		AbilityChance: 1
		HasBreath:     true
	}
}
