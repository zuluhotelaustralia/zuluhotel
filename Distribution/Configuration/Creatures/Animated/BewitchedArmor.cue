package Animated

BewitchedArmor: {
	Name:                 "Bewitched Armor"
	CorpseNameOverride:   "corpse of Bewitched Armor"

	Str:                  210
	Int:                  110
	Dex:                  110
	AlwaysMurderer:       true
	BaseSoundID:          380
	Body:                 527
	CreatureType:         "Animated"
	FightMode:            "Aggressor"
	HitsMax:              210
	LootItemChance:       1
	LootTable:            "48"
	ManaMaxSeed:          0
	ProvokeSkillOverride: 94
	StamMaxSeed:          100
	Skills: {
		Tactics:     100
		Wrestling:   140
		MagicResist: 70
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 6
			Max: 36
		}
		HitSound:  382
		MissSound: 385
	}
}
