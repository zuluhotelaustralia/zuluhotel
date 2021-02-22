using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Server.Configurations;
using Server.Json;
using ZuluContent.Configuration;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server
{
    public static class ZHConfig
    {
        public static MessagingConfiguration Messaging => Get<MessagingConfiguration>();
        public static ResourceConfiguration Resources => Get<ResourceConfiguration>();
        public static AlchemyConfiguration Alchemy => Get<AlchemyConfiguration>();
        
        private static readonly Dictionary<Type, object> Cache = new();

        // ReSharper disable once UnusedMember.Global
        [CallPriority(int.MaxValue)]
        public static void Configure()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Starting load of Zuluhotel configurations.");

            Add<MessagingConfiguration, MessagingConfiguration>();
            Add<ResourceConfiguration, ResourceConfiguration>();
            Add<AlchemyConfiguration, AlchemyConfiguration>();
            
            stopwatch.Stop();
            Console.WriteLine($"Finished loading Zuluhotel configurations ({stopwatch.Elapsed.TotalSeconds:F2} seconds).");
        }

        public static bool Add<TTargetType, TInstanceType>() where TInstanceType : BaseSingleton<TInstanceType> => 
            Cache.TryAdd(typeof(TTargetType), BaseSingleton<TInstanceType>.Instance);

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

        public static T DeserializeJsonConfig<T>(string configFile)
        {
            var path = Path.Combine(Core.BaseDirectory, configFile);
            Console.Write($"\tDeserializeJsonConfig<{typeof(T).Name}>: loading {path} ... ");
            
            if (!File.Exists(path))
            {
                throw new FileLoadException($"DeserializeJsonConfig<{typeof(T).Name}>: {path} was not found!");
            }

            var options = JsonConfig.GetOptions(new TextDefinitionConverterFactory());
            var config = JsonConfig.Deserialize<T>(path, options);

            if (config == null)
                throw new DataException($"DeserializeJsonConfig<{typeof(T).Name}>: failed to deserialize {path}!");

            Console.WriteLine("Done.");

            return config;
        }
    }

}