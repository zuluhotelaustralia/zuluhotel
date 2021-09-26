package Animal

GiantFrostSpider: {
	Name:                 "a giant frost spider"
	CorpseNameOverride:   "corpse of a giant frost spider"
  Str:                  90
	Int:                  60
	Dex:                  125
	AlwaysMurderer:       true
	BaseSoundID:          904
	Body:                 28
	CreatureType:         "Animal"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              90
	Hue:                  1154
	ManaMaxSeed:          0
	MinTameSkill:         80
	ProvokeSkillOverride: 100
	StamMaxSeed:          70
	Tamable:              true
	Resistances: Poison: 3
	Skills: {
		Tactics:     70
		Wrestling:   100
		MagicResist: 90
	}
	Attack: {
		Speed: 38
		Damage: {
			Min: 9
			Max: 21
		}
		HitSound: 389
		HasWebs:  true
		HitPoison: "Regular"
	}
}
