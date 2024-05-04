using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Server.Collections;
using Server.Json;
using Server.Logging;
using Server.Network;
using Server.Utilities;

namespace Server.Engines.Spawners
{
    public static class GenerateSpawnersCommand
    {
        private static readonly ILogger logger = LogFactory.GetLogger(typeof(GenerateSpawnersCommand));

        public static void Initialize()
        {
            CommandSystem.Register("GenerateSpawners", AccessLevel.Developer, GenerateSpawners_OnCommand);
        }

        private static void GenerateSpawners_OnCommand(CommandEventArgs e)
        {
            var from = e.Mobile;

            if (e.Arguments.Length == 0)
            {
                from.SendMessage("Usage: [GenerateSpawners <relative search pattern to distribution>");
                return;
            }

            Generate(e.Arguments[0]);
        }

        public static void Generate(string filePattern)
        {
            var di = new DirectoryInfo(Core.BaseDirectory);

            var patternMatches = new Matcher()
                .AddInclude(filePattern)
                .Execute(new DirectoryInfoWrapper(di))
                .Files;

            List<FileInfo> files = new List<FileInfo>();
            foreach (var match in patternMatches)
            {
                files.Add(new FileInfo(match.Path));
            }

            if (files.Count == 0)
            {
                World.Broadcast(0x35, true, "GenerateSpawners: No files found matching the pattern");
                return;
            }

            var watch = Stopwatch.StartNew();

            var allSpawners = new Dictionary<Guid, ISpawner>();
            foreach (var item in World.Items.Values)
            {
                if (item is ISpawner spawner)
                {
                    allSpawners[spawner.Guid] = spawner;
                }
            }

            var options = JsonConfig.GetOptions(new TextDefinitionConverterFactory());
            var totalGenerated = 0;
            var totalFailures = 0;

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                World.Broadcast(0x35, true, "GenerateSpawners: Generating spawners from {0}...", file.Name);
                logger.Information($"Generating spawners from {file.FullName}");

                NetState.FlushAll();

                try
                {
                    var spawners = JsonConfig.Deserialize<List<DynamicJson>>(file.FullName);
                    ParseSpawnerList(spawners, options, allSpawners, out var generated, out var failed);
                    totalGenerated += generated;
                    totalFailures += failed;
                }
                catch (JsonException)
                {
                    World.Broadcast(0x35, true, "GenerateSpawners: Exception parsing {0}, file may not be in the correct format.",
                        file.FullName);
                }
            }

            watch.Stop();

            logger.Information("Generated {0} spawners ({1:F2} seconds, {2} failures)");
            World.Broadcast(0x35, true, "GenerateSpawners: Generated {0} spawners ({1:F2} seconds, {2} failures)",
                totalGenerated,
                watch.Elapsed.TotalSeconds,
                totalFailures);
        }

        private static void ParseSpawnerList(
            List<DynamicJson> spawners,
            JsonSerializerOptions options,
            Dictionary<Guid, ISpawner> allSpawners,
            out int totalGenerated,
            out int failureCount
        )
        {
            failureCount = 0;
            totalGenerated = 0;

            using var queue = PooledRefQueue<Item>.Create();
            for (var i = 0; i < spawners.Count; i++)
            {
                var json = spawners[i];
                var type = AssemblyHandler.FindTypeByName(json.Type);

                if (type == null || !typeof(BaseSpawner).IsAssignableFrom(type))
                {
                    logger.Error($"Invalid spawner type {json.Type ?? "(-null-)"} ({i}).");
                    failureCount++;
                    continue;
                }

                json.GetProperty("location", options, out Point3D location);
                json.GetProperty("map", options, out Map map);

                // Delete all spawners at this location.
                // Probably shouldn't do this outside of migrations? Is there a better way to find/fix spawners?
                var eable = map.GetItemsInRange<BaseSpawner>(location, 0);
                foreach (var spawner in eable)
                {
                    if (spawner.GetType() == type)
                    {
                        queue.Enqueue(spawner);
                        allSpawners.Remove(spawner.Guid);
                    }
                }

                while (queue.Count > 0)
                {
                    queue.Dequeue().Delete();
                }

                eable.Free();

                try
                {
                    var spawner = type.CreateInstance<ISpawner>(json, options);

                    spawner!.MoveToWorld(location, map);
                    spawner!.Respawn();

                    if (allSpawners.Remove(spawner.Guid, out var oldSpawner))
                    {
                        oldSpawner.Delete();
                    }

                    allSpawners.Add(spawner.Guid, spawner);
                    totalGenerated++;
                }
                catch (Exception ex)
                {
                    json.GetProperty("guid", options, out Guid guid);
                    TraceException(ex, $"Failed to generate spawner {guid}.");

                    failureCount++;
                }
            }
        }

        private static void TraceException(Exception ex, string message = "")
        {
            try
            {
                using var op = new StreamWriter("spawner-errors.log", true);
                op.WriteLine("# {0}", Core.Now);

                if (!string.IsNullOrEmpty(message))
                {
                    op.WriteLine(message);
                }

                op.WriteLine(ex);

                op.WriteLine();
                op.WriteLine();
            }
            catch
            {
                // ignored
            }

#if DEBUG
            logger.Error($"{message}\n{ex}");
#endif
        }
    }
}