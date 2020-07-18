using System;
using System.Collections.Generic;
using Server.Regions;

namespace Server.Mobiles
{
    public class LoyaltyTimer : Timer
    {
        private static TimeSpan InternalDelay = TimeSpan.FromMinutes(5.0);

        public static void Initialize()
        {
            new LoyaltyTimer().Start();
        }

        public LoyaltyTimer() : base(InternalDelay, InternalDelay)
        {
            m_NextHourlyCheck = DateTime.Now + TimeSpan.FromHours(1.0);
            Priority = TimerPriority.FiveSeconds;
        }

        private DateTime m_NextHourlyCheck;

        protected override void OnTick()
        {
            if (DateTime.Now >= m_NextHourlyCheck)
                m_NextHourlyCheck = DateTime.Now + TimeSpan.FromHours(1.0);
            else
                return;

            List<BaseCreature> toRelease = new List<BaseCreature>();

            // added array for wild creatures in house regions to be removed
            List<BaseCreature> toRemove = new List<BaseCreature>();

            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m is BaseCreature)
                {
                    BaseCreature c = (BaseCreature) m;

                    if (c.Controlled && c.Commandable)
                    {
                        if (c.Map != Map.Internal)
                        {
                            c.Loyalty -= BaseCreature.MaxLoyalty / 10;

                            if (c.Loyalty < BaseCreature.MaxLoyalty / 10)
                            {
                                c.Say(1043270, c.Name); // * ~1_NAME~ looks around desperately *
                                c.PlaySound(c.GetIdleSound());
                            }

                            if (c.Loyalty <= 0)
                                toRelease.Add(c);
                        }
                    }

                    // added lines to check if a wild creature in a house region has to be removed or not
                    if (!c.Controlled && !c.IsStabled &&
                        (c.Region.IsPartOf(typeof(HouseRegion)) && c.CanBeDamaged() ||
                         c.RemoveIfUntamed && c.Spawner == null))
                    {
                        c.RemoveStep++;

                        if (c.RemoveStep >= 20)
                            toRemove.Add(c);
                    }
                    else
                    {
                        c.RemoveStep = 0;
                    }
                }
            }

            foreach (BaseCreature c in toRelease)
            {
                c.Say(1043255, c.Name); // ~1_NAME~ appears to have decided that is better off without a master!
                c.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy
                c.ControlTarget = null;
                //c.ControlOrder = OrderType.Release;
                c.AIObject
                    .DoOrderRelease(); // this will prevent no release of creatures left alone with AI disabled (and consequent bug of Followers)
                c.DropBackpack();
            }

            // added code to handle removing of wild creatures in house regions
            foreach (BaseCreature c in toRemove)
            {
                c.Delete();
            }
        }
    }
}