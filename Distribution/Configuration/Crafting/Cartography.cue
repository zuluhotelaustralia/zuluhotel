package Crafting
import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Cartography: Types.#CraftSettings & {
	MainSkill:      "Cartography"
	GumpTitleId:    1044008
	MinCraftDelays: 3
	MaxCraftDelays: 3
	Delay:          2.0
	MinCraftChance: 0
	CraftWorkSound: 585
	CraftEndSound:  585
	CraftEntries: [
		{
			ItemType:  "LocalMap"
			Name:      1015230
			GroupName: 1044448
			Skill:     10
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "CityMap"
			Name:      1015231
			GroupName: 1044448
			Skill:     25
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "SeaChart"
			Name:      1015232
			GroupName: 1044448
			Skill:     35
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "WorldMap"
			Name:      1015233
			GroupName: 1044448
			Skill:     39.5
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "LocalMap"
			Name:      1015230
			GroupName: 1044448
			Skill:     10
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "CityMap"
			Name:      1015231
			GroupName: 1044448
			Skill:     25
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "SeaChart"
			Name:      1015232
			GroupName: 1044448
			Skill:     35
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
		{
			ItemType:  "WorldMap"
			Name:      1015233
			GroupName: 1044448
			Skill:     39.5
			Resources: [
				{
					ItemType: "BlankMap"
					Name:     1044449
					Amount:   1
					Message:  1044450
				},
			]
		},
	]

}
