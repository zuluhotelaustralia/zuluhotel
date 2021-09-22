package Daemon

UnholyMenace: {
	Name:                 "an Unholy Menace"
	CorpseNameOverride:   "corpse of an Unholy Menace"

	Str:                  1250
	Int:                  500
	Dex:                  450
	PassiveSpeed:         0.2
	AlwaysMurderer:       true
	AutoDispel:           true
	BardImmune:           true
	BaseSoundID:          1165
	Body:                 309
	CreatureType:         "Daemon"
	VirtualArmor:         75
	FightMode:            "Aggressor"
	HitsMax:              1000
	Hue:                  1305
	LootItemChance:       80
	LootItemLevel:        6
	LootTable:            "9"
	ManaMaxSeed:          500
	ProvokeSkillOverride: 160
	StamMaxSeed:          500
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		Water:         50
		MagicImmunity: 7
		Earth:         75
		Necro:         100
	}
	Skills: {
		Tactics:      150
		Wrestling:    150
		Magery:       150
		MagicResist:  100
		Parry:        100
		DetectHidden: 200
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 15
			Max: 150
		}
		HitSound:  572
		HasBreath: true
	}
	Equipment: [{
		ItemType:    "SHeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
