package Resources

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Sand: Types.#OreSettings & {

	BankWidth:  1
	BankHeight: 1
	MinTotal:   45
	MaxTotal:   90
	MinRespawn: 10.0
	MaxRespawn: 20.0
	Skill:      "Mining"
	MaxRange:   2
	MaxChance:  155
	ResourceEffect: {
		Actions: [11]
		Sounds: [66]
		Counts: [4]
		Delay:      1.6
		SoundDelay: 0.9
	}
	Messages: {
		NoResourcesMessage:     1044629 // There is no sand here to mine.
		DoubleHarvestMessage:   1044629 // here is no sand here to mine.
		TimedOutOfRangeMessage: 503041 // You have moved too far away to continue mining.
		OutOfRangeMessage:      500446 // That is too far away.
		FailMessage:            1044630 // You dig for a while but fail to find any of sufficient quality for glassblowing.
		PackFullMessage:        1044632 // Your backpack can't hold the sand, and it is lost!
		ToolBrokeMessage:       1044038 // You have worn out your tool!
	}
	Entries: [
		{
			Name:                 "units of sand"
			ResourceType:         "Sand"
			SmeltType:            "RawGlass"
			HarvestSkillRequired: 0.0
			SmeltSkillRequired:   0.0
			CraftSkillRequired:   0.0
			VeinChance:           100.0
			Hue:                  0
			Quality:              1.0
			Enchantments: []
		},
	]
}
