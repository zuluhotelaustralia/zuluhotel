using System.Linq;
using System.Text;
using Scripts.Engines.Magic;
using Scripts.Zulu.Packets;
using Server;
using Server.Items;
using Server.Network;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static class ClothingSingleClickHandler
    {
        public static void HandleSingleClick(BaseClothing clothing, Mobile m)
        {
            if (!ClilocList.Entries.TryGetValue(clothing.LabelNumber, out var desc))
                return;

            var values = clothing.MagicProps.GetAllValues()
                .Where(v => v.Info != null && !string.IsNullOrEmpty(v.EnchantName));

            var prefixes = values
                .Where(v => v.Info.Place == EnchantNameType.Prefix)
                .Select(v => v.EnchantName);
            
            var suffixes = values
                .Where(v => v.Info.Place == EnchantNameType.Suffix)
                .Select(v => v.EnchantName);

            var prefix = string.Join(' ', prefixes);
            var suffix = string.Join(' ', suffixes);

            var text = $"{prefix} {desc} {suffix}";

            m.Send(new AsciiMessage(clothing.Serial, clothing.ItemID, MessageType.Label, 0x3B2, 3, string.Empty, text));
        }
    }
}