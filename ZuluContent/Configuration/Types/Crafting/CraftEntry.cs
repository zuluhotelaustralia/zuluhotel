using System;
using Server;
using Server.Misc;

namespace ZuluContent.Configuration.Types.Crafting
{
    public record CraftEntry
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public TextDefinition GroupName { get; init; }
        public double Skill { get; init; }
        public SkillName? SecondarySkill { get; init; }
        public double? Skill2 { get; init; }

        public CraftResource[] Resources { get; init; }
            
        public bool UseAllRes { get; init; }
        public bool NeedHeat { get; init; }
        public bool NeedOven { get; init; }
        public bool NeedMill { get; init; }
    }
}