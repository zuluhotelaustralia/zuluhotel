import (
	Creatures "github.com/zuluhotelaustralia/zuluhotel/Creatures"
	Crafting "github.com/zuluhotelaustralia/zuluhotel/Crafting"
	Resources "github.com/zuluhotelaustralia/zuluhotel/Resources"
	Loot "github.com/zuluhotelaustralia/zuluhotel/Loot"
	Magic "github.com/zuluhotelaustralia/zuluhotel/Magic"
	Skills "github.com/zuluhotelaustralia/zuluhotel/Skills"
)

Core: {
	Expansion: "T2A"
	InsuranceEnabled: false
	ActionDelay: 500
}

Messaging: {
	SuccessHue:            55
	FailureHue:            33
	VisibleDamage:         true
	StaffRevealMagicItems: true
	ObjectPropertyList:    true
	GuildClickMessage:     true
	AsciiClickMessage:     true
	SingleClickProps:      true
}

Email: {
	Enabled:              false
	FromAddress:          "support@modernuo.com"
	FromName:             "ModernUO Team"
	CrashAddress:         "crashes@modernuo.com"
	CrashName:            "Crash Log"
	SpeechLogPageAddress: "support@modernuo.com"
	SpeechLogPageName:    "GM Support Conversation"
	EmailServer:          "smtp.gmail.com"
	EmailPort:            465
	EmailUsername:        "support@modernuo.com"
	EmailPassword:        "Some Password 123"
	EmailSendRetryCount:  5
	EmailSendRetryDelay:  3
}

Creatures: {
	Entries: Creatures
}

Loot: {
	Loot
}

Crafting: {
	Crafting
}

Resources: {
	Crafting
}

Magic: {
	Magic
}

Skills: {
	Skills
}