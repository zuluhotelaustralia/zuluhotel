using Earth = Scripts.Zulu.Spells.Earth;
using Necromancy = Scripts.Zulu.Spells.Necromancy;
using static Server.Spells.SpellRegistry;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Server.Spells
{
    public class SpellSetup
    {
        public static void Initialize()
        {
            Register(SpellType.None, (caster, scroll) => (Spell)null);

            // First circle
            Register(SpellType.Clumsy, (caster, scroll) => new First.ClumsySpell(caster, scroll));
            Register(SpellType.CreateFood, (caster, scroll) => new First.CreateFoodSpell(caster, scroll));
            Register(SpellType.Feeblemind, (caster, scroll) => new First.FeeblemindSpell(caster, scroll));
            Register(SpellType.Heal, (caster, scroll) => new First.HealSpell(caster, scroll));
            Register(SpellType.MagicArrow, (caster, scroll) => new First.MagicArrowSpell(caster, scroll));
            Register(SpellType.NightSight, (caster, scroll) => new First.NightSightSpell(caster, scroll));
            Register(SpellType.ReactiveArmor, (caster, scroll) => new First.ReactiveArmorSpell(caster, scroll));
            Register(SpellType.Weaken, (caster, scroll) => new First.WeakenSpell(caster, scroll));

            // Second circle
            Register(SpellType.Agility, (caster, scroll) => new Second.AgilitySpell(caster, scroll));
            Register(SpellType.Cunning, (caster, scroll) => new Second.CunningSpell(caster, scroll));
            Register(SpellType.Cure, (caster, scroll) => new Second.CureSpell(caster, scroll));
            Register(SpellType.Harm, (caster, scroll) => new Second.HarmSpell(caster, scroll));
            Register(SpellType.MagicTrap, (caster, scroll) => new Second.MagicTrapSpell(caster, scroll));
            Register(SpellType.RemoveTrap, (caster, scroll) => new Second.RemoveTrapSpell(caster, scroll));
            Register(SpellType.Protection, (caster, scroll) => new Second.ProtectionSpell(caster, scroll));
            Register(SpellType.Strength, (caster, scroll) => new Second.StrengthSpell(caster, scroll));

            // Third circle
            Register(SpellType.Bless, (caster, scroll) => new Third.BlessSpell(caster, scroll));
            Register(SpellType.Fireball, (caster, scroll) => new Third.FireballSpell(caster, scroll));
            Register(SpellType.MagicLock, (caster, scroll) => new Third.MagicLockSpell(caster, scroll));
            Register(SpellType.Poison, (caster, scroll) => new Third.PoisonSpell(caster, scroll));
            Register(SpellType.Telekinesis, (caster, scroll) => new Third.TelekinesisSpell(caster, scroll));
            Register(SpellType.Teleport, (caster, scroll) => new Third.TeleportSpell(caster, scroll));
            Register(SpellType.Unlock, (caster, scroll) => new Third.UnlockSpell(caster, scroll));
            Register(SpellType.WallOfStone, (caster, scroll) => new Third.WallOfStoneSpell(caster, scroll));

            // Fourth circle
            Register(SpellType.ArchCure, (caster, scroll) => new Fourth.ArchCureSpell(caster, scroll));
            Register(SpellType.ArchProtection, (caster, scroll) => new Fourth.ArchProtectionSpell(caster, scroll));
            Register(SpellType.Curse, (caster, scroll) => new Fourth.CurseSpell(caster, scroll));
            Register(SpellType.FireField, (caster, scroll) => new Fourth.FireFieldSpell(caster, scroll));
            Register(SpellType.GreaterHeal, (caster, scroll) => new Fourth.GreaterHealSpell(caster, scroll));
            Register(SpellType.Lightning, (caster, scroll) => new Fourth.LightningSpell(caster, scroll));
            Register(SpellType.ManaDrain, (caster, scroll) => new Fourth.ManaDrainSpell(caster, scroll));
            Register(SpellType.Recall, (caster, scroll) => new Fourth.RecallSpell(caster, scroll));

            // Fifth circle
            Register(SpellType.BladeSpirits, (caster, scroll) => new Fifth.BladeSpiritsSpell(caster, scroll));
            Register(SpellType.DispelField, (caster, scroll) => new Fifth.DispelFieldSpell(caster, scroll));
            Register(SpellType.Incognito, (caster, scroll) => new Fifth.IncognitoSpell(caster, scroll));
            Register(SpellType.MagicReflect, (caster, scroll) => new Fifth.MagicReflectSpell(caster, scroll));
            Register(SpellType.MindBlast, (caster, scroll) => new Fifth.MindBlastSpell(caster, scroll));
            Register(SpellType.Paralyze, (caster, scroll) => new Fifth.ParalyzeSpell(caster, scroll));
            Register(SpellType.PoisonField, (caster, scroll) => new Fifth.PoisonFieldSpell(caster, scroll));
            Register(SpellType.SummonCreature, (caster, scroll) => new Fifth.SummonCreatureSpell(caster, scroll));

            // Sixth circle
            Register(SpellType.Dispel, (caster, scroll) => new Sixth.DispelSpell(caster, scroll));
            Register(SpellType.EnergyBolt, (caster, scroll) => new Sixth.EnergyBoltSpell(caster, scroll));
            Register(SpellType.Explosion, (caster, scroll) => new Sixth.ExplosionSpell(caster, scroll));
            Register(SpellType.Invisibility, (caster, scroll) => new Sixth.InvisibilitySpell(caster, scroll));
            Register(SpellType.Mark, (caster, scroll) => new Sixth.MarkSpell(caster, scroll));
            Register(SpellType.MassCurse, (caster, scroll) => new Sixth.MassCurseSpell(caster, scroll));
            Register(SpellType.ParalyzeField, (caster, scroll) => new Sixth.ParalyzeFieldSpell(caster, scroll));
            Register(SpellType.Reveal, (caster, scroll) => new Sixth.RevealSpell(caster, scroll));

            // Seventh circle
            Register(SpellType.ChainLightning, (caster, scroll) => new Seventh.ChainLightningSpell(caster, scroll));
            Register(SpellType.EnergyField, (caster, scroll) => new Seventh.EnergyFieldSpell(caster, scroll));
            Register(SpellType.FlameStrike, (caster, scroll) => new Seventh.FlameStrikeSpell(caster, scroll));
            Register(SpellType.GateTravel, (caster, scroll) => new Seventh.GateTravelSpell(caster, scroll));
            Register(SpellType.ManaVampire, (caster, scroll) => new Seventh.ManaVampireSpell(caster, scroll));
            Register(SpellType.MassDispel, (caster, scroll) => new Seventh.MassDispelSpell(caster, scroll));
            Register(SpellType.MeteorSwarm, (caster, scroll) => new Seventh.MeteorSwarmSpell(caster, scroll));
            Register(SpellType.Polymorph, (caster, scroll) => new Seventh.PolymorphSpell(caster, scroll));

            // Eighth circle
            Register(SpellType.Earthquake, (caster, scroll) => new Eighth.EarthquakeSpell(caster, scroll));
            Register(SpellType.EnergyVortex, (caster, scroll) => new Eighth.EnergyVortexSpell(caster, scroll));
            Register(SpellType.Resurrection, (caster, scroll) => new Eighth.ResurrectionSpell(caster, scroll));
            Register(SpellType.AirElemental, (caster, scroll) => new Eighth.AirElementalSpell(caster, scroll));
            Register(SpellType.SummonDaemon, (caster, scroll) => new Eighth.SummonDaemonSpell(caster, scroll));
            Register(SpellType.EarthElemental, (caster, scroll) => new Eighth.EarthElementalSpell(caster, scroll));
            Register(SpellType.FireElemental, (caster, scroll) => new Eighth.FireElementalSpell(caster, scroll));
            Register(SpellType.WaterElemental, (caster, scroll) => new Eighth.WaterElementalSpell(caster, scroll));

            Register(SpellType.ControlUndead, (caster, scroll) => new Necromancy.ControlUndeadSpell(caster, scroll));
            Register(SpellType.Darkness, (caster, scroll) => new Necromancy.DarknessSpell(caster, scroll));
            Register(SpellType.DecayingRay, (caster, scroll) => new Necromancy.DecayingRaySpell(caster, scroll));
            Register(SpellType.SpectresTouch, (caster, scroll) => new Necromancy.SpectresTouchSpell(caster, scroll));
            Register(SpellType.AbyssalFlame, (caster, scroll) => new Necromancy.AbyssalFlameSpell(caster, scroll));
            Register(SpellType.AnimateDead, (caster, scroll) => new Necromancy.AnimateDeadSpell(caster, scroll));
            Register(SpellType.Sacrifice, (caster, scroll) => new Necromancy.SacrificeSpell(caster, scroll));
            Register(SpellType.WraithBreath, (caster, scroll) => new Necromancy.WraithBreathSpell(caster, scroll));
            Register(SpellType.SorcerorsBane, (caster, scroll) => new Necromancy.SorcerorsBaneSpell(caster, scroll));
            Register(SpellType.SummonSpirit, (caster, scroll) => new Necromancy.SummonSpiritSpell(caster, scroll));
            Register(SpellType.WraithForm, (caster, scroll) => new Necromancy.WraithFormSpell(caster, scroll));
            Register(SpellType.WyvernStrike, (caster, scroll) => new Necromancy.WyvernStrikeSpell(caster, scroll));
            Register(SpellType.Kill, (caster, scroll) => new Necromancy.KillSpell(caster, scroll));
            Register(SpellType.LicheForm, (caster, scroll) => new Necromancy.LicheFormSpell(caster, scroll));
            Register(SpellType.Plague, (caster, scroll) => new Necromancy.PlagueSpell(caster, scroll));
            Register(SpellType.Spellbind, (caster, scroll) => new Necromancy.SpellbindSpell(caster, scroll));

            Register(SpellType.Antidote, (caster, scroll) => new Earth.AntidoteSpell(caster, scroll));
            Register(SpellType.OwlSight, (caster, scroll) => new Earth.OwlSightSpell(caster, scroll));
            Register(SpellType.ShiftingEarth, (caster, scroll) => new Earth.ShiftingEarthSpell(caster, scroll));
            Register(SpellType.SummonMammals, (caster, scroll) => new Earth.SummonMammalsSpell(caster, scroll));
            Register(SpellType.CallLightning, (caster, scroll) => new Earth.CallLightningSpell(caster, scroll));
            Register(SpellType.EarthsBlessing, (caster, scroll) => new Earth.EarthsBlessingSpell(caster, scroll));
            Register(SpellType.EarthPortal, (caster, scroll) => new Earth.EarthPortalSpell(caster, scroll));
            Register(SpellType.NaturesTouch, (caster, scroll) => new Earth.NaturesTouchSpell(caster, scroll));
            Register(SpellType.GustOfAir, (caster, scroll) => new Earth.GustOfAirSpell(caster, scroll));
            Register(SpellType.RisingFire, (caster, scroll) => new Earth.RisingFireSpell(caster, scroll));
            Register(SpellType.Shapeshift, (caster, scroll) => new Earth.ShapeshiftSpell(caster, scroll));
            Register(SpellType.IceStrike, (caster, scroll) => new Earth.IceStrikeSpell(caster, scroll));
            Register(SpellType.EarthSpirit, (caster, scroll) => new Earth.EarthSpiritSpell(caster, scroll));
            Register(SpellType.FireSpirit, (caster, scroll) => new Earth.FireSpiritSpell(caster, scroll));
            Register(SpellType.StormSpirit, (caster, scroll) => new Earth.StormSpiritSpell(caster, scroll));
            Register(SpellType.WaterSpirit, (caster, scroll) => new Earth.WaterSpiritSpell(caster, scroll));
        }
    }
}