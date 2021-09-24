package Orc

EliteOrcWarrior: {
	Name:                 "an elite orc warrior"
	CorpseNameOverride:   "corpse of an elite orc warrior"
  Str:                  215
	Int:                  35
	Dex:                  195
	AlwaysMurderer:       true
	BaseSoundID:          313
	Body:                 41
	CreatureType:         "Orc"
	VirtualArmor:         25
	FightMode:            "Aggressor"
	HitsMax:              215
	LootTable:            "43"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 70
	StamMaxSeed:          70
	Skills: {
		Wrestling:   100
		Tactics:     75
		MagicResist: 60
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 315
	}
}
