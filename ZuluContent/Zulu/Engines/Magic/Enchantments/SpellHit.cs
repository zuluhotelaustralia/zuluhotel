using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SpellHit : Enchantment<SpellHitInfo>
    {
        [IgnoreMember] public override string AffixName => string.Empty;
        
        [Key(1)]
        public SpellType SpellType { get; set; } = SpellType.None;

        [Key(2)]
        public double Chance { get; set; } = 0.0;
    }

    public class SpellHitInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Air Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } =
            MakeProtNames("Air", IEnchantmentInfo.DefaultElementalProtectionNames);

        public override int Hue { get; protected set; } = 1001;
        public override int CursedHue { get; protected set; } = 1001;
    }
}