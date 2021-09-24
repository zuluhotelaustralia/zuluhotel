using System;
using Server.Misc;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server.Configurations
{
    public partial class ResourceConfiguration : BaseSingleton<ResourceConfiguration>
    {
        public readonly ResourceSettings<OreEntry> Ores;
        public readonly ResourceSettings<OreEntry> Sand;
        public readonly ResourceSettings<OreEntry> Clay;
        public readonly ResourceSettings<LogEntry> Logs;
        public readonly ResourceSettings<HideEntry> Hides;
        public readonly ResourceSettings<FishEntry> Fish;

        protected ResourceConfiguration()
        {
            const string baseDir = "Data/Crafting";
            Ores = ZhConfig.DeserializeJsonConfig<ResourceSettings<OreEntry>>($"{baseDir}/ores.json");
            Sand = ZhConfig.DeserializeJsonConfig<ResourceSettings<OreEntry>>($"{baseDir}/sand.json");
            Clay = ZhConfig.DeserializeJsonConfig<ResourceSettings<OreEntry>>($"{baseDir}/clay.json");
            Logs = ZhConfig.DeserializeJsonConfig<ResourceSettings<LogEntry>>($"{baseDir}/logs.json");
            Hides = ZhConfig.DeserializeJsonConfig<ResourceSettings<HideEntry>>($"{baseDir}/hides.json");
            Fish = ZhConfig.DeserializeJsonConfig<ResourceSettings<FishEntry>>($"{baseDir}/fish.json");
        }
    }
    
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
    
    public record LogEntry
    {
        public string Name { get; init; }
        public Type ResourceType { get; init; }
        public double HarvestSkillRequired { get; init; }
        public double CraftSkillRequired { get; init; }
        public double VeinChance { get; init; }
        public int Hue { get; init; }
        public double Quality { get; init; }
    }
    
    public record HideEntry
    {
        public string Name { get; init; }
        public Type ResourceType { get; init; }
        public double CraftSkillRequired { get; init; }
        public int Hue { get; init; }
        public double Quality { get; init; }
        public EnchantmentEntry[] Enchantments { get; init; }
    }
    
    public record FishEntry
    {
        public string Name { get; init; }
        public Type ResourceType { get; init; }
        public double HarvestSkillRequired { get; init; }
        public double VeinChance { get; init; }
    }
    
    public record EnchantmentEntry
    {
        public Type EnchantmentType { get; init; }
        public int EnchantmentValue { get; init; }
    }

    public record ResourceSettings<T>
    {
        public int BankWidth { get; init; }
        public int BankHeight { get; init; }
        public int MinTotal { get; init; }
        public int MaxTotal { get; init; }
        public double MinRespawn { get; init; }
        public double MaxRespawn { get; init; }
        public SkillName Skill { get; init; }
        public int MaxRange { get; init; }
        public int MaxChance { get; init; }
        public Effect ResourceEffect { get; init; }
        public Message Messages { get; init; }
        public T[] Entries { get; init; }

        public record Effect
        {
            public int[] Actions { get; init; }
            public int[] Sounds { get; init; }
            public int[] Counts { get; init; }
            public double Delay { get; init; }
            public double SoundDelay { get; init; }
        }
        
        public record Message
        {
            public TextDefinition NoResourcesMessage { get; init; }
            public TextDefinition? DoubleHarvestMessage { get; init; }
            public TextDefinition? TimedOutOfRangeMessage { get; init; }
            public TextDefinition OutOfRangeMessage { get; init; }
            public TextDefinition FailMessage { get; init; }
            public TextDefinition PackFullMessage { get; init; }
            public TextDefinition ToolBrokeMessage { get; init; }
        }
    }
}