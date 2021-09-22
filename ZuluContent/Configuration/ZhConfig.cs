using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Server.Configurations;
using Server.Json;
using Scripts.Configuration;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server
{
    public static class ZhConfig
    {
        public static MessagingConfiguration Messaging => Get<MessagingConfiguration>();
        public static ResourceConfiguration Resources => Get<ResourceConfiguration>();
        public static CraftConfiguration Crafting => Get<CraftConfiguration>();
        public static LootConfiguration Loot => Get<LootConfiguration>();
        public static SkillConfiguration Skills => Get<SkillConfiguration>();
        public static SpellConfiguration Spells => Get<SpellConfiguration>();
        public static CreatureConfiguration Creatures => Get<CreatureConfiguration>();
        
        private static readonly Dictionary<Type, object> Cache = new();

        public static JsonSerializerOptions DefaultSerializerOptions { get; private set; }

        // ReSharper disable once UnusedMember.Global
        [CallPriority(ushort.MaxValue)]
        public static void Configure()
        {
            DefaultSerializerOptions = JsonConfig.GetOptions(
                new TextDefinitionConverterFactory(),
                new CreaturePropConverterFactory(),
                new BodyConverterFactory(),
                new PoisonConverterFactory(),
                new WeaponAbilityConverterFactory(),
                new RaceConverterFactory()
            );

            DefaultSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            DefaultSerializerOptions.PropertyNameCaseInsensitive = true;
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Starting load of Zuluhotel configurations.");

            Add<MessagingConfiguration>();
            Add<ResourceConfiguration>();
            Add<CraftConfiguration>();
            Add<LootConfiguration>();
            Add<SkillConfiguration>();
            Add<SpellConfiguration>();
            Add<CueConfiguration>();
            Add<CreatureConfiguration>();

            stopwatch.Stop();
            Console.WriteLine($"Finished loading Zuluhotel configurations ({stopwatch.Elapsed.TotalSeconds:F2} seconds).");
        }
        
        public static bool Add<TTargetType>() where TTargetType : BaseSingleton<TTargetType>
        {
            if (!Cache.ContainsKey(typeof(TTargetType)))
            {
                Cache.Add(typeof(TTargetType), BaseSingleton<TTargetType>.Instance);
                return true;
            }

            return false;
        }

        public static bool Add<TTargetType, TInstanceType>() where TInstanceType : BaseSingleton<TInstanceType>
        {
            if (!Cache.ContainsKey(typeof(TTargetType)))
            {
                Cache.Add(typeof(TTargetType), BaseSingleton<TInstanceType>.Instance);
                return true;
            }

            return false;
        }

        public static void Replace<TTargetType, TInstanceType>() where TInstanceType : BaseSingleton<TInstanceType>
        {
            if (!Add<TTargetType, TInstanceType>())
                Cache[typeof(TTargetType)] = BaseSingleton<TInstanceType>.Instance;
        }
        
        public static T Get<T>() where T : BaseSingleton<T> {
            
            if (Cache.TryGetValue(typeof(T), out var value) && value is T singleton) {
                return singleton;
            }

            throw new ArgumentNullException($"{typeof(T)} is not registered");
        }

        public static T DeserializeJsonConfig<T>(string configFile, JsonSerializerOptions options = null)
        {
            var path = Path.Combine(Core.BaseDirectory, configFile);
            Console.Write($"\tDeserializeJsonConfig<{typeof(T).Name}>: loading {path} ... ");
            
            if (!File.Exists(path))
            {
                throw new FileLoadException($"DeserializeJsonConfig<{typeof(T).Name}>: {path} was not found!");
            }
            
            var config = JsonConfig.Deserialize<T>(path, options ?? DefaultSerializerOptions);

            if (config == null)
                throw new DataException($"DeserializeJsonConfig<{typeof(T).Name}>: failed to deserialize {path}!");

            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("done.");
            Utility.PopColor();

            return config;
        }
    }

}