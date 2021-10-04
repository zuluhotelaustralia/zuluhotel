using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using Server.Json;
using Server.Logging;
using ZuluContent.Configuration.Types;
using static Scripts.Cue.CueHelpers;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server
{
    public static class ZhConfig
    {
        private static RootConfiguration _root;
        private static readonly ILogger Logger = LogFactory.GetLogger(typeof(ZhConfig));
        public static CoreConfiguration Core => _root.Core;
        public static EmailConfiguration Email => _root.Email;
        public static MessagingConfiguration Messaging => _root.Messaging;
        public static ResourceConfiguration Resources => _root.Resources;
        public static CraftConfiguration Crafting => _root.Crafting;
        public static LootConfiguration Loot => _root.Loot;
        public static SkillConfiguration Skills => _root.Skills;
        public static MagicConfiguration Magic => _root.Magic;
        public static CreatureConfiguration Creatures => _root.Creatures;
        public static JsonSerializerOptions DefaultSerializerOptions { get; private set; }

        // ReSharper disable once UnusedMember.Global
        [CallPriority(ushort.MinValue)]
        public static void Configure()
        {
            DefaultSerializerOptions = JsonConfig.GetOptions(
                new TextDefinitionConverterFactory(),
                new CreaturePropConverterFactory(),
                new BodyConverterFactory(),
                new PoisonConverterFactory(),
                new WeaponAbilityConverterFactory(),
                new RaceConverterFactory(),
                new LootTableConverterFactory(),
                new SkillHandlerConverterFactory()
            );

            DefaultSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            DefaultSerializerOptions.PropertyNameCaseInsensitive = true;
            
            _root = LoadCueConfiguration();
        }
        
        private static RootConfiguration LoadCueConfiguration()
        {
            #if DEBUG
            /*ConvertTypesToCue(
                "Assemblies",
                "ZuluContent",
                "github.com/zuluhotelaustralia/zuluhotel",
                new[]
                {
                    // Add types to bootstrap rough CUE types into `Configuration/Generated`
                    typeof(MessagingConfiguration).FullName,
                    typeof(CoreConfiguration).FullName,
                    typeof(EmailConfiguration).FullName,
                }
            );*/
            #endif

            var configRoot = Path.Combine(Server.Core.BaseDirectory, "Configuration");
            const string jsonCacheFile = ".zhcache.json";
            var jsonCachePath = Path.Combine(configRoot, jsonCacheFile);

            var cueArgs = $"export --force --outfile {jsonCacheFile} Zuluhotel.cue";

            var files = Directory.GetFiles(configRoot, "*.cue",
                new EnumerationOptions { RecurseSubdirectories = true });
            var lastWrite = files.Select(File.GetLastWriteTimeUtc).Max();

            if (!File.Exists(jsonCachePath) || lastWrite != File.GetLastWriteTimeUtc(jsonCachePath))
            {
                Logger.Information("CUE Configuration is out of date, rebuilding via cli {0} ... ", $"cue {cueArgs}");

                var (_, stderr, exitCode) = RunCueCli(configRoot, cueArgs);
                if (exitCode != 0)
                    throw new ApplicationException(
                        $"Failed to run cli command line, received non-zero exit code {exitCode}: {stderr}"
                    );
                    
                Logger.Information("Finished building CUE configuration.");

                File.SetLastWriteTime(jsonCachePath, lastWrite);
            }
                
            return DeserializeJsonConfig<RootConfiguration>(jsonCachePath);
        }


        public static T DeserializeJsonConfig<T>(string configFile, JsonSerializerOptions options = null)
        {
            var path = Path.Combine(Server.Core.BaseDirectory, configFile);
            
            if (!File.Exists(path))
            {
                throw new FileLoadException($"Configuration not found {path}!");
            }
            
            Logger.Information("Deserializing {0} ... ", path);

            var config = JsonConfig.Deserialize<T>(path, options ?? DefaultSerializerOptions);

            if (config == null)
                throw new DataException($"DeserializeJsonConfig<{typeof(T).Name}>: failed to deserialize {path}!");
            
            Logger.Information("Finished deserialization.");
            
            return config;
        }
    }

}