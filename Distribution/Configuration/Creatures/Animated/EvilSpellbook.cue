package Animated

EvilSpellbook: {
	Name:               "evil spellbook"
	CorpseNameOverride: "corpse of evil spellbook"
	Str:                400
	Int:                900
	Dex:                600
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        608
	Body:               985
	CreatureType:       "Animated"
	VirtualArmor:       25
	FightMode:          "Closest"
	HitsMax:            400
	Hue:                1158
	LootItemChance:     50
	LootItemLevel:      5
	LootTable:          "35"
	ManaMaxSeed:        900
	StamMaxSeed:        100
	PreferredSpells: ["EnergyBolt", "Lightning", "Fireball", "Explosion", "MassCurse", "Earthquake", "MagicReflect", "Paralyze", "GreaterHeal"]
	Resistances: MagicImmunity: 6
	Skills: {
		Parry:       65
		MagicResist: 90
		Tactics:     85
		Wrestling:   175
		Magery:      150
		Healing:     99
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 8
			Max: 64
		}
		HitSound:  610
		MissSound: 611
	}
}
