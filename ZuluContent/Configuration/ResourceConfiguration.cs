using System;
using System.Data;
using System.IO;
using Server.Json;

namespace Server.Configurations
{
    public static class ResourceConfiguration
    {
        public static OreConfig OreConfiguration { get; private set; }

        public static LogConfig LogConfiguration { get; private set; }


        public static void Initialize()
        {
            OreConfiguration = LoadResourceConfig<OreConfig>("ores");
            LogConfiguration = LoadResourceConfig<LogConfig>("logs");
        }

        private static T LoadResourceConfig<T>(string configFile)
        {
            var path = Path.Combine(Core.BaseDirectory, $"Data/Crafting/{configFile}.json");
            Console.Write($"Ore Configuration: loading {path}... ");

            var options = JsonConfig.GetOptions(new TextDefinitionConverterFactory());
            var config = JsonConfig.Deserialize<T>(path, options);

            if (config == null)
                throw new DataException($"Ore Configuration: failed to deserialize {path}!");

            Console.WriteLine("Done.");

            return config;
        }
    }

    public record OreConfig
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
        public OreEntry[] Entries { get; init; }

        public record Effect
        {
            public int[] Actions { get; init; }
            public int[] Sounds { get; init; }
            public int[] Counts { get; init; }
            public double Delay { get; init; }
            public double SoundDelay { get; init; }
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

    public record LogConfig
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
        public LogEntry[] Entries { get; init; }

        public record Effect
        {
            public int[] Actions { get; init; }
            public int[] Sounds { get; init; }
            public int[] Counts { get; init; }
            public double Delay { get; init; }
            public double SoundDelay { get; init; }
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
}