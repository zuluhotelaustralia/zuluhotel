using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Engines.Magic;
using Server.Engines.PartySystem;
using Server.Guilds;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using static Server.Spells.SpellRegistry;
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
        private static readonly bool RestrictTravelCombat = false;

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

        private static readonly Func<Map, Point3D, bool>[] Validators =
        {
            IsFeluccaT2A,
            IsFeluccaWind,
            IsFeluccaDungeon,
            IsSafeZone
        };

        private static readonly bool[,] Rules =
        {
            /*T2A(Fel),	Wind(Fel),	Dungeons(Fel),	SafeZone */
            /* Recall From */ {false, false, true, true},
            /* Recall To */ {false, false, false, false},
            /* Gate From */ {false, false, true, false},
            /* Gate To */ {false, false, false, false},
            /* Mark In */ {false, false, false, false},
            /* Tele From */ {true, true, true, true},
            /* Tele To */ {true, true, true, false}
        };
        
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

            return m.Aggressed.Any(info => info.Defender.Player && DateTime.Now - info.LastCombatTime < CombatHeatDelay);
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

        public static bool TryResist(Mobile caster, Mobile target, SpellCircle circle)
        {
            if (!caster.Alive || !target.Alive || target.Hidden)
                return false;

            var points = (int) circle * 40.0;
            var magery = caster.Skills[SkillName.Magery].Value;
            var resist = target.Skills[SkillName.MagicResist].Value;
            var chance = resist / 6.0;
            var secondaryChance = resist - ((magery / 4.0) + ((int) circle * 6.0));

            if (secondaryChance > chance)
                chance = secondaryChance;

            if (target.ClassContainsSkill(SkillName.Magery))
            {
                chance *= target.GetClassModifier(SkillName.Magery);
            }
            /*
             * TODO: Replace hacky class checking
             * This is a bit of a hack since we don't want to reference "Warrior" as a class so we're not tightly coupled
             * because the Warrior class can be replaced or removed entirely. This approximates checking a melee-like class.
             * A better solution might be to have positive/negative affinities, i.e. Warrior has negative affinity to magic
             */
            else if (target.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
            {
                var bonus = target.GetClassModifier(SkillName.Swords);
                chance = chance / bonus / 2;
                resist /= bonus;
            }

            // Same as above, just the inverse for the caster
            if (caster.ClassContainsSkill(SkillName.Magery))
            {
                chance /= caster.GetClassModifier(SkillName.Magery);
            }
            else if (caster.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
            {
                var bonus = caster.GetClassModifier(SkillName.Swords);
                chance = chance * bonus * 2;
                resist *= bonus;
            }
            
            if (resist < 25.0)
            {
                AwardPoints(target, SkillName.MagicResist, (int)points / 3);
            }
            
            return Utility.RandomMinMax(0, 100) <= chance;
        }

        public static int TryResistDamage(Mobile caster, Mobile target, SpellCircle circle, int damage)
        {
            if (TryResist(caster, target, circle))
            {
                target.SendLocalizedMessage(502635); // You feel yourself resisting magical energy!
                target.PlaySound(0x1E6);
                target.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);

                damage = GetDamageAfterResist(caster, target, damage);
            }

            return damage;
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

            damage = (int) (damage * (1.0 + (evalInt - resist) / 200.0));
            
            if (damage < 0)
                damage = 0;

            return (int) damage;
        }

        public static int CalcSpellDamage(Mobile caster, Mobile target, Spell spell, bool areaSpell = false)
        {
            const int mageryDivider = 5;
            const int playerDivider = 3;
            const int circleMultiplier = 3;
            const int dices = 5;

            if (!caster.Alive || !target.Alive || target.Hidden)
                return 0;

            var circle = (int) spell.Info.Circle;
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

            return (int)damage;
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

        public static bool ValidIndirectTarget(Mobile caster, Mobile target)
        {
            if (caster == target)
                return true;

            if (target.Hidden && target.AccessLevel > caster.AccessLevel)
                return false;

            var casterGuild = GetGuildFor(caster);
            var toGuild = GetGuildFor(target);

            if (casterGuild != null && toGuild != null && (casterGuild == toGuild || casterGuild.IsAlly(toGuild)))
                return false;

            var p = Party.Get(caster);

            if (p != null && p.Contains(target))
                return false;

            if (target is BaseCreature targetCreature)
            {
                if (targetCreature.Controlled || targetCreature.Summoned)
                {
                    if (targetCreature.ControlMaster == caster || targetCreature.SummonMaster == caster)
                        return false;

                    if (p != null && (p.Contains(targetCreature.ControlMaster) || p.Contains(targetCreature.SummonMaster)))
                        return false;
                }
            }

            if (caster is BaseCreature casterCreature)
            {
                if (casterCreature.Controlled || casterCreature.Summoned)
                {
                    if (casterCreature.ControlMaster == target || casterCreature.SummonMaster == target)
                        return false;

                    p = Party.Get(target);

                    if (p != null && (p.Contains(casterCreature.ControlMaster) || p.Contains(casterCreature.SummonMaster)))
                        return false;
                }
            }

            if (target is BaseCreature {Controlled: false, InitialInnocent: true})
                return true;

            var noto = Notoriety.Compute(caster, target);

            return noto != Notoriety.Innocent || caster.Kills >= 5;
        }

        public static void Summon(BaseCreature creature, Mobile caster, int sound, TimeSpan? duration = null, bool scaleStats = true)
        {
            if (duration == null)
            {
                var secs = caster.Skills[SkillName.Magery].Value * 2.0;
                caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref secs));
                duration = TimeSpan.FromSeconds(secs);
            }
            
            
            var map = caster.Map;

            if (map == null)
                return;

            if (scaleStats)
            {
                var magery = caster.Skills.Magery.Value * caster.GetClassModifier(SkillName.Magery);
                var power = magery / 1.5;
                caster.FireHook(h => h.OnModifyWithMagicEfficiency(caster, ref power));

                // The scaled stats should never exceed base stats of the creature
                power = power switch
                {
                    > 100 => 100,
                    < 1 => 1,
                    _ => power
                };

                creature.RawStr = (int) (creature.RawStr * power / 100);
                creature.Hits = creature.HitsMax;

                creature.RawDex = (int) (creature.RawDex * power / 100);
                creature.Stam = creature.StamMax;

                creature.RawInt = (int) (creature.RawInt * power / 100);
                creature.Mana = creature.ManaMax;
            }

            var p = new Point3D(caster);

            if (FindValidSpawnLocation(map, ref p, true))
            {
                BaseCreature.Summon(creature, caster, p, sound, duration.Value);
                return;
            }


            creature.Delete();
            caster.SendFailureMessage(501942); // That location is blocked.
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
            switch (type)
            {
                case TravelCheckType.RecallTo:
                case TravelCheckType.GateTo:
                    caster.SendLocalizedMessage(1019004); // You are not allowed to travel there.
                    break;
                case TravelCheckType.TeleportTo:
                    caster.SendLocalizedMessage(501035); // You cannot teleport caster here to the destination.
                    break;
                case TravelCheckType.RecallFrom:
                    break;
                case TravelCheckType.GateFrom:
                    break;
                case TravelCheckType.Mark:
                    break;
                case TravelCheckType.TeleportFrom:
                    break;
                default:
                    caster.SendLocalizedMessage(501802); // Thy spell doth not appear to work...
                    break;
            }
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
            int damage,
            Mobile target,
            Mobile caster = null,
            Spell spell = null,
            TimeSpan? delay = null,
            ElementalType? damageType = null,
            DFAlgorithm dfa = DFAlgorithm.Standard
        )
        {
            damageType ??= spell != null && SpellInfos.TryGetValue(spell.GetType(), out var info)
                ? info.DamageType
                : ElementalType.None;

            if (delay.HasValue && delay.Value > TimeSpan.Zero)
                await Timer.Pause(delay.Value);
            
            target.FireHook(h => h.OnSpellDamage(caster, target, spell, damageType.Value, ref damage));
            damage = TryResistDamage(caster, target, spell?.Circle ?? SpellCircle.First, damage);

            WeightOverloading.DFA = dfa;

            if (damage > 0)
                target.Damage(damage, caster);
            else
                target.Heal(damage * -1);

            if (caster != null) // sanity check
                DoLeech(damage, caster, target);

            WeightOverloading.DFA = DFAlgorithm.Standard;

            if (target is BaseCreature c && caster != null)
                c.OnDamagedBySpell(caster);
        }

        private static void DoLeech(int damageGiven, Mobile caster, Mobile target)
        {
        }

        public static void Heal(double amount, Mobile target, Mobile caster, Spell spell, bool message = true)
        {
            target.FireHook(h => h.OnHeal(caster, target, spell, ref amount));
            target.Heal((int) amount, caster, message);
        }
        
        public static bool CheckValidHands(Mobile mobile)
        {
            var one = mobile.FindItemOnLayer(Layer.OneHanded)?.AllowEquippedCast(mobile);
            var two = mobile.FindItemOnLayer(Layer.TwoHanded)?.AllowEquippedCast(mobile);

            return (one == null || one == true) && (two == null || two == true);
        }
    }
}