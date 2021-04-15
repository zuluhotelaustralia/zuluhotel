using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Scripts.Zulu.Packets;
using Server;
using Server.Engines.Craft;
using Server.Items;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        public static readonly TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

        public static bool StaffRevealedMagicItems = true;

        public static bool AsciiClickMessage { get; set; } = true;

        private static string GetItemDesc(Item item)
        {
            return ZhConfig.Messaging.Cliloc.TryGetValue(item.LabelNumber, out var desc)
                ? TextInfo.ToTitleCase(desc)
                : null;
        }

        private static (IEnumerable<string>, IEnumerable<string>) GetAffixes(IMagicItem item)
        {
            var values = item.Enchantments.Values.Values.Where(v => !string.IsNullOrEmpty(v.AffixName));

            var prefixes = values
                .Where(v => v.Info.Place == EnchantNameType.Prefix)
                .Select(v => v.AffixName)
                .ToList();

            var suffixes = values
                .Where(v => v.Info.Place == EnchantNameType.Suffix)
                .Select(v => v.AffixName)
                .ToList();

            return (prefixes, suffixes);
        }

        public static string GetMagicItemName(IMagicItem item)
        {
            var (prefixes, suffixes) = GetAffixes(item);

            var prefix = prefixes.Any() ? $"{string.Join(' ', prefixes)} " : string.Empty;
            var suffix = suffixes.Any() ? $" of {string.Join(' ', suffixes)}" : string.Empty;

            string text;

            if (item is ICraftable {PlayerConstructed: true} craftableItem)
                text = GetCraftableItemName(craftableItem);
            else if (item is IGMItem gmItem)
                text = gmItem.Name;
            else
                text = $"{prefix}{GetItemDesc(item as Item)}{suffix}";
            
            return text;
        }

        public static string GetCraftableItemName(ICraftable craftable)
        {
            return
                $"{GetCraftedFortified(craftable)}{GetCraftedExceptional(craftable)}{GetCraftedResource(craftable)}{GetItemDesc(craftable as Item)}{GetCraftedBy(craftable)}";
        }

        private static void DefaultHandleSingleClick<T>(T item, Mobile m) where T : Item, IMagicItem
        {
            if (!Validate(m, item))
                return;

            var text = GetMagicItemName(item);

            SendResponse(m, item, text);
        }

        private static void CraftableHandleSingleClick<T>(T item, Mobile m) where T : Item, ICraftable
        {
            if (!Validate(m, item))
                return;

            var text = GetCraftableItemName(item);

            SendResponse(m, item, text);
        }

        private static string GetCraftedFortified(ICraftable craftable)
        {
            return craftable is BaseHat {Fortified: ItemFortificationType.Fortified} ? "Fortified " : "";
        }

        private static string GetCraftedExceptional(ICraftable craftable)
        {
            return craftable.Mark == MarkQuality.Exceptional ? "Exceptional " : "";
        }

        private static string GetCraftedResource(ICraftable craftable)
        {
            var resName = craftable is IResource craftableResource
                ? CraftResources.GetName(craftableResource.Resource)
                : String.Empty;
            return resName.Length > 0 ? resName + " " : resName;
        }

        private static string GetCraftedBy(ICraftable craftable)
        {
            return craftable.Crafter != null
                ? $" [Crafted by {craftable.Crafter.Name}]"
                : string.Empty;
        }

        private static bool Validate(Mobile m, Item item)
        {
            if (!ZhConfig.Messaging.Cliloc.TryGetValue(item.LabelNumber, out var desc))
                return false;

            if (item is IMagicItem magicItem && !magicItem.Identified)
            {
                var itemName = magicItem is IGMItem ? desc : $"a magic {desc}";
                SendResponse(m, item, itemName);
                return false;
            }

            return true;
        }

        private static void SendResponse(Mobile m, Item item, string text)
        {
            m.NetState.SendMessage(item.Serial, item.ItemID, MessageType.Label, 0, 3, true, null, "", text);
        }
    }
}