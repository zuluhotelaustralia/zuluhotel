using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Server;
using Server.Items;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SlayerHit : Enchantment<SlayerHitInfo>
    {
        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName((int) Type, Cursed);

        [IgnoreMember]
        public override EnchantmentInfo Info => SlayerHitInfo.Variants[Type];

        [Key(1)] public CreatureType Type { get; set; } = CreatureType.None;

        [Key(2)] public double Chance { get; set; } = 0.0;

        public override void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor,
            ref int damage)
        {
            if (Chance > Utility.RandomDouble() && attacker is BaseCreature creature && creature.CreatureType == Type)
                damage /= 2;
        }

        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && defender is BaseCreature creature && creature.CreatureType == Type)
                damage *= 2;
        }
    }

    public class SlayerHitInfo : EnchantmentInfo
    {
        private static readonly SlayerHitInfo DefaultVariant = new SlayerHitInfo();

        public override string Description { get; protected set; } = "Creature Slayer";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public static readonly IReadOnlyDictionary<CreatureType, SlayerHitInfo> Variants =
            new Dictionary<CreatureType, SlayerHitInfo>
            {
                [CreatureType.None] = DefaultVariant,
                [CreatureType.Slime] = DefaultVariant,
                [CreatureType.Ratkin] = DefaultVariant,
                [CreatureType.Plant] = DefaultVariant,
                [CreatureType.Beholder] = DefaultVariant,
                [CreatureType.Terathan] = DefaultVariant,
                [CreatureType.Ophidian] = DefaultVariant,
                [CreatureType.Animated] = DefaultVariant,
                [CreatureType.Gargoyle] = DefaultVariant,
                [CreatureType.Giantkin] = DefaultVariant,
                [CreatureType.Daemon] = DefaultVariant,
                [CreatureType.Animal] = new SlayerHitInfo
                {
                    Hue = 871,
                    CursedHue = 867
                },
                [CreatureType.Orc] = new SlayerHitInfo
                {
                    Hue = 845,
                    CursedHue = 842
                },
                [CreatureType.Troll] = new SlayerHitInfo
                {
                    Hue = 816,
                    CursedHue = 812
                },
                [CreatureType.Elemental] = new SlayerHitInfo
                {
                    Hue = 901,
                    CursedHue = 897
                },
                [CreatureType.Undead] = new SlayerHitInfo
                {
                    Hue = 856,
                    CursedHue = 852
                },
                [CreatureType.Dragonkin] = new SlayerHitInfo
                {
                    Hue = 641,
                    CursedHue = 631
                },
                [CreatureType.Human] = new SlayerHitInfo
                {
                    Hue = 1002,
                    CursedHue = 1020
                },
            };

        // When LINQ goes wrong...
        public override string[,] Names { get; protected set; } =
        {
            // These are in order of (int)CreatureType
            {string.Empty, string.Empty},
            {"Slime Slayer", "Slime Protector"},
            {"Ratkin Slayer", "Ratkin Protector"},
            {"Plant Slayer", "Plant Protector"},
            {"Animal Slayer", "Animal Protector"},
            {"Beholder Slayer", "Beholder Protector"},
            {"Orc Slayer", "Orc Protector"},
            {"Terathan Slayer", "Terathan Protector"},
            {"Ophidian Slayer", "Ophidian Protector"},
            {"Bewitched Slayer", "Bewitched Protector"},
            {"Gargoyle Slayer", "Gargoyle Protector"},
            {"Troll Slayer", "Troll Protector"},
            {"Giant Slayer", "Giant Protector"},
            {"Elemental Slayer", "Elemental Protector"},
            {"Silver", "Undead Protector"},
            {"Holy", "Unholy"},
            {"Dragon Slayer", "Dragon Protector"},
            {"Assassin's", "Peacekeeping"},
        };
    }
}