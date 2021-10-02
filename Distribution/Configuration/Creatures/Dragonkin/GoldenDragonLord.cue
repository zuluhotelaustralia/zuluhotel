package Dragonkin

GoldenDragonLord: {
	Name:                 "a Golden Dragon Lord"
	CorpseNameOverride:   "corpse of a Golden Dragon Lord"
	Str:                  2250
	Int:                  500
	Dex:                  450
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BardImmune:           true
	BaseSoundID:          362
	Body:                 12
	CreatureType:         "Dragonkin"
	VirtualArmor:         100
	FightMode:            "Closest"
	HitsMaxSeed:          5250
	Hue:                  1300
	LootItemChance:       100
	LootItemLevel:        9
	LootTable:            "150"
	ManaMaxSeed:          800
	ProvokeSkillOverride: 160
	StamMaxSeed:          500
	PreferredSpells: ["EnergyBolt", "Lightning", "MassCurse", "GreaterHeal", "Earthquake", "ManaVampire", "Paralyze"]
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           50
		Earth:         100
		Necro:         50
		MagicImmunity: 7
	}
	Skills: {
		Tactics:      250
		Wrestling:    150
		Magery:       150
		MagicResist:  300
		Parry:        300
		DetectHidden: 200
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 15
			Max: 150
		}
		HitSound:  362
		HasBreath: true
	}
}
