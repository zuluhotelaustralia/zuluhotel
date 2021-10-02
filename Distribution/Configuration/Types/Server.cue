package Types

#CoreConfiguration: {
    Expansion: #Expansion
    InsuranceEnabled: bool
    ActionDelay: int32
}
#EmailConfiguration: {
    Enabled: *false | bool
    FromAddress: string
    FromName: string
    CrashAddress: string
    CrashName: string
    SpeechLogPageAddress: string
    SpeechLogPageName: string
    EmailServer: string
    EmailPort: int32
    EmailUsername: string
    EmailPassword: string
    EmailSendRetryCount: int32
    EmailSendRetryDelay: int32
}
#MessagingConfiguration: {
    SuccessHue: int32
    FailureHue: int32
    VisibleDamage: bool
    ObjectPropertyList: bool
    GuildClickMessage: bool
    AsciiClickMessage: bool
    SingleClickProps: bool
    StaffRevealMagicItems: bool
}

#SkillName:
	"Alchemy" |
	"Anatomy" |
	"AnimalLore" |
	"ItemID" |
	"ArmsLore" |
	"Parry" |
	"Begging" |
	"Blacksmith" |
	"Fletching" |
	"Peacemaking" |
	"Camping" |
	"Carpentry" |
	"Cartography" |
	"Cooking" |
	"DetectHidden" |
	"Discordance" |
	"EvalInt" |
	"Healing" |
	"Fishing" |
	"Forensics" |
	"Herding" |
	"Hiding" |
	"Provocation" |
	"Inscribe" |
	"Lockpicking" |
	"Magery" |
	"MagicResist" |
	"Tactics" |
	"Snooping" |
	"Musicianship" |
	"Poisoning" |
	"Archery" |
	"SpiritSpeak" |
	"Stealing" |
	"Tailoring" |
	"AnimalTaming" |
	"TasteID" |
	"Tinkering" |
	"Tracking" |
	"Veterinary" |
	"Swords" |
	"Macing" |
	"Fencing" |
	"Wrestling" |
	"Lumberjacking" |
	"Mining" |
	"Meditation" |
	"Stealth" |
	"RemoveTrap" |
	"Necromancy" |
	"Focus" |
	"Chivalry" |
	"Bushido" |
	"Ninjitsu" |
	"Spellweaving" |
	"Mysticism" |
	"Imbuing" |
	"Throwing"

#Poison: "Lesser" | "Regular" | "Greater" | "Deadly" | "Lethal" | null

#Body: int & >=0

#ResistanceType: "None" |
	"Water" |
	"Air" |
	"Physical" |
	"Fire" |
	"Poison" |
	"Earth" |
	"Necro" |
	"Paralysis" |
	"HealingBonus" |
	"MagicImmunity" |
	"MagicReflection"

#Race: "Human" | null

#Expansion:  "None" | 
		"T2A" |
    "UOR" |
    "UOTD" |
    "LBR" |
    "AOS" |
    "SE" |
    "ML" |
    "SA" |
    "HS" |
    "TOL" |
    "EJ"