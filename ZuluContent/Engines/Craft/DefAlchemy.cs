using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Server.Items;
using Server.Json;

namespace Server.Engines.Craft
{
    public sealed class DefAlchemy : CraftSystem
    {
        public static CraftSystem DefaultCraftSystem { get; private set; }
        public static CraftSystem PlusCraftSystem { get; private set; }

        public AlchemyConfig Config { get; private set; }
        private static Type TypeOfPotion => typeof(BasePotion);
        public override SkillName MainSkill => Config.MainSkill;
        public override int GumpTitleNumber => Config.GumpTitleId;

        public int CraftWorkSound => Config.CraftWorkSound;
        public int CraftEndSound => Config.CraftEndSound;


        public override double GetChanceAtMin(CraftItem item) => Config.MinCraftChance;

        // This causes the craft system to be initialized at server start rather than triggered by skill use
        //ReSharper disable once UnusedMember.Global
        public static void Initialize()
        {
            DefaultCraftSystem = LoadCraftSystem("alchemy");
            PlusCraftSystem = LoadCraftSystem("alchemyplus");
        }

        private static CraftSystem LoadCraftSystem(string configFile)
        {
            var path = Path.Combine(Core.BaseDirectory, $"Data/Crafting/{configFile}.json");
            Console.Write($"Alchemy Configuration: loading {path}... ");

            var config = JsonConfig.Deserialize<AlchemyConfig>(path);

            if (config == null)
                throw new DataException($"Alchemy Configuration: failed to deserialize {path}!");

            var craftSystem = new DefAlchemy(config);

            Console.WriteLine($"Done, loaded {config.CraftEntries.Length} entries.");

            return craftSystem;
        }

        private DefAlchemy(AlchemyConfig config) : base(config.MinCraftDelays, config.MaxCraftDelays, config.Delay)
        {
            Config = config;
            InitCraftList();
        }

        public override void InitCraftList()
        {
            if (Config == null)
                return;
            
            foreach (var entry in Config.CraftEntries)
            {
                var firstResource = entry.Resources.FirstOrDefault();

                if (firstResource == null)
                    throw new ArgumentNullException($"Alchemy entry {entry.ItemType} must have at least one resource");

                var idx = AddCraft(
                    entry.ItemType,
                    entry.GroupNameId,
                    entry.NameId,
                    entry.MinSkill,
                    entry.MaxSkill,
                    // AddCraft() needs at least one resource by default
                    firstResource.ItemType,
                    firstResource.NameId,
                    firstResource.Amount,
                    firstResource.MessageId
                );

                foreach (var c in entry.Resources.Skip(1))
                {
                    AddRes(idx, c.ItemType, c.NameId, c.Amount, c.MessageId);
                }
            }
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(CraftWorkSound);
        }

        public static bool IsPotion(Type type)
        {
            return TypeOfPotion.IsAssignableFrom(type);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (IsPotion(item.ItemType))
                {
                    if (item.Resources.Cast<CraftRes>().Any(r => r.ItemType == typeof(Bottle))) 
                        from.AddToBackpack(new Bottle());
                    
                    return 500287; // You fail to create a useful potion.
                }
                else
                {
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                }
            }
            else
            {
                from.PlaySound(CraftEndSound); // Sound of a filling bottle

                if (IsPotion(item.ItemType))
                {
                    return quality == -1 ? 1048136 : 500279;
                }
                else
                {
                    return 1044154; // You create the item.
                }
            }
        }
    }

    // ReSharper disable UnassignedGetOnlyAutoProperty ClassNeverInstantiated.Global UnusedAutoPropertyAccessor.Global
    public record AlchemyConfig
    {
        public SkillName MainSkill { get; init; }
        public int GumpTitleId { get; init; }
        public PotionCraftEntry[] CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }

        public record PotionCraftEntry
        {
            public Type ItemType { get; init; }
            public int NameId { get; init; }
            public int GroupNameId { get; init; }
            public double MinSkill { get; init; }
            public double MaxSkill { get; init; }

            public PotionResource[] Resources { get; init; }
        }

        public record PotionResource
        {
            public Type ItemType { get; init; }
            public int NameId { get; init; }
            public int Amount { get; init; }
            public int MessageId { get; init; }
        }
    }
    // ReSharper restore UnassignedGetOnlyAutoProperty ClassNeverInstantiated.Global UnusedAutoPropertyAccessor.Global
}