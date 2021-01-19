using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicImmunity : Enchantment<MagicImmunityInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)] 
        public int Value { get; set; } = 0;
    }
    
    public class MagicImmunityInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Magic Protection With Charges";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Raw Moonstone", "Chipped Moonstone"},
            {"Cut Moonstone", "Cracked Moonstone"},
            {"Refined Moonstone", "Flawed Moonstone"},
            {"Prepared Moonstone", "Inferior Moonstone"},
            {"Enchanted Moonstone", "Chaotic Moonstone"},
            {"Flawless Moonstone", "Corrupted Moonstone"},
        };
    }
}