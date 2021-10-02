package Types

#AutoLoopSettings: {
	Delay: number
}

#CraftResource: {
	ItemType: #Type
	Name:     #TextDefinition
	Amount:   int32
	Message:  #TextDefinition
}

#CraftEntry: {
	ItemType:       #Type
	Name:           #TextDefinition
	GroupName:      #TextDefinition
	Skill:          number
	SecondarySkill?: #SkillName
	Skill2?:        number
	Resources: [...#CraftResource]
	UseAllRes?: bool
	NeedHeat?:  bool
	NeedOven?:  bool
	NeedMill?:  bool
}

#CraftSettings: {
	MainSkill:   #SkillName
	GumpTitleId: #TextDefinition
	CraftEntries: [...#CraftEntry]
	MinCraftDelays: int32
	MaxCraftDelays: int32
	Delay:          number
	MinCraftChance: number
	CraftWorkSound: int32
	CraftEndSound:  int32
}

#ResourceSettings: {
	BankWidth:      int32
	BankHeight:     int32
	MinTotal:       int32
	MaxTotal:       int32
	MinRespawn:     number
	MaxRespawn:     number
	Skill:          #SkillName
	MaxRange:       int32
	MaxChance:      int32
	ResourceEffect: #ResourceEffect
	Messages:       #ResourceMessage
}

#ResourceEffect: {
	Actions: [...int]
	Sounds: [...int]
	Counts: [...int]
	Delay:      number
	SoundDelay: number
}

#ResourceMessage: {
	NoResourcesMessage:     #TextDefinition
	DoubleHarvestMessage?:   #TextDefinition
	TimedOutOfRangeMessage?: #TextDefinition
	OutOfRangeMessage:      #TextDefinition
	FailMessage:            #TextDefinition
	PackFullMessage:        #TextDefinition
	ToolBrokeMessage:       #TextDefinition
}

#OreSettings: {
	#ResourceSettings
	Entries: [...#OreEntry]
}

#LogSettings: {
	#ResourceSettings
	Entries: [...#LogEntry]
}

#HideSettings: {
	Entries: [...#HideEntry]
}

#FishSettings: {
	#ResourceSettings
	Entries: [...#FishEntry]
}

#EnchantmentEntry: {
	EnchantmentType:  #Type
	EnchantmentValue: int32 | null
}

#OreEntry: {
	Name:                 string
	ResourceType:         #Type
	SmeltType:            #Type
	HarvestSkillRequired: number
	SmeltSkillRequired:   number
	CraftSkillRequired:   number
	VeinChance:           number
	Hue:                  int32
	Quality:              number
	Enchantments: [...#EnchantmentEntry]
}

#LogEntry: {
	Name:                 string
	ResourceType:         #Type
	HarvestSkillRequired: number
	CraftSkillRequired:   number
	VeinChance:           number
	Hue:                  int32
	Quality:              number
}

#HideEntry: {
	Name:               string
	ResourceType:       #Type
	CraftSkillRequired: number
	Hue:                int32
	Quality:            number
	Enchantments: [...#EnchantmentEntry]
}

#FishEntry: {
	Name:                 string
	ResourceType:         #Type
	HarvestSkillRequired: number
	VeinChance:           number
}
