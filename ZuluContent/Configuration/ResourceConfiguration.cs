using System;
using System.Data;
using System.IO;
using Server.Json;

namespace Server.Configurations
{
    public static class ResourceConfiguration
    {
        public static OreConfig OreConfiguration { get; private set; }

        public static void Initialize()
        {
            OreConfiguration = LoadResourceConfig("ores");
        }

        private static OreConfig LoadResourceConfig(string configFile)
        {
            var path = Path.Combine(Core.BaseDirectory, $"Data/Crafting/{configFile}.json");
            Console.Write($"Ore Configuration: loading {path}... ");

            var options = JsonConfig.GetOptions(new TextDefinitionConverterFactory());
            var config = JsonConfig.Deserialize<OreConfig>(path, options);

            if (config == null)
                throw new DataException($"Ore Configuration: failed to deserialize {path}!");

            Console.WriteLine($"Done, loaded {config.Entries.Length} entries.");

            return config;
        }
    }

    public record OreConfig
    {
        public OreEntry[] Entries { get; init; }

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
}