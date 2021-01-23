using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Scripts.Zulu.Packets;
using Server;
using Server.Engines.Craft;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        private static TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

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

            var text = $"{prefix}{GetItemDesc(item as Item)}{suffix}{GetCraftedBy(item as Item)}";

            return text;
        }

        private static void DefaultHandleSingleClick<T>(T item, Mobile m) where T : Item, IMagicItem
        {
            if (!Validate(m, item))
                return;

            var text = GetMagicItemName(item);

            SendResponse(m, item, text);
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

            if (item is IMagicItem magicItem && m.AccessLevel == AccessLevel.Player && !magicItem.Identified)
            {
                SendResponse(m, item, $"a magic {desc}");
                return false;
            }

            return true;
        }

        private static void SendResponse(Mobile m, Item item, string text)
        {
            item.LabelTo(m, text.Trim());
        }
    }
}