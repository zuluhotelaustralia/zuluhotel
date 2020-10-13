using System;
using MessagePack;
using Server;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicalWeapon : Enchantment<MagicalWeaponInfo>
    {
        [IgnoreMember] 
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [IgnoreMember] 
        public override EnchantmentInfo Info => MagicalWeaponInfo.GetVariant(Value);
        [Key(1)] 
        public MagicalWeaponType Value { get; set; } = MagicalWeaponType.None;

        public override void OnGetDelay(ref TimeSpan delay, Mobile m)
        {
            var percentage = Value switch
            {
                MagicalWeaponType.Swift => 0.20,
                MagicalWeaponType.Stygian => 0.15,
                _ => 0.0
            };

            var reduction = delay.TotalMilliseconds * percentage;
            
            m.SendMessage($"Decreasing Swing delay by {reduction} ms");

            delay = delay.Subtract(TimeSpan.FromMilliseconds(reduction));
        }
    }

    public class MagicalWeaponInfo : EnchantmentInfo
    {
        private static readonly MagicalWeaponInfo SwiftInfo = new MagicalWeaponInfo { Hue = 621 };
        private static readonly MagicalWeaponInfo StygianInfo = new MagicalWeaponInfo { Hue = 1174 };
        private static readonly MagicalWeaponInfo MysticalInfo = new MagicalWeaponInfo { Hue = 6 };

        public override string Description { get; protected set; } = "Magical Weapon Type";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty}, // None
            {"Swift", "Swift"},
            {"Stygian", "Stygian"},
            {"Mystical", "Mystical"},
        };

        public static MagicalWeaponInfo GetVariant(MagicalWeaponType value)
        {
            return value switch
            {
                MagicalWeaponType.Swift => SwiftInfo,
                MagicalWeaponType.Stygian => StygianInfo,
                MagicalWeaponType.Mystical => MysticalInfo,
                _ => MysticalInfo
            };
        }
    }
}