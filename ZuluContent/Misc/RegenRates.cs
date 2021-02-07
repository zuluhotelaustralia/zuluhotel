using System;
using Server.Items;

namespace Server.Misc
{
    public class RegenRates
    {
        [CallPriority(10)]
        public static void Configure()
        {
            Mobile.DefaultHitsRate = TimeSpan.FromSeconds(11.0);
            Mobile.DefaultStamRate = TimeSpan.FromSeconds(7.0);
            Mobile.DefaultManaRate = TimeSpan.FromSeconds(7.0);

            Mobile.ManaRegenRateHandler = Mobile_ManaRegenRate;
        }
        
        private static TimeSpan Mobile_ManaRegenRate(Mobile from)
        {
            // Meditation regen is in the skill handler
            return Mobile.DefaultManaRate; 
        }
    }
}