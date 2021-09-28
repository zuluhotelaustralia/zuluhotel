package Undead

FlamingSkeleton: {
	Name:               "a flaming skeleton"
	CorpseNameOverride: "corpse of a flaming skeleton"
	Str:                170
	Int:                180
	Dex:                180
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        451
	Body:               57
	CreatureType:       "Undead"
	VirtualArmor:       20
	FightMode:          "Closest"
	HitsMaxSeed:        170
	Hue:                1201
	LootItemChance:     1
	LootTable:          "23"
	ManaMaxSeed:        80
	StamMaxSeed:        50
	PreferredSpells: [
		"Fireball",
		"Explosion",
	]
	Resistances: {
		Poison: 6
		Necro:  75
	}
	Skills: {
		MagicResist: 80
		Tactics:     50
		Wrestling:   120
		Magery:      75
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 5
			Max: 40
		}
		HitSound: 454
	}
}
