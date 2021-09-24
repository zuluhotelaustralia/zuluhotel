package Daemon

DaemonOfDeath: {
	Name:                 "a Daemon Of Death"
	CorpseNameOverride:   "corpse of a Daemon Of Death"
  Str:                  2000
	Int:                  2000
	Dex:                  200
	PassiveSpeed:         0.2
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BardImmune:           true
	BaseSoundID:          604
	Body:                 38
	CanFly:               true
	CreatureType:         "Daemon"
	VirtualArmor:         30
	FightMode:            "Closest"
	FightRange:           5
	HitsMax:              6000
	Hue:                  1160
	LootItemChance:       90
	LootItemLevel:        9
	LootTable:            "150"
	ManaMaxSeed:          2000
	ProvokeSkillOverride: 160
	StamMaxSeed:          300
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "Plague", "SorcerersBane", "WyvernStrike", "Earthquake", "DispelField", "SpectresTouch", "WraithBreath"]
	Resistances: {
		Fire:   100
		Water:  75
		Air:    75
		Poison: 6
		Earth:  75
		Necro:  100
	}
	Skills: {
		Parry:        200
		MagicResist:  300
		Tactics:      200
		Wrestling:    200
		Macing:       150
		Magery:       300
		EvalInt:      200
		DetectHidden: 200
		Hiding:       200
	}
	Attack: {
		Speed: 65
		Damage: {
			Min: 57
			Max: 99
		}
		HitSound:  606
		MissSound: 360
		MaxRange:  5
	}
	Equipment: [{
		ItemType:    "HeaterShield"
		Name:        "Shield AR40"
		ArmorRating: 40
	}]
}
