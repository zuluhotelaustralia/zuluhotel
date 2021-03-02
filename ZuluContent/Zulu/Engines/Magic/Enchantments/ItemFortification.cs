using MessagePack;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ItemFortification : Enchantment<ItemFortificationInfo>
    {
        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)] public ItemFortificationType Value { get; set; }
    }

    public class ItemFortificationInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Fortification";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty}, // Regular
            {"Fortified", "Fortified"}
        };
    }
}