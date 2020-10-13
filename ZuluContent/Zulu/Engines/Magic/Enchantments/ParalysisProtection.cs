using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ParalysisProtection : Enchantment<ParalysisProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)] 
        public int Value { get; set; } = 0;
    }
    
    public class ParalysisProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Paralysis Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override string[,] Names { get; protected set; } = new[,]
        {
            {string.Empty, string.Empty},
            {"Free Action", "Prisoners"},
        };
        public override int Hue { get; protected set; } = 590;
        public override int CursedHue { get; protected set; } = 590;
    }
}