using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class ParalysisProtection : Enchantment<ParalysisProtectionInfo>, IDistinctEnchantment
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)] 
        public int Value { get; set; } = 0;

        public override void OnParalysis(Mobile mobile, ref TimeSpan duration, ref bool paralyze)
        {
            var protectionLevelFromParalysis = GetProtectionLevelForResist(Value);

            if (Cursed > CurseType.None && protectionLevelFromParalysis == ElementalProtectionLevel.Bane)
            {
                duration *= 2;
                return;
            }

            if (protectionLevelFromParalysis == ElementalProtectionLevel.Bane)
            {
                paralyze = false;
                NotifyMobile(mobile, "You are magically protected from paralyzing effects.");
            }
        }
        
        public int CompareTo(object obj) => obj switch
        {
            ParalysisProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
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