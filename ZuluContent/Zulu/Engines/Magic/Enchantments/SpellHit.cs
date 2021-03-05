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
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SpellHit : Enchantment<SpellHitInfo>
    {
        // TODO: Replace with ITargetableAsyncSpell
        public static readonly IReadOnlyDictionary<SpellEntry, Action<Mobile, Mobile>> Spells =
            new Dictionary<SpellEntry, Action<Mobile, Mobile>>
            {
                [SpellEntry.None]  = (a, d) => {},
                // [SpellEntry.Clumsy] = (a, d) => Spell.Create<ClumsySpell>(a, null, true).Target(d),
                // [SpellEntry.Feeblemind] = (a, d) => Spell.Create<FeeblemindSpell>(a, null, true).Target(d),
                // [SpellEntry.MagicArrow] = (a, d) => Spell.Create<MagicArrowSpell>(a, null, true).OnTargetAsync(new TargetResponse<Mobile> { Target = d }),
                // [SpellEntry.Weaken] = (a, d) => Spell.Create<WeakenSpell>(a, null, true).Target(d),
                // [SpellEntry.Harm] = (a, d) => Spell.Create<HarmSpell>(a, null, true).Target(d),
                // [SpellEntry.Fireball] = (a, d) => Spell.Create<FireballSpell>(a, null, true).Target(d),
                // [SpellEntry.Curse] = (a, d) => Spell.Create<CurseSpell>(a, null, true).Target(d),
                // [SpellEntry.Lightning] = (a, d) => Spell.Create<LightningSpell>(a, null, true).Target(d),
                // [SpellEntry.ManaDrain] = (a, d) => Spell.Create<ManaDrainSpell>(a, null, true).Target(d),
                // [SpellEntry.MindBlast] = (a, d) => Spell.Create<MindBlastSpell>(a, null, true).Target(d),
                // [SpellEntry.Paralyze] = (a, d) => Spell.Create<ParalyzeSpell>(a, null, true).Target(d),
                // [SpellEntry.EnergyBolt] = (a, d) => Spell.Create<EnergyBoltSpell>(a, null, true).Target(d),
                // [SpellEntry.Explosion] = (a, d) => Spell.Create<ExplosionSpell>(a, null, true).Target(d),
                // [SpellEntry.MassCurse] = (a, d) => Spell.Create<MassCurseSpell>(a, null, true).Target(d),
                // [SpellEntry.ChainLightning] = (a, d) => Spell.Create<ChainLightningSpell>(a, null, true).Target(d),
                // [SpellEntry.FlameStrike] = (a, d) => Spell.Create<FlameStrikeSpell>(a, null, true).Target(d),
                // [SpellEntry.MeteorSwarm] = (a, d) => Spell.Create<MeteorSwarmSpell>(a, null, true).Target(d),
                // [SpellEntry.Earthquake] = (a, _) => Spell.Create<EarthquakeSpell>(a, null, true).OnCast(),
            };

        private static readonly List<SpellEntry> Entries = Spells.Keys.ToList();

        private SpellEntry m_SpellEntry = SpellEntry.None;

        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Entries.IndexOf(SpellEntry), Cursed);

        [Key(1)]
        public SpellEntry SpellEntry
        {
            get => m_SpellEntry;
            // Not all spells are allowed, so if someone sets an unsupported spell we make it None instead.
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
        
        public override string[,] Names { get; protected set; } =
        {
            // These are in order of SpellHit.Entries.IndexOf()
            { string.Empty, string.Empty },
            { "Bungling", "Clumsiness" },
            { "Senility", "Foolishness" },
            { "Burning", "Pain" },
            { "Weakening", "Weakness" },
            { "Wounding", "Anguish" },
            { "Daemon's Breath", "Daemonic Torment" },
            { "Evil", "Punishment" },
            { "Thunder", "Storm Seeking" },
            { "Mage's Bane", "Magical Disruption" },
            { "Mental Strike", "Mental Futility" },
            { "Entrapment", "Entangling" },
            { "Disruption", "Static Discharge" },
            { "Conflagration", "Flaming Death" },
            { "Corruption", "Daemonic Influence" },
            { "Heaven's Wrath", "Heaven's Vengance" },
            { "Hellfire", "Hell's Torment" },
            { "Celestial Fury", "Celestial Folly" },
            { "Gaia's Wrath", "Gaia's Vengeance" },
        };
    }
}