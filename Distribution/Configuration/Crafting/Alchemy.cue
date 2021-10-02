package Crafting

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Alchemy: Types.#CraftSettings & {
	MainSkill:      "Alchemy"
	GumpTitleId:    1044001 // <CENTER>ALCHEMY MENU</CENTER>
	MinCraftDelays: 4
	MaxCraftDelays: 4
	Delay:          1.0
	MinCraftChance: 0.0
	CraftWorkSound: 578
	CraftEndSound:  576
	CraftEntries: [
		{
			ItemType:  "RefreshPotion"
			Name:      1044538
			GroupName: 1044530
			Skill:     10
			Resources: [
				{
					ItemType: "BlackPearl"
					Name:     1044353
					Amount:   1
					Message:  1044361
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "TotalRefreshPotion"
			Name:      1044539
			GroupName: 1044530
			Skill:     50
			Resources: [
				{
					ItemType: "BlackPearl"
					Name:     1044353
					Amount:   5
					Message:  1044361
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "AgilityPotion"
			Name:      1044540
			GroupName: 1044531
			Skill:     30
			Resources: [
				{
					ItemType: "Bloodmoss"
					Name:     1044354
					Amount:   1
					Message:  1044362
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterAgilityPotion"
			Name:      1044541
			GroupName: 1044531
			Skill:     70
			Resources: [
				{
					ItemType: "Bloodmoss"
					Name:     1044354
					Amount:   3
					Message:  1044362
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "NightSightPotion"
			Name:      1044542
			GroupName: 1044532
			Skill:     10
			Resources: [
				{
					ItemType: "SpidersSilk"
					Name:     1044360
					Amount:   1
					Message:  1044368
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "LesserHealPotion"
			Name:      1044543
			GroupName: 1044533
			Skill:     15
			Resources: [
				{
					ItemType: "Ginseng"
					Name:     1044356
					Amount:   1
					Message:  1044364
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "HealPotion"
			Name:      1044544
			GroupName: 1044533
			Skill:     60
			Resources: [
				{
					ItemType: "Ginseng"
					Name:     1044356
					Amount:   3
					Message:  1044364
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterHealPotion"
			Name:      1044545
			GroupName: 1044533
			Skill:     85
			Resources: [
				{
					ItemType: "Ginseng"
					Name:     1044356
					Amount:   7
					Message:  1044364
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "StrengthPotion"
			Name:      1044546
			GroupName: 1044534
			Skill:     30
			Resources: [
				{
					ItemType: "MandrakeRoot"
					Name:     1044357
					Amount:   2
					Message:  1044365
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterStrengthPotion"
			Name:      1044547
			GroupName: 1044534
			Skill:     70
			Resources: [
				{
					ItemType: "MandrakeRoot"
					Name:     1044357
					Amount:   5
					Message:  1044365
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "LesserPoisonPotion"
			Name:      1044548
			GroupName: 1044535
			Skill:     15
			Resources: [
				{
					ItemType: "Nightshade"
					Name:     1044358
					Amount:   1
					Message:  1044366
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "PoisonPotion"
			Name:      1044549
			GroupName: 1044535
			Skill:     50
			Resources: [
				{
					ItemType: "Nightshade"
					Name:     1044358
					Amount:   2
					Message:  1044366
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterPoisonPotion"
			Name:      1044550
			GroupName: 1044535
			Skill:     90
			Resources: [
				{
					ItemType: "Nightshade"
					Name:     1044358
					Amount:   4
					Message:  1044366
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "DeadlyPoisonPotion"
			Name:      1044551
			GroupName: 1044535
			Skill:     100
			Resources: [
				{
					ItemType: "Nightshade"
					Name:     1044358
					Amount:   8
					Message:  1044366
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "LesserCurePotion"
			Name:      1044552
			GroupName: 1044536
			Skill:     15
			Resources: [
				{
					ItemType: "Garlic"
					Name:     1044355
					Amount:   1
					Message:  1044363
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "CurePotion"
			Name:      1044553
			GroupName: 1044536
			Skill:     50
			Resources: [
				{
					ItemType: "Garlic"
					Name:     1044355
					Amount:   3
					Message:  1044363
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterCurePotion"
			Name:      1044554
			GroupName: 1044536
			Skill:     90
			Resources: [
				{
					ItemType: "Garlic"
					Name:     1044355
					Amount:   6
					Message:  1044363
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "LesserExplosionPotion"
			Name:      1044555
			GroupName: 1044537
			Skill:     25
			Resources: [
				{
					ItemType: "SulfurousAsh"
					Name:     1044359
					Amount:   3
					Message:  1044367
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "ExplosionPotion"
			Name:      1044556
			GroupName: 1044537
			Skill:     70
			Resources: [
				{
					ItemType: "SulfurousAsh"
					Name:     1044359
					Amount:   5
					Message:  1044367
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
		{
			ItemType:  "GreaterExplosionPotion"
			Name:      1044557
			GroupName: 1044537
			Skill:     90
			Resources: [
				{
					ItemType: "SulfurousAsh"
					Name:     1044359
					Amount:   10
					Message:  1044367
				},
				{
					ItemType: "Bottle"
					Name:     1044529
					Amount:   1
					Message:  500315
				},
			]
		},
	]
}
