package Elemental

LesserShadow: {
	Name:               "a Lesser Shadow"
	CorpseNameOverride: "corpse of a Lesser Shadow"
	Str:                300
	Int:                500
	Dex:                200
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        640
	Body:               573
	CanSwim:            true
	CreatureType:       "Elemental"
	FightMode:          "Closest"
	HitsMaxSeed:        300
	Hue:                1
	LootItemChance:     15
	LootItemLevel:      3
	LootTable:          "77"
	ManaMaxSeed:        125
	StamMaxSeed:        80
	PreferredSpells: ["WyvernStrike", "AbyssalFlame", "EnergyBolt", "SorcerersBane", "WyvernStrike", "DecayingRay", "Darkness", "DispelField"]
	Resistances: {
		Poison:        6
		Necro:         100
		MagicImmunity: 4
	}
	Skills: {
		MagicResist:  105
		Tactics:      125
		Wrestling:    130
		EvalInt:      100
		DetectHidden: 130
	}
	Attack: {
		Speed: 37
		Damage: {
			Min: 13
			Max: 53
		}
		HitSound:  642
		MissSound: 641
		Ability: {
			SpellType: "Darkness"
		}
		AbilityChance: 1
	}
	Equipment: [{
		ItemType:    "BoneGloves"
		Name:        "Red Bone Gloves AR10"
		Hue:         1172
		ArmorRating: 10
	}, {
		ItemType:    "BoneHelm"
		Name:        "Red Bone Helm AR45"
		Hue:         1172
		ArmorRating: 45
	}]
}
