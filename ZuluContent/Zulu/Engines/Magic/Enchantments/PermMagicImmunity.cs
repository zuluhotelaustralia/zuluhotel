using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermMagicImmunity : Enchantment<PermMagicImmunityInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed, CurseLevel);

        [Key(1)] 
        public int Value { get; set; } = 0;

        public override void OnSpellDamage(Mobile attacker, Mobile defender, SpellCircle circle, ElementalType damageType, ref int damage)
        {
            var protectionLevelFromCircle = GetProtectionLevelForResist(Value);

            if (Cursed)
            {
                NotifyMobile(defender, "Your items prevent all absorbtion!");
                return;
            }

            if ((int) protectionLevelFromCircle >= (int) circle)
            {
                damage = 0;
                NotifyMobile(defender, attacker.Name + "'s spell is absorbed by your magical protection!");
                NotifyMobile(defender, attacker, "The spell dissipates upon contact with " + defender.Name + "'s magical barrier!");
            }
        }
    }
    
    public class PermMagicImmunityInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Permanent Magic Immunity";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Raw Blackrock", "Chipped Blackrock"},
            {"Refined Blackrock", "Cracked Blackrock"},
            {"Processed Blackrock", "Flawed Blackrock"},
            {"Smelted Blackrock", "Inferior Blackrock"},
            {"Forged Blackrock", "Chaotic Blackrock"},
            {"Tempered Blackrock", "Corrupted Blackrock"},
        };
    }
}