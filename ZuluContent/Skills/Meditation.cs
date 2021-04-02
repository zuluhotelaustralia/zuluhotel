using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace Server.SkillHandlers
{
    class Meditation
    {
        private static readonly TimeSpan DefaultDelay = ZhConfig.Skills.Entries[SkillName.Meditation].DelayTimespan;
        
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Meditation].Callback = OnUse;
        }
        
        public static TimeSpan OnUse(Mobile mobile)
        {
            mobile.RevealingAction();

            if (mobile.Mana >= mobile.ManaMax)
            {
                mobile.SendSuccessMessage(501846); // You are at peace.
                return DefaultDelay;
            }

            if (mobile.Poisoned)
            {
                mobile.SendFailureMessage("You can't meditate while poisoned.");
                return DefaultDelay;
            }

            if (mobile.Warmode)
            {
                mobile.SendFailureMessage("You can't meditate in war mode.");
                return DefaultDelay;
            }

            if (!SpellHelper.CheckValidHands(mobile))
            {
                mobile.SendFailureMessage(502626); // Your hands must be free to cast spells or meditate.
                return DefaultDelay;
            }
            
            if (GetMagicEfficiencyModifier(mobile) <= 0)
            {
                mobile.SendFailureMessage("Regenerative forces cannot penetrate your armor.");
                return DefaultDelay;
            }
            
            if (!mobile.ShilCheckSkill(SkillName.Meditation))
            {
                mobile.SendFailureMessage("You cannot focus your concentration.");
                return DefaultDelay;
            }
            
            mobile.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "*Meditating*");
            mobile.SendSuccessMessage(501851); // You enter a meditative trance.
            mobile.Meditating = true;

            mobile.PlaySound(0xF9);
                
            var regenBase = (int) (mobile.Skills[SkillName.Meditation].Value / 25 + mobile.Int / 35.0);
            var interval = 5.0;

            mobile.FireHook(h => h.OnMeditation(mobile, ref regenBase, ref interval));
                
            new InternalTimer( mobile, regenBase, TimeSpan.FromSeconds(interval)).Start();
            return TimeSpan.FromSeconds(10.0);
        }
        
        public static double GetMagicEfficiencyModifier(Mobile from)
        {
            if (from is IZuluClassed classed)
            {
                var penalty = classed.ZuluClass.GetMagicEfficiencyPenalty();
                return 100 - penalty * 2;
            }

            return 0;
        }
        
        private class InternalTimer : Timer
        {
            private readonly Mobile m_Mobile;
            private readonly int m_RegenBase;
            private readonly int m_StartHits;
            private readonly Point3D m_StartLocation;

            public InternalTimer(Mobile mobile, int regenBase, TimeSpan interval) : base(interval, interval)
            {
                m_Mobile = mobile;
                m_RegenBase = regenBase;
                m_StartLocation = mobile.Location;
                m_StartHits = mobile.Hits;
                Priority = TimerPriority.TwoFiftyMS;
            }

            private bool ShouldBreakConcentration()
            {
                if (m_Mobile.Location != m_StartLocation)
                    return true;

                if (m_Mobile.Mana == m_Mobile.ManaMax)
                    return true;

                if (m_Mobile.Poisoned)
                    return true;

                if (m_Mobile.Warmode)
                    return true;

                if (!SpellHelper.CheckValidHands(m_Mobile))
                    return true;

                if (m_Mobile.Hits < m_StartHits)
                    return true;

                return false;
            }

            protected override void OnTick()
            {
                if (ShouldBreakConcentration())
                {
                    m_Mobile.DisruptiveAction();
                    m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds;
                }

                if (!m_Mobile.Meditating)
                {
                    Stop();
                    return;
                }
                
                var modifier = GetMagicEfficiencyModifier(m_Mobile);

                if (modifier > 0)
                {
                    var restored = (int)(m_RegenBase * modifier / 100);
                    if (restored >= 0)
                    {
                        m_Mobile.Mana += restored;
                    }
                    else
                    {
                        m_Mobile.SendFailureMessage("Regenerative forces cannot penetrate your armor.");
                        m_Mobile.DisruptiveAction();
                        m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds;;
                        Stop();
                        return;
                    }
                }

                if (m_Mobile.Mana == m_Mobile.ManaMax)
                {
                    m_Mobile.DisruptiveAction();
                    m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds;;
                    Stop();
                    return;
                }

                // Delay skill use long enough for the next meditation tick
                m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds * 2;
            }
        }
    }
}