using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Engines.PartySystem;
using Server.Guilds;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Regions;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using static Server.Spells.SpellRegistry;
using static Scripts.Zulu.Engines.Classes.ZuluClassExtensions;
using static Scripts.Zulu.Engines.Classes.SkillCheck;

namespace Server
{
    public class DefensiveSpell
    {
        public static void Nullify(Mobile caster)
        {
            if (!caster.CanBeginAction(typeof(DefensiveSpell)))
                new InternalTimer(caster).Start();
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

        public static bool CheckMulti(Point3D p, Map map, bool houses = true, int housingRange = 0)
        {
            if (map == null || map == Map.Internal)
                return false;

            var sector = map.GetSector(p.X, p.Y);

            foreach (var multi in sector.Multis)
            {
                if (multi is BaseHouse bh)
                {
                    if (houses && bh.IsInside(p, 16) || housingRange > 0 && bh.InRange(p, housingRange))
                        return true;
                }
                else if (multi.Contains(p))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Turn(Mobile caster, object to)
        {
            var target = to as IPoint3D;

            switch (target)
            {
                case null:
                    return;
                case Item item:
                {
                    if (item.RootParent != caster)
                        caster.Direction = caster.GetDirectionTo(item.GetWorldLocation());
                    break;
                }
                default:
                {
                    if (!ReferenceEquals(caster, target))
                    {
                        caster.Direction = caster.GetDirectionTo(target);
                    }

                    break;
                }
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

        public static int TryResistDamage(Mobile caster, Mobile target, SpellCircle circle, int damage)
        {
            if (!caster.Alive || !target.Alive || target.Hidden)
                return 0;

            var points = (int) circle * 40.0;
            var magery = caster.Skills[SkillName.Magery].Value;
            var evalInt = caster.Skills[SkillName.EvalInt].Value;
            var resist = target.Skills[SkillName.MagicResist].Value;
            var chance = resist / 6.0;
            var secondaryChance = resist - magery / 4.0 + (int) circle * 6.0;

            if (secondaryChance > chance)
                chance = secondaryChance;

            if (target.ClassContainsSkill(SkillName.Magery))
            {
                chance *= target.GetClassBonus(SkillName.Magery);
            }
            /*
             * TODO: Replace hacky class checking
             * This is a bit of a hack since we don't want to reference "Warrior" as a class so we're not tightly coupled
             * because the Warrior class can be replaced or removed entirely. This approximates checking a melee-like class.
             * A better solution might be to have positive/negative affinities, i.e. Warrior has negative affinity to magic
             */
            else if (target.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
            {
                var bonus = target.GetClassBonus(SkillName.Swords);
                chance = chance / bonus / 2;
                resist /= bonus;
            }

            // Same as above, just the inverse for the caster
            if (caster.ClassContainsSkill(SkillName.Magery))
            {
                chance /= caster.GetClassBonus(SkillName.Magery);
            }
            else if (caster.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
            {
                var bonus = caster.GetClassBonus(SkillName.Swords);
                chance = chance * bonus * 2;
                resist *= bonus;
            }
            
            if (resist < 25.0)
            {
                AwardPoints(target, SkillName.MagicResist, (int)points / 3);
            }

            if (Utility.RandomMinMax(0, 100) <= chance)
            {
                target.SendLocalizedMessage(502635); // You feel yourself resisting magical energy!
                target.PlaySound(0x1E6);
                target.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);

                damage = GetDamageAfterResist(caster, target, damage);
            }

            return damage;
        }

        public static double CalcSpellDamage(Mobile caster, Mobile target, Spell spell, bool areaSpell = false)
        {
            const int mageryDivider = 5;
            const int playerDivider = 3;
            const int circleMultiplier = 3;
            const int dices = 5;

            if (!caster.Alive || !target.Alive || target.Hidden)
                return 0.0;

            var circle = (int) spell.Info.Circle + 1;
            if (areaSpell)
                circle -= 3;

            if (circle < 1)
                circle = 1;

            var damage = Utility.RandomMinMax(circle * circleMultiplier, circle * circleMultiplier * dices) +
                         caster.Skills[SkillName.Magery].Value / mageryDivider;

            var circleMaxDamage = circle * (13 + circle);
            if (damage > circleMaxDamage)
                damage = circleMaxDamage;

            if (target.Player)
                damage /= playerDivider;

            return damage;
        }

        public static double GetEffectiveness(Mobile caster)
        {
            // TODO: Think about what makes sense here.

            var skill = caster.Skills[SkillName.Magery].Value / 130.0;
            var stat = caster.Int / 130.0;
            var spec = caster is PlayerMobile mobile && mobile.ZuluClass.Type == ZuluClassType.Mage
                ? mobile.ZuluClass.Bonus
                : 1.0;

            return 2 + 0.4 * skill + 0.3 * stat + 0.3 * spec;
        }

        public static int GetDamageAfterResist(Mobile caster, Mobile target, double damage)
        {
            if (!caster.Alive || !target.Alive)
                return 0;

            var evalInt = caster.Skills[SkillName.EvalInt].Value;
            var resist = target.Skills[SkillName.MagicResist].Value;

            damage /= 2;

            if (damage < 1)
                damage = 1;

            damage = (int) (damage * (1.0 + evalInt - resist) / 200.0);

            // Inverting the efficiency bonus, e.g. Mages get less spell damage, warriors get more
            var temp = damage;
            target.FireHook(h => h.OnModifyWithMagicEfficiency(target, ref temp));
            damage -= damage - temp;

            if (damage < 0)
                damage = 0;

            return (int) damage;
        }

        public static bool CanRevealCaster(Mobile m)
        {
            return m is BaseCreature {Controlled: false};
        }

        public static void GetSurfaceTop(ref IPoint3D point)
        {
            point = GetSurfaceTop(point);
        }

        public static Point3D GetSurfaceTop(IPoint3D point)
        {
            switch (point)
            {
                case Item item:
                    return item.GetSurfaceTop();
                case StaticTarget target:
                {
                    var t = target;
                    var z = t.Z;

                    if ((t.Flags & TileFlag.Surface) == 0)
                        z -= TileData.ItemTable[t.ItemID & TileData.MaxItemValue].CalcHeight;

                    return new Point3D(t.X, t.Y, z);
                }
                default:
                    return new Point3D(point);
            }
        }

        public static bool AddStatOffset(Mobile m, StatType type, int offset, TimeSpan duration)
        {
            return offset switch
            {
                > 0 => AddStatBonus(m, m, type, offset, duration),
                < 0 => AddStatCurse(m, m, type, -offset, duration),
                _ => true
            };
        }

        public static bool AddStatBonus(Mobile caster, Mobile target, StatType type)
        {
            return AddStatBonus(caster, target, type, GetModAmount(caster, target, type, false),
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
            return AddStatCurse(caster, target, type, GetModAmount(caster, target, type, true),
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
            var duration = caster.Skills[SkillName.Magery].Value * 4.0;
            caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref duration));

            return TimeSpan.FromSeconds(duration < 1.0 ? 1.0 : duration);
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

        public static int GetModAmount(Mobile caster, Mobile target, StatType? type = null, bool curse = false)
        {
            var modAmount = Utility.RandomMinMax(0, 15) + caster.Skills[SkillName.Magery].Value / 10;
            caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref modAmount));

            return (int) (modAmount < 1.0 ? 1.0 : modAmount);
        }

        public static Guild GetGuildFor(Mobile m)
        {
            var g = m.Guild as Guild;

            if (g == null && m is BaseCreature c)
            {
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

        public static bool ValidIndirectTarget(Mobile caster, Mobile to)
        {
            if (caster == to)
                return true;

            if (to.Hidden && to.AccessLevel > caster.AccessLevel)
                return false;

            var casterGuild = GetGuildFor(caster);
            var toGuild = GetGuildFor(to);

            if (casterGuild != null && toGuild != null && (casterGuild == toGuild || casterGuild.IsAlly(toGuild)))
                return false;

            var p = Party.Get(caster);

            if (p != null && p.Contains(to))
                return false;

            if (to is BaseCreature)
            {
                var c = (BaseCreature) to;

                if (c.Controlled || c.Summoned)
                {
                    if (c.ControlMaster == caster || c.SummonMaster == caster)
                        return false;

                    if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                        return false;
                }
            }

            if (caster is BaseCreature)
            {
                var c = (BaseCreature) caster;

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

            var noto = Notoriety.Compute(caster, to);

            return noto != Notoriety.Innocent || caster.Kills >= 5;
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
                caster.SendLocalizedMessage(501035); // You cannot teleport caster here to the destination.
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
            if (caster is BaseCreature bc &&
                (type == TravelCheckType.TeleportTo || type == TravelCheckType.TeleportFrom))
            {
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

        public static async void Damage(
            double damage,
            Mobile target,
            Mobile attacker = null,
            Spell spell = null,
            TimeSpan? delay = null,
            ElementalType? damageType = null,
            DFAlgorithm dfa = DFAlgorithm.Standard
        )
        {
            delay ??= spell != null ? GetDamageDelayForSpell(spell) : TimeSpan.Zero;

            if (spell != null && damageType == null && SpellInfos.TryGetValue(spell.GetType(), out var info))
                damageType = info.DamageType;
            else
                damageType = ElementalType.None;

            var iDamage = (int) damage;

            if (delay.Value > TimeSpan.Zero)
                await Timer.Pause(delay.Value);

            DoDamage(attacker, target, spell, iDamage, damageType.Value, dfa);
        }

        private static void DoDamage(
            Mobile attacker,
            Mobile target,
            Spell spell,
            int damage,
            ElementalType damageType,
            DFAlgorithm dfa
        )
        {
            target.FireHook(h => h.OnSpellDamage(attacker, target, spell, damageType, ref damage));

            damage = TryResistDamage(attacker, target, spell.Circle, damage);

            WeightOverloading.DFA = dfa;

            if (damage > 0)
                target.Damage(damage, attacker);
            else
                target.Heal(damage * -1);

            if (attacker != null) // sanity check
                DoLeech(damage, attacker, target);

            WeightOverloading.DFA = DFAlgorithm.Standard;

            if (target is BaseCreature c && attacker != null)
                c.OnDamagedBySpell(attacker);

            spell?.RemoveDelayedDamageContext(target);
        }

        private static void DoLeech(int damageGiven, Mobile caster, Mobile target)
        {
        }

        public static void Heal(double amount, Mobile target, Mobile caster, Spell spell, bool message = true)
        {
            target.FireHook(h => h.OnHeal(caster, target, spell, ref amount));
            target.Heal((int) amount, caster, message);
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
                Mobile caster,
                int damage,
                TimeSpan delay,
                ElementalType damageType,
                DFAlgorithm dfa = DFAlgorithm.Standard
            )
                : base(delay)
            {
                m_Target = target;
                m_From = caster;
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