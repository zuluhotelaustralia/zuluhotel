using System.Collections.Generic;
using ZuluContent.Configuration.Types.Rituals;

namespace ZuluContent.Configuration.Types
{
    public record RitualConfiguration
    {
        public RitualDrawSettings[] DrawCircle { get; init; }
        public RitualDrawSettings[] UndrawCircle { get; init; }
        
        public Dictionary<string, RitualSettings[]> RitualsSettings { get; init; }
    }
}