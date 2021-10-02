using System.Collections.Generic;
using Server.Spells;

namespace ZuluContent.Configuration.Types
{
    public record MagicConfiguration
    {
        public List<SpellCircle> Circles { get; init; }
        public Dictionary<SpellEntry, SpellInfo> Spells { get; init; }
    }
}