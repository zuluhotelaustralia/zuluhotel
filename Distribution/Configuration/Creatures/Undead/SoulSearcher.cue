package Undead

SoulSearcher: {
	Name:               "a Soul Searcher"
	CorpseNameOverride: "corpse of a Soul Searcher"
	Str:                1200
	Int:                2750
	Dex:                310
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        362
	Body:               970
	CreatureType:       "Undead"
	VirtualArmor:       45
	FightMode:          "Closest"
	HitsMaxSeed:            1200
	Hue:                1170
	LootItemChance:     100
	LootItemLevel:      7
	LootTable:          "9"
	ManaMaxSeed:        2750
	StamMaxSeed:        50
	PreferredSpells: ["EnergyBolt", "Explosion", "WraithBreath", "Plague", "Darkness", "WyvernStrike", "AbyssalFlame"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 8
	}
	Skills: {
		Magery:       195
		MagicResist:  150
		Tactics:      150
		Wrestling:    160
		DetectHidden: 200
	}
	Attack: {
		Damage: {
			Min: 10
			Max: 60
		}
		HitSound: 364
	}
}
