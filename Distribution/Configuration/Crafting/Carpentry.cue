package Crafting

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Carpentry: Types.#CraftSettings & {
	MainSkill:      "Carpentry"
	GumpTitleId:    1044004
	MinCraftDelays: 3
	MaxCraftDelays: 3
	Delay:          2.0
	MinCraftChance: 0
	CraftWorkSound: 573
	CraftEndSound:  573
	CraftEntries: [
		{
			ItemType:  "FootStoolDeed"
			Name:      1022910
			GroupName: 1015076
			Skill:     28
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "StoolDeed"
			Name:      1022602
			GroupName: 1015076
			Skill:     68
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   21
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenThroneDeed"
			Name:      1044304
			GroupName: 1015076
			Skill:     15
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   12
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "ThroneDeed"
			Name:      1044305
			GroupName: 1015076
			Skill:     84
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   19
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "FancyWoodenChairCushionDeed"
			Name:      1044302
			GroupName: 1015076
			Skill:     52
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenChairCushionDeed"
			Name:      1044303
			GroupName: 1015076
			Skill:     52
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenChairDeed"
			Name:      1044301
			GroupName: 1015076
			Skill:     31
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   13
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "BambooChairDeed"
			Name:      1044300
			GroupName: 1015076
			Skill:     31
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   13
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBenchDeed"
			Name:      1022860
			GroupName: 1015076
			Skill:     10
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "LoomBenchDeed"
			Name:      1024169
			GroupName: 1015076
			Skill:     10
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench2EastDeed"
			Name:      "long wooden bench (east)"
			GroupName: 1015076
			Skill:     63
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   17
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench2SouthDeed"
			Name:      "long wooden bench (south)"
			GroupName: 1015076
			Skill:     63
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   17
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench3EastDeed"
			Name:      "long wooden bench 2 (east)"
			GroupName: 1015076
			Skill:     83
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench3SouthDeed"
			Name:      "long wooden bench 2 (south)"
			GroupName: 1015076
			Skill:     83
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench4EastDeed"
			Name:      "wooden booth (east)"
			GroupName: 1015076
			Skill:     95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   35
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenBench4SouthDeed"
			Name:      "wooden booth (south)"
			GroupName: 1015076
			Skill:     95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   35
					Message:  1044351
				},
			]
		},
		{
			ItemType:       "StoneBenchEastDeed"
			Name:           "stone bench (east)"
			GroupName:      1015076
			Skill:          50
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   70
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StoneBenchSouthDeed"
			Name:           "stone bench (south)"
			GroupName:      1015076
			Skill:          50
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   70
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StoneChairDeed"
			Name:           1024635
			GroupName:      1015076
			Skill:          40
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   70
					Message:  1044037
				},
			]
		},
		{
			ItemType:  "WoodenBox"
			Name:      1023709
			GroupName: 1044292
			Skill:     44
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "SmallCrate"
			Name:      1044309
			GroupName: 1044292
			Skill:     20
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   8
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "MediumCrate"
			Name:      1044310
			GroupName: 1044292
			Skill:     41
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "LargeCrate"
			Name:      1044311
			GroupName: 1044292
			Skill:     57
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   18
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenChest"
			Name:      1023650
			GroupName: 1044292
			Skill:     84
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "EmptyBookcase"
			Name:      1022718
			GroupName: 1044292
			Skill:     42
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "FullBookcase"
			Name:      1022718
			GroupName: 1044292
			Skill:     42
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "FancyArmoire"
			Name:      1044312
			GroupName: 1044292
			Skill:     94
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   35
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Armoire"
			Name:      1022643
			GroupName: 1044292
			Skill:     94
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   35
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "PicnicBasket"
			Name:      1023706
			GroupName: 1044292
			Skill:     15
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   3
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Keg"
			Name:      1023711
			GroupName: 1044292
			Skill:     75
			Resources: [
				{
					ItemType: "BarrelStaves"
					Name:     1044288
					Amount:   3
					Message:  1044253
				},
				{
					ItemType: "BarrelHoops"
					Name:     1044289
					Amount:   1
					Message:  1044253
				},
				{
					ItemType: "BarrelLid"
					Name:     1044251
					Amount:   1
					Message:  1044253
				},
			]
		},
		{
			ItemType:  "DresserDeed"
			Name:      1022620
			GroupName: 1015086
			Skill:     100
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "NightstandDeed"
			Name:      1044306
			GroupName: 1015086
			Skill:     52
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   17
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WritingTableDeed"
			Name:      1022890
			GroupName: 1015086
			Skill:     73
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   17
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "YewWoodTableDeed"
			Name:      1044308
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "LargeTableDeed"
			Name:      1044307
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "TableEastDeed"
			Name:      "table (east)"
			GroupName: 1015086
			Skill:     75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "TableSouthDeed"
			Name:      "table (south)"
			GroupName: 1015086
			Skill:     75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table2EastDeed"
			Name:      "table 2 (east)"
			GroupName: 1015086
			Skill:     75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table2SouthDeed"
			Name:      "table 2 (south)"
			GroupName: 1015086
			Skill:     75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table3EastDeed"
			Name:      "table 3 (east)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table3SouthDeed"
			Name:      "table 3 (south)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table4EastDeed"
			Name:      "table 4 (east)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table4SouthDeed"
			Name:      "table 4 (south)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table5EastDeed"
			Name:      "table 5 (east)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "Table5SouthDeed"
			Name:      "table 5 (south)"
			GroupName: 1015086
			Skill:     85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   50
					Message:  1044351
				},
			]
		},
		{
			ItemType:       "LargeStoneTableEastDeed"
			Name:           1044511
			GroupName:      1015086
			Skill:          60
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   85
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "LargeStoneTableSouthDeed"
			Name:           1044512
			GroupName:      1015086
			Skill:          60
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   85
					Message:  1044037
				},
			]
		},
		{
			ItemType:  "BlackStaff"
			Name:      1023568
			GroupName: 1044295
			Skill:     94
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "ShepherdsCrook"
			Name:      1023713
			GroupName: 1044295
			Skill:     89
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   7
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "QuarterStaff"
			Name:      1023721
			GroupName: 1044295
			Skill:     84
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   6
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "GnarledStaff"
			Name:      1025112
			GroupName: 1044295
			Skill:     89
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   7
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenKiteShield"
			Name:      1027032
			GroupName: 1044295
			Skill:     62
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   9
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WoodenShield"
			Name:      1027034
			GroupName: 1044295
			Skill:     62
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   9
					Message:  1044351
				},
			]
		},
		{
			ItemType:       "SmallForgeDeed"
			Name:           1044330
			GroupName:      1044296
			Skill:          84
			SecondarySkill: "Blacksmith"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   75
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "LargeForgeEastDeed"
			Name:           1044331
			GroupName:      1044296
			Skill:          84
			SecondarySkill: "Blacksmith"
			Skill2:         90
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   100
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "LargeForgeSouthDeed"
			Name:           1044332
			GroupName:      1044296
			Skill:          84
			SecondarySkill: "Blacksmith"
			Skill2:         90
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   100
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "AnvilEastDeed"
			Name:           1044333
			GroupName:      1044296
			Skill:          84
			SecondarySkill: "Blacksmith"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   150
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "AnvilSouthDeed"
			Name:           1044334
			GroupName:      1044296
			Skill:          84
			SecondarySkill: "Blacksmith"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   150
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "FishingPole"
			Name:           1023519
			GroupName:      1044297
			Skill:          78
			SecondarySkill: "Tailoring"
			Skill2:         50
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   5
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "TrainingDummyEastDeed"
			Name:           1044335
			GroupName:      1044297
			Skill:          78
			SecondarySkill: "Tailoring"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   55
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   60
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "TrainingDummySouthDeed"
			Name:           1044336
			GroupName:      1044297
			Skill:          78
			SecondarySkill: "Tailoring"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   55
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   60
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "PickpocketDipEastDeed"
			Name:           1044337
			GroupName:      1044297
			Skill:          84
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   65
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   60
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "PickpocketDipSouthDeed"
			Name:           1044338
			GroupName:      1044297
			Skill:          84
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   65
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   60
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "DressformDeed"
			Name:           1044339
			GroupName:      1044298
			Skill:          73
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   10
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "SpinningwheelEastDeed"
			Name:           1044341
			GroupName:      1044298
			Skill:          84
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   75
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   25
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "SpinningwheelSouthDeed"
			Name:           1044342
			GroupName:      1044298
			Skill:          84
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   75
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   25
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "LoomEastDeed"
			Name:           1044343
			GroupName:      1044298
			Skill:          94
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   25
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "LoomSouthDeed"
			Name:           1044344
			GroupName:      1044298
			Skill:          94
			SecondarySkill: "Tailoring"
			Skill2:         75
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   25
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Drums"
			Name:           1023740
			GroupName:      1044293
			Skill:          68
			SecondarySkill: "Tailoring"
			Skill2:         55
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   10
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Tambourine"
			Name:           1023742
			GroupName:      1044293
			Skill:          68
			SecondarySkill: "Tailoring"
			Skill2:         55
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   15
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   10
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Harp"
			Name:           1023761
			GroupName:      1044293
			Skill:          89
			SecondarySkill: "Tailoring"
			Skill2:         55
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   35
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   15
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "LapHarp"
			Name:           1023762
			GroupName:      1044293
			Skill:          73
			SecondarySkill: "Tailoring"
			Skill2:         55
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   10
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Lute"
			Name:           1023763
			GroupName:      1044293
			Skill:          78
			SecondarySkill: "Tailoring"
			Skill2:         55
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   25
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   10
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "StoneOvenEastDeed"
			Name:           1044345
			GroupName:      1044299
			Skill:          78
			SecondarySkill: "Tinkering"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StoneOvenSouthDeed"
			Name:           1044346
			GroupName:      1044299
			Skill:          78
			SecondarySkill: "Tinkering"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "FlourMillEastDeed"
			Name:           1044347
			GroupName:      1044299
			Skill:          105
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   50
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "FlourMillSouthDeed"
			Name:           1044348
			GroupName:      1044299
			Skill:          105
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   50
					Message:  1044037
				},
			]
		},
		{
			ItemType:  "WaterTroughEastDeed"
			Name:      1044349
			GroupName: 1044299
			Skill:     105
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   150
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "WaterTroughSouthDeed"
			Name:      1044350
			GroupName: 1044299
			Skill:     105
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   150
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "BarrelStaves"
			Name:      1027857
			GroupName: 1044294
			Skill:     2
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "BarrelLid"
			Name:      1027608
			GroupName: 1044294
			Skill:     4
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   4
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "TallMusicStandDeed"
			Name:      1044315
			GroupName: 1044294
			Skill:     91
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   20
					Message:  1044351
				},
			]
		},
		{
			ItemType:       "StoneFireplaceEastDeed"
			Name:           1061848
			GroupName:      1044294
			Skill:          75
			SecondarySkill: "Tinkering"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StoneFireplaceSouthDeed"
			Name:           1061849
			GroupName:      1044294
			Skill:          75
			SecondarySkill: "Tinkering"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   85
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "WaterVatDeed"
			Name:           1025460
			GroupName:      1044294
			Skill:          80
			SecondarySkill: "Tinkering"
			Skill2:         50
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   75
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   15
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "SmallBedSouthDeed"
			Name:           1044321
			GroupName:      1044294
			Skill:          105
			SecondarySkill: "Tailoring"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   100
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "SmallBedEastDeed"
			Name:           1044322
			GroupName:      1044294
			Skill:          105
			SecondarySkill: "Tailoring"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   100
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "LargeBedSouthDeed"
			Name:           1044323
			GroupName:      1044294
			Skill:          105
			SecondarySkill: "Tailoring"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   150
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   150
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "LargeBedEastDeed"
			Name:           1044324
			GroupName:      1044294
			Skill:          105
			SecondarySkill: "Tailoring"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   150
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   150
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Painting1EastDeed"
			Name:           "painting (east)"
			GroupName:      1044294
			Skill:          50
			SecondarySkill: "Tailoring"
			Skill2:         30
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   5
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Painting1SouthDeed"
			Name:           "painting (south)"
			GroupName:      1044294
			Skill:          50
			SecondarySkill: "Tailoring"
			Skill2:         30
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   5
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Painting2EastDeed"
			Name:           "painting 2 (east)"
			GroupName:      1044294
			Skill:          50
			SecondarySkill: "Tailoring"
			Skill2:         30
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   5
					Message:  1044287
				},
			]
		},
		{
			ItemType:       "Painting2SouthDeed"
			Name:           "painting 2 (south)"
			GroupName:      1044294
			Skill:          50
			SecondarySkill: "Tailoring"
			Skill2:         30
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
				{
					ItemType: "Cloth"
					Name:     1044286
					Amount:   5
					Message:  1044287
				},
			]
		},
		{
			ItemType:  "DartBoardSouthDeed"
			Name:      1044325
			GroupName: 1044294
			Skill:     26
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
			]
		},
		{
			ItemType:  "DartBoardEastDeed"
			Name:      1044326
			GroupName: 1044294
			Skill:     26
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   5
					Message:  1044351
				},
			]
		},
		{
			ItemType:       "PentagramDeed"
			Name:           1044328
			GroupName:      1044294
			Skill:          110
			SecondarySkill: "Magery"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   40
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "AltarDeed"
			Name:           1024628
			GroupName:      1044294
			Skill:          110
			SecondarySkill: "Magery"
			Skill2:         60
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   100
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   40
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StatueEastDeed"
			Name:           "statue (east)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "StatueSouthDeed"
			Name:           "statue (south)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "Statue2EastDeed"
			Name:           "statue 2 (east)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "Statue2SouthDeed"
			Name:           "statue 2 (south)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         95
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   125
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "Statue3EastDeed"
			Name:           "statue 3 (east)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   100
					Message:  1044037
				},
			]
		},
		{
			ItemType:       "Statue3SouthDeed"
			Name:           "statue 3 (south)"
			GroupName:      1044294
			Skill:          95
			SecondarySkill: "Tinkering"
			Skill2:         85
			Resources: [
				{
					ItemType: "Log"
					Name:     1044041
					Amount:   10
					Message:  1044351
				},
				{
					ItemType: "IronIngot"
					Name:     1044036
					Amount:   100
					Message:  1044037
				},
			]
		},
	]
}
