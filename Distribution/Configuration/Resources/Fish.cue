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
		NoResourcesMessage:     503172
		DoubleHarvestMessage:   1044629
		TimedOutOfRangeMessage: 500976
		OutOfRangeMessage:      500976
		FailMessage:            503171
		PackFullMessage:        503176
		ToolBrokeMessage:       503174
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
