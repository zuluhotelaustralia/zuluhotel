using System;
using System.Collections.Generic;
using Server.Engines.PartySystem;
using Server.Guilds;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.SkillHandlers;
using Server.Spells.Seventh;

namespace Server.Misc
{
    public class NotorietyHandlers
    {
        public static void Initialize()
        {
            Notoriety.Hues[Notoriety.Innocent] = 0x59;
            Notoriety.Hues[Notoriety.Ally] = 0x3F;
            Notoriety.Hues[Notoriety.CanBeAttacked] = 0x3B2;
            Notoriety.Hues[Notoriety.Criminal] = 0x3B2;
            Notoriety.Hues[Notoriety.Enemy] = 0x90;
            Notoriety.Hues[Notoriety.Murderer] = 0x22;
            Notoriety.Hues[Notoriety.Invulnerable] = 0x35;

            Notoriety.Handler = MobileNotoriety;

            Mobile.AllowBeneficialHandler = Mobile_AllowBeneficial;
            Mobile.AllowHarmfulHandler = Mobile_AllowHarmful;
        }

        private enum GuildStatus
        {
            None,
            Peaceful,
            Waring
        }

        private static GuildStatus GetGuildStatus(Mobile m)
        {
            if (m.Guild == null)
                return GuildStatus.None;
            if (((Guild)m.Guild).Enemies.Count == 0 && m.Guild.Type == GuildType.Regular)
                return GuildStatus.Peaceful;

            return GuildStatus.Waring;
        }

        private static bool CheckBeneficialStatus(GuildStatus from, GuildStatus target)
        {
            if (from == GuildStatus.Waring || target == GuildStatus.Waring)
                return false;

            return true;
        }

        /*private static bool CheckHarmfulStatus( GuildStatus from, GuildStatus target )
        {
          if ( from == GuildStatus.Waring && target == GuildStatus.Waring )
            return true;
    
          return false;
        }*/

        public static bool Mobile_AllowBeneficial(Mobile from, Mobile target)
        {
            if (from == null || target == null || from.AccessLevel > AccessLevel.Player ||
                target.AccessLevel > AccessLevel.Player)
                return true;

            var map = from.Map;

            if (map != null && (map.Rules & MapRules.BeneficialRestrictions) == 0)
                return true; // In felucca, anything goes

            if (!from.Player)
                return true; // NPCs have no restrictions

            if (target is BaseCreature { Controlled: false})
                return false; // Players cannot heal uncontrolled mobiles

            if (@from is PlayerMobile { Young: true } && (!(target is PlayerMobile) || !((PlayerMobile)target).Young))
                return false; // Young players cannot perform beneficial actions towards older players

            var fromGuild = from.Guild as Guild;
            var targetGuild = target.Guild as Guild;

            if (fromGuild != null && targetGuild != null && (targetGuild == fromGuild || fromGuild.IsAlly(targetGuild)))
                return true; // Guild members can be beneficial

            return CheckBeneficialStatus(GetGuildStatus(from), GetGuildStatus(target));
        }

        public static bool Mobile_AllowHarmful(Mobile from, Mobile target)
        {
            if (from == null || target == null || from.AccessLevel > AccessLevel.Player ||
                target.AccessLevel > AccessLevel.Player)
                return true;

            var map = from.Map;

            if (map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0)
                return true; // In felucca, anything goes

            var bc = from as BaseCreature;

            if (!from.Player &&
                !(bc != null && bc.GetMaster() != null && bc.GetMaster().AccessLevel == AccessLevel.Player))
            {
                if (!CheckAggressor(from.Aggressors, target) && !CheckAggressed(from.Aggressed, target) &&
                    target is PlayerMobile && ((PlayerMobile)target).CheckYoungProtection(from))
                    return false;

                return true; // Uncontrolled NPCs are only restricted by the young system
            }

            var fromGuild = GetGuildFor(from.Guild as Guild, from);
            var targetGuild = GetGuildFor(target.Guild as Guild, target);

            if (fromGuild != null && targetGuild != null &&
                (fromGuild == targetGuild || fromGuild.IsAlly(targetGuild) || fromGuild.IsEnemy(targetGuild)))
                return true; // Guild allies or enemies can be harmful

            if (target is BaseCreature && (((BaseCreature)target).Controlled ||
                                           ((BaseCreature)target).Summoned &&
                                           from != ((BaseCreature)target).SummonMaster))
                return false; // Cannot harm other controlled mobiles

            if (target.Player)
                return false; // Cannot harm other players

            if (!(target is BaseCreature && ((BaseCreature)target).InitialInnocent))
                if (Notoriety.Compute(from, target) == Notoriety.Innocent)
                    return false; // Cannot harm innocent mobiles

            return true;
        }

        public static Guild GetGuildFor(Guild def, Mobile m)
        {
            var g = def;

            var c = m as BaseCreature;

            if (c != null && c.Controlled && c.ControlMaster != null)
            {
                c.DisplayGuildTitle = false;

                if (c.Map != Map.Internal && (c.ControlOrder == OrderType.Attack || c.ControlOrder == OrderType.Guard))
                    g = (Guild)(c.Guild = c.ControlMaster.Guild);
                else if (c.Map == Map.Internal || c.ControlMaster.Guild == null)
                    g = (Guild)(c.Guild = null);
            }

            return g;
        }

        public static int CorpseNotoriety(Mobile source, Corpse target)
        {
            if (target.AccessLevel > AccessLevel.Player)
                return Notoriety.CanBeAttacked;

            Body body = target.Amount;

            var cretOwner = target.Owner as BaseCreature;

            if (cretOwner != null)
            {
                var sourceGuild = GetGuildFor(source.Guild as Guild, source);
                var targetGuild = GetGuildFor(target.Guild, target.Owner);

                if (sourceGuild != null && targetGuild != null)
                {
                    if (sourceGuild == targetGuild || sourceGuild.IsAlly(targetGuild))
                        return Notoriety.Ally;
                    if (sourceGuild.IsEnemy(targetGuild))
                        return Notoriety.Enemy;
                }

                if (CheckHouseFlag(source, target.Owner, target.Location, target.Map))
                    return Notoriety.CanBeAttacked;

                var actual = Notoriety.CanBeAttacked;

                if (target.Kills >= 5 || body.IsMonster && IsSummoned(target.Owner as BaseCreature) ||
                    target.Owner is BaseCreature && ((BaseCreature)target.Owner).AlwaysMurderer)
                    actual = Notoriety.Murderer;

                if (DateTime.Now >= target.TimeOfDeath + Corpse.MonsterLootRightSacrifice)
                    return actual;

                var sourceParty = Party.Get(source);

                var list = target.Aggressors;

                for (var i = 0; i < list.Count; ++i)
                    if (list[i] == source || sourceParty != null && Party.Get(list[i]) == sourceParty)
                        return actual;

                return Notoriety.Innocent;
            }
            else
            {
                if (target.Kills >= 5 || body.IsMonster && IsSummoned(target.Owner as BaseCreature) ||
                    target.Owner is BaseCreature && ((BaseCreature)target.Owner).AlwaysMurderer)
                    return Notoriety.Murderer;

                if (target.Criminal && target.Map != null && (target.Map.Rules & MapRules.HarmfulRestrictions) == 0)
                    return Notoriety.Criminal;

                var sourceGuild = GetGuildFor(source.Guild as Guild, source);
                var targetGuild = GetGuildFor(target.Guild, target.Owner);

                if (sourceGuild != null && targetGuild != null)
                {
                    if (sourceGuild == targetGuild || sourceGuild.IsAlly(targetGuild))
                        return Notoriety.Ally;
                    if (sourceGuild.IsEnemy(targetGuild))
                        return Notoriety.Enemy;
                }

                if (target.Owner != null && target.Owner is BaseCreature &&
                    ((BaseCreature)target.Owner).AlwaysAttackable)
                    return Notoriety.CanBeAttacked;

                if (CheckHouseFlag(source, target.Owner, target.Location, target.Map))
                    return Notoriety.CanBeAttacked;

                if (!(target.Owner is PlayerMobile) && !IsPet(target.Owner as BaseCreature))
                    return Notoriety.CanBeAttacked;

                var list = target.Aggressors;

                for (var i = 0; i < list.Count; ++i)
                    if (list[i] == source)
                        return Notoriety.CanBeAttacked;

                return Notoriety.Innocent;
            }
        }

        public static int MobileNotoriety(Mobile source, Mobile target)
        {
            if (target.AccessLevel > AccessLevel.Player)
                return Notoriety.CanBeAttacked;

            if (source.Player && !target.Player && source is PlayerMobile && target is BaseCreature bc)
            {
                var master = bc.GetMaster();

                if (master is { AccessLevel: > AccessLevel.Player })
                    return Notoriety.CanBeAttacked;
            }

            if (target.Kills >= 5 || target.Body.IsMonster && IsSummoned(target as BaseCreature) ||
                target is BaseCreature { AlwaysMurderer: true })
                return Notoriety.Murderer;

            if (target.Criminal)
                return Notoriety.Criminal;

            var sourceGuild = GetGuildFor(source.Guild as Guild, source);
            var targetGuild = GetGuildFor(target.Guild as Guild, target);

            if (sourceGuild != null && targetGuild != null)
            {
                if (sourceGuild == targetGuild || sourceGuild.IsAlly(targetGuild))
                    return Notoriety.Ally;
                if (sourceGuild.IsEnemy(targetGuild))
                    return Notoriety.Enemy;
            }

            if (Stealing.ClassicMode && target is PlayerMobile mobile &&
                mobile.PermaFlags.Contains(source))
                return Notoriety.CanBeAttacked;

            if (target is BaseCreature {AlwaysAttackable: true})
                return Notoriety.CanBeAttacked;

            if (CheckHouseFlag(source, target, target.Location, target.Map))
                return Notoriety.CanBeAttacked;

            if (target is not BaseCreature { InitialInnocent: true}
               ) //If Target is NOT A baseCreature, OR it's a BC and the BC is initial innocent...
                if (!target.Body.IsHuman && !target.Body.IsGhost && !IsPet(target as BaseCreature) &&
                    !(target is PlayerMobile) || !target.CanBeginAction(typeof(PolymorphSpell)))
                    return Notoriety.CanBeAttacked;

            if (CheckAggressor(source.Aggressors, target))
                return Notoriety.CanBeAttacked;

            if (CheckAggressed(source.Aggressed, target))
                return Notoriety.CanBeAttacked;

            if (target is BaseCreature targetCreature)
            {
                if (targetCreature.Controlled && targetCreature.ControlOrder == OrderType.Guard && targetCreature.ControlTarget == source)
                    return Notoriety.CanBeAttacked;
            }

            if (source is BaseCreature sourceCreature)
            {
                var master = sourceCreature.GetMaster();

                if (master != null)
                    if (CheckAggressor(master.Aggressors, target) ||
                        MobileNotoriety(master, target) == Notoriety.CanBeAttacked ||
                        target is BaseCreature)
                        return Notoriety.CanBeAttacked;
            }

            return Notoriety.Innocent;
        }

        public static bool CheckHouseFlag(Mobile from, Mobile m, Point3D p, Map map)
        {
            var house = BaseHouse.FindHouseAt(p, map);

            if (house == null || house.Public || !house.IsFriend(from))
                return false;

            if (m != null && house.IsFriend(m))
                return false;

            if (m is BaseCreature { Deleted: false, Controlled: true, ControlMaster: { } } c)
                return !house.IsFriend(c.ControlMaster);

            return true;
        }

        public static bool IsPet(BaseCreature c)
        {
            return c != null && c.Controlled;
        }

        public static bool IsSummoned(BaseCreature c)
        {
            return c != null && /*c.Controlled &&*/ c.Summoned;
        }

        public static bool CheckAggressor(List<AggressorInfo> list, Mobile target)
        {
            for (var i = 0; i < list.Count; ++i)
                if (list[i].Attacker == target)
                    return true;

            return false;
        }

        public static bool CheckAggressed(List<AggressorInfo> list, Mobile target)
        {
            for (var i = 0; i < list.Count; ++i)
            {
                var info = list[i];

                if (!info.CriminalAggression && info.Defender == target)
                    return true;
            }

            return false;
        }
    }
}