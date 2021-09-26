package Animal

GiantRockSpider: {
	Name:                 "a giant rock spider"
	CorpseNameOverride:   "corpse of a giant rock spider"
  Str:                  185
	Int:                  50
	Dex:                  110
	AlwaysMurderer:       true
	BaseSoundID:          904
	Body:                 28
	CreatureType:         "Animal"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              185
	Hue:                  1118
	ManaMaxSeed:          40
	MinTameSkill:         80
	ProvokeSkillOverride: 90
	StamMaxSeed:          70
	Tamable:              true
	Resistances: Poison: 3
	Skills: {
		Tactics:     100
		Wrestling:   110
		MagicResist: 40
	}
	Attack: {
		Speed: 38
		Damage: {
			Min: 9
			Max: 25
		}
		HitSound: 389
		HasWebs:  true
		HitPoison: "Regular"
	}
}
