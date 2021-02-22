using System;
using Server;
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace ZuluContent.Configuration
{
    public class AlchemyConfiguration : BaseSingleton<AlchemyConfiguration>
    {
        public readonly AlchemySettings Normal;
        public readonly AlchemySettings Plus;
        
        protected AlchemyConfiguration()
        {
            const string baseDir = "Data/Crafting";
            Normal = ZHConfig.DeserializeJsonConfig<AlchemySettings>($"{baseDir}/alchemy.json");
            Plus = ZHConfig.DeserializeJsonConfig<AlchemySettings>($"{baseDir}/alchemyplus.json");
        }
    }
    
    public record AlchemySettings
    {
        public SkillName MainSkill { get; init; }
        public TextDefinition GumpTitleId { get; init; }
        public PotionCraftEntry[] CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }

        public record PotionCraftEntry
        {
            public Type ItemType { get; init; }
            public TextDefinition Name { get; init; }
            public TextDefinition GroupName { get; init; }
            public double Skill { get; init; }
            public PotionResource[] Resources { get; init; }
        }

        public record PotionResource
        {
            public Type ItemType { get; init; }
            public TextDefinition Name { get; init; }
            public int Amount { get; init; }
            public TextDefinition Message { get; init; }
        }
    }
}