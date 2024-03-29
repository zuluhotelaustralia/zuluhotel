using System;
using Scripts.Zulu.Utilities;
using Server.Mobiles;

namespace Server.Misc
{
    public enum DFAlgorithm
    {
        Standard,
        PainSpike
    }

    public class WeightOverloading
    {
        public static void Initialize()
        {
            EventSink.Movement += EventSink_Movement;
        }

        public static DFAlgorithm DFA { get; set; }

        public static void FatigueOnDamage(Mobile m, int damage)
        {
            var fatigue = 0.0;
            
            switch (DFA)
            {
                case DFAlgorithm.Standard:
                {
                    fatigue = Utility.RandomMinMax(1, 3) * (Math.Max(100, damage) / 100.0);
                    break;
                }
                case DFAlgorithm.PainSpike:
                {
                    fatigue = damage * (25.0 / m.Hits + (50.0 + m.Stam) / 100 - 1.0) - 5.0;
                    break;
                }
            }

            if (fatigue > 0)
                m.Stam -= (int) fatigue;
        }

        public const int OverloadAllowance = 4; // We can be four stones overweight without getting fatigued

        public static int GetMaxWeight(Mobile m)
        {
            return m.MaxWeight;
        }

        public static void EventSink_Movement(MovementEventArgs e)
        {
            Mobile from = e.Mobile;

            if (!from.Alive || from.AccessLevel > AccessLevel.Player)
                return;

            int maxWeight = GetMaxWeight(from) + OverloadAllowance;
            int overWeight = Mobile.BodyWeight + @from.TotalWeight - maxWeight;

            if (overWeight > 0)
            {
                from.Stam -= GetStamLoss(from, overWeight, (e.Direction & Direction.Running) != 0);

                if (from.Stam == 0)
                {
                    from.SendFailureMessage(
                        500109); // You are too fatigued to move, because you are carrying too much weight!
                    e.Blocked = true;
                    return;
                }
            }

            if (from.Stam == 0)
            {
                from.SendFailureMessage(500110); // You are too fatigued to move.
                e.Blocked = true;
                return;
            }

            if (from is PlayerMobile pm)
            {
                int amt = pm.Mounted ? 48 : 16;

                if (++pm.StepsTaken % amt == 0)
                    --pm.Stam;
            }
        }

        public static int GetStamLoss(Mobile from, int overWeight, bool running)
        {
            int loss = 5 + overWeight / 25;

            if (from.Mounted)
                loss /= 3;

            if (running)
                loss *= 2;

            return loss;
        }

        public static bool IsOverloaded(Mobile m)
        {
            if (!m.Player || !m.Alive || m.AccessLevel > AccessLevel.Player)
                return false;

            return Mobile.BodyWeight + m.TotalWeight > GetMaxWeight(m) + OverloadAllowance;
        }
    }
}