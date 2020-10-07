using MessagePack;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ArmorProtection : Enchantment<ArmorProtectionInfo>
    {
        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Value);

        [Key(1)] public ArmorProtectionLevel Value { get; set; } = ArmorProtectionLevel.Regular;
    }
    
    public class ArmorProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Armor Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = new[,]
        {
            {string.Empty, string.Empty},
            {"Defense", "Penetration"},
            {"Guarding", "Vulnerability"},
            {"Hardening", "Disharmony"},
            {"Fortification", "Distress"},
            {"Invulnerability", "Disaster"},
            {"Invincibility", "Catastrophe"},
        };
    }
    

}