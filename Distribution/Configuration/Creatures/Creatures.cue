package Creatures

import (
	Animal "github.com/zuluhotelaustralia/zuluhotel/Creatures/Animal"
	Animated "github.com/zuluhotelaustralia/zuluhotel/Creatures/Animated"
	Beholder "github.com/zuluhotelaustralia/zuluhotel/Creatures/Beholder"
	Daemon "github.com/zuluhotelaustralia/zuluhotel/Creatures/Daemon"
	Dragonkin "github.com/zuluhotelaustralia/zuluhotel/Creatures/Dragonkin"
	Elemental "github.com/zuluhotelaustralia/zuluhotel/Creatures/Elemental"
	Familiars "github.com/zuluhotelaustralia/zuluhotel/Creatures/Familiars"
	Gargoyle "github.com/zuluhotelaustralia/zuluhotel/Creatures/Gargoyle"
	Giantkin "github.com/zuluhotelaustralia/zuluhotel/Creatures/Giantkin"
	Human "github.com/zuluhotelaustralia/zuluhotel/Creatures/Human"
	Misc "github.com/zuluhotelaustralia/zuluhotel/Creatures/Misc"
	Ophidian "github.com/zuluhotelaustralia/zuluhotel/Creatures/Ophidian"
	Orc "github.com/zuluhotelaustralia/zuluhotel/Creatures/Orc"
	Plant "github.com/zuluhotelaustralia/zuluhotel/Creatures/Plant"
	Ratkin "github.com/zuluhotelaustralia/zuluhotel/Creatures/Ratkin"
	Slime "github.com/zuluhotelaustralia/zuluhotel/Creatures/Slime"
	Terathan "github.com/zuluhotelaustralia/zuluhotel/Creatures/Terathan"
	Troll "github.com/zuluhotelaustralia/zuluhotel/Creatures/Troll"
	Undead "github.com/zuluhotelaustralia/zuluhotel/Creatures/Undead"

	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

Creatures: {
	[string]: Types.#NpcEntry
	Animal
	Animated
	Beholder
	Daemon
	Dragonkin
	Elemental
	Familiars
	Gargoyle
	Giantkin
	Human
	Misc
	Ophidian
	Orc
	Plant
	Ratkin
	Slime
	Terathan
	Troll
	Undead
}
