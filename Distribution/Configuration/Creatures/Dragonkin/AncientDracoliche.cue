package Dragonkin

AncientDracoliche: {
	Name:                 "an Ancient Dracoliche"
	CorpseNameOverride:   "corpse of an Ancient Dracoliche"
  Str:                  3000
	Int:                  1500
	Dex:                  175
	AiType:               "AI_Mage"
	AlwaysMurderer:       true
	BaseSoundID:          1160
	Body:                 104
	ClassLevel:           5
	ClassType:            "Mage"
	CreatureType:         "Dragonkin"
	FightMode:            "Closest"
	HitsMaxSeed:              2000
	Hue:                  1282
	LootItemChance:       70
	LootItemLevel:        7
	LootTable:            "9"
	ManaMaxSeed:          1500
	MinTameSkill:         170
	ProvokeSkillOverride: 170
	SaySpellMantra:       false
	StamMaxSeed:          175
	Tamable:              true
	PreferredSpells: ["MindBlast", "WyvernStrike", "AbyssalFlame", "IceStrike", "Plague", "SorcerersBane", "Earthquake", "MassDispel", "Darkness"]
	Resistances: {
		Poison:        6
		Water:         50
		Fire:          100
		Physical:      100
		Earth:         100
		MagicImmunity: 8
	}
	Skills: {
		Parry:        200
		Tactics:      200
		Wrestling:    150
		MagicResist:  200
		Magery:       200
		DetectHidden: 200
		EvalInt:      200
	}
	Attack: {
		Damage: {
			Min: 0
			Max: 0
		}
	}
}
