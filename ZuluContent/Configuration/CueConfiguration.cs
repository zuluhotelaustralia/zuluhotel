using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Server;
using Server.Json;
using Server.Mobiles;
using Server.Spells;
using static Scripts.Cue.CueHelpers;

namespace Scripts.Configuration
{
    public record RootConfig
    {
        [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<CreatureProperties>))]
        public Dictionary<string, CreatureProperties> Creatures { get; init; }
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
                    typeof(CreatureAttack).FullName,
                    typeof(CreatureEquip).FullName,
                    typeof(CreatureProperties).FullName,
                    typeof(OppositionGroup).FullName
                }
            );

            var configRoot = Path.Combine(Core.BaseDirectory, "Configuration/");
            var jsonCachePath = Path.Combine(configRoot, ".zhcache.json");
            var cueArgs = $"export --force --outfile {jsonCachePath} Zuluhotel.cue";
            
            try
            {
                
                var files = Directory.GetFiles(configRoot, "*.cue", new EnumerationOptions { RecurseSubdirectories = true });
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