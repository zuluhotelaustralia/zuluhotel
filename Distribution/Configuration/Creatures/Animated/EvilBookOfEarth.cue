package Animated

EvilBookOfEarth: {
	Name:               "an Evil Book Of The Earth"
	CorpseNameOverride: "corpse of an Evil Book Of The Earth"
	Str:                600
	Int:                910
	Dex:                600
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        608
	Body:               985
	CreatureType:       "Animated"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMaxSeed:            850
	Hue:                1162
	LootItemChance:     60
	LootItemLevel:      5
	LootTable:          "140"
	ManaMaxSeed:        1600
	StamMaxSeed:        200
	PreferredSpells: ["ShiftingEarth", "CallLightning", "GustOfAir", "RisingFire", "IceStrike", "MassCurse", "Earthquake"]
	Resistances: {
		Poison:        6
		Air:           100
		Water:         100
		Fire:          75
		Physical:      75
		Necro:         100
		Earth:         75
		MagicImmunity: 8
	}
	Skills: {
		Parry:       100
		MagicResist: 120
		Tactics:     100
		Magery:      180
		Healing:     100
	}
	Attack: {
		Damage: {
			Min: 25
			Max: 70
		}
		Skill:     "Swords"
		HitSound:  610
		MissSound: 611
	}
}
