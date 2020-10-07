using MessagePack;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class WeaponDamageBonus : Enchantment<WeaponDamageBonusInfo>
    {
        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)] public WeaponDamageLevel Value { get; set; } = 0;
    }
    
    public class WeaponDamageBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Weapon Damage";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = new[,]
        {
            {string.Empty, string.Empty},
            {"Ruin", "Gentle Touch"},
            {"Might", "Feather's Touch"},
            {"Force", "Calmness"},
            {"Power", "Even Temper"},
            {"Vanquishing", "Tranquility"},
            {"Devastation", "Pacifism"},
        };
    }
}