using System;

namespace Server
{
    public class PoisonImpl : Poison
    {
        // ReSharper disable once UnusedMember.Global
        [CallPriority(ushort.MinValue)]
        public static void Configure()
        {
            Register(new PoisonImpl("Lesser", 0, 4, 26, 2.500, 3.5, 3.0, 10, 2));
            Register(new PoisonImpl("Regular", 1, 5, 26, 3.125, 3.5, 3.0, 10, 2));
            Register(new PoisonImpl("Greater", 2, 6, 26, 6.250, 3.5, 3.0, 10, 2));
            Register(new PoisonImpl("Deadly", 3, 7, 26, 12.500, 3.5, 4.0, 10, 2));
            Register(new PoisonImpl("Lethal", 4, 9, 26, 25.000, 3.5, 5.0, 10, 2));
        }

        public static Poison IncreaseLevel(Poison oldPoison)
        {
            Poison newPoison = oldPoison == null ? null : GetPoison(oldPoison.Level + 1);

            return newPoison ?? oldPoison;
        }

        // Info

        // Damage
        private readonly int m_Minimum;
        private readonly int m_Maximum;
        private readonly double m_Scalar;

        // Timers
        private readonly TimeSpan m_Delay;
        private readonly TimeSpan m_Interval;
        private readonly int m_Count;
        private readonly int m_MessageInterval;

        public PoisonImpl(string name, int level, int min, int max, double percent, double delay, double interval,
            int count, int messageInterval)
        {
            Name = name;
            Level = level;
            m_Minimum = min;
            m_Maximum = max;
            m_Scalar = percent * 0.01;
            m_Delay = TimeSpan.FromSeconds(delay);
            m_Interval = TimeSpan.FromSeconds(interval);
            m_Count = count;
            m_MessageInterval = messageInterval;
        }

        public override string Name { get; }

        public override int Level { get; }

        public class PoisonTimer : Timer
        {
            private readonly PoisonImpl m_Poison;
            private readonly Mobile m_Mobile;
            private int m_LastDamage;
            private int m_Index;

            public Mobile From { get; set; }

            public PoisonTimer(Mobile m, PoisonImpl p) : base(p.m_Delay, p.m_Interval)
            {
                From = m;
                m_Mobile = m;
                m_Poison = p;
            }

            protected override void OnTick()
            {
                if (m_Index++ == m_Poison.m_Count)
                {
                    m_Mobile.SendLocalizedMessage(502136); // The poison seems to have worn off.
                    m_Mobile.Poison = null;

                    Stop();
                    return;
                }

                int damage;

                if (m_LastDamage != 0 && Utility.RandomBool())
                {
                    damage = m_LastDamage;
                }
                else
                {
                    damage = 1 + (int) (m_Mobile.Hits * m_Poison.m_Scalar);

                    if (damage < m_Poison.m_Minimum)
                        damage = m_Poison.m_Minimum;
                    else if (damage > m_Poison.m_Maximum)
                        damage = m_Poison.m_Maximum;

                    m_LastDamage = damage;
                }

                if (From != null)
                    From.DoHarmful(m_Mobile, true);

                m_Mobile.Damage(damage, From);

                // OSI: randomly revealed between first and third damage tick, guessing 60% chance
                if (0.60 <= Utility.RandomDouble()) 
                    m_Mobile.RevealingAction();

                if (m_Index % m_Poison.m_MessageInterval == 0)
                    m_Mobile.OnPoisoned(From, m_Poison, m_Poison);
            }
        }

        public override Timer ConstructTimer(Mobile m)
        {
            return new PoisonTimer(m, this);
        }
    }
}