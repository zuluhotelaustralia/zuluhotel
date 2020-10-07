using MessagePack;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{

    
    [MessagePackObject]
    public class WeaponAccuracyBonus : Enchantment<WeaponAccuracyBonusInfo>
    {
        [IgnoreMember]public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)] public WeaponAccuracyLevel Value { get; set; } = WeaponAccuracyLevel.Regular;
    }
    
    public class WeaponAccuracyBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Weapon Accuracy Bonus";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 1109;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = new[,]
        {
            {string.Empty, string.Empty}, // None
            {"Iron", "Glass"},
            {"Steel", "Rusty"},
            {"Meteoric Steel", "Aluminum"},
            {"Obsidian", "Pitted"},
            {"Onyx", "Dirty"},
            {"Adamantium", "Tarnished"}
        };
    }
}