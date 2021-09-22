package Types

import (
	Mobiles "github.com/zuluhotelaustralia/zuluhotel/Server/Mobiles"
)

#NpcEntry: {
	Kind:   "NpcEntry"
	for key, val in Mobiles.#CreatureProperties {
		"\(key)": *#NpcDefaults[key] | val
	}
	Attack: {
		for key, val in Mobiles.#CreatureAttack {
			"\(key)": *#NpcDefaults.Attack[key] | val
		}
	}
	...
}