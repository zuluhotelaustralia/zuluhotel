using System;
using System.Collections.Generic;
using Server;
using ZuluContent.Zulu.Skills;

namespace ZuluContent.Configuration.Types.Skills
{
    public record SkillEntry
    {
        public BaseSkillHandler OnUseHandler { get; init; }
        public TimeSpan Delay { get; init; }
        public int DefaultPoints { get; init; }
        public Dictionary<StatType, StatAdvancement> StatAdvancements { get; init; }
    }
}