package Skills

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Types.#SkillSettings

MaxStatCap: 60000
StatCap:    1300
Entries: {
	Anatomy: {
		OnUseHandler:  "Anatomy"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.8
			MinGain: 9
			MaxGain: 21
		}
	}
	Fencing: {
		Delay:         "00:00:00"
		DefaultPoints: 20
		StatAdvancements: {
			Str: {
				Chance:  0.4
				MinGain: 4
				MaxGain: 16
			}
			Dex: {
				Chance:  0.6
				MinGain: 4
				MaxGain: 24
			}
		}
	}
	Healing: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: {
			Int: {
				Chance:  0.6
				MinGain: 14
				MaxGain: 26
			}
			Dex: {
				Chance:  0.3
				MinGain: 3
				MaxGain: 12
			}
		}
	}
	Macing: {
		Delay:         "00:00:00"
		DefaultPoints: 20
		StatAdvancements: Str: {
			Chance:  0.65
			MinGain: 7
			MaxGain: 31
		}
	}
	Parry: {
		Delay:         "00:00:10"
		DefaultPoints: 20
		StatAdvancements: {
			Str: {
				Chance:  0.7
				MinGain: 1
				MaxGain: 4
			}
			Dex: {
				Chance:  0.6
				MinGain: 2
				MaxGain: 6
			}
		}
	}
	Swords: {
		Delay:         "00:00:00"
		DefaultPoints: 20
		StatAdvancements: {
			Str: {
				Chance:  0.5
				MinGain: 4
				MaxGain: 24
			}
			Dex: {
				Chance:  0.4
				MinGain: 9
				MaxGain: 21
			}
		}
	}
	Tactics: {
		Delay:         "00:00:00"
		DefaultPoints: 15
		StatAdvancements: Dex: {
			Chance:  0.5
			MinGain: 7
			MaxGain: 13
		}
	}
	Wrestling: {
		Delay:         "00:00:00"
		DefaultPoints: 20
		StatAdvancements: {
			Str: {
				Chance:  0.7
				MinGain: 8
				MaxGain: 32
			}
			Dex: {
				Chance:  0.3
				MinGain: 7
				MaxGain: 56
			}
		}
	}
	Alchemy: {
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  1
			MinGain: 14
			MaxGain: 30
		}
	}
	EvalInt: {
		OnUseHandler:  "EvalInt"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.9
			MinGain: 25
			MaxGain: 50
		}
	}
	Inscribe: {
		OnUseHandler:  "Inscribe"
		Delay:         "00:00:10"
		DefaultPoints: 600
		StatAdvancements: Int: {
			Chance:  0.6
			MinGain: 14
			MaxGain: 34
		}
	}
	ItemID: {
		OnUseHandler:  "ItemId"
		Delay:         "00:00:04"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.7
			MinGain: 20
			MaxGain: 40
		}
	}
	Magery: {
		Delay:         "00:00:00"
		DefaultPoints: 300
		StatAdvancements: Int: {
			Chance:  1
			MinGain: 23
			MaxGain: 58
		}
	}
	MagicResist: {
		Delay:         "00:00:00"
		DefaultPoints: 50
		StatAdvancements: Int: {
			Chance:  0.5
			MinGain: 18
			MaxGain: 27
		}
	}
	Meditation: {
		OnUseHandler:  "Meditation"
		Delay:         "00:00:10"
		DefaultPoints: 250
		StatAdvancements: Int: {
			Chance:  0.7
			MinGain: 20
			MaxGain: 40
		}
	}
	SpiritSpeak: {
		OnUseHandler:  "SpiritSpeak"
		Delay:         "00:00:10"
		DefaultPoints: 20
		StatAdvancements: Int: {
			Chance:  1
			MinGain: 40
			MaxGain: 75
		}
	}
	AnimalLore: {
		OnUseHandler:  "AnimalLore"
		Delay:         "00:00:05"
		DefaultPoints: 250
		StatAdvancements: {}
	}
	AnimalTaming: {
		OnUseHandler:  "AnimalTaming"
		Delay:         "00:00:10"
		DefaultPoints: 25
		StatAdvancements: Int: {
			Chance:  0.8
			MinGain: 14
			MaxGain: 26
		}
	}
	Archery: {
		Delay:         "00:00:00"
		DefaultPoints: 20
		StatAdvancements: {
			Str: {
				Chance:  0.5
				MinGain: 4
				MaxGain: 16
			}
			Dex: {
				Chance:  0.6
				MinGain: 12
				MaxGain: 32
			}
		}
	}
	Camping: {
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: {
			Int: {
				Chance:  0.1
				MinGain: 1
				MaxGain: 6
			}
			Dex: {
				Chance:  0.15
				MinGain: 1
				MaxGain: 8
			}
		}
	}
	Cooking: {
		Delay:         "00:00:10"
		DefaultPoints: 300
		StatAdvancements: {
			Int: {
				Chance:  0.3
				MinGain: 3
				MaxGain: 12
			}
			Dex: {
				Chance:  0.5
				MinGain: 3
				MaxGain: 12
			}
		}
	}
	Fishing: {
		Delay:         "00:00:05"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  0.8
			MinGain: 12
			MaxGain: 24
		}
	}
	Tracking: {
		OnUseHandler:  "Tracking"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: {
			Int: {
				Chance:  0.7
				MinGain: 9
				MaxGain: 13
			}
			Dex: {
				Chance:  0.7
				MinGain: 23
				MaxGain: 31
			}
		}
	}
	Veterinary: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: Int: {
			Chance:  0.2
			MinGain: 3
			MaxGain: 12
		}
	}
	ArmsLore: {
		OnUseHandler:  "ArmsLore"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: {
			Str: {
				Chance:  0.5
				MinGain: 3
				MaxGain: 12
			}
			Int: {
				Chance:  0.7
				MinGain: 9
				MaxGain: 21
			}
		}
	}
	Blacksmith: {
		Delay:         "00:00:10"
		DefaultPoints: 30
		StatAdvancements: Str: {
			Chance:  0.8
			MinGain: 14
			MaxGain: 34
		}
	}
	Carpentry: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: Str: {
			Chance:  0.8
			MinGain: 8
			MaxGain: 23
		}
	}
	Fletching: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: {
			Str: {
				Chance:  0.6
				MinGain: 11
				MaxGain: 20
			}
			Dex: {
				Chance:  0.6
				MinGain: 11
				MaxGain: 26
			}
		}
	}
	Lumberjacking: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: Str: {
			Chance:  0.8
			MinGain: 4
			MaxGain: 24
		}
	}
	Mining: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: Str: {
			Chance:  0.5
			MinGain: 8
			MaxGain: 17
		}
	}
	Tailoring: {
		Delay:         "00:00:10"
		DefaultPoints: 50
		StatAdvancements: Dex: {
			Chance:  0.5
			MinGain: 8
			MaxGain: 17
		}
	}
	Tinkering: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: {
			Str: {
				Chance:  0.1
				MinGain: 12
				MaxGain: 18
			}
			Dex: {
				Chance:  0.6
				MinGain: 14
				MaxGain: 34
			}
		}
	}
	Begging: {
		OnUseHandler:  "Begging"
		Delay:         "00:00:20"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.8
			MinGain: 9
			MaxGain: 21
		}
	}
	Cartography: {
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.6
			MinGain: 8
			MaxGain: 17
		}
	}
	Discordance: {
		OnUseHandler:  "Discordance"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.15
			MinGain: 1
			MaxGain: 8
		}
	}
	Herding: {
		Delay:         "00:00:00"
		DefaultPoints: 100
		StatAdvancements: {}
	}
	Musicianship: {
		Delay:         "00:00:15"
		DefaultPoints: 20
		StatAdvancements: {
			Int: {
				Chance:  0.8
				MinGain: 3
				MaxGain: 12
			}
			Dex: {
				Chance:  0.7
				MinGain: 11
				MaxGain: 23
			}
		}
	}
	Peacemaking: {
		OnUseHandler:  "Peacemaking"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.15
			MinGain: 1
			MaxGain: 8
		}
	}
	Provocation: {
		OnUseHandler:  "Provocation"
		Delay:         "00:00:10"
		DefaultPoints: 20
		StatAdvancements: Dex: {
			Chance:  0.5
			MinGain: 4
			MaxGain: 40
		}
	}
	TasteID: {
		OnUseHandler:  "TasteId"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.85
			MinGain: 19
			MaxGain: 31
		}
	}
	Hiding: {
		OnUseHandler:  "Hiding"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  0.7
			MinGain: 14
			MaxGain: 34
		}
	}
	Lockpicking: {
		Delay:         "00:00:10"
		DefaultPoints: 100
		StatAdvancements: Dex: {
			Chance:  0.8
			MinGain: 15
			MaxGain: 24
		}
	}
	Poisoning: {
		OnUseHandler:  "Poisoning"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: {
			Int: {
				Chance:  0.6
				MinGain: 35
				MaxGain: 50
			}
			Dex: {
				Chance:  0.8
				MinGain: 45
				MaxGain: 70
			}
		}
	}
	RemoveTrap: {
		OnUseHandler:  "RemoveTrap"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  0.9
			MinGain: 20
			MaxGain: 55
		}
	}
	Snooping: {
		Delay:         "00:00:05"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  0.8
			MinGain: 14
			MaxGain: 34
		}
	}
	Stealing: {
		OnUseHandler:  "Stealing"
		Delay:         "00:00:15"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  0.9
			MinGain: 12
			MaxGain: 21
		}
	}
	Stealth: {
		OnUseHandler:  "Stealth"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: Dex: {
			Chance:  1
			MinGain: 51
			MaxGain: 81
		}
	}
	DetectHidden: {
		OnUseHandler:  "DetectHidden"
		Delay:         "00:00:10"
		DefaultPoints: 200
		StatAdvancements: {
			Int: {
				Chance:  0.4
				MinGain: 3
				MaxGain: 18
			}
			Dex: {
				Chance:  0.8
				MinGain: 13
				MaxGain: 25
			}
		}
	}
	Forensics: {
		OnUseHandler:  "Forensics"
		Delay:         "00:00:15"
		DefaultPoints: 200
		StatAdvancements: Int: {
			Chance:  0.9
			MinGain: 16
			MaxGain: 28
		}
	}
}
