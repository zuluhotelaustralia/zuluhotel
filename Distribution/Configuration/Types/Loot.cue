package Types

#LootEntry: {
	MinQuantity: int
	MaxQuantity: int
	Chance:      number
	Value:       string
}

#LootTable: {
	MinQuantity: int
	MaxQuantity: int
	Chance:      number
	Value:       string | [...#LootEntry]
}
