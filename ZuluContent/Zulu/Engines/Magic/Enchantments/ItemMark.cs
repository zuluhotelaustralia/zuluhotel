using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ItemMark : Enchantment<ItemMarkInfo>
    {
        [IgnoreMember] private int m_Value = 0;

        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Value, Cursed, CurseLevel);

        [Key(1)]
        public int Value
        {
            get => Cursed ? -m_Value : m_Value;
            set => m_Value = value;
        }
    }

    public class ItemMarkInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Mark";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {"Makeshift", "Makeshift"},
            {string.Empty, string.Empty}, // Regular
            {"Exceptional", "Exceptional"}
        };
    }
}