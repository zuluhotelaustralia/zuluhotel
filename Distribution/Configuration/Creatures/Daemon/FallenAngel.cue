package Daemon

FallenAngel: {
	Name:                 "a fallen angel"
	CorpseNameOverride:   "corpse of a fallen angel"
  Str:                  600
	Int:                  285
	Dex:                  285
	AlwaysMurderer:       true
	BaseSoundID:          372
	Body:                 4
	CanSwim:              true
	CreatureType:         "Daemon"
	VirtualArmor:         40
	FightMode:            "Aggressor"
	HitsMax:              600
	Hue:                  1174
	LootItemChance:       25
	LootItemLevel:        2
	LootTable:            "35"
	ManaMaxSeed:          85
	ProvokeSkillOverride: 150
	StamMaxSeed:          75
	Resistances: {
		Poison:        6
		Earth:         50
		Necro:         100
		MagicImmunity: 6
	}
	Skills: {
		MagicResist:  150
		Tactics:      100
		Wrestling:    150
		DetectHidden: 130
	}
	Attack: {
		Speed: 40
		Damage: {
			Min: 21
			Max: 51
		}
		HitSound:      642
		MissSound:     641
		Ability:       "TriElementalStrike"
		AbilityChance: 1
	}
}
