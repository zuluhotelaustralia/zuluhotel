using MessagePack;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class WaterProtection : Enchantment<WaterProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName =>
            EnchantmentInfo.GetName(IElementalResistible.GetProtectionLevelForResist(Value), Cursed);

        [Key(1)]
        public int Value { get; set; } = 0;
        
        public override void OnSpellDamage(Mobile attacker, Mobile defender, ElementalType damageType, ref int damage)
        {
            if (damageType == ElementalType.Water) 
                damage -= (int) (damage * ((double) Value / 100));
        }
    }
    
    public class WaterProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Water Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } =
            MakeProtNames("Water", IEnchantmentInfo.DefaultElementalProtectionNames);

        public override int Hue { get; protected set; } = 1001;
        public override int CursedHue { get; protected set; } = 1001;
    }
}