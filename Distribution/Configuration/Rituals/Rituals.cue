package Rituals

import (
	Types "github.com/zuluhotelaustralia/zuluhotel/Types"
)

RitualsSettings: {
	[string]: [...Types.#RitualSetting]
	
	FreeMovement: [
		{
            Speech:      "By the power of Air, I call upon the great winds"
            SoundEffect: 20
            Text:        "*A large gust of wind rushes through the area*"
            Appear: [
                {
                    XMod: 0,
                    YMod: 2,
                    ItemId: 14120
                },
                {
                    XMod: 3,
                    YMod: 3,
                    ItemId: 14120
                },
                {
                    XMod: 2,
                    YMod: 6,
                    ItemId: 14120
                },
                {
                    XMod: -1,
                    YMod: 7,
                    ItemId: 14120
                },
                {
                    XMod: -2,
                    YMod: 4,
                    ItemId: 14120
                }
            ]
        },
        {
            Speech:      "Winds that carry all in your embrace, I ask a boon"
            SoundEffect: 0
            Text:        "*The winds rush around the item, and gently lift it from the ground*"
            Appear:      []
        },
        {
            Speech:      "To be carried free from all shackles that might hinder a cause"
            SoundEffect: 0
            Text:        "*Faster and faster the winds swirl around the item*"
            Appear:      []
        },
        {
            Speech:      "And to forever be carried free by your wings"
            SoundEffect: 254
            Text:        "*The winds blow harshly around the item, lifting it, and pouring new energy into it*"
            Appear:      []
        },
	],
	
	Immutability: [
        {
            Speech:      "Spirits of rock and Earth, those that know stability"
            SoundEffect: 544
            Text:        "*The Earth trembles gently beneath your feet*"
            Appear: [
                {
                    XMod: 0,
                    YMod: 2,
                    ItemId: 2280
                },
                {
                    XMod: 3,
                    YMod: 3,
                    ItemId: 2280
                },
                {
                    XMod: 2,
                    YMod: 6,
                    ItemId: 2280
                },
                {
                    XMod: -1,
                    YMod: 7,
                    ItemId: 2280
                },
                {
                    XMod: -2,
                    YMod: 4,
                    ItemId: 2280
                }
            ]
        },
        {
            Speech:     "Heed my call and answer my summons"
            SoundEffect: 0
            Text:        "*In response to your call, the Earth shakes mightily*"
            Appear:      []
        },
        {
            Speech:     "Bind yourselves to this great Circle"
            SoundEffect: 0
            Text:        "*With your words, you sanctify the Circle with the power of Earth*"
            Appear:      []
        },
        {
            Speech:     "So that it might stand as strong as the very mountains!"
            SoundEffect: 0
            Text:        "*You watch as Earth grants its blessing*"
            Appear:      []
        },
        {
            Speech:     "So that all who oppose me shall taste only ashes!"
            SoundEffect: 287
            Text:        "*You watch as Fire grants its blessing*"
            Appear:      []
        },
    ]
}
