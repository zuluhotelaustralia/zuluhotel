using System;

namespace Server.Mobiles
{
    [Flags]
    public enum CreatureType
    {
        Human = 1,
        Undead,
        Elemental,
        Dragonkin,
        Animal,
        Daemon,
        Beholder,
        Animated,
        Slime,
        Terathan,
        Plant,
        Orc,
        Troll,
        Gargoyle,
        Ophidian,
        Ratkin,
        Giantkin
    }
}