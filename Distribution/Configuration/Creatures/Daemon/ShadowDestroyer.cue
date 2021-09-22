package Daemon

ShadowDestroyer: {
	Name:                 "Shadow Destroyer"
	CorpseNameOverride:   "corpse of Shadow Destroyer"

	Str:                  1550
	Int:                  500
	Dex:                  450
	ActiveSpeed:          0.15
	PassiveSpeed:         0.3
	AlwaysMurderer:       true
	AutoDispel:           true
	BardImmune:           true
	BaseSoundID:          570
	Body:                 311
	CreatureType:         "Daemon"
	VirtualArmor:         75
	FightMode:            "Aggressor"
	HitsMax:              1200
	Hue:                  1172
	LootItemChance:       90
	LootItemLevel:        7
	LootTable:            "9"
	ManaMaxSeed:          500
	ProvokeSkillOverride: 160
	StamMaxSeed:          500
	Resistances: {
		Poison:        6
		Fire:          100
		Air:           100
		MagicImmunity: 7
		Necro:         100
		Earth:         100
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
		HitSound: 572
	}
	Equipment: [{
		ItemType:    "SHeaterShield"
		Name:        "Shield AR50"
		ArmorRating: 50
	}]
}
