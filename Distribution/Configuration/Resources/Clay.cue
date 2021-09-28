package Resources

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Clay: Types.#OreSettings & {
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
		Actions: [ 11]
		Sounds: [ 66]
		Counts: [ 4]
		Delay:      1.6
		SoundDelay: 0.9
	}
	Messages: {
		NoResourcesMessage:     "There is no clay here to mine."
		DoubleHarvestMessage:   "There is no clay here to mine."
		TimedOutOfRangeMessage: 503041
		OutOfRangeMessage:      500446
		FailMessage:            "You dig for a while but fail to find any clay."
		PackFullMessage:        "Your backpack can't hold the clay, and it is lost!"
		ToolBrokeMessage:       1044038
	}
	Entries: [
		{
			Name:                 "blocks of clay"
			ResourceType:         "Clay"
			SmeltType:            null
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
