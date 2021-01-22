using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class EarthProtection : Enchantment<EarthProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(IElementalResistible.GetProtectionLevelForResist(Value));

        [Key(1)] 
        public int Value { get; set; } = 0;
        
        public override void OnSpellDamage(Mobile attacker, Mobile defender, SpellCircle circle, ElementalType damageType, ref int damage)
        {
            if (damageType == ElementalType.Earth) 
                damage -= (int) (damage * ((double) Value / 100));
        }
    }
    
    public class EarthProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Earth Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override string[,] Names { get; protected set; } = MakeProtNames("Earth", IEnchantmentInfo.DefaultElementalProtectionNames);
        public override int Hue { get; protected set; } = 343;
        public override int CursedHue { get; protected set; } = 343;
    }
}