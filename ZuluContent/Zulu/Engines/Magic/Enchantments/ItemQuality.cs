using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ItemQuality : Enchantment<ItemQualityInfo>
    {
        [IgnoreMember]public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)] public int Value { get; set; } = 0;
    }
    
    public class ItemQualityInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Quality";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {"Makeshift", "Makeshift"},
            {string.Empty, string.Empty}, // Regular
            {"Exceptional", "Exceptional"}
        };
    }
}