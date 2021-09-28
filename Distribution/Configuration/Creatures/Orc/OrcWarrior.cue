package Orc

OrcWarrior: {
	Name:                 "<random> the orc warrior"
	CorpseNameOverride:   "corpse of <random> the orc warrior"
  Str:                  160
	Int:                  35
	Dex:                  190
	AlwaysMurderer:       true
	BaseSoundID:          313
	Body:                 41
	CreatureType:         "Orc"
	VirtualArmor:         20
	FightMode:            "Closest"
	HitsMaxSeed:              160
	LootTable:            "43"
	ManaMaxSeed:          25
	ProvokeSkillOverride: 70
	StamMaxSeed:          70
	Skills: {
		Wrestling:   95
		Tactics:     90
		MagicResist: 60
	}
	Attack: {
		Damage: {
			Min: 8
			Max: 29
		}
		HitSound: 315
	}
}
