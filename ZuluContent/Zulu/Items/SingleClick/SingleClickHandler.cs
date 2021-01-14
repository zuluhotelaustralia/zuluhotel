using System.Collections.Generic;
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
        private static string GetItemDesc(Item item)
        {
            return ClilocList.Entries.TryGetValue(item.LabelNumber, out var desc) ? desc : null;
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

        private static void DefaultHandleSingleClick<T>(T item, Mobile m) where T : Item, IMagicItem
        {
            if (!Validate(m, item))
                return;
            
            var (prefixes, suffixes) = GetAffixes(item);

            var prefix = prefixes.Any() ? $"{string.Join(' ', prefixes)} " : string.Empty;
            var suffix = suffixes.Any() ? $" of {string.Join(' ', suffixes)}" : string.Empty;
            
            var text = $"{prefix}{GetItemDesc(item)}{suffix}{GetCraftedBy(item)}";

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

            if (item is IMagicItem magicItem && !magicItem.Identified)
            {
                SendResponse(m, item, $"a magical {desc}");
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