using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Scripts.Zulu.Spells.Earth;
using Server;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Spells;
using Server.Spells.Eighth;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Third;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SpellHit : Enchantment<SpellHitInfo>
    {
        public static readonly IReadOnlyDictionary<SpellEntry, Action<Mobile, Mobile>> Spells =
            new Dictionary<SpellEntry, Action<Mobile, Mobile>>
            {
                [SpellEntry.None]  = (a, d) => {},
                [SpellEntry.Clumsy] = (a, d) => Spell.Create<ClumsySpell>(a, null, true).Target(d),
                [SpellEntry.Feeblemind] = (a, d) => Spell.Create<FeeblemindSpell>(a, null, true).Target(d),
                [SpellEntry.MagicArrow] = (a, d) => Spell.Create<MagicArrowSpell>(a, null, true).Target(d),
                [SpellEntry.Weaken] = (a, d) => Spell.Create<WeakenSpell>(a, null, true).Target(d),
                [SpellEntry.Harm] = (a, d) => Spell.Create<HarmSpell>(a, null, true).Target(d),
                [SpellEntry.Fireball] = (a, d) => Spell.Create<FireballSpell>(a, null, true).Target(d),
                [SpellEntry.Curse] = (a, d) => Spell.Create<CurseSpell>(a, null, true).Target(d),
                [SpellEntry.Lightning] = (a, d) => Spell.Create<LightningSpell>(a, null, true).Target(d),
                [SpellEntry.ManaDrain] = (a, d) => Spell.Create<ManaDrainSpell>(a, null, true).Target(d),
                [SpellEntry.MindBlast] = (a, d) => Spell.Create<MindBlastSpell>(a, null, true).Target(d),
                [SpellEntry.Paralyze] = (a, d) => Spell.Create<ParalyzeSpell>(a, null, true).Target(d),
                [SpellEntry.EnergyBolt] = (a, d) => Spell.Create<EnergyBoltSpell>(a, null, true).Target(d),
                [SpellEntry.Explosion] = (a, d) => Spell.Create<ExplosionSpell>(a, null, true).Target(d),
                [SpellEntry.MassCurse] = (a, d) => Spell.Create<MassCurseSpell>(a, null, true).Target(d),
                [SpellEntry.ChainLightning] = (a, d) => Spell.Create<ChainLightningSpell>(a, null, true).Target(d),
                [SpellEntry.FlameStrike] = (a, d) => Spell.Create<FlameStrikeSpell>(a, null, true).Target(d),
                [SpellEntry.MeteorSwarm] = (a, d) => Spell.Create<MeteorSwarmSpell>(a, null, true).Target(d),
                [SpellEntry.Earthquake] = (a, _) => Spell.Create<EarthquakeSpell>(a, null, true).OnCast(),
            };

        private SpellEntry m_SpellEntry = SpellEntry.None;

        [IgnoreMember] public override string AffixName => SpellHitInfo.SpellSuffixes[SpellEntry][Cursed ? 1 : 0];

        [Key(1)]
        public SpellEntry SpellEntry
        {
            get => m_SpellEntry;
            set => m_SpellEntry = Spells.ContainsKey(value) ? value : SpellEntry.None;
        }

        [Key(2)] public double Chance { get; set; } = 0.0;

        public override void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            if (Chance > Utility.RandomDouble() && Spells.TryGetValue(SpellEntry, out var action))
                action(attacker, defender);
        }
    }

    public class SpellHitInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Spell On Hit";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public static readonly IReadOnlyDictionary<SpellEntry, string[]> SpellSuffixes =
            new Dictionary<SpellEntry, string[]>
            {
                [SpellEntry.None]  = new[] { string.Empty, string.Empty },
                [SpellEntry.Clumsy] = new[] { "Bungling", "Clumsiness" },
                [SpellEntry.Feeblemind] = new[] { "Senility", "Foolishness" },
                [SpellEntry.MagicArrow] = new[] { "Burning", "Pain" },
                [SpellEntry.Weaken] = new[] { "Weakening", "Weakness" },
                [SpellEntry.Harm] = new[] { "Wounding", "Anguish" },
                [SpellEntry.Fireball] = new[] { "Daemon's Breath", "Daemonic Torment" },
                [SpellEntry.Curse] = new[] { "Evil", "Punishment" },
                [SpellEntry.Lightning] = new[] { "Thunder", "Storm Seeking" },
                [SpellEntry.ManaDrain] = new[] { "Mage's Bane", "Magical Disruption" },
                [SpellEntry.MindBlast] = new[] { "Mental Strike", "Mental Futility" },
                [SpellEntry.Paralyze] = new[] { "Entrapment", "Entangling" },
                [SpellEntry.EnergyBolt] = new[] { "Disruption", "Static Discharge" },
                [SpellEntry.Explosion] = new[] { "Conflagration", "Flaming Death" },
                [SpellEntry.MassCurse] = new[] { "Corruption", "Daemonic Influence" },
                [SpellEntry.ChainLightning] = new[] { "Heaven's Wrath", "Heaven's Vengance" },
                [SpellEntry.FlameStrike] = new[] { "Hellfire", "Hell's Torment" },
                [SpellEntry.MeteorSwarm] = new[] { "Celestial Fury", "Celestial Folly" },
                [SpellEntry.Earthquake] = new[] { "Gaia's Wrath", "Gaia's Vengeance" },
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