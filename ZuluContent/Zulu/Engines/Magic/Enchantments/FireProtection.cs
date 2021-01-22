using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class FireProtection : Enchantment<FireProtectionInfo>
    {
        [IgnoreMember]
        public override string AffixName =>
            EnchantmentInfo.GetName(IElementalResistible.GetProtectionLevelForResist(Value));

        [Key(1)] 
        public int Value { get; set; } = 0;
        
        public override void OnSpellDamage(Mobile attacker, Mobile defender, SpellCircle circle, ElementalType damageType, ref int damage)
        {
            if (damageType == ElementalType.Fire) 
                damage -= (int) (damage * ((double) Value / 100));
        }
    }

    public class FireProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Fire Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } =
            MakeProtNames("Fire", IEnchantmentInfo.DefaultElementalProtectionNames);

        public override int Hue { get; protected set; } = 240;
        public override int CursedHue { get; protected set; } = 240;
    }
}