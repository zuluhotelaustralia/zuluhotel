package Loot

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Tables: {
	[string]: [...Types.#LootTable]
	"1": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.6
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "CheeseSlice"
		},
	]

	"10": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Longsword"
		},
	]

	"100": [
		{
			MinQuantity: 3
			MaxQuantity: 90
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 0
			MaxQuantity: 2
			Chance:      0.5
			Value:       #Groups.Junk
		},
	]

	"109": [
		{
			MinQuantity: 105
			MaxQuantity: 225
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"11": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarMace"
		},
	]

	"110": [
		{
			MinQuantity: 210
			MaxQuantity: 450
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"111": [
		{
			MinQuantity: 505
			MaxQuantity: 750
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"112": [
		{
			MinQuantity: 510
			MaxQuantity: 1000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"113": [
		{
			MinQuantity: 501
			MaxQuantity: 1000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.35
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"114": [
		{
			MinQuantity: 501
			MaxQuantity: 1250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"115": [
		{
			MinQuantity: 1001
			MaxQuantity: 2000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.6
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"116": [
		{
			MinQuantity: 1001
			MaxQuantity: 2000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"117": [
		{
			MinQuantity: 1001
			MaxQuantity: 2000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 4
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 3
			MaxQuantity: 18
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"118": [
		{
			MinQuantity: 1001
			MaxQuantity: 2500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 3
			MaxQuantity: 5
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"119": [
		{
			MinQuantity: 1501
			MaxQuantity: 3000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 5
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 4
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 4
			MaxQuantity: 24
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"12": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "ShortSpear"
		},
	]

	"121": [
		{
			MinQuantity: 22
			MaxQuantity: 120
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.Junk
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      0.1
			Value:       #Groups.Gems
		},
	]

	"122": [
		{
			MinQuantity: 154
			MaxQuantity: 750
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.1D3-1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.5
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 20
			Chance:      0.5
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
	]

	"123": [
		{
			MinQuantity: 25
			MaxQuantity: 115
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"124": [
		{
			MinQuantity: 2
			MaxQuantity: 8
			Chance:      1
			Value:       "VolcanicAsh"
		},
		{
			MinQuantity: 3
			MaxQuantity: 15
			Chance:      1
			Value:       "SulfurousAsh"
		},
	]

	"125": [
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"126": [
		{
			MinQuantity: 41
			MaxQuantity: 150
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.BoneArmor
		},
	]

	"127": [
		{
			MinQuantity: 101
			MaxQuantity: 500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Gems
		},
	]

	"128": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 4
			MaxQuantity: 8
			Chance:      1
			Value:       #Groups.PaganReagents
		},
	]

	"129": [
		{
			MinQuantity: 51
			MaxQuantity: 550
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.PaganReagents
		},
	]

	"13": [
		{
			MinQuantity: 160
			MaxQuantity: 250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "OrcHelm"
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
			Value:       "TwoHandedAxe"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "RingmailArms"
		},
	]

	"130": [
		{
			MinQuantity: 251
			MaxQuantity: 1250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.5
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.35
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 3
			MaxQuantity: 15
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 6
			MaxQuantity: 18
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       "TreasureMapLevel5"
		},
	]

	"131": [
		{
			MinQuantity: 251
			MaxQuantity: 850
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.35
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       "TreasureMapLevel5"
		},
	]

	"132": [
		{
			MinQuantity: 501
			MaxQuantity: 1750
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.65
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.08
			Value:       "TreasureMapLevel5"
		},
	]

	"133": [
		{
			MinQuantity: 101
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.EarthItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.elfhead"
		},
	]

	"134": [
		{
			MinQuantity: 101
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.elfhead"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       "TreasureMapLevel3"
		},
	]

	"135": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.8
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       "TreasureMapLevel5"
		},
	]

	"136": [
		{
			MinQuantity: 502
			MaxQuantity: 2500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.6
			Value:       #Groups.EarthItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
	]

	"137": [
		{
			MinQuantity: 502
			MaxQuantity: 2500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.6
			Value:       #Groups.NecroItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
	]

	"138": [
		{
			MinQuantity: 101
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.NecroItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
	]

	"139": [
		{
			MinQuantity: 101
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       "TreasureMapLevel3"
		},
	]

	"14": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"140": [
		{
			MinQuantity: 1501
			MaxQuantity: 3000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.EarthItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.6
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"141": [
		{
			MinQuantity: 2001
			MaxQuantity: 4000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.3
			Value:       #Groups.NecroItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.7
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.6
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.6
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 5
			MaxQuantity: 15
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"142": [
		{
			MinQuantity: 2001
			MaxQuantity: 3000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 20
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.skullofzanroth"
		},
	]

	"143": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "TreasureMapLevel6"
		},
	]

	"15": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.BoneArmor
		},
	]

	"150": [
		{
			MinQuantity: 1040
			MaxQuantity: 21000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.15
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.5
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.GMWeapon
		},
		{
			MinQuantity: 4
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 6
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.7
			Value:       "TreasureMapLevel6"
		},
	]

	"16": [
		{
			MinQuantity: 81
			MaxQuantity: 160
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.BoneArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
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
			Value:       "Scimitar"
		},
	]

	"17": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BattleAxe"
		},
	]

	"18": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "WarMace"
		},
	]

	"19": [
		{
			MinQuantity: 115
			MaxQuantity: 250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel2"
		},
	]

	"2": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.6
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Club"
		},
	]

	"20": [
		{
			MinQuantity: 15
			MaxQuantity: 375
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "MissingObject.KewlMasks"
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.37
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"200": [
		{
			MinQuantity: 5010
			MaxQuantity: 35000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       #Groups.Artifacts
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.3
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.3
			Value:       #Groups.EarthItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.3
			Value:       #Groups.NecroItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.5
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
	]

	"201": [
		{
			MinQuantity: 5001
			MaxQuantity: 5250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.GMArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.GMWeapon
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.8
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 5
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "TreasureMapLevel6"
		},
	]

	"202": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.MiscMagicItem
		},
	]

	"203": [
		{
			MinQuantity: 2004
			MaxQuantity: 2400
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       "RadiantNimbusDiamondOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMWeapon
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.21
			Value:       "MissingObject.RitualReagents"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "Eggs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.65
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      0.65
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.3
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 5
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       "TreasureMapLevel5"
		},
	]

	"204": [
		{
			MinQuantity: 1004
			MaxQuantity: 1400
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "DarkSableRubyOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMWeapon
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.21
			Value:       "MissingObject.RitualReagents"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "Eggs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.65
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      0.65
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.3
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 5
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel4"
		},
	]

	"205": [
		{
			MinQuantity: 1004
			MaxQuantity: 1400
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       "EbonTwilightSapphireOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.GMWeapon
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.MiscMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.21
			Value:       "MissingObject.RitualReagents"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "Eggs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.65
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      0.65
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.3
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 5
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel4"
		},
	]

	"21": [
		{
			MinQuantity: 31
			MaxQuantity: 40
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FertileDirt"
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ores
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       "TreasureMapLevel3"
		},
	]

	"22": [
		{
			MinQuantity: 160
			MaxQuantity: 600
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      0.5
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.3
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.07
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.05
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.4
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.35
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel3"
		},
	]

	"23": [
		{
			MinQuantity: 60
			MaxQuantity: 150
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.06
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"24": [
		{
			MinQuantity: 6
			MaxQuantity: 25
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.BoneArmor
		},
	]

	"25": [
		{
			MinQuantity: 60
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"26": [
		{
			MinQuantity: 85
			MaxQuantity: 225
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel2"
		},
	]

	"27": [
		{
			MinQuantity: 60
			MaxQuantity: 300
			Chance:      1
			Value:       "Gold"
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
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "VikingSword"
		},
	]

	"28": [
		{
			MinQuantity: 110
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Gems
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
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"29": [
		{
			MinQuantity: 50
			MaxQuantity: 140
			Chance:      1
			Value:       "Gold"
		},
	]

	"3": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       "CheeseSlice"
		},
	]

	"30": [
		{
			MinQuantity: 35
			MaxQuantity: 75
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.2
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"31": [
		{
			MinQuantity: 55
			MaxQuantity: 100
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "MissingObject.KewlMasks"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"32": [
		{
			MinQuantity: 15
			MaxQuantity: 40
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Log"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.PaganReagents
		},
	]

	"33": [
		{
			MinQuantity: 6
			MaxQuantity: 15
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.PaganReagents
		},
	]

	"34": [
		{
			MinQuantity: 110
			MaxQuantity: 250
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "MissingObject.reaperwood"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Log"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.PaganReagents
		},
	]

	"35": [
		{
			MinQuantity: 1254
			MaxQuantity: 2050
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.15
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "Eggs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.2
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.65
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 4
			Chance:      0.65
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.3
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 5
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel5"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       "TreasureMapLevel4"
		},
	]

	"36": [
		{
			MinQuantity: 203
			MaxQuantity: 350
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"37": [
		{
			MinQuantity: 1004
			MaxQuantity: 1400
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "Eggs"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.15
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 5
			Chance:      0.45
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      0.45
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.07
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       "TreasureMapLevel5"
		},
	]

	"38": [
		{
			MinQuantity: 103
			MaxQuantity: 160
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel1"
		},
	]

	"39": [
		{
			MinQuantity: 101
			MaxQuantity: 150
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.2
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"4": [
		{
			MinQuantity: 15
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Axe"
		},
	]

	"40": [
		{
			MinQuantity: 210
			MaxQuantity: 300
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"41": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"42": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
	]

	"43": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"44": [
		{
			MinQuantity: 85
			MaxQuantity: 175
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"45": [
		{
			MinQuantity: 55
			MaxQuantity: 100
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"46": [
		{
			MinQuantity: 260
			MaxQuantity: 350
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.05
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.01
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.01
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.02
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel3"
		},
	]

	"47": [
		{
			MinQuantity: 60
			MaxQuantity: 100
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.05
			Value:       #Groups.Gems
		},
	]

	"48": [
		{
			MinQuantity: 102
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.05
			Value:       #Groups.Gems
		},
	]

	"49": [
		{
			MinQuantity: 11
			MaxQuantity: 25
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
	]

	"5": [
		{
			MinQuantity: 510
			MaxQuantity: 1000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"50": [
		{
			MinQuantity: 13
			MaxQuantity: 70
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.Junk
		},
	]

	"51": [
		{
			MinQuantity: 12
			MaxQuantity: 40
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
	]

	"52": [
		{
			MinQuantity: 12
			MaxQuantity: 40
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
	]

	"53": [
		{
			MinQuantity: 13
			MaxQuantity: 55
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
	]

	"54": [
		{
			MinQuantity: 55
			MaxQuantity: 125
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Gems
		},
	]

	"55": [
		{
			MinQuantity: 58
			MaxQuantity: 170
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.02
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"56": [
		{
			MinQuantity: 160
			MaxQuantity: 300
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.05
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"57": [
		{
			MinQuantity: 60
			MaxQuantity: 100
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.02
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       #Groups.Circle6Scrolls
		},
	]

	"58": [
		{
			MinQuantity: 25
			MaxQuantity: 45
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Junk
		},
	]

	"59": [
		{
			MinQuantity: 203
			MaxQuantity: 350
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
	]

	"6": [
		{
			MinQuantity: 501
			MaxQuantity: 1500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.Reagents
		},
	]

	"60": [
		{
			MinQuantity: 201
			MaxQuantity: 400
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "MissingObject.Books"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel4"
		},
	]

	"61": [
		{
			MinQuantity: 82
			MaxQuantity: 120
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "Brimstone"
		},
	]

	"62": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Junk
		},
	]

	"63": [
		{
			MinQuantity: 81
			MaxQuantity: 160
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
	]

	"64": [
		{
			MinQuantity: 14
			MaxQuantity: 30
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "BreadLoaf"
		},
	]

	"65": [
		{
			MinQuantity: 125
			MaxQuantity: 350
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
	]

	"66": [
		{
			MinQuantity: 10
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel2"
		},
	]

	"67": [
		{
			MinQuantity: 110
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
	]

	"68": [
		{
			MinQuantity: 203
			MaxQuantity: 260
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
	]

	"69": [
		{
			MinQuantity: 10
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.25
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
	]

	"7": [
		{
			MinQuantity: 1001
			MaxQuantity: 2000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 4
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 3
			MaxQuantity: 18
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"70": [
		{
			MinQuantity: 10
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
	]

	"71": [
		{
			MinQuantity: 203
			MaxQuantity: 260
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
	]

	"72": [
		{
			MinQuantity: 60
			MaxQuantity: 100
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel1"
		},
	]

	"73": [
		{
			MinQuantity: 210
			MaxQuantity: 300
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.Junk
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "MissingObject.KewlMasks"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       "TreasureMapLevel3"
		},
	]

	"74": [
		{
			MinQuantity: 110
			MaxQuantity: 300
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "FertileDirt"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Ores
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.03
			Value:       "TreasureMapLevel4"
		},
	]

	"75": [
		{
			MinQuantity: 210
			MaxQuantity: 700
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.1
			Value:       #Groups.MagicWands78
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.2
			Value:       #Groups.MagicWands56
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel4"
		},
	]

	"76": [
		{
			MinQuantity: 10
			MaxQuantity: 200
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.15
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel2"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.05
			Value:       "TreasureMapLevel1"
		},
	]

	"77": [
		{
			MinQuantity: 110
			MaxQuantity: 600
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Junk
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       "EbonTwilightSapphireOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel3"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel4"
		},
	]

	"78": [
		{
			MinQuantity: 410
			MaxQuantity: 900
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.25
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.35
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Junk
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 3
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       "DarkSableRubyOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel5"
		},
	]

	"79": [
		{
			MinQuantity: 1010
			MaxQuantity: 1500
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.55
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Junk
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.3
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 6
			MaxQuantity: 18
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "DarkSableRubyOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.4
			Value:       "EbonTwilightSapphireOre"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.02
			Value:       "TreasureMapLevel4"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       "TreasureMapLevel5"
		},
	]

	"8": [
		{
			MinQuantity: 1501
			MaxQuantity: 3000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.5
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      0.75
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 3
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 4
			MaxQuantity: 9
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 4
			MaxQuantity: 12
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 4
			MaxQuantity: 24
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
	]

	"80": [
		{
			MinQuantity: 30
			MaxQuantity: 70
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.2
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 4
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.35
			Value:       "RadiantNimbusDiamondOre"
		},
	]

	"81": [
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       "Feather"
		},
		{
			MinQuantity: 210
			MaxQuantity: 700
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.04
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.1
			Value:       #Groups.PaganMagicItem
		},
	]

	"9": [
		{
			MinQuantity: 2001
			MaxQuantity: 4000
			Chance:      1
			Value:       "Gold"
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.5
			Value:       #Groups.PaganMagicItem
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Jewelry
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Clothes
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      0.5
			Value:       #Groups.MagicWeapons
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.5
			Value:       #Groups.NormalArmor
		},
		{
			MinQuantity: 2
			MaxQuantity: 6
			Chance:      0.5
			Value:       #Groups.NormalWeapons
		},
		{
			MinQuantity: 4
			MaxQuantity: 6
			Chance:      1
			Value:       #Groups.Ammo
		},
		{
			MinQuantity: 6
			MaxQuantity: 10
			Chance:      1
			Value:       #Groups.Gems
		},
		{
			MinQuantity: 5
			MaxQuantity: 15
			Chance:      1
			Value:       #Groups.PaganReagents
		},
		{
			MinQuantity: 5
			MaxQuantity: 30
			Chance:      1
			Value:       #Groups.Reagents
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      1
			Value:       #Groups.Circle8Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 2
			Chance:      1
			Value:       #Groups.Circle7Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 3
			Chance:      1
			Value:       #Groups.Circle6Scrolls
		},
		{
			MinQuantity: 1
			MaxQuantity: 1
			Chance:      0.01
			Value:       #Groups.Ammo
		},
	]

}
