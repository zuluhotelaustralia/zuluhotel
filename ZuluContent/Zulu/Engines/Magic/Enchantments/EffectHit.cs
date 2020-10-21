using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Server;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class EffectHit : Enchantment<EffectHitInfo>
    {
        public static readonly IReadOnlyDictionary<EffectHitType, WeaponAbility> Effects =
            new Dictionary<EffectHitType, WeaponAbility>
            {
                [EffectHitType.None] = null,
                [EffectHitType.Piercing] = new PiercingStrike(),
                [EffectHitType.Banish] = new BanishStrike(),
                [EffectHitType.Poison] = null,
                [EffectHitType.LifeDrain] = new LifeDrainStrike(),
                [EffectHitType.ManaDrain] = new ManaDrainStrike(),
                [EffectHitType.StamDrain] = new StamDrainStrike(),
                [EffectHitType.Blackrock] = new BlackrockStrike(),
                [EffectHitType.Void] = new VoidStrike(),
                [EffectHitType.TriElemental] = new TriElementalStrike(),
            };

        [IgnoreMember] public override string AffixName => EffectHitInfo.SpellSuffixes[EffectHitType][Cursed ? 1 : 0];

        [IgnoreMember] public override EnchantmentInfo Info => EffectHitInfo.Variants[EffectHitType];

        [Key(1)] public EffectHitType EffectHitType { get; set; } = EffectHitType.None;

        [Key(2)] public double Chance { get; set; } = 0.0;

        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && Effects.TryGetValue(EffectHitType, out var ability))
                ability.OnHit(attacker, defender, ref damage);
        }
    }

    public class EffectHitInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Effect On Hit";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public static readonly IReadOnlyDictionary<EffectHitType, EffectHitInfo> Variants =
            new Dictionary<EffectHitType, EffectHitInfo>
            {
                [EffectHitType.None] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0
                },
                [EffectHitType.Piercing] = new EffectHitInfo
                {
                    Hue = 1141, 
                    CursedHue = 1645
                },
                [EffectHitType.Banish] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0
                },
                [EffectHitType.Poison] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0
                },
                [EffectHitType.LifeDrain] = new EffectHitInfo
                {
                    Hue = 138, 
                    CursedHue = 137, 
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.ManaDrain] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0,
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.StamDrain] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0,
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.Blackrock] = new EffectHitInfo
                {
                    Hue = 1157, 
                    CursedHue = 1174
                },
                [EffectHitType.Void] = new EffectHitInfo
                {
                    Hue = 1175,
                    CursedHue = 1174
                },
                [EffectHitType.TriElemental] = new EffectHitInfo
                {
                    Hue = 0, 
                    CursedHue = 0
                },
            };

        public static readonly IReadOnlyDictionary<EffectHitType, string[]> SpellSuffixes =
            new Dictionary<EffectHitType, string[]>
            {
                [EffectHitType.None] = new[] {string.Empty, string.Empty},
                [EffectHitType.Piercing] = new[] {"of Piercing", "of Bleeding"},
                [EffectHitType.Banish] = new[] {"Banishing", "Summoning"},
                [EffectHitType.Poison] = new[] {"Poisoned", "Envenomed"},
                [EffectHitType.LifeDrain] = new[] {"Bloody", "Lifegiver's"},
                [EffectHitType.ManaDrain] = new[] {"Magehunting", "Mind Twisting"},
                [EffectHitType.StamDrain] = new[] {"Leeching", "Parasitic"},
                [EffectHitType.Blackrock] = new[] {"Blackrock", "Poor Quality Blackrock"},
                [EffectHitType.Void] = new[] {"the Void", "the Void's Touch"},
                [EffectHitType.TriElemental] = new[] {"Elemental Fury", "Elemental Vengeance"},
            };

        // When LINQ goes wrong...
        public override string[,] Names { get; protected set; } = SpellSuffixes.Values.Aggregate(
            (array: new string[SpellSuffixes.Count, 2], index: 0),
            (acc, cur) =>
            {
                var (array, index) = acc;
                array[index, 0] = cur[0];
                array[index, 1] = cur[1];

                index++;
                return acc;
            }).array;
    }
}