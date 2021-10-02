using System.Collections.Generic;
using Server;
using Server.Misc;

namespace ZuluContent.Configuration.Types.Crafting
{
    public record CraftSettings
    {
        public SkillName MainSkill { get; init; }
        public TextDefinition GumpTitleId { get; init; }
        public List<CraftEntry> CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }
    }
}