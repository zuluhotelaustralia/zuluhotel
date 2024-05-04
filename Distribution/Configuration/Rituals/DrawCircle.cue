package Rituals

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

DrawCircle: Types.#RitualDrawSettings & [
    {
        Speech:     "Great cosmic energy, I humbly call upon your aid"
        Direction:   0
        XMod:        0
        YMod:        0
        SoundEffect: 0
        Text:        "*The air begins to thicken, and the sky darkens as clouds race across the sky*"
        Appear:      "RitualRuneNorth"
    },
    {
        Speech:     "By the power of Earth, I consecrate this Circle"
        Direction:   0
        XMod:        0
        YMod:        0
        SoundEffect: 0
        Text:        "*The earth shudders violently for a moment*"
        Appear:      "RitualRuneEast"
    },
    {
        Speech:     "By the power of Water, I consecrate this Circle"
        Direction:   2
        XMod:        5
        YMod:        4
        SoundEffect: 0
        Text:        "*The air becomes heavy with moisture*"
        Appear:      "RitualRuneSouth"
    },
    {
        Speech:     "By the power of Fire, I consecrate this Circle"
        Direction:   4
        XMod:        0
        YMod:        9
        SoundEffect: 0
        Text:        "*Waves of heat shimmer in the newly warmed air*"
        Appear:      "RitualRuneWest"
    },
    {
        Speech:     "By the power of Air, I consecrate this Circle"
        Direction:   6
        XMod:        -4
        YMod:        5
        SoundEffect: 0
        Text:        "*A small breeze blows fiercely for a moment*"
        Appear:      "None"
    },
    {
        Speech:     "Great Elements, I summon you! Appear before me!"
        Direction:   4
        XMod:        0
        YMod:        0
        SoundEffect: 0
        Text:        ""
        Appear:      "None"
    }
]