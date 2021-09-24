package Types

#NpcEntry: {
	Kind:   "NpcEntry"
	for key, val in #CreatureProperties {
		"\(key)": *#NpcDefaults[key] | val
	}
	Attack: {
		for key, val in #CreatureAttack {
			"\(key)": *#NpcDefaults.Attack[key] | val
		}
	}
}