package Beholder

FrigidEye: {
	Name:                 "a frigid eye"
	CorpseNameOverride:   "corpse of a frigid eye"

	Str:                  250
	Int:                  450
	Dex:                  100
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          377
	Body:                 22
	CreatureType:         "Beholder"
	VirtualArmor:         20
	FightMode:            "Closest"
	HitsMax:              250
	Hue:                  1154
	LootItemChance:       2
	LootTable:            "45"
	ManaMaxSeed:          120
	ProvokeSkillOverride: 95
	StamMaxSeed:          65
	PreferredSpells: ["Explosion", "MassCurse", "Weaken", "Paralyze", "Fireball", "Lightning"]
	Skills: {
		MagicResist: 80
		Tactics:     65
		Wrestling:   100
		Magery:      95
	}
	Attack: {
		Speed: 33
		Damage: {
			Min: 5
			Max: 35
		}
		HitSound: 379
	}
}
