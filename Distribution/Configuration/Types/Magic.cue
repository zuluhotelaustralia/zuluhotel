package Types

#SpellCircle: {
	Id:              int32
	Name?:            string
	Mana:            int32
	Difficulty:      int32
	PointValue:      int32
	Delay:           int32
	EffectiveCircle?: int32
}

#SpellEntryEnum: {
	None:           "None"
	Clumsy:         "Clumsy"
	CreateFood:     "CreateFood"
	Feeblemind:     "Feeblemind"
	Heal:           "Heal"
	MagicArrow:     "MagicArrow"
	NightSight:     "NightSight"
	ReactiveArmor:  "ReactiveArmor"
	Weaken:         "Weaken"
	Agility:        "Agility"
	Cunning:        "Cunning"
	Cure:           "Cure"
	Harm:           "Harm"
	MagicTrap:      "MagicTrap"
	RemoveTrap:     "RemoveTrap"
	Protection:     "Protection"
	Strength:       "Strength"
	Bless:          "Bless"
	Fireball:       "Fireball"
	MagicLock:      "MagicLock"
	Poison:         "Poison"
	Telekinesis:    "Telekinesis"
	Teleport:       "Teleport"
	Unlock:         "Unlock"
	WallOfStone:    "WallOfStone"
	ArchCure:       "ArchCure"
	ArchProtection: "ArchProtection"
	Curse:          "Curse"
	FireField:      "FireField"
	GreaterHeal:    "GreaterHeal"
	Lightning:      "Lightning"
	ManaDrain:      "ManaDrain"
	Recall:         "Recall"
	BladeSpirits:   "BladeSpirits"
	DispelField:    "DispelField"
	Incognito:      "Incognito"
	MagicReflect:   "MagicReflect"
	MindBlast:      "MindBlast"
	Paralyze:       "Paralyze"
	PoisonField:    "PoisonField"
	SummonCreature: "SummonCreature"
	Dispel:         "Dispel"
	EnergyBolt:     "EnergyBolt"
	Explosion:      "Explosion"
	Invisibility:   "Invisibility"
	Mark:           "Mark"
	MassCurse:      "MassCurse"
	ParalyzeField:  "ParalyzeField"
	Reveal:         "Reveal"
	ChainLightning: "ChainLightning"
	EnergyField:    "EnergyField"
	FlameStrike:    "FlameStrike"
	GateTravel:     "GateTravel"
	ManaVampire:    "ManaVampire"
	MassDispel:     "MassDispel"
	MeteorSwarm:    "MeteorSwarm"
	Polymorph:      "Polymorph"
	Earthquake:     "Earthquake"
	EnergyVortex:   "EnergyVortex"
	Resurrection:   "Resurrection"
	AirElemental:   "AirElemental"
	SummonDaemon:   "SummonDaemon"
	EarthElemental: "EarthElemental"
	FireElemental:  "FireElemental"
	WaterElemental: "WaterElemental"
	ControlUndead:  "ControlUndead"
	Darkness:       "Darkness"
	DecayingRay:    "DecayingRay"
	SpectresTouch:  "SpectresTouch"
	AbyssalFlame:   "AbyssalFlame"
	AnimateDead:    "AnimateDead"
	Sacrifice:      "Sacrifice"
	WraithBreath:   "WraithBreath"
	SorcerersBane:  "SorcerersBane"
	SummonSpirit:   "SummonSpirit"
	WraithForm:     "WraithForm"
	WyvernStrike:   "WyvernStrike"
	Kill:           "Kill"
	LicheForm:      "LicheForm"
	Plague:         "Plague"
	Spellbind:      "Spellbind"
	EtherealMount:  "EtherealMount"
	SpiritSpeak:    "SpiritSpeak"
	Antidote:       "Antidote"
	OwlSight:       "OwlSight"
	ShiftingEarth:  "ShiftingEarth"
	SummonMammals:  "SummonMammals"
	CallLightning:  "CallLightning"
	EarthsBlessing: "EarthsBlessing"
	EarthPortal:    "EarthPortal"
	NaturesTouch:   "NaturesTouch"
	GustOfAir:      "GustOfAir"
	RisingFire:     "RisingFire"
	Shapeshift:     "Shapeshift"
	IceStrike:      "IceStrike"
	EarthSpirit:    "EarthSpirit"
	FireSpirit:     "FireSpirit"
	StormSpirit:    "StormSpirit"
	WaterSpirit:    "WaterSpirit"
}
#SpellEntry: or(
		[
			for _, v in #SpellEntryEnum {
			v
		},
	],
	)

#ReagentCost: {
	Reagent: #Type
	Amount:  int32 & >0
}

#SpellInfo: {
	Type:             #Type
	Name:             string
	Mantra:           string
	Circle:           string | int
	DamageType:       #ElementalType
	RightHandEffect:  int32
	LeftHandEffect:   int32
	Action:           int32
	ShowHandMovement: bool
	RevealOnCast:     bool
	Resistable:       bool
	Reflectable:      bool
	ClearHandsOnCast: bool
	BlocksMovement:   bool
	AllowTown:        bool
	AllowDead:        bool
	TargetOptions?:    #TargetOptions
	ReagentCosts: [...#ReagentCost]
	Amounts:  [...int]
	Reagents: [...#Type]
}
