package Human

TaintedRanger: {
	Name:               "<random> the Tainted Ranger"
	CorpseNameOverride: "corpse of <random> the Tainted Ranger"
	Str:                300
	Int:                60
	Dex:                150
	AiType:             "AI_Archer"
	AlwaysMurderer:     true
	BaseSoundID:        250
	Body:               435
	ClassLevel:         1
	ClassType:          "Ranger"
	CreatureType:       "Human"
	VirtualArmor:       40
	FightMode:          "Closest"
	FightRange:         7
	HitsMax:            300
	Hue:                1157
	LootItemChance:     50
	LootItemLevel:      4
	LootTable:          "131"
	ManaMaxSeed:        0
	RiseCreatureDelay:  "00:00:02"
	StamMaxSeed:        150
	Skills: {
		Tactics:      100
		Archery:      130
		MagicResist:  90
		Hiding:       120
		DetectHidden: 100
	}
	Attack: {
		Damage: {
			Min: 17
			Max: 57
		}
		HitSound: 252
		ProjectileEffectId: 0x37C4
	}
	Equipment: [
		{
			ItemType: "Bow"
			Name:     "Tainted Ranger Weapon"
			Hue:      1171
		},
		{
			ItemType:    "LeatherGloves"
			Name:        "a pair of dark leather gloves"
			Hue:         1302
			ArmorRating: 1
		},
		{
			ItemType: "Boots"
			Name:     "a pair of black leather boots"
			Hue:      1
		},
		{
			ItemType: "LongPants"
			Name:     "a pair of black leather pants"
			Hue:      1157
		},
		{
			ItemType:    "LeatherChest"
			Name:        "a Tunic of Woven Shadows"
			Hue:         1302
			ArmorRating: 1
		},
		{
			ItemType:    "BoneArms"
			Name:        "a Bracer of Woven Shadows"
			Hue:         1302
			ArmorRating: 1
		},
	]
}
