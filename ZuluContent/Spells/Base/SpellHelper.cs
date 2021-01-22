using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Engines.PartySystem;
using Server.Guilds;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server
{
    public class DefensiveSpell
    {
        public static void Nullify(Mobile from)
        {
            if (!from.CanBeginAction(typeof(DefensiveSpell)))
                new InternalTimer(from).Start();
        }

        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;

            public InternalTimer(Mobile m)
                : base(TimeSpan.FromMinutes(1.0))
            {
                m_Mobile = m;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Mobile.EndAction(typeof(DefensiveSpell));
            }
        }
    }
}

namespace Server.Spells
{
    public enum TravelCheckType
    {
        RecallFrom,
        RecallTo,
        GateFrom,
        GateTo,
        Mark,
        TeleportFrom,
        TeleportTo
    }

    public class SpellHelper
    {
        private static readonly TimeSpan DefaultDamageDelay = TimeSpan.FromSeconds(0.5);

        private static readonly TimeSpan CombatHeatDelay = TimeSpan.FromSeconds(30.0);
        private static readonly bool RestrictTravelCombat = true;

        private static readonly int[] Offsets =
        {
            -1, -1,
            -1, 0,
            -1, 1,
            0, -1,
            0, 1,
            1, -1,
            1, 0,
            1, 1
        };

        private static readonly TravelValidator[] Validators =
        {
            IsFeluccaT2A,
            IsFeluccaWind,
            IsFeluccaDungeon,
            IsSafeZone
        };

        private static readonly bool[,] Rules =
        {
            /*T2A(Fel),	Wind(Fel),	Dungeons(Fel),	SafeZone */
            /* Recall From */ {false, false, false, true},
            /* Recall To */ {false, false, false, false},
            /* Gate From */ {false, false, false, false},
            /* Gate To */ {false, false, false, false},
            /* Mark In */ {false, false, false, false},
            /* Tele From */ {true, true, true, true},
            /* Tele To */ {true, true, true, false}
        };

        public static bool DisableSkillCheck { get; set; }

        public static TimeSpan GetDamageDelayForSpell(Spell sp)
        {
            return !sp.DelayedDamage ? TimeSpan.Zero : DefaultDamageDelay;
        }

        public static bool CheckMulti(Point3D p, Map map)
        {
            return CheckMulti(p, map, true, 0);
        }

        public static bool CheckMulti(Point3D p, Map map, bool houses)
        {
            return CheckMulti(p, map, houses, 0);
        }

        public static bool CheckMulti(Point3D p, Map map, bool houses, int housingrange)
        {
            if (map == null || map == Map.Internal)
                return false;

            var sector = map.GetSector(p.X, p.Y);

            for (var i = 0; i < sector.Multis.Count; ++i)
            {
                var multi = sector.Multis[i];

                if (multi is BaseHouse bh)
                {
                    if (houses && bh.IsInside(p, 16) || housingrange > 0 && bh.InRange(p, housingrange))
                        return true;
                }
                else if (multi.Contains(p))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Turn(Mobile from, object to)
        {
            var target = to as IPoint3D;

            if (target == null)
                return;

            if (target is Item)
            {
                var item = (Item) target;

                if (item.RootParent != from)
                    from.Direction = from.GetDirectionTo(item.GetWorldLocation());
            }
            else if (from != target)
            {
                from.Direction = from.GetDirectionTo(target);
            }
        }

        public static bool CheckCombat(Mobile m)
        {
            if (!RestrictTravelCombat)
                return false;

            for (var i = 0; i < m.Aggressed.Count; ++i)
            {
                var info = m.Aggressed[i];

                if (info.Defender.Player && DateTime.Now - info.LastCombatTime < CombatHeatDelay)
                    return true;
            }

            return false;
        }

        public static bool AdjustField(ref Point3D p, Map map, int height, bool mobsBlock)
        {
            if (map == null)
                return false;

            for (var offset = 0; offset < 10; ++offset)
            {
                var loc = new Point3D(p.X, p.Y, p.Z - offset);

                if (map.CanFit(loc, height, true, mobsBlock))
                {
                    p = loc;
                    return true;
                }
            }

            return false;
        }

        public static double GetEffectiveness(Mobile caster)
        {
            // TODO: Think about what makes sense here.

            var skill = caster.Skills[SkillName.Magery].Value / 130.0;
            var stat = caster.Int / 130.0;
            var spec = caster is PlayerMobile mobile && mobile.Spec.SpecName == SpecName.Mage
                ? mobile.Spec.Bonus
                : 1.0;

            return 2 + 0.4 * skill + 0.3 * stat + 0.3 * spec;
        }


        public static bool CanRevealCaster(Mobile m)
        {
            if (m is BaseCreature)
            {
                var c = (BaseCreature) m;

                if (!c.Controlled)
                    return true;
            }

            return false;
        }

        public static void GetSurfaceTop(ref IPoint3D p)
        {
            if (p is Item)
            {
                p = ((Item) p).GetSurfaceTop();
            }
            else if (p is StaticTarget)
            {
                var t = (StaticTarget) p;
                var z = t.Z;

                if ((t.Flags & TileFlag.Surface) == 0)
                    z -= TileData.ItemTable[t.ItemID & TileData.MaxItemValue].CalcHeight;

                p = new Point3D(t.X, t.Y, z);
            }
        }

        public static bool AddStatOffset(Mobile m, StatType type, int offset, TimeSpan duration)
        {
            if (offset > 0)
                return AddStatBonus(m, m, type, offset, duration);
            if (offset < 0)
                return AddStatCurse(m, m, type, -offset, duration);

            return true;
        }

        public static bool AddStatBonus(Mobile caster, Mobile target, StatType type)
        {
            return AddStatBonus(caster, target, type, GetOffset(caster, target, type, false),
                GetDuration(caster, target));
        }

        public static bool AddStatBonus(Mobile caster, Mobile target, StatType type, int bonus, TimeSpan duration)
        {
            var offset = bonus;
            var name = $"[Magic] {type} Offset";

            var mod = target.GetStatMod(name);

            if (mod != null && mod.Offset < 0)
            {
                target.AddStatMod(new StatMod(type, name, mod.Offset + offset, duration));
                return true;
            }

            if (mod == null || mod.Offset < offset)
            {
                target.AddStatMod(new StatMod(type, name, offset, duration));
                return true;
            }

            return false;
        }

        public static bool AddStatCurse(Mobile caster, Mobile target, StatType type)
        {
            return AddStatCurse(caster, target, type, GetOffset(caster, target, type, true),
                GetDuration(caster, target));
        }

        public static bool AddStatCurse(Mobile caster, Mobile target, StatType type, int curse, TimeSpan duration)
        {
            var offset = -curse;
            var name = $"[Magic] {type} Offset";

            var mod = target.GetStatMod(name);

            if (mod != null && mod.Offset > 0)
            {
                target.AddStatMod(new StatMod(type, name, mod.Offset + offset, duration));
                return true;
            }

            if (mod == null || mod.Offset > offset)
            {
                target.AddStatMod(new StatMod(type, name, offset, duration));
                return true;
            }

            return false;
        }

        public static TimeSpan GetDuration(Mobile caster, Mobile target)
        {
            return TimeSpan.FromSeconds(caster.Skills[SkillName.Magery].Value * 1.2);
        }

        public static double GetOffsetScalar(Mobile caster, Mobile target, bool curse)
        {
            double percent;

            if (curse)
                percent = 8 + caster.Skills.EvalInt.Fixed / 100 - target.Skills.MagicResist.Fixed / 100;
            else
                percent = 1 + caster.Skills.EvalInt.Fixed / 100;

            percent *= 0.01;

            if (percent < 0)
                percent = 0;

            return percent;
        }

        public static int GetOffset(Mobile caster, Mobile target, StatType type, bool curse)
        {
            return 1 + (int) (caster.Skills[SkillName.Magery].Value * 0.1);
        }

        public static Guild GetGuildFor(Mobile m)
        {
            var g = m.Guild as Guild;

            if (g == null && m is BaseCreature)
            {
                var c = (BaseCreature) m;
                m = c.ControlMaster;

                if (m != null)
                    g = m.Guild as Guild;

                if (g == null)
                {
                    m = c.SummonMaster;

                    if (m != null)
                        g = m.Guild as Guild;
                }
            }

            return g;
        }

        public static bool ValidIndirectTarget(Mobile from, Mobile to)
        {
            if (from == to)
                return true;

            if (to.Hidden && to.AccessLevel > from.AccessLevel)
                return false;

            var fromGuild = GetGuildFor(from);
            var toGuild = GetGuildFor(to);

            if (fromGuild != null && toGuild != null && (fromGuild == toGuild || fromGuild.IsAlly(toGuild)))
                return false;

            var p = Party.Get(from);

            if (p != null && p.Contains(to))
                return false;

            if (to is BaseCreature)
            {
                var c = (BaseCreature) to;

                if (c.Controlled || c.Summoned)
                {
                    if (c.ControlMaster == from || c.SummonMaster == from)
                        return false;

                    if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                        return false;
                }
            }

            if (from is BaseCreature)
            {
                var c = (BaseCreature) from;

                if (c.Controlled || c.Summoned)
                {
                    if (c.ControlMaster == to || c.SummonMaster == to)
                        return false;

                    p = Party.Get(to);

                    if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                        return false;
                }
            }

            if (to is BaseCreature && !((BaseCreature) to).Controlled && ((BaseCreature) to).InitialInnocent)
                return true;

            var noto = Notoriety.Compute(from, to);

            return noto != Notoriety.Innocent || from.Kills >= 5;
        }

        public static void Summon(BaseCreature creature, Mobile caster, int sound, TimeSpan duration,
            bool scaleDuration, bool scaleStats)
        {
            var map = caster.Map;

            if (map == null)
                return;

            var scale = 1.0 + (caster.Skills[SkillName.Magery].Value - 100.0) / 200.0;

            if (scaleDuration)
                duration = TimeSpan.FromSeconds(duration.TotalSeconds * scale);

            if (scaleStats)
            {
                creature.RawStr = (int) (creature.RawStr * scale);
                creature.Hits = creature.HitsMax;

                creature.RawDex = (int) (creature.RawDex * scale);
                creature.Stam = creature.StamMax;

                creature.RawInt = (int) (creature.RawInt * scale);
                creature.Mana = creature.ManaMax;
            }

            var p = new Point3D(caster);

            if (FindValidSpawnLocation(map, ref p, true))
            {
                BaseCreature.Summon(creature, caster, p, sound, duration);
                return;
            }


            creature.Delete();
            caster.SendLocalizedMessage(501942); // That location is blocked.
        }

        public static bool FindValidSpawnLocation(Map map, ref Point3D p, bool surroundingsOnly)
        {
            if (map == null) //sanity
                return false;

            if (!surroundingsOnly)
            {
                if (map.CanSpawnMobile(p)) //p's fine.
                {
                    p = new Point3D(p);
                    return true;
                }

                var z = map.GetAverageZ(p.X, p.Y);

                if (map.CanSpawnMobile(p.X, p.Y, z))
                {
                    p = new Point3D(p.X, p.Y, z);
                    return true;
                }
            }

            var offset = Utility.Random(8) * 2;

            for (var i = 0; i < Offsets.Length; i += 2)
            {
                var x = p.X + Offsets[(offset + i) % Offsets.Length];
                var y = p.Y + Offsets[(offset + i + 1) % Offsets.Length];

                if (map.CanSpawnMobile(x, y, p.Z))
                {
                    p = new Point3D(x, y, p.Z);
                    return true;
                }

                var z = map.GetAverageZ(x, y);

                if (map.CanSpawnMobile(x, y, z))
                {
                    p = new Point3D(x, y, z);
                    return true;
                }
            }

            return false;
        }

        public static void SendInvalidMessage(Mobile caster, TravelCheckType type)
        {
            if (type == TravelCheckType.RecallTo || type == TravelCheckType.GateTo)
                caster.SendLocalizedMessage(1019004); // You are not allowed to travel there.
            else if (type == TravelCheckType.TeleportTo)
                caster.SendLocalizedMessage(501035); // You cannot teleport from here to the destination.
            else
                caster.SendLocalizedMessage(501802); // Thy spell doth not appear to work...
        }

        public static bool CheckTravel(Mobile caster, TravelCheckType type)
        {
            return CheckTravel(caster, caster.Map, caster.Location, type);
        }

        public static bool CheckTravel(Mobile caster, Map map, Point3D loc, TravelCheckType type)
        {
            if (IsInvalid(map, loc)) // null, internal, out of bounds
            {
                if (caster != null)
                    SendInvalidMessage(caster, type);

                return false;
            }

            if (caster != null && caster.AccessLevel == AccessLevel.Player && caster.Region.IsPartOf<JailRegion>())
            {
                caster.SendLocalizedMessage(1114345); // You'll need a better jailbreak plan than that!
                return false;
            }

            // Always allow monsters to teleport
            if (caster is BaseCreature && (type == TravelCheckType.TeleportTo || type == TravelCheckType.TeleportFrom))
            {
                var bc = (BaseCreature) caster;

                if (!bc.Controlled && !bc.Summoned)
                    return true;
            }

            var v = (int) type;
            var isValid = true;

            for (var i = 0; isValid && i < Validators.Length; ++i)
                isValid = Rules[v, i] || !Validators[i](map, loc);

            if (!isValid && caster != null)
                SendInvalidMessage(caster, type);

            return isValid;
        }

        public static bool IsWindLoc(Point3D loc)
        {
            int x = loc.X, y = loc.Y;

            return x >= 5120 && y >= 0 && x < 5376 && y < 256;
        }

        public static bool IsFeluccaWind(Map map, Point3D loc)
        {
            return map == Map.Felucca && IsWindLoc(loc);
        }

        public static bool IsFeluccaT2A(Map map, Point3D loc)
        {
            int x = loc.X, y = loc.Y;

            return map == Map.Felucca && x >= 5120 && y >= 2304 && x < 6144 && y < 4096;
        }

        public static bool IsFeluccaDungeon(Map map, Point3D loc)
        {
            var region = Region.Find(loc, map);
            return region.IsPartOf<DungeonRegion>() && region.Map == Map.Felucca;
        }

        public static bool IsSafeZone(Map map, Point3D loc)
        {
            return false;
        }

        public static bool IsInvalid(Map map, Point3D loc)
        {
            if (map == null || map == Map.Internal)
                return true;

            int x = loc.X, y = loc.Y;

            return x < 0 || y < 0 || x >= map.Width || y >= map.Height;
        }

        public static bool IsTown(Point3D loc, Mobile caster)
        {
            var map = caster.Map;

            if (map == null)
                return false;

            var reg = (GuardedRegion) Region.Find(loc, map).GetRegion(typeof(GuardedRegion));

            return reg != null && !reg.IsDisabled();
        }

        public static bool CheckTown(IPoint3D loc, Mobile caster)
        {
            if (loc is Item)
                loc = ((Item) loc).GetWorldLocation();

            return CheckTown(new Point3D(loc), caster);
        }

        public static bool CheckTown(Point3D loc, Mobile caster)
        {
            if (IsTown(loc, caster))
            {
                caster.SendLocalizedMessage(500946); // You cannot cast this in town!
                return false;
            }

            return true;
        }

        public static void CheckReflect(int circle, Mobile caster, ref Mobile target)
        {
            CheckReflect(circle, ref caster, ref target);
        }

        public static void CheckReflect(int circle, ref Mobile caster, ref Mobile target)
        {
            if (target.MagicDamageAbsorb > 0)
            {
                ++circle;

                target.MagicDamageAbsorb -= circle;

                // This order isn't very intuitive, but you have to nullify reflect before target gets switched

                var reflect = target.MagicDamageAbsorb >= 0;

                if (target is BaseCreature creature)
                    creature.CheckReflect(caster, ref reflect);

                if (target.MagicDamageAbsorb <= 0)
                {
                    target.MagicDamageAbsorb = 0;
                    DefensiveSpell.Nullify(target);
                }

                if (reflect)
                {
                    target.FixedEffect(0x37B9, 10, 5);

                    var temp = caster;
                    caster = target;
                    target = temp;
                }
            }
            else if (target is BaseCreature creature)
            {
                var reflect = false;

                creature.CheckReflect(caster, ref reflect);

                if (reflect)
                {
                    creature.FixedEffect(0x37B9, 10, 5);

                    var temp = caster;
                    caster = creature;
                    target = temp;
                }
            }
        }

        public static void Damage(
            double damage,
            Mobile defender,
            Mobile attacker = null,
            Spell spell = null,
            TimeSpan? delay = null,
            ElementalType damageType = ElementalType.None,
            DFAlgorithm dfa = DFAlgorithm.Standard
        )
        {
            if (spell != null)
            {
                delay ??= GetDamageDelayForSpell(spell);

                if (damageType == ElementalType.None &&
                    SpellRegistry.SpellInfos.TryGetValue(spell.GetType(), out var info))
                {
                    damageType = info.DamageType;
                }
            }

            delay ??= TimeSpan.Zero;

            var iDamage = (int) damage;

            if (delay.Value == TimeSpan.Zero)
                DoDamage(attacker, defender, spell, iDamage, damageType, dfa);
            else
                new SpellDamageTimer(spell, defender, attacker, iDamage, delay.Value, damageType, dfa)
                    .Start();
        }

        private static void DoDamage(
            Mobile attacker,
            Mobile defender,
            Spell spell,
            int damage,
            ElementalType damageType,
            DFAlgorithm dfa
        )
        {
            defender.FireHook(h => h.OnSpellDamage(attacker, defender, spell.Circle, damageType, ref damage));

            WeightOverloading.DFA = dfa;

            defender.Damage(damage, attacker);

            if (attacker != null) // sanity check
                DoLeech(damage, attacker, defender);

            WeightOverloading.DFA = DFAlgorithm.Standard;

            if (defender is BaseCreature c && attacker != null)
            {
                c.OnHarmfulSpell(attacker);
                c.OnDamagedBySpell(attacker);
            }

            spell?.RemoveDelayedDamageContext(defender);
        }

        private static void DoLeech(int damageGiven, Mobile from, Mobile target)
        {
        }

        public static void Heal(int amount, Mobile target, Mobile from, bool message = true)
        {
            //TODO: All Healing *spells* go through ArcaneEmpowerment
            target.Heal(amount, from, message);
        }

        private delegate bool TravelValidator(Map map, Point3D loc);

        private class SpellDamageTimer : Timer
        {
            private readonly int m_Damage;
            private readonly ElementalType m_DamageType;
            private readonly DFAlgorithm m_Dfa;
            private readonly Spell m_Spell;
            private readonly Mobile m_Target;
            private readonly Mobile m_From;

            public SpellDamageTimer(
                Spell s,
                Mobile target,
                Mobile from,
                int damage,
                TimeSpan delay,
                ElementalType damageType,
                DFAlgorithm dfa = DFAlgorithm.Standard
            )
                : base(delay)
            {
                m_Target = target;
                m_From = from;
                m_Damage = damage;
                m_DamageType = damageType;
                m_Dfa = dfa;
                m_Spell = s;

                if (m_Spell != null && m_Spell.DelayedDamage && !m_Spell.DelayedDamageStacking)
                    m_Spell.StartDelayedDamageContext(target, this);

                Priority = TimerPriority.TwentyFiveMS;
            }

            protected override void OnTick()
            {
                DoDamage(m_From, m_Target, m_Spell, m_Damage, m_DamageType, m_Dfa);
            }
        }
    }
}