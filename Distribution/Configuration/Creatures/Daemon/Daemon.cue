package Daemon

Daemon: {
	Name:               "<random> the Daemon"
	CorpseNameOverride: "corpse of <random> the Daemon"
	Str:                450
	Int:                350
	Dex:                80
	AiType:             "AI_Mage"
	AlwaysMurderer:     true
	BaseSoundID:        357
	Body:               9
	CanFly:             true
	CreatureType:       "Daemon"
	VirtualArmor:       30
	FightMode:          "Closest"
	HitsMaxSeed:            450
	Hue:                33784
	LootItemChance:     40
	LootItemLevel:      5
	LootTable:          "22"
	ManaMaxSeed:        200
	StamMaxSeed:        70
	PreferredSpells: ["FlameStrike", "EnergyBolt", "Lightning", "Harm", "MindBlast", "MagicArrow", "Fireball", "Explosion", "MassCurse"]
	Resistances: {
		Earth:         75
		MagicImmunity: 3
	}
	Skills: {
		Parry:       65
		MagicResist: 90
		Tactics:     100
		Wrestling:   120
		Magery:      100
		EvalInt:     100
	}
	Attack: {
		Speed: 45
		Damage: {
			Min: 10
			Max: 40
		}
	}
}
