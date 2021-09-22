package Daemon

WingedAvenger: {
	Name:                 "a Winged Avenger"
	CorpseNameOverride:   "corpse of a Winged Avenger"

	Str:                  305
	Int:                  295
	Dex:                  350
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          402
	Body:                 30
	CreatureType:         "Daemon"
	VirtualArmor:         30
	FightMode:            "Closest"
	HitsMax:              305
	Hue:                  1109
	LootItemChance:       50
	LootItemLevel:        2
	LootTable:            "46"
	ManaMaxSeed:          200
	ProvokeSkillOverride: 75
	StamMaxSeed:          50
	PreferredSpells: ["EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Fireball", "Weaken", "MassCurse"]
	Skills: {
		Tactics:     140
		Wrestling:   200
		MagicResist: 90
		Magery:      85
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 43
		}
		HitSound: 404
	}
}
