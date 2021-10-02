package Resources

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Fish: Types.#FishSettings & {
	BankWidth:  1
	BankHeight: 1
	MinTotal:   5
	MaxTotal:   25
	MinRespawn: 10.0
	MaxRespawn: 20.0
	Skill:      "Fishing"
	MaxRange:   6
	MaxChance:  0
	ResourceEffect: {
		Actions: [12]
		Sounds: [0]
		Counts: [1]
		Delay:      2.0
		SoundDelay: 8.0
	}
	Messages: {
		NoResourcesMessage:     503172  // The fish don't seem to be biting here.
		DoubleHarvestMessage:   1044629 // There is no sand here to mine.
		TimedOutOfRangeMessage: 500976  // You need to be closer to the water to fish!
		OutOfRangeMessage:      500976  // You need to be closer to the water to fish!
		FailMessage:            503171  // You fish a while, but fail to catch anything.
		PackFullMessage:        503176  // You do not have room in your backpack for a fish.
		ToolBrokeMessage:       503174  // You broke your fishing pole.
	}
	Entries: [
		{
			Name:                 "fish"
			ResourceType:         "Fish"
			HarvestSkillRequired: 0.0
			VeinChance:           100.0
		},
	]
}
