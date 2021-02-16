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
        public static TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

        public static bool StaffRevealedMagicItems = true;

        public static bool AsciiClickMessage { get; set; } = true;

        private static string GetItemDesc(Item item)
        {
            return ClilocList.Entries.TryGetValue(item.LabelNumber, out var desc) ? TextInfo.ToTitleCase(desc) : null;
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

            var text = item is ICraftable craftable && craftable.PlayerConstructed
                ? $"{GetCraftedExceptional(item as Item)}{GetCraftedResource(item as Item)}{GetItemDesc(item as Item)}{GetCraftedBy(item as Item)}"
                : $"{prefix}{GetItemDesc(item as Item)}{suffix}";

            return text;
        }

        private static void DefaultHandleSingleClick<T>(T item, Mobile m) where T : Item, IMagicItem
        {
            if (!Validate(m, item))
                return;

            var text = GetMagicItemName(item);

            SendResponse(m, item, text);
        }

        private static string GetCraftedExceptional(Item item)
        {
            var isExceptional = false;
            if (item is BaseArmor armor && armor.Mark == ArmorQuality.Exceptional)
                isExceptional = true;
            else if (item is BaseWeapon weapon && weapon.Mark == WeaponQuality.Exceptional)
                isExceptional = true;
            return isExceptional ? "Exceptional " : "";
        }

        private static string GetCraftedResource(Item item)
        {
            var itemName = "";
            if (item is BaseArmor armor)
                itemName = CraftResources.GetName(armor.Resource);
            else if (item is BaseWeapon weapon)
                itemName = CraftResources.GetName(weapon.Resource);
            else if (item is BaseJewel jewel)
                itemName = CraftResources.GetName(jewel.Resource);
            return itemName + " ";
        }

        private static string GetCraftedBy(Item item)
        {
            return item is ICraftable craftable && craftable.Crafter != null
                ? $" [Crafted by {craftable.Crafter.Name}]"
                : string.Empty;
        }

        private static bool Validate(Mobile m, Item item)
        {
            if (!ClilocList.Entries.TryGetValue(item.LabelNumber, out var desc))
                return false;

            if (item is IMagicItem magicItem && (StaffRevealedMagicItems && m.AccessLevel == AccessLevel.Player) &&
                !magicItem.Identified)
            {
                SendResponse(m, item, $"a magic {desc}");
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