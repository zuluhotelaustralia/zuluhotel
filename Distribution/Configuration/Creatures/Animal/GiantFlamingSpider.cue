package Animal

GiantFlamingSpider: {
	Name:                 "a giant flaming spider"
	CorpseNameOverride:   "corpse of a giant flaming spider"
  Str:                  250
	Int:                  90
	Dex:                  80
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	BaseSoundID:          904
	Body:                 28
	CreatureType:         "Animal"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              250
	Hue:                  232
	ManaMaxSeed:          40
	MinTameSkill:         90
	ProvokeSkillOverride: 90
	StamMaxSeed:          70
	Tamable:              true
	PreferredSpells: [
		"Fireball",
	]
	Skills: {
		Tactics:     70
		Wrestling:   85
		Magery:      100
		MagicResist: 80
	}
	Attack: {
		Damage: {
			Min: 15
			Max: 35
		}
		HitSound:  389
		HasBreath: true
		HasWebs:   true
		HitPoison: "Regular"
	}
}
