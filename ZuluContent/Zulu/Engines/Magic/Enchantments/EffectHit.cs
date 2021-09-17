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
                [EffectHitType.LifeDrain] = new LifeDrainStrike(),
                [EffectHitType.ManaDrain] = new ManaDrainStrike(),
                [EffectHitType.StamDrain] = new StamDrainStrike(),
                [EffectHitType.Blackrock] = new BlackrockStrike(),
                [EffectHitType.Void] = new VoidStrike(),
                [EffectHitType.TriElemental] = new TriElementalStrike(),
            };

        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName((int) EffectHitType, Cursed);

        [IgnoreMember] public override EnchantmentInfo Info => EffectHitInfo.Variants[EffectHitType];

        [Key(1)] public EffectHitType EffectHitType { get; set; } = EffectHitType.None;

        [Key(2)] public double Chance { get; set; } = 0.0;
        
        [CallPriority(1)]
        public override bool GetShouldDye() => true;

        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && Effects.TryGetValue(EffectHitType, out var ability))
                ability.OnHit(attacker, defender, ref damage);
        }
    }

    public class EffectHitInfo : EnchantmentInfo
    {
        private static EffectHitInfo DefaultVariant = new();
        
        public override string Description { get; protected set; } = "Effect On Hit";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public static readonly IReadOnlyDictionary<EffectHitType, EffectHitInfo> Variants =
            new Dictionary<EffectHitType, EffectHitInfo>
            {
                [EffectHitType.None] = DefaultVariant,
                [EffectHitType.Banish] = DefaultVariant,
                [EffectHitType.TriElemental] = DefaultVariant,
                [EffectHitType.Piercing] = new()
                {
                    Hue = 1141, 
                    CursedHue = 1645
                },
                [EffectHitType.LifeDrain] = new()
                {
                    Hue = 138, 
                    CursedHue = 137, 
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.ManaDrain] = new()
                {
                    Hue = 0, 
                    CursedHue = 0,
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.StamDrain] = new()
                {
                    Hue = 0, 
                    CursedHue = 0,
                    Place = EnchantNameType.Prefix
                },
                [EffectHitType.Blackrock] = new()
                {
                    Hue = 1157, 
                    CursedHue = 1174
                },
                [EffectHitType.Void] = new()
                {
                    Hue = 1175,
                    CursedHue = 1174
                },
            };

        public override string[,] Names { get; protected set; } =
        {
            // These are in order of (int)EffectHitType
            {string.Empty, string.Empty},
            {"of Piercing", "of Bleeding"},
            {"Banishing", "Summoning"},
            {"Bloody", "Lifegiver's"},
            {"Magehunting", "Mind Twisting"},
            {"Leeching", "Parasitic"},
            {"Blackrock", "Poor Quality Blackrock"},
            {"the Void", "the Void's Touch"},
            {"Elemental Fury", "Elemental Vengeance"},
        };
    }
}