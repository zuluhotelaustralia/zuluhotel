package Loot

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

#Groups: {
	[string]: [...Types.#LootEntry]
	
	Ammo: [
		{
			MinQuantity: 2
			MaxQuantity: 10
			Chance:      0.5
			Value:       "ThunderBolt"
		},
		{
			MinQuantity: 7
			MaxQuantity: 15
			Chance:      1
			Value:       "IceArrow"
		},
		{
			MinQuantity: 7
			MaxQuantity: 15
			Chance:      1
			Value:       "FireArrow"
		},
		{
			MinQuantity: 12
			MaxQuantity: 20
			Chance:      1
			Value:       "Bolt"
		},
		{
			MinQuantity: 12
			MaxQuantity: 20
			Chance:      1
			Value:       "Arrow"
		},
	]

	Artifacts: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.earringsofmanycolors"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.necklaceofmanycolors"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.braceletofmanycolors"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ringofmanycolors"
		},
	]

	BoneArmor: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneGloves"
		},
	]

	Circle6Scrolls: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RevealScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ParalyzeFieldScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MassCurseScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "MarkScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "InvisibilityScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ExplosionScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EnergyBoltScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DispelScroll"
		},
	]

	Circle7Scrolls: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PolymorphScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "MeteorSwarmScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MassDispelScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ManaVampireScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "GateTravelScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "FlamestrikeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "EnergyFieldScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainLightningScroll"
		},
	]

	Circle8Scrolls: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonWaterElementalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonFireElementalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonEarthElementalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "SummonDaemonScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonAirElementalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "ResurrectionScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "EnergyVortexScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthquakeScroll"
		},
	]

	Clothes: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StrawHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TallStrawHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BearMask"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OrcishKinMask"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DeerMask"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TribalMask"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TribalMask"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ThighBoots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Boots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shoes"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Sandals"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WizardsHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JesterHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TricorneHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FeatheredHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bonnet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WideBrimHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FloppyHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SkullCap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BodySash"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bandana"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FullApron"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HalfApron"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LongPants"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Kilt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortPants"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Skirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cloak"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Surcoat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Tunic"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JesterSuit"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Doublet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Robe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlainDress"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FancyDress"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FancyShirt"
		},
	]

	EarthItem: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "WaterSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "StormSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "FlameSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "EarthSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "IceStrikeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShapeshiftScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "RisingFireScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "GustOfAirScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "NaturesTouchScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "EarthPortalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "EarthBlessingScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "CallLightningScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonMammalsScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShiftingEarthScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OwlSightScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "AntidoteScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "Earthbook"
		},
	]

	Eggs: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "FrenziedOstardEgg"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OstardEgg"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "DragonEgg"
		},
	]

	GMArmor: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.KrakenLeatherLeggings"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.KrakenLeatherTunic"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.KrakenLeatherSleeves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.KrakenLeatherGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SageRobe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FemaleSylvianChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SylvianChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SylvianLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SylvianArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SylvianGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SylvianGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "InfernalGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "InfernalLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "InfernalArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "InfernalChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ZephyrChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ZephyrLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ZephyrCoif"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ShieldofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ShieldofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ShieldofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ShieldofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlatemailofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGorgetofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGlovesofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateLeggingsofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateArmsofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateHelmofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.FemalePlateofTheDamned"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlatemailofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGorgetofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGlovesofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateLeggingsofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateArmsofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateHelmofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.FemalePlateofTheAbyss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlatemailofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGorgetofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGlovesofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateLeggingsofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateArmsofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateHelmofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.FemalePlateofTheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlatemailofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGorgetofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateGlovesofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateLeggingsofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateArmsofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.PlateHelmofHavoc"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.FemalePlateofHavoc"
		},
	]

	GMWeapon: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.bowoftaintedblood"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.kryssoftheholyavenger"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.sniperscrossbowoferadication"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "XarafaxsAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OmerosPickaxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.SwordofthePowerPlayer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.MaceofMutilation"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.HalberdofthefallenPaladin"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.MaceoftheWarlord"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.katanaoftheDeadlyAssassin"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.StaffofMenace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.Undeadsaxeofexecution"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.ScimitarofIcyDoom"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.CrossbowoftheGods"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.Scalpeloftheduelist"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.LanceOftheLostSouls"
		},
	]

	Gems: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Diamond"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "StarSapphire"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "Amethyst"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "Emerald"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "Sapphire"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "Ruby"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       "Tourmaline"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       "Citrine"
		},
		{
			MinQuantity: 1
			MaxQuantity: 5
			Chance:      1
			Value:       "Amber"
		},
	]

	Jewelry: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldEarrings"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SilverBracelet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldNecklace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldBeadNecklace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Necklace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldEarrings"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldBracelet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GoldRing"
		},
	]

	Junk: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cabbage"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Lettuce"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Watermelon"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Lime"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Lemon"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Grapes"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pumpkin"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Banana"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Garlic"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Onion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Peach"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Apple"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SlabOfBacon"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Sausage"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bacon"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Ham"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Frypan"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WoodenBowl"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RollingPin"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Muffins"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BreadLoaf"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cake"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ApplePie"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.wine"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "CheeseSlice"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "CheesePizza"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ThighBoots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Boots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shoes"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Sandals"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MortarPestle"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TinkersTools"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SmoothingPlane"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MouldingPlane"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JointingPlane"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Scorp"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Inshave"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Froe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DrawKnife"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DovetailSaw"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Saw"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Hammer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Coconut"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.hairtonic"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.hairbrush"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Torch"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pouch"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Backpack"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bag"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.straightrazor"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Scissors"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SewingKit"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WizardsHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JesterHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TricorneHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FeatheredHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bonnet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WideBrimHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FloppyHat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SkullCap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BodySash"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bandana"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FullApron"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HalfApron"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LongPants"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Kilt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortPants"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cloak"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Surcoat"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Tunic"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JesterSuit"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Doublet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Robe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlainDress"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FancyShirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Shovel"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "JarHoney"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Beeswax"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Candle"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RecallRune"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Tambourine"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Drums"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Lute"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Harp"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BoneGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Robe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MetalShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WoodenShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MetalKiteShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HeaterShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WoodenKiteShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Buckler"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BronzeShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "NorseHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bascinet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Helmet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "CloseHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainCoif"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ThighBoots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Boots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherShorts"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedBustierArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherBustierArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherSkirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FemaleLeatherChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FemaleStuddedChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherCap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BlackStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShepherdsCrook"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "QuarterStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GnarledStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BlackStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pitchfork"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SmithHammer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Club"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SkinningKnife"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cleaver"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ButcherKnife"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pickaxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Hatchet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "VikingSword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Scimitar"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Kryss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Kryss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Katana"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Dagger"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cutlass"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Broadsword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarFork"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarFork"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Spear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Spear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortSpear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortSpear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Halberd"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bardiche"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarMace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarHammer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Maul"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Mace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HammerPick"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TwoHandedAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LargeBattleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ExecutionersAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DoubleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BattleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Axe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HeavyCrossbow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Crossbow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Longsword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Coconut"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LifeCrystal"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterCurePotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "CurePotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LesserCurePotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "NightSightPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterStrengthPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StrengthPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StrengthPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TotalRefreshPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RefreshPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterAgilityPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "AgilityPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "AgilityPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterExplosionPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ExplosionPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LesserExplosionPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DeadlyPoisonPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterPoisonPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PoisonPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LesserPoisonPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterHealPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HealPotion"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HealPotion"
		},
	]

	MagicWands56: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand65"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand48"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand47"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand46"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand45"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand44"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand43"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WeaknessWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ManaDrainWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MagicArrowWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LightningWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HealWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HarmWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GreaterHealWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FireballWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FeebleWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ClumsyWand"
		},
	]

	MagicWands78: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "IDWand"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand67"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand66"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand64"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand63"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand62"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand61"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand60"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand59"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand58"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand57"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand56"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand55"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand54"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand53"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand52"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand51"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand50"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.magicwand49"
		},
	]

	MagicWeapons: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "IceBow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "FireBow"
		},
	]

	MiscMagicItem: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram1"
		},
	]

	NecroItem: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "SpellbindScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "PlagueScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "LicheScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "KillScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "WyvernStrikeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "WraithformScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "SummonSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "SorcerersBaneScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "WraithsBreathScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "SacrificeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "AnimateDeadScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "AbyssalFlameScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "SpectresTouchScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "DecayingRayScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "DarknessScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "ControlUndeadScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "Codex"
		},
	]

	NormalArmor: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OrderShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChaosShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MetalShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WoodenShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MetalKiteShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HeaterShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WoodenKiteShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Buckler"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BronzeShield"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "PlateHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "NorseHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bascinet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Helmet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "CloseHelm"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ChainCoif"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ThighBoots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Boots"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherShorts"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedBustierArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherBustierArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherSkirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FemaleLeatherChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FemaleStuddedChest"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "StuddedGorget"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherArms"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherLegs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherCap"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGloves"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LeatherGorget"
		},
	]

	NormalWeapons: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.paladinsword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.compositebow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.RepeatingCrossbow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.crescentblade"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.lance"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.pike"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.bladedstaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.scepter"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.boneharvester"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.scythe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.doublebladedstaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShepherdsCrook"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "QuarterStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "GnarledStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BlackStaff"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pitchfork"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SmithHammer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Club"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SkinningKnife"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cleaver"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ButcherKnife"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Pickaxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Hatchet"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "VikingSword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Scimitar"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Kryss"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Katana"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Dagger"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Cutlass"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Broadsword"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarFork"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Spear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortSpear"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Halberd"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bardiche"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarMace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarHammer"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Maul"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Mace"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HammerPick"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TwoHandedAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "LargeBattleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ExecutionersAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "DoubleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BattleAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Axe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "HeavyCrossbow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Crossbow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Bow"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Longsword"
		},
	]

	Ores: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "NewZuluOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       "GoddessOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.07
			Value:       "DoomOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "CrystalOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.13
			Value:       "AnraOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.16
			Value:       "DestructionOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.19
			Value:       "PeachblueOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.22
			Value:       "ExecutorOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       "DripstoneOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.28
			Value:       "IceRockOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.31
			Value:       "AzuriteOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.34
			Value:       "LavarockOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.37
			Value:       "MalachiteOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.4
			Value:       "VirginityOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.43
			Value:       "PyriteOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.46
			Value:       "UndeadOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.49
			Value:       "RedElvenOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.52
			Value:       "OldBritainOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.55
			Value:       "OnyxOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.58
			Value:       "SpectralOre"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.61
			Value:       "CopperOre"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.64
			Value:       "MysticOre"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.67
			Value:       "DullCopperOre"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.7
			Value:       "PlatinumOre"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.73
			Value:       "SilverRockOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.76
			Value:       "DarkPaganOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.79
			Value:       "BronzeOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.82
			Value:       "BlackDwarfOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.85
			Value:       "FruityOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.88
			Value:       "SpikeOre"
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      0.91
			Value:       "IronOre"
		},
	]

	PaganMagicItem: [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "PoisonElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShadowElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "FireElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "AirElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "EarthElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram9"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram8"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram7"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram6"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "WaterElementalPentagram1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "WaterSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "StormSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "FlameSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "EarthSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "IceStrikeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "ShapeshiftScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "RisingFireScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "GustOfAirScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "NaturesTouchScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "EarthPortalScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "EarthBlessingScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       "CallLightningScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "SummonMammalsScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShiftingEarthScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OwlSightScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "AntidoteScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "Earthbook"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "SpellbindScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "PlagueScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "LicheScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       "KillScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "WyvernStrikeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "WraithformScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "SummonSpiritScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "SorcerersBaneScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "WraithsBreathScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "SacrificeScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "AnimateDeadScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       "AbyssalFlameScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "SpectresTouchScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "DecayingRayScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "DarknessScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.9
			Value:       "ControlUndeadScroll"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "Codex"
		},
	]

	PaganReagents: [
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "DragonsBlood"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "DaemonBone"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "Bloodspawn"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "WyrmsHeart"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "VialOfBlood"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "PigIron"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "Obsidian"
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       "VolcanicAsh"
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       "Pumice"
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       "EyeOfNewt"
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       "Brimstone"
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       "Blackmoor"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "SerpentScale"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "FertileDirt"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "ExecutionersCap"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "DeadWood"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "Bone"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "BatWing"
		},
	]

	Reagents: [
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "BlackPearl"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "Bloodmoss"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "Ginseng"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "Nightshade"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "SulfurousAsh"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "MandrakeRoot"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "SpidersSilk"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       "Garlic"
		},
	]
}
