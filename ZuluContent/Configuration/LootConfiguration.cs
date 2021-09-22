using System;
using System.Collections.Generic;
using Server;
using Server.Scripts.Engines.Loot;

namespace Scripts.Configuration
{
    public class LootConfiguration : BaseSingleton<LootConfiguration>
    {
        private record LootConfigEntry
        {
            public int MinQuantity { get; set; }
            public int MaxQuantity { get; set; }
            public double Chance { get; set; }
            public string Value { get; set; }
        }

        public readonly IReadOnlyDictionary<string, LootTable> Tables;
        public readonly IReadOnlyDictionary<string, LootGroup> Groups;


        protected LootConfiguration()
        {
            Groups = LoadLootGroups();
            Tables = LoadLootTables(Groups);
        }

        private static Dictionary<string, LootGroup> LoadLootGroups(string path = "Data/Loot/groups.json")
        {
            var lootGroups =
                ZhConfig.DeserializeJsonConfig<Dictionary<string, List<LootConfigEntry>>>(path);

            var groups = new Dictionary<string, LootGroup>(StringComparer.OrdinalIgnoreCase);
            foreach (var (name, list) in lootGroups)
            {
                var g = new LootGroup();
                foreach (var entry in list)
                {
                    if (entry.Value.Contains("MissingObject"))
                        continue;

                    var type = AssemblyHandler.FindTypeByFullName(entry.Value);
                    if (type == null)
                        continue;

                    g.Add(type, entry.MinQuantity, entry.MaxQuantity, entry.Chance);
                }

                groups.TryAdd($"LootGroup.{name}", g);
            }

            return groups;
        }

        private static Dictionary<string, LootTable> LoadLootTables(
            IReadOnlyDictionary<string, LootGroup> groups,
            string path = "Data/Loot/tables.json"
        )
        {
            var lootTables =
                ZhConfig.DeserializeJsonConfig<Dictionary<string, List<LootConfigEntry>>>(path);

            var tables = new Dictionary<string, LootTable>(StringComparer.OrdinalIgnoreCase);
            foreach (var (name, list) in lootTables)
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

                    var type = AssemblyHandler.FindTypeByFullName(entry.Value);

                    if (type == null)
                        continue;

                    t.Add(type, entry.MinQuantity, entry.MaxQuantity, entry.Chance);
                }

                tables.TryAdd(name, t);
            }

            return tables;
        }
    }
}