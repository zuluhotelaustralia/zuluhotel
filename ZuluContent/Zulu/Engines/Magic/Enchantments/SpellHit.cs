using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Server;
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
        public static readonly Dictionary<SpellEntry, Func<Mobile, Spell>> Spells = new()
        {
            [SpellEntry.None] = _ => null,
            [SpellEntry.Clumsy] = caster => new ClumsySpell(caster),
            [SpellEntry.Feeblemind] = caster => new FeeblemindSpell(caster),
            [SpellEntry.MagicArrow] = caster => new MagicArrowSpell(caster),
            [SpellEntry.Weaken] = caster => new WeakenSpell(caster),
            [SpellEntry.Harm] = caster => new HarmSpell(caster),
            [SpellEntry.Fireball] = caster => new FireballSpell(caster),
            [SpellEntry.Curse] = caster => new CurseSpell(caster),
            [SpellEntry.Lightning] = caster => new LightningSpell(caster),
            [SpellEntry.ManaDrain] = caster => new ManaDrainSpell(caster),
            [SpellEntry.MindBlast] = caster => new MindBlastSpell(caster),
            [SpellEntry.Paralyze] = caster => new ParalyzeSpell(caster),
            [SpellEntry.EnergyBolt] = caster => new EnergyBoltSpell(caster),
            [SpellEntry.Explosion] = caster => new ExplosionSpell(caster),
            [SpellEntry.MassCurse] = caster => new MassCurseSpell(caster),
            [SpellEntry.ChainLightning] = caster => new ChainLightningSpell(caster),
            [SpellEntry.FlameStrike] = caster => new FlameStrikeSpell(caster),
            [SpellEntry.MeteorSwarm] = caster => new MeteorSwarmSpell(caster),
            [SpellEntry.Earthquake] = caster => new EarthquakeSpell(caster),
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
            {
                var spell = action(attacker);

                switch (spell)
                {
                    case ITargetableAsyncSpell<Mobile> targetableAsyncSpell:
                        targetableAsyncSpell.OnTargetAsync(new TargetResponse<Mobile>
                        {
                            Target = defender,
                            Type = TargetResponseType.Success
                        });
                        break;
                    case IAsyncSpell asyncSpell:
                        asyncSpell.CastAsync();
                        break;
                }
            }
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
            {string.Empty, string.Empty},
            {"Bungling", "Clumsiness"},
            {"Senility", "Foolishness"},
            {"Burning", "Pain"},
            {"Weakening", "Weakness"},
            {"Wounding", "Anguish"},
            {"Daemon's Breath", "Daemonic Torment"},
            {"Evil", "Punishment"},
            {"Thunder", "Storm Seeking"},
            {"Mage's Bane", "Magical Disruption"},
            {"Mental Strike", "Mental Futility"},
            {"Entrapment", "Entangling"},
            {"Disruption", "Static Discharge"},
            {"Conflagration", "Flaming Death"},
            {"Corruption", "Daemonic Influence"},
            {"Heaven's Wrath", "Heaven's Vengance"},
            {"Hellfire", "Hell's Torment"},
            {"Celestial Fury", "Celestial Folly"},
            {"Gaia's Wrath", "Gaia's Vengeance"},
        };
    }
}