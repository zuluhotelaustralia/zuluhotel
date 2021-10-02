using System;

namespace ZuluContent.Configuration.Types.Resources
{
    public record OreEntry
    {
        public string Name { get; init; }
        public Type ResourceType { get; init; }
        public Type SmeltType { get; init; }
        public double HarvestSkillRequired { get; init; }
        public double SmeltSkillRequired { get; init; }
        public double CraftSkillRequired { get; init; }
        public double VeinChance { get; init; }
        public int Hue { get; init; }
        public double Quality { get; init; }
        public EnchantmentEntry[] Enchantments { get; init; }
    }
}