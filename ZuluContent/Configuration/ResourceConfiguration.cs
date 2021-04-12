using System;
using System.Data;
using System.IO;
using Server.Json;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server.Configurations
{
    public partial class ResourceConfiguration : BaseSingleton<ResourceConfiguration>
    {
        public readonly OreSettings Ores;
        public readonly OreSettings Sand;
        public readonly OreSettings Clay;
        public readonly LogSettings Logs;
        public readonly HideSettings Hides;

        protected ResourceConfiguration()
        {
            const string baseDir = "Data/Crafting";
            Ores = ZhConfig.DeserializeJsonConfig<OreSettings>($"{baseDir}/ores.json");
            Sand = ZhConfig.DeserializeJsonConfig<OreSettings>($"{baseDir}/sand.json");
            Clay = ZhConfig.DeserializeJsonConfig<OreSettings>($"{baseDir}/clay.json");
            Logs = ZhConfig.DeserializeJsonConfig<LogSettings>($"{baseDir}/logs.json");
            Hides = ZhConfig.DeserializeJsonConfig<HideSettings>($"{baseDir}/hides.json");
        }
    }

    public record OreSettings
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
        public Effect OreEffect { get; init; }
        public Message Messages { get; init; }
        public OreEntry[] Entries { get; init; }

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
            public TextDefinition DoubleHarvestMessage { get; init; }
            public TextDefinition TimedOutOfRangeMessage { get; init; }
            public TextDefinition OutOfRangeMessage { get; init; }
            public TextDefinition FailMessage { get; init; }
            public TextDefinition PackFullMessage { get; init; }
            public TextDefinition ToolBrokeMessage { get; init; }
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

        public record EnchantmentEntry
        {
            public Type EnchantmentType { get; init; }
            public int EnchantmentValue { get; init; }
        }
    }

    public record LogSettings
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
        public Effect LogEffect { get; init; }
        public Message Messages { get; init; }
        public LogEntry[] Entries { get; init; }

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
            public TextDefinition OutOfRangeMessage { get; init; }
            public TextDefinition FailMessage { get; init; }
            public TextDefinition PackFullMessage { get; init; }
            public TextDefinition ToolBrokeMessage { get; init; }
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
    }

    public record HideSettings
    {
        public HideEntry[] Entries { get; init; }

        public record HideEntry
        {
            public string Name { get; init; }
            public Type ResourceType { get; init; }
            public double CraftSkillRequired { get; init; }
            public int Hue { get; init; }
            public double Quality { get; init; }
            public EnchantmentEntry[] Enchantments { get; init; }
        }

        public record EnchantmentEntry
        {
            public Type EnchantmentType { get; init; }
            public int EnchantmentValue { get; init; }
        }
    }
}