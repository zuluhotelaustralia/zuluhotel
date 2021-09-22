package Daemon

DaemonKnight: {
	Name:               "<random> the Daemon Knight"
	CorpseNameOverride: "corpse of <random> the Daemon Knight"
	BaseType:           "Server.Mobiles.BaseCreatureTemplate"
	Str:                450
	Int:                300
	Dex:                80
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        357
	Body:               10
	CanFly:             true
	CreatureType:       "Daemon"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMax:            450
	Hue:                33784
	LootItemChance:     60
	LootItemLevel:      5
	LootTable:          "22"
	ManaMaxSeed:        200
	StamMaxSeed:        70
	PreferredSpells: ["FlameStrike", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Fireball", "Explosion", "MassCurse"]
	Resistances: {
		Earth:         100
		MagicImmunity: 3
	}
	Skills: {
		Parry:       65
		MagicResist: 90
		Tactics:     100
		Wrestling:   120
		Magery:      100
		EvalInt:     80
	}
	Attack: {
		Speed: 35
		Damage: {
			Min: 21
			Max: 45
		}
	}
}
