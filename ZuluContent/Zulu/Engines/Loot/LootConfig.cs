using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using Server.Items;
using Server.Text;

// ReSharper disable IdentifierTypo
// ReSharper disable UnusedType.Global
// ReSharper disable NotAccessedField.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable 1591

namespace Server.Scripts.Engines.Loot
{

    public static class LootConfig
    {
        private record LootConfigEntry
        {
            public int MinQuantity { get; set; }
            public int MaxQuantity { get; set; }
            public double Chance { get; set; }
            public string Value { get; set; }
        }

        public static IReadOnlyDictionary<string, LootTable> Tables { get; private set; }
        
        public static void Initialize()
        {
            var groupConfig = DeserializeConfig("lootgroups.json");
            var groups = new Dictionary<string, LootGroup>(StringComparer.OrdinalIgnoreCase);

            foreach (var (name, list) in groupConfig)
            {
                var g = new LootGroup();
                foreach (var entry in list)
                {
                    if (entry.Value.Contains("MissingObject"))
                        continue;
                    
                    var type = AssemblyHandler.FindTypeByName(entry.Value);
                    if (type == null)
                        continue;

                    g.Add(type, entry.MinQuantity, entry.MaxQuantity, entry.Chance);
                }

                groups.TryAdd($"LootGroup.{name}", g);
            }
            
            var tableConfig = DeserializeConfig("loottables.json");
            
            var tables = new Dictionary<string, LootTable>(StringComparer.OrdinalIgnoreCase);
            foreach (var (name, list) in tableConfig)
            {
                var t = new LootTable();
                foreach (var entry in list)
                {
                    if (entry.Value.Contains("MissingObject"))
                        continue;
                    
                    if (groups.TryGetValue(entry.Value, out var group))
                    {
                        t.Add(group, entry.MinQuantity, entry.MaxQuantity, entry.Chance);
                        continue;
                    }
                    
                    var type = AssemblyHandler.FindTypeByName(entry.Value);
                    
                    if (type == null)
                        continue;

                    t.Add(type, entry.MinQuantity, entry.MaxQuantity, entry.Chance);
                }

                tables.TryAdd(name, t);
            }

            Tables = tables;
        }

        private static Dictionary<string, List<LootConfigEntry>> DeserializeConfig(string name)
        {
            var path = Path.Join(Core.BaseDirectory, $"Data/{name}");

            if (!File.Exists(path))
            {
                Utility.PushColor(ConsoleColor.Red);
                Console.WriteLine($"Loot Configuration: {path} was not found, no entries will be loaded!");
                Utility.PopColor();
                return new Dictionary<string, List<LootConfigEntry>>();
            }
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            
            var text = File.ReadAllText(path, TextEncoding.UTF8);
            return JsonSerializer.Deserialize<Dictionary<string, List<LootConfigEntry>>>(text, options);
        }
    }
    
}