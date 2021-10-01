using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Server;
using Server.Configurations;
using Server.Json;
using Server.Mobiles;
using Server.Scripts.Engines.Loot;
using Server.Spells;
using static Scripts.Cue.CueHelpers;

namespace Scripts.Configuration
{
    public record Crafting
    {
        public AutoLoopSettings AutoLoop { get; init; }
        public CraftSettings Alchemy { get; init; }
        public CraftSettings AlchemyPlus { get; init; }
        public CraftSettings Blacksmithy { get; init; }
        public CraftSettings Carpentry { get; init; }
        public CraftSettings Cartography { get; init; }
        public CraftSettings Cooking { get; init; }
        public CraftSettings Fletching { get; init; }
        public CraftSettings Inscription { get; init; }
        public CraftSettings Tailoring { get; init; }
        public CraftSettings Tinkering { get; init; }
    }
    
    public record Resources
    {
        public ResourceSettings<OreEntry> Ores { get; init; }
        public ResourceSettings<OreEntry> Sand { get; init; }
        public ResourceSettings<OreEntry> Clay { get; init; }
        public ResourceSettings<LogEntry> Logs { get; init; }
        public ResourceSettings<HideEntry> Hides { get; init; }
        public ResourceSettings<FishEntry> Fish { get; init; }
    }

    public record Loot
    {
        [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<LootTable>))]
        public Dictionary<string, LootTable> Tables { get; init; }
    }

    public record Magic
    {
        public List<SpellCircle> Circles { get; init; }
        public Dictionary<SpellEntry, SpellInfo> Spells { get; init; }
    }
    
    public record RootConfig
    {
        [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<CreatureProperties>))]
        public Dictionary<string, CreatureProperties> Creatures { get; init; }
        public Crafting Crafting { get; init; }
        public Resources Resources { get; init; }
        public Loot Loot { get; init; }
        public Magic Magic { get; init; }
        public SkillSettings Skills { get; init; }
    }

    public class CueConfiguration : BaseSingleton<CueConfiguration>
    {
        public readonly RootConfig RootConfig;

        protected CueConfiguration()
        {
            ConvertTypesToCue(
                "Assemblies",
                "ZuluContent",
                "github.com/zuluhotelaustralia/zuluhotel",
                new[]
                {
                    typeof(SpellEntry).FullName,
                    typeof(CraftEntry).FullName,
                    typeof(CraftSettings).FullName,
                    typeof(CreatureAttack).FullName,
                    typeof(AutoLoopSettings).FullName,
                    typeof(OreEntry).FullName,
                    typeof(LogEntry).FullName,
                    typeof(HideEntry).FullName,
                    typeof(FishEntry).FullName,
                    typeof(CreatureEquip).FullName,
                    typeof(CreatureProperties).FullName,
                    typeof(OppositionGroup).FullName,
                    typeof(SpellInfo).FullName,
                    typeof(SpellCircle).FullName,
                    typeof(SkillEntry).FullName,
                    typeof(StatAdvancement).FullName,
                    typeof(SkillSettings).FullName
                    
                }
            );

            var configRoot = Path.Combine(Core.BaseDirectory, "Configuration");
            const string jsonCacheFile = ".zhcache.json";
            var jsonCachePath = Path.Combine(configRoot, jsonCacheFile);

            var cueArgs = $"export --force --outfile {jsonCacheFile} Zuluhotel.cue";

            try
            {
                var files = Directory.GetFiles(configRoot, "*.cue",
                    new EnumerationOptions { RecurseSubdirectories = true });
                var lastWrite = files.Select(File.GetLastWriteTimeUtc).Max();

                if (!File.Exists(jsonCachePath) || lastWrite != File.GetLastWriteTimeUtc(jsonCachePath))
                {
                    Console.Write($"\tCUE Configuration is out of date, rebuilding via cli `cue {cueArgs}` ... ");

                    var (_, stderr, exitCode) = RunCueCli(configRoot, cueArgs);
                    if (exitCode != 0)
                        throw new ApplicationException(
                            $"Failed to run cli command line, received non-zero exit code {exitCode}: {stderr}"
                        );

                    Utility.PushColor(ConsoleColor.Green);
                    Console.WriteLine("done.");
                    Utility.PopColor();

                    File.SetLastWriteTime(jsonCachePath, lastWrite);
                }

                RootConfig = ZhConfig.DeserializeJsonConfig<RootConfig>(jsonCachePath);
                
                Utility.PushColor(ConsoleColor.Green);
                Console.WriteLine("done.");
                Utility.PopColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}