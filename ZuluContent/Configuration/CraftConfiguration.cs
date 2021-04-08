using System;
using Server;
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace ZuluContent.Configuration
{
    public class CraftConfiguration : BaseSingleton<CraftConfiguration>
    {
        public readonly CraftSettings Alchemy;
        public readonly CraftSettings AlchemyPlus;
        public readonly CraftSettings Tinkering;
        
        protected CraftConfiguration()
        {
            const string baseDir = "Data/Crafting";
            Alchemy = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/alchemy.json");
            AlchemyPlus = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/alchemyplus.json");
            Tinkering = ZhConfig.DeserializeJsonConfig<CraftSettings>($"{baseDir}/tinkering.json");
        }
    }
    
    public record CraftSettings
    {
        public SkillName MainSkill { get; init; }
        public TextDefinition GumpTitleId { get; init; }
        public CraftEntry[] CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }

        public record CraftEntry
        {
            public Type ItemType { get; init; }
            public TextDefinition Name { get; init; }
            public TextDefinition GroupName { get; init; }
            public double Skill { get; init; }
            public CraftResource[] Resources { get; init; }
        }

        public record CraftResource
        {
            public Type ItemType { get; init; }
            public TextDefinition Name { get; init; }
            public int Amount { get; init; }
            public TextDefinition Message { get; init; }
        }
    }
}