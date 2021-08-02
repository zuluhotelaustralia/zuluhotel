using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;

namespace Server.SkillHandlers
{
    public class Snooping
    {
        public static TimeSpan Delay => ZhConfig.Skills.Entries[SkillName.Snooping].Delay;

        public static void Configure()
        {
            Container.SnoopHandler = Container_Snoop;
        }

        private static bool CheckSnoopAllowed(Mobile from, Mobile victim)
        {
            if (victim.Player)
                return from.CanBeHarmful(victim, false, true); // normal restrictions

            return true;
        }

        private static int CheckSnoopVictimDifficulty(Mobile victim)
        {
            if (victim is BaseVendor)
                return 40;

            if (victim is Warrior)
                return 80;

            if (victim is BaseGuard)
                return 100;

            return 0;
        }

        public static async void Container_Snoop(Container cont, Mobile from)
        {
            if (from.AccessLevel == AccessLevel.Player)
            {
                if (from.NextSkillTime > Core.TickCount)
                {
                    from.SendLocalizedMessage(1045157); // You must wait to perform another action.
                    return;
                }

                if ((from.FindItemOnLayer(Layer.OneHanded) ?? from.FindItemOnLayer(Layer.TwoHanded)) != null)
                {
                    from.SendFailureMessage("You'll need your hands empty for that!");
                    return;
                }

                if (!from.InRange(cont.GetWorldLocation(), 1))
                {
                    from.SendFailureMessage(500446); // That is too far away.
                    return;
                }
            }
            
            var victim = cont.RootParent as Mobile;

            if (victim is {Alive: false})
                return;

            if (victim is {AccessLevel: > AccessLevel.Player} && from.AccessLevel == AccessLevel.Player)
            {
                from.SendFailureMessage(500209); // You can not peek into the container.
                return;
            }

            if (victim != null && from.AccessLevel == AccessLevel.Player && !CheckSnoopAllowed(from, victim))
            {
                from.SendFailureMessage(1001018); // You cannot perform negative acts on your target.
                return;
            }

            var victimDifficulty = CheckSnoopVictimDifficulty(victim);
            var snoopingSkill = from.Skills[SkillName.Snooping].Value;

            if (victimDifficulty > snoopingSkill + 20.0)
            {
                from.SendFailureMessage("You'd be caught red-handed!!!");
                from.SendFailureMessage("You have to improve thy snooping skill...");
                return;
            }

            var difficulty = victim.Dex - from.Dex +
                             Math.Max(victim.Skills[SkillName.Snooping].Value, victim.Int / 2);
            difficulty = Math.Max(difficulty, 0.0);

            if (from.HandArmor is not ThiefGloves)
                difficulty += 20.0;
            
            // TODO: Should classed thieves get a bonus to the difficulty check here?

            if (from.AccessLevel == AccessLevel.Player && !from.ShilCheckSkill(SkillName.Snooping,
                (int) difficulty, (int) (difficulty * 20.0)))
            {
                from.RevealingAction();

                var map = from.Map;

                if (map != null)
                {
                    var message = $"You notice {from.Name} attempting to peek into {victim.Name}'s belongings.";

                    var range = (int) (15 - from.Skills[SkillName.Stealth].Value / 10);
                    range = Math.Max(range, 3);

                    var eable = map.GetClientsInRange(from.Location, range);

                    foreach (var ns in eable)
                        if (ns.Mobile != from)
                            ns.Mobile.SendMessage(message);

                    eable.Free();
                }

                var lossKarma = from.Karma > -625 ? -Utility.RandomMinMax(1, 300) : 0;
                Titles.AwardKarma(from, lossKarma, true);

                from.SendFailureMessage("You are noticed snooping in the backpack!");
                from.SendFailureMessage(500210); // You failed to peek into the container.
                from.CriminalAction(false);

                return;
            }

            if (from.AccessLevel == AccessLevel.Player)
            {
                from.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true,
                    "You attempt to open the backpack...", from.NetState);

                var snoopDelay = (int) (10 - snoopingSkill / 10) * 1000;

                from.NextSkillTime = Core.TickCount + snoopDelay;
                await Timer.Pause(snoopDelay);

                if (!from.InRange(cont.GetWorldLocation(), 1))
                {
                    from.SendFailureMessage($"You need to stay close to {victim.Name}.");
                    return;
                }

                if (cont is TrapableContainer container && container.ExecuteTrap(from))
                    return;

                from.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "...backpack opened!",
                    from.NetState);
                cont.DisplayTo(from);
            }
            else
            {
                cont.DisplayTo(from);
            }

            if (from.AccessLevel == AccessLevel.Player)
                from.NextSkillTime = Core.TickCount + (int) Delay.TotalMilliseconds;
        }
    }
}