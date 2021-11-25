package Magic

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Spells: {
	[Types.#SpellEntry]: Types.#SpellInfo
	Clumsy: {
		Type:             "ClumsySpell"
		Name:             "Clumsy"
		Mantra:           "Uus Jux"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	CreateFood: {
		Type:             "CreateFoodSpell"
		Name:             "Create Food"
		Mantra:           "In Mani Ylem"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9011
		LeftHandEffect:   9011
		Action:           224
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Feeblemind: {
		Type:             "FeeblemindSpell"
		Name:             "Feeblemind"
		Mantra:           "Rel Wis"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Heal: {
		Type:             "HealSpell"
		Name:             "Heal"
		Mantra:           "In Mani"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           224
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	MagicArrow: {
		Type:             "MagicArrowSpell"
		Name:             "Magic Arrow"
		Mantra:           "In Por Ylem"
		Circle:           "First"
		DamageType:       "Fire"
		RightHandEffect:  9041
		LeftHandEffect:   9041
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	NightSight: {
		Type:             "NightSightSpell"
		Name:             "Night Sight"
		Mantra:           "In Lor"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "SulfurousAsh"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	ReactiveArmor: {
		Type:             "ReactiveArmorSpell"
		Name:             "Reactive Armor"
		Mantra:           "Flam Sanct"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9011
		LeftHandEffect:   9011
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Weaken: {
		Type:             "WeakenSpell"
		Name:             "Weaken"
		Mantra:           "Des Mani"
		Circle:           "First"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Agility: {
		Type:             "AgilitySpell"
		Name:             "Agility"
		Mantra:           "Ex Uus"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Cunning: {
		Type:             "CunningSpell"
		Name:             "Cunning"
		Mantra:           "Uus Wis"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Cure: {
		Type:             "CureSpell"
		Name:             "Cure"
		Mantra:           "An Nox"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}]
	}
	Harm: {
		Type:             "HarmSpell"
		Name:             "Harm"
		Mantra:           "An Mani"
		Circle:           "Second"
		DamageType:       "Necro"
		RightHandEffect:  9041
		LeftHandEffect:   9041
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Nightshade"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	MagicTrap: {
		Type:             "MagicTrapSpell"
		Name:             "Magic Trap"
		Mantra:           "In Jux"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9001
		LeftHandEffect:   9001
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	RemoveTrap: {
		Type:             "RemoveTrapSpell"
		Name:             "Remove Trap"
		Mantra:           "An Jux"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9001
		LeftHandEffect:   9001
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Protection: {
		Type:             "ProtectionSpell"
		Name:             "Protection"
		Mantra:           "Uus Sanct"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9011
		LeftHandEffect:   9011
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Strength: {
		Type:             "StrengthSpell"
		Name:             "Strength"
		Mantra:           "Uus Mani"
		Circle:           "Second"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Bless: {
		Type:             "BlessSpell"
		Name:             "Bless"
		Mantra:           "Rel Sanct"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           203
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Fireball: {
		Type:             "FireballSpell"
		Name:             "Fireball"
		Mantra:           "Vas Flam"
		Circle:           "Third"
		DamageType:       "Fire"
		RightHandEffect:  9041
		LeftHandEffect:   9041
		Action:           203
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}]
	}
	MagicLock: {
		Type:             "MagicLockSpell"
		Name:             "Magic Lock"
		Mantra:           "An Por"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9001
		LeftHandEffect:   9001
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Poison: {
		Type:             "PoisonSpell"
		Name:             "Poison"
		Mantra:           "In Nox"
		Circle:           "Third"
		DamageType:       "Poison"
		RightHandEffect:  9051
		LeftHandEffect:   9051
		Action:           203
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Telekinesis: {
		Type:             "TelekinesisSpell"
		Name:             "Telekinesis"
		Mantra:           "Ort Por Ylem"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           203
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Teleport: {
		Type:             "TeleportSpell"
		Name:             "Teleport"
		Mantra:           "Rel Por"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Unlock: {
		Type:             "UnlockSpell"
		Name:             "Unlock Spell"
		Mantra:           "Ex Por"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9001
		LeftHandEffect:   9001
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	WallOfStone: {
		Type:             "WallOfStoneSpell"
		Name:             "Wall of Stone"
		Mantra:           "In Sanct Ylem"
		Circle:           "Third"
		DamageType:       "None"
		RightHandEffect:  9011
		LeftHandEffect:   9011
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Garlic"
			Amount:  1
		}]
	}
	ArchCure: {
		Type:             "ArchCureSpell"
		Name:             "Arch Cure"
		Mantra:           "Vas An Nox"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	ArchProtection: {
		Type:             "ArchProtectionSpell"
		Name:             "Arch Protection"
		Mantra:           "Vas Uus Sanct"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9011
		LeftHandEffect:   9011
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Curse: {
		Type:             "CurseSpell"
		Name:             "Curse"
		Mantra:           "Des Sanct"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Nightshade"
			Amount:  1
		}, {
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	FireField: {
		Type:             "FireFieldSpell"
		Name:             "Fire Field"
		Mantra:           "In Flam Grav"
		Circle:           "Fourth"
		DamageType:       "Fire"
		RightHandEffect:  9041
		LeftHandEffect:   9041
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	GreaterHeal: {
		Type:             "GreaterHealSpell"
		Name:             "Greater Heal"
		Mantra:           "In Vas Mani"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           204
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	Lightning: {
		Type:             "LightningSpell"
		Name:             "Lightning"
		Mantra:           "Por Ort Grav"
		Circle:           "Fourth"
		DamageType:       "Air"
		RightHandEffect:  9021
		LeftHandEffect:   9021
		Action:           239
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	ManaDrain: {
		Type:             "ManaDrainSpell"
		Name:             "Mana Drain"
		Mantra:           "Ort Rel"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           215
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	Recall: {
		Type:             "RecallSpell"
		Name:             "Recall"
		Mantra:           "Kal Ort Por"
		Circle:           "Fourth"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           239
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	BladeSpirits: {
		Type:             "BladeSpiritsSpell"
		Name:             "Blade Spirits"
		Mantra:           "In Jux Hur Ylem"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9040
		LeftHandEffect:   9040
		Action:           266
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	DispelField: {
		Type:             "DispelFieldSpell"
		Name:             "Dispel Field"
		Mantra:           "An Grav"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           206
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}, {
			Reagent: "Garlic"
			Amount:  1
		}]
	}
	Incognito: {
		Type:             "IncognitoSpell"
		Name:             "Incognito"
		Mantra:           "Kal In Ex"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           206
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	MagicReflect: {
		Type:             "MagicReflectSpell"
		Name:             "Magic Reflection"
		Mantra:           "In Jux Sanct"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           242
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	MindBlast: {
		Type:             "MindBlastSpell"
		Name:             "Mind Blast"
		Mantra:           "Por Corp Wis"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9032
		LeftHandEffect:   9032
		Action:           218
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	Paralyze: {
		Type:             "ParalyzeSpell"
		Name:             "Paralyze"
		Mantra:           "An Ex Por"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           218
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	PoisonField: {
		Type:             "PoisonFieldSpell"
		Name:             "Poison Field"
		Mantra:           "In Nox Grav"
		Circle:           "Fifth"
		DamageType:       "Poison"
		RightHandEffect:  9052
		LeftHandEffect:   9052
		Action:           230
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	SummonCreature: {
		Type:             "SummonCreatureSpell"
		Name:             "Summon Creature"
		Mantra:           "Kal Xen"
		Circle:           "Fifth"
		DamageType:       "None"
		RightHandEffect:  0
		LeftHandEffect:   0
		Action:           16
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	Dispel: {
		Type:             "DispelSpell"
		Name:             "Dispel"
		Mantra:           "An Ort"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           218
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	EnergyBolt: {
		Type:             "EnergyBoltSpell"
		Name:             "Energy Bolt"
		Mantra:           "Corp Por"
		Circle:           "Sixth"
		DamageType:       "Air"
		RightHandEffect:  9022
		LeftHandEffect:   9022
		Action:           230
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Explosion: {
		Type:             "ExplosionSpell"
		Name:             "Explosion"
		Mantra:           "Vas Ort Flam"
		Circle:           "Sixth"
		DamageType:       "Fire"
		RightHandEffect:  9041
		LeftHandEffect:   9041
		Action:           230
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Invisibility: {
		Type:             "InvisibilitySpell"
		Name:             "Invisibility"
		Mantra:           "An Lor Xen"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           206
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Mark: {
		Type:             "MarkSpell"
		Name:             "Mark"
		Mantra:           "Kal Por Ylem"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           218
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	MassCurse: {
		Type:             "MassCurseSpell"
		Name:             "Mass Curse"
		Mantra:           "Vas Des Sanct"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           218
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	ParalyzeField: {
		Type:             "ParalyzeFieldSpell"
		Name:             "Paralyze Field"
		Mantra:           "In Ex Grav"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           230
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	Reveal: {
		Type:             "RevealSpell"
		Name:             "Reveal"
		Mantra:           "Wis Quas"
		Circle:           "Sixth"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           206
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	ChainLightning: {
		Type:             "ChainLightningSpell"
		Name:             "Chain Lightning"
		Mantra:           "Vas Ort Grav"
		Circle:           "Seventh"
		DamageType:       "Air"
		RightHandEffect:  9022
		LeftHandEffect:   9022
		Action:           209
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	EnergyField: {
		Type:             "EnergyFieldSpell"
		Name:             "Energy Field"
		Mantra:           "In Sanct Grav"
		Circle:           "Seventh"
		DamageType:       "Air"
		RightHandEffect:  9022
		LeftHandEffect:   9022
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	FlameStrike: {
		Type:             "FlameStrikeSpell"
		Name:             "Flame Strike"
		Mantra:           "Kal Vas Flam"
		Circle:           "Seventh"
		DamageType:       "Fire"
		RightHandEffect:  9042
		LeftHandEffect:   9042
		Action:           245
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	GateTravel: {
		Type:             "GateTravelSpell"
		Name:             "Gate Travel"
		Mantra:           "Vas Rel Por"
		Circle:           "Seventh"
		DamageType:       "None"
		RightHandEffect:  9032
		LeftHandEffect:   9032
		Action:           263
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	ManaVampire: {
		Type:             "ManaVampireSpell"
		Name:             "Mana Vampire"
		Mantra:           "Ort Sanct"
		Circle:           "Seventh"
		DamageType:       "None"
		RightHandEffect:  9032
		LeftHandEffect:   9032
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	MassDispel: {
		Type:             "MassDispelSpell"
		Name:             "Mass Dispel"
		Mantra:           "Vas An Ort"
		Circle:           "Seventh"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           263
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	MeteorSwarm: {
		Type:             "MeteorSwarmSpell"
		Name:             "Meteor Swarm"
		Mantra:           "Flam Kal Des Ylem"
		Circle:           "Seventh"
		DamageType:       "None"
		RightHandEffect:  9042
		LeftHandEffect:   9042
		Action:           233
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	Polymorph: {
		Type:             "PolymorphSpell"
		Name:             "Polymorph"
		Mantra:           "Vas Ylem Rel"
		Circle:           "Seventh"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}]
	}
	Earthquake: {
		Type:             "EarthquakeSpell"
		Name:             "Earthquake"
		Mantra:           "In Vas Por"
		Circle:           "Eighth"
		DamageType:       "Earth"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           233
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	EnergyVortex: {
		Type:             "EnergyVortexSpell"
		Name:             "Energy Vortex"
		Mantra:           "Vas Corp Por"
		Circle:           "Eighth"
		DamageType:       "Air"
		RightHandEffect:  9032
		LeftHandEffect:   9032
		Action:           260
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "BlackPearl"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "Nightshade"
			Amount:  1
		}]
	}
	Resurrection: {
		Type:             "ResurrectionSpell"
		Name:             "Resurrection"
		Mantra:           "An Corp"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9062
		LeftHandEffect:   9062
		Action:           245
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        true
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "Garlic"
			Amount:  1
		}, {
			Reagent: "Ginseng"
			Amount:  1
		}]
	}
	AirElemental: {
		Type:             "AirElementalSpell"
		Name:             "Air Elemental"
		Mantra:           "Kal Vas Xen Hur"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9010
		LeftHandEffect:   9010
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	SummonDaemon: {
		Type:             "SummonDaemonSpell"
		Name:             "Summon Daemon"
		Mantra:           "Kal Vas Xen Corp"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9050
		LeftHandEffect:   9050
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	EarthElemental: {
		Type:             "EarthElementalSpell"
		Name:             "Earth Elemental"
		Mantra:           "Kal Vas Xen Ylem"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9020
		LeftHandEffect:   9020
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	FireElemental: {
		Type:             "FireElementalSpell"
		Name:             "Fire Elemental"
		Mantra:           "Kal Vas Xen Flam"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9050
		LeftHandEffect:   9050
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}, {
			Reagent: "SulfurousAsh"
			Amount:  1
		}]
	}
	WaterElemental: {
		Type:             "WaterElementalSpell"
		Name:             "Water Elemental"
		Mantra:           "Kal Vas Xen An Flam"
		Circle:           "Eighth"
		DamageType:       "None"
		RightHandEffect:  9070
		LeftHandEffect:   9070
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "Bloodmoss"
			Amount:  1
		}, {
			Reagent: "MandrakeRoot"
			Amount:  1
		}, {
			Reagent: "SpidersSilk"
			Amount:  1
		}]
	}
	ControlUndead: {
		Type:             "ControlUndeadSpell"
		Name:             "Control Undead"
		Mantra:           "Nutu Magistri Supplicare"
		Circle:           "Lower lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Bloodspawn"
			Amount:  1
		}, {
			Reagent: "Blackmoor"
			Amount:  1
		}, {
			Reagent: "Bone"
			Amount:  1
		}]
	}
	Darkness: {
		Type:             "DarknessSpell"
		Name:             "Darkness"
		Mantra:           "In Caligne Abditus"
		Circle:           "Lower lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Pumice"
			Amount:  1
		}, {
			Reagent: "PigIron"
			Amount:  1
		}]
	}
	DecayingRay: {
		Type:             "DecayingRaySpell"
		Name:             "Decaying Ray"
		Mantra:           "Umbra Aufero Vita"
		Circle:           "Lower lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "VialOfBlood"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}, {
			Reagent: "DaemonBone"
			Amount:  1
		}]
	}
	SpectresTouch: {
		Type:             "SpectresTouchSpell"
		Name:             "Spectres Touch"
		Mantra:           "Enevare"
		Circle:           "Lower lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "ExecutionersCap"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "DaemonBone"
			Amount:  1
		}]
	}
	AbyssalFlame: {
		Type:             "AbyssalFlameSpell"
		Name:             "Abyssal Flame"
		Mantra:           "Orinundus Barathrum Erado Hostes Hostium"
		Circle:           "Higher lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}, {
			Reagent: "DaemonBone"
			Amount:  1
		}]
	}
	AnimateDead: {
		Type:             "AnimateDeadSpell"
		Name:             "Animate Dead"
		Mantra:           "Corpus Sine Nomine Expergefaceret"
		Circle:           "Higher lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}, {
			Reagent: "Bone"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}]
	}
	Sacrifice: {
		Type:             "SacrificeSpell"
		Name:             "Sacrifice"
		Mantra:           "Animus Ex Corporis Resolveretur"
		Circle:           "Higher lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  0
		LeftHandEffect:   0
		Action:           16
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "ExecutionersCap"
			Amount:  1
		}, {
			Reagent: "Bloodspawn"
			Amount:  1
		}, {
			Reagent: "WyrmsHeart"
			Amount:  1
		}, {
			Reagent: "Bone"
			Amount:  1
		}]
	}
	WraithBreath: {
		Type:             "WraithBreathSpell"
		Name:             "Wraith Breath"
		Mantra:           "Manes Sollicti Mi Compellere"
		Circle:           "Higher lesser Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Pumice"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}, {
			Reagent: "Bone"
			Amount:  1
		}, {
			Reagent: "Blackmoor"
			Amount:  1
		}]
	}
	SorcerersBane: {
		Type:             "SorcerersBaneSpell"
		Name:             "Sorceror's Bane"
		Mantra:           "Fluctus Perturbo Magus Navitas"
		Circle:           "Lower greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "VolcanicAsh"
			Amount:  1
		}, {
			Reagent: "Pumice"
			Amount:  1
		}, {
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "DeadWood"
			Amount:  1
		}]
	}
	SummonSpirit: {
		Type:             "SummonSpiritSpell"
		Name:             "Summon Spirit"
		Mantra:           "Manes Turbidi Sollictique Resolverent"
		Circle:           "Lower greater Chants of Necromancy"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "DaemonBone"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "Bloodspawn"
			Amount:  1
		}]
	}
	WraithForm: {
		Type:             "WraithFormSpell"
		Name:             "Wraith Form"
		Mantra:           "Manes Sollicti Mihi Infundite"
		Circle:           "Lower greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "DaemonBone"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "Bloodspawn"
			Amount:  1
		}]
	}
	WyvernStrike: {
		Type:             "WyvernStrikeSpell"
		Name:             "Wyvern Strike"
		Mantra:           "Ubrae Tenebrae Venarent"
		Circle:           "Lower greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "SerpentScale"
			Amount:  1
		}, {
			Reagent: "Blackmoor"
			Amount:  1
		}, {
			Reagent: "Bloodspawn"
			Amount:  1
		}]
	}
	Kill: {
		Type:             "KillSpell"
		Name:             "Kill"
		Mantra:           "Ulties Manum Necarent"
		Circle:           "Higher greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "DaemonBone"
			Amount:  1
		}, {
			Reagent: "ExecutionersCap"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}, {
			Reagent: "WyrmsHeart"
			Amount:  1
		}]
	}
	LicheForm: {
		Type:             "LicheFormSpell"
		Name:             "Liche Form"
		Mantra:           "Umbrae Tenebrae Miserere"
		Circle:           "Higher greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "DaemonBone"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}]
	}
	Plague: {
		Type:             "PlagueSpell"
		Name:             "Plague"
		Mantra:           "Fluctus Puter Se Aresceret"
		Circle:           "Higher greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           227
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   true
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "VolcanicAsh"
			Amount:  1
		}, {
			Reagent: "BatWing"
			Amount:  1
		}, {
			Reagent: "DaemonBone"
			Amount:  1
		}, {
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "Bloodspawn"
			Amount:  1
		}, {
			Reagent: "Pumice"
			Amount:  1
		}, {
			Reagent: "SerpentScale"
			Amount:  1
		}]
	}
	Spellbind: {
		Type:             "SpellbindSpell"
		Name:             "Spellbind"
		Mantra:           "Nutu Magistri Se Compellere"
		Circle:           "Higher greater Chants of Necromancy"
		DamageType:       "Necro"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "EyeOfNewt"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}, {
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "PigIron"
			Amount:  1
		}]
	}
	Antidote: {
		Type:             "AntidoteSpell"
		Name:             "Antidote"
		Mantra:           "Puissante Terre Traite Ce Patient"
		Circle:           "Lower 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           212
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "DeadWood"
			Amount:  1
		}, {
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "ExecutionersCap"
			Amount:  1
		}]
	}
	OwlSight: {
		Type:             "OwlSightSpell"
		Name:             "Owl Sight"
		Mantra:           "Vista Da Noite"
		Circle:           "Lower 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "EyeOfNewt"
			Amount:  1
		}]
	}
	ShiftingEarth: {
		Type:             "ShiftingEarthSpell"
		Name:             "Shifting Earth"
		Mantra:           "Esmagamento Con Pedra"
		Circle:           "Lower 1st Circle of Earth spells"
		DamageType:       "Earth"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}, {
			Reagent: "DeadWood"
			Amount:  1
		}]
	}
	SummonMammals: {
		Type:             "SummonMammalsSpell"
		Name:             "Summon Mammals"
		Mantra:           "Chame O Mamifero Agora"
		Circle:           "Lower 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  0
		LeftHandEffect:   0
		Action:           16
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        false
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "SerpentScale"
			Amount:  1
		}, {
			Reagent: "PigIron"
			Amount:  1
		}, {
			Reagent: "EyeOfNewt"
			Amount:  1
		}]
	}
	CallLightning: {
		Type:             "CallLightningSpell"
		Name:             "Call Lightning"
		Mantra:           "Batida Do Deus"
		Circle:           "Higher 1st Circle of Earth spells"
		DamageType:       "Air"
		RightHandEffect:  9031
		LeftHandEffect:   9031
		Action:           236
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "WyrmsHeart"
			Amount:  1
		}, {
			Reagent: "PigIron"
			Amount:  1
		}, {
			Reagent: "Bone"
			Amount:  1
		}]
	}
	EarthsBlessing: {
		Type:             "EarthsBlessingSpell"
		Name:             "Earths Blessing"
		Mantra:           "Foria Da Terra"
		Circle:           "Higher 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           203
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "PigIron"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}]
	}
	EarthPortal: {
		Type:             "EarthPortalSpell"
		Name:             "Earth Portal"
		Mantra:           "Destraves Limites Da Natureza"
		Circle:           "Higher 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9032
		LeftHandEffect:   9032
		Action:           263
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "None"
		}
		ReagentCosts: [{
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "ExecutionersCap"
			Amount:  1
		}, {
			Reagent: "EyeOfNewt"
			Amount:  1
		}]
	}
	NaturesTouch: {
		Type:             "NaturesTouchSpell"
		Name:             "Natures Touch"
		Mantra:           "Guerissez Par Terre"
		Circle:           "Higher 1st Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9061
		LeftHandEffect:   9061
		Action:           204
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Beneficial"
		}
		ReagentCosts: [{
			Reagent: "Pumice"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}]
	}
	GustOfAir: {
		Type:             "GustOfAirSpell"
		Name:             "Gust Of Air"
		Mantra:           "Gust Do Ar"
		Circle:           "Lower 2nd Circle of Earth spells"
		DamageType:       "Air"
		RightHandEffect:  9022
		LeftHandEffect:   9022
		Action:           230
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BatWing"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}]
	}
	RisingFire: {
		Type:             "RisingFireSpell"
		Name:             "Rising Fire"
		Mantra:           "Batida Do Fogo"
		Circle:           "Lower 2nd Circle of Earth spells"
		DamageType:       "Fire"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           233
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "BatWing"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}, {
			Reagent: "VialOfBlood"
			Amount:  1
		}]
	}
	Shapeshift: {
		Type:             "ShapeshiftSpell"
		Name:             "Shapeshift"
		Mantra:           "Mude Minha Forma"
		Circle:           "Lower 2nd Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9002
		LeftHandEffect:   9002
		Action:           221
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "WyrmsHeart"
			Amount:  1
		}, {
			Reagent: "Blackmoor"
			Amount:  1
		}, {
			Reagent: "BatWing"
			Amount:  1
		}]
	}
	IceStrike: {
		Type:             "IceStrikeSpell"
		Name:             "Ice Strike"
		Mantra:           "Geada Com Inverno"
		Circle:           "Higher 2nd Circle of Earth spells"
		DamageType:       "Water"
		RightHandEffect:  9012
		LeftHandEffect:   9012
		Action:           233
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		TargetOptions: {
			Range:         15
			AllowGround:   false
			AllowMultis:   true
			AllowNonLocal: false
			CheckLos:      true
			Flags:         "Harmful"
		}
		ReagentCosts: [{
			Reagent: "Bone"
			Amount:  1
		}, {
			Reagent: "BatWing"
			Amount:  1
		}, {
			Reagent: "Brimstone"
			Amount:  1
		}]
	}
	EarthSpirit: {
		Type:             "EarthSpiritSpell"
		Name:             "Earth Spirit"
		Mantra:           "Chame A Terra Elemental"
		Circle:           "Higher 2nd Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9010
		LeftHandEffect:   9010
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "DragonsBlood"
			Amount:  1
		}, {
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}]
	}
	FireSpirit: {
		Type:             "FireSpiritSpell"
		Name:             "Fire Spirit"
		Mantra:           "Chame O Fogo Elemental"
		Circle:           "Higher 2nd Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9010
		LeftHandEffect:   9010
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "EyeOfNewt"
			Amount:  1
		}, {
			Reagent: "Blackmoor"
			Amount:  1
		}, {
			Reagent: "Obsidian"
			Amount:  1
		}]
	}
	StormSpirit: {
		Type:             "StormSpiritSpell"
		Name:             "Storm Spirit"
		Mantra:           "Chame O Ar Elemental"
		Circle:           "Higher 2nd Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9010
		LeftHandEffect:   9010
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "FertileDirt"
			Amount:  1
		}, {
			Reagent: "VolcanicAsh"
			Amount:  1
		}, {
			Reagent: "BatWing"
			Amount:  1
		}]
	}
	WaterSpirit: {
		Type:             "WaterSpiritSpell"
		Name:             "Water Spirit"
		Mantra:           "Chame A Agua Elemental"
		Circle:           "Higher 2nd Circle of Earth spells"
		DamageType:       "None"
		RightHandEffect:  9010
		LeftHandEffect:   9010
		Action:           269
		ShowHandMovement: true
		RevealOnCast:     true
		Resistable:       true
		Reflectable:      true
		ClearHandsOnCast: true
		BlocksMovement:   false
		AllowTown:        true
		AllowDead:        false
		ReagentCosts: [{
			Reagent: "WyrmsHeart"
			Amount:  1
		}, {
			Reagent: "SerpentScale"
			Amount:  1
		}, {
			Reagent: "EyeOfNewt"
			Amount:  1
		}]
	}
}
