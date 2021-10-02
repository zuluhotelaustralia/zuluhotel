using System;

namespace ZuluContent.Configuration.Types.Resources
{
    public record FishEntry
    {
        public string Name { get; init; }
        public Type ResourceType { get; init; }
        public double HarvestSkillRequired { get; init; }
        public double VeinChance { get; init; }
    }
}