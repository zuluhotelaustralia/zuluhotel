package Daemon

MindFlayer: {
	Name:               "a Mind Flayer"
	CorpseNameOverride: "corpse of a Mind Flayer"
	Str:                1200
	Int:                2750
	Dex:                310
	PassiveSpeed:       0.2
	AlwaysMurderer:     true
	BardImmune:         true
	BaseSoundID:        1001
	Body:               24
	ClassLevel:         6
	ClassType:          "Warrior"
	CreatureType:       "Daemon"
	VirtualArmor:       60
	FightMode:          "Aggressor"
	FightRange:         6
	HitsMax:            1200
	Hue:                2222
	LootItemChance:     90
	LootItemLevel:      6
	LootTable:          "35"
	ManaMaxSeed:        2750
	StamMaxSeed:        50
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Magery:       375
		MagicResist:  150
		Tactics:      150
		Wrestling:    150
		DetectHidden: 200
	}
	Attack: {
		Speed: 60
		Damage: {
			Min: 14
			Max: 50
		}
		HitSound: 531
		MaxRange: 6
		Ability: {
			SpellType: "ControlUndead"
		}
		AbilityChance: 1
	}
}
