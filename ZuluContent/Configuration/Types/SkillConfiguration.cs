using System.Collections.Generic;
using Server;
using ZuluContent.Configuration.Types.Skills;

namespace ZuluContent.Configuration.Types
{
    public record SkillConfiguration
    {
        public int MaxStatCap { get; init; }
        public int StatCap { get; init; }
        public Dictionary<SkillName, SkillEntry> Entries { get; init; }
    }
}