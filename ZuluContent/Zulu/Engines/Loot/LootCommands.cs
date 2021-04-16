using System;
using System.Diagnostics;
using Server.Items;
using Server.Json;
using Server.Targeting;

namespace Server.Scripts.Engines.Loot
{
    public static class LootCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("lootgen", AccessLevel.Developer, LootGen_OnCommand);
            CommandSystem.Register("lootconfigreload", AccessLevel.Developer, LootConfigReload_OnCommand);
        }

        [Usage("lootgen <table> <itemLevel> <itemChance>")]
        [Description("Generates loot into the target container")]
        private static void LootGen_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length != 3)
                return;

            var table = e.GetString(0);
            var itemLevel = e.GetInt32(1);
            var itemChance = e.GetDouble(2);


            e.Mobile.SendMessage("Target a container.");
            e.Mobile.Target = new InternalTarget(table, itemLevel, itemChance);
        }
        
        [Usage("lootconfigreload")]
        [Description("Reloads the loot groups/tables from the data directory (loottables.json/lootgroups.json)")]
        private static void LootConfigReload_OnCommand(CommandEventArgs e)
        {
            // TODO: implement me!
        }
        
        [Usage("BenchmarkLoot <iterations> <table> <itemLevel> <itemChance>")]
        [Description("Run a benchmark on the loot generation system.")]
        private static void BenchmarkHooks_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length != 1)
                return;
            
            var iterations = e.GetUInt32(0);
            
            var tableId = e.GetString(1);
            var itemLevel = e.GetInt32(2);
            var itemChance = e.GetDouble(3);
            
            var backpack = new BackpackOfHolding();
            e.Mobile.AddToBackpack(backpack);
            
            ZhConfig.Loot.Tables.TryGetValue(tableId, out var table);

            var watch = new Stopwatch();
            watch.Start();

            var items = 0;

            for (var i = 0; i < iterations; i++)
            {
                items += LootGenerator.MakeLoot(e.Mobile, backpack, table, itemLevel, itemChance);
            }

            watch.Stop();
            Console.WriteLine($"Ran {iterations} iterations generating {items} which took {watch.Elapsed.TotalSeconds:F2} seconds.");
        }

        private class InternalTarget : Target
        {
            private readonly string m_Table;
            private readonly int m_Level;
            private readonly double m_Chance;

            public InternalTarget(string table, int level, double chance) : base(-1, false, TargetFlags.None)
            {
                m_Table = table;
                m_Level = level;
                m_Chance = chance;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!ZhConfig.Loot.Tables.TryGetValue(m_Table, out var lootTable))
                    return;
                
                if (targeted is Container container)
                    LootGenerator.MakeLoot(from, container, lootTable, m_Level, m_Chance);
            }
        }
        

        
    }
}