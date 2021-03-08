using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
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
        
        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            if (m.Mana >= m.ManaMax)
            {
                m.SendLocalizedMessage(501846); // You are at peace.
                return DefaultDelay;
            }

            if (m.Poisoned)
            {
                m.SendAsciiMessage("You can't meditate while poisoned.");
                return DefaultDelay;
            }

            if (m.Warmode)
            {
                m.SendAsciiMessage("You can't meditate in war mode.");
                return DefaultDelay;
            }

            if (!CheckValidHands(m))
            {
                m.SendLocalizedMessage(502626); // Your hands must be free to cast spells or meditate.
                return DefaultDelay;
            }
            
            if (GetMagicEfficiencyModifier(m) <= 0)
            {
                m.SendAsciiMessage("Regenerative forces cannot penetrate your armor.");
                return DefaultDelay;
            }
            
            if (!m.ShilCheckSkill(SkillName.Meditation))
            {
                m.SendAsciiMessage("You cannot focus your concentration.");
                return DefaultDelay;
            }
            
            m.SendLocalizedMessage(501851); // You enter a meditative trance.
            m.Meditating = true;

            m.PlaySound(0xF9);
                
            var regenBase = (int) (m.Skills[SkillName.Meditation].Value / 25 + m.Int / 35.0);
            var interval = 5.0;

            m.FireHook(h => h.OnMeditation(m, ref regenBase, ref interval));
                
            new InternalTimer( m, regenBase, TimeSpan.FromSeconds(interval)).Start();
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


        public static bool CheckValidHands(Mobile m)
        {
            var items = new[] {m.FindItemOnLayer(Layer.OneHanded), m.FindItemOnLayer(Layer.TwoHanded)};
            
            return items.All(item =>
            {
                switch (item)
                {
                    case null:
                    case Spellbook:
                    case Runebook:
                    case BaseStaff:
                        return true;
                    case IEnchanted enchanted:
                    {
                        var magical = enchanted.Enchantments.Get((MagicalWeapon e) => e.Value);
                        return magical == MagicalWeaponType.Mystical || magical == MagicalWeaponType.Stygian;
                    }
                    default:
                        return false;
                }
            });
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

                if (!CheckValidHands(m_Mobile))
                    return true;

                if (m_Mobile.Hits < m_StartHits)
                    return true;

                return false;
            }

            protected override void OnTick()
            {
                if (ShouldBreakConcentration() || !m_Mobile.Meditating)
                {
                    // m_Mobile.SendAsciiMessage("You lost your concentration.");
                    m_Mobile.DisruptiveAction();
                    m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds;
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
                        m_Mobile.SendAsciiMessage("Regenerative forces cannot penetrate your armor.");
                        m_Mobile.DisruptiveAction();
                        m_Mobile.NextSkillTime = Core.TickCount + (int)DefaultDelay.TotalMilliseconds;;
                        Stop();
                        return;
                    }
                }

                if (m_Mobile.Mana == m_Mobile.ManaMax)
                {
                    // m_Mobile.SendAsciiMessage("You stop meditating.");
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