package Terathan

TerathanAvenger: {
	Name:                 "a Terathan Avenger"
	CorpseNameOverride:   "corpse of a Terathan Avenger"
  Str:                  225
	Int:                  250
	Dex:                  160
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	BaseSoundID:          589
	Body:                 70
	CreatureType:         "Terathan"
	VirtualArmor:         30
	FightMode:            "Aggressor"
	HitsMax:              225
	LootItemChance:       10
	LootItemLevel:        2
	LootTable:            "68"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 90
	StamMaxSeed:          70
	PreferredSpells: ["EnergyBolt", "Lightning", "DispelField", "Explosion", "MassCurse"]
	Resistances: {
		Air:   75
		Earth: 75
	}
	Skills: {
		Magery:      80
		Wrestling:   95
		Tactics:     100
		MagicResist: 130
		Parry:       75
	}
	Attack: {
		Speed: 47
		Damage: {
			Min: 8
			Max: 44
		}
		HitSound:  588
		MissSound: 589
		HitPoison: "Regular"
	}
}
