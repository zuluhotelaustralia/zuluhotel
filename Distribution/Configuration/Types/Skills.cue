package Types

#StatAdvancement: {
	Chance:  number
	MinGain: int32
	MaxGain: int32
}

#SkillSettings: {
	MaxStatCap: int32
	StatCap:    int32
	Entries: {
		[#SkillName]: #SkillEntry
	}
}
#SkillEntry: {
	OnUseHandler?:  #Type
	Delay:         #TimeSpan
	DefaultPoints: int32
	StatAdvancements: {
		#StatType: [...#StatAdvancement]
	}
}
