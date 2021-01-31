using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Server.Json;
using Server.Utilities;

namespace Server.Engines.Spawners
{
    public static class GenerateSpawners
    {
        public static void Initialize()
        {
            CommandSystem.Register("GenerateSpawners", AccessLevel.Developer, GenerateSpawners_OnCommand);
        }

        private static void GenerateSpawners_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (e.Arguments.Length == 0)
            {
               World.Broadcast(0x35, true, "Usage: [GenerateSpawners <path|search pattern>");
                return;
            }

            Generate(e.Arguments[0]);
        }

        public static void Generate(string filePattern)
        {
            var di = new DirectoryInfo(Core.BaseDirectory);

            var files = di.GetFiles(filePattern, SearchOption.AllDirectories);
            

            if (files.Length == 0)
            {
               World.Broadcast(0x35, true, "GenerateSpawners: No files found matching the pattern");
                return;
            }

            var options = JsonConfig.GetOptions(new TextDefinitionConverterFactory());

            foreach (var file in files)
            {
                World.Broadcast(0x35, true, "GenerateSpawners: Generating spawners for {0}...", file.Name);
                try
                {
                    var spawners = JsonConfig.Deserialize<List<DynamicJson>>(file.FullName);
                    ParseSpawnerList(spawners, options);
                }
                catch (JsonException)
                {
                    World.Broadcast(0x35, true, "GenerateSpawners: Exception parsing {0}, file may not be in the correct format.",
                        file.FullName);
                }
            }
        }

        private static void ParseSpawnerList(List<DynamicJson> spawners, JsonSerializerOptions options)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<string> failures = new List<string>();
            int count = 0;

            for (var i = 0; i < spawners.Count; i++)
            {
                var json = spawners[i];
                Type type = AssemblyHandler.FindTypeByName(json.Type);

                if (type == null || !typeof(BaseSpawner).IsAssignableFrom(type))
                {
                    string failure = $"GenerateSpawners: Invalid spawner type {json.Type ?? "(-null-)"} ({i})";
                    if (!failures.Contains(failure))
                    {
                        failures.Add(failure);
                       World.Broadcast(0x35, true, failure);
                    }

                    continue;
                }

                json.GetProperty("location", options, out Point3D location);
                json.GetProperty("map", options, out Map map);

                var eable = map.GetItemsInRange<BaseSpawner>(location, 0);

                if (eable.Any(sp => sp.GetType() == type))
                {
                    eable.Free();
                    continue;
                }

                eable.Free();

                try
                {
                    var spawner = type.CreateInstance<ISpawner>(json, options);

                    spawner!.MoveToWorld(location, map);
                    spawner!.Respawn();
                }
                catch (Exception)
                {
                    string failure = $"GenerateSpawners: Spawner {type} failed to construct";
                    if (!failures.Contains(failure))
                    {
                        failures.Add(failure);
                       World.Broadcast(0x35, true, failure);
                    }

                    continue;
                }

                count++;
            }

            watch.Stop();
           World.Broadcast(0x35, true, "GenerateSpawners: Generated {0} spawners ({1:F2} seconds, {2} failures)", count,
                watch.Elapsed.TotalSeconds, failures.Count);
        }
    }
}