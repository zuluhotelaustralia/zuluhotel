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

        private static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(10.0);
        public static void Initialize()
        {
            SkillInfo.Table[46].Callback = OnUse;
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

            if ((m as IShilCheckSkill)?.CheckSkill(SkillName.Meditation, -1,
                SkillCheck.Configs[SkillName.Meditation].DefaultPoints) == true)
            {
                m.SendLocalizedMessage(501851); // You enter a meditative trance.
                m.Meditating = true;

                m.PlaySound(0xFA);
                
                var regenBase = (int) (m.Skills[SkillName.Meditation].Value / 25 + m.Int / 35.0);
                var interval = 5.0;
                
                new InternalTimer( m, regenBase, TimeSpan.FromSeconds(interval)).Start();
                return TimeSpan.FromSeconds(10.0);
            }

            
            m.SendAsciiMessage("You cannot focus your concentration.");
            return DefaultDelay;
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

        public static double GetMagicEfficiencyModifier(Mobile from)
        {
            var armour = new[]
            {
                from.ShieldArmor as BaseArmor,
                from.NeckArmor as BaseArmor,
                from.HandArmor as BaseArmor,
                from.HeadArmor as BaseArmor,
                from.ArmsArmor as BaseArmor,
                from.LegsArmor as BaseArmor,
                from.ChestArmor as BaseArmor,
            };

            var magicPenalty = armour.Sum(a => a?.Enchantments.Get((MagicEfficiencyPenalty e) => e.Value) ?? 0);

            // TODO: remove this when items have MagicEfficiencyPenalty properties
            if (magicPenalty == 0) 
                magicPenalty = armour.Sum(GetArmorMeditationValue) / 4;

            return 100 - magicPenalty * 2;
        }

        private static double GetArmorMeditationValue(BaseArmor ar)
        {
            if (ar == null)
                return 0.0;

            switch (ar.MeditationAllowance)
            {
                default:
                case ArmorMeditationAllowance.None: return ar.BaseArmorRatingScaled;
                case ArmorMeditationAllowance.Half: return ar.BaseArmorRatingScaled / 2.0;
                case ArmorMeditationAllowance.All: return 0.0;
            }
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
                if (ShouldBreakConcentration())
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