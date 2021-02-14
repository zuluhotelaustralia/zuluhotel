// Ideas
// When you run on animals the panic
// When if ( distance < 8 && Utility.RandomDouble() * Math.Sqrt( (8 - distance) / 6 ) >= incoming.Skills[SkillName.AnimalTaming].Value )
// More your close, the more it can panic
/*
 * AnimalHunterAI, AnimalHidingAI, AnimalDomesticAI...
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Mobiles
{
    public class FamiliarAI : BaseAI
    {
        private static readonly Dictionary<string, Action<BaseCreature, PlayerMobile>> FamiliarActions = new()
        {
            ["heal"] = (familiar, _) =>
            {
                new GreaterHealSpell(familiar, null).Cast();
            },
            ["cure"] = (familiar, _) =>
            {
                new CureSpell(familiar, null).Cast();
            },
            ["protect"] = (familiar, _) =>
            {
                new ArchProtectionSpell(familiar, null).Cast();
            },
            ["bless"] = (familiar, _) =>
            {
                new BlessSpell(familiar, null).Cast();
            },
            ["loot"] = (familiar, _) =>
            {
                //DoLoot()
            },
            ["fetch"] = (familiar, _) =>
            {
                //DoFetch()
            },
            ["speak"] = (familiar, _) =>
            {
                familiar.PlaySound(0x253);
            }
        };

        public FamiliarAI(BaseCreature m) : base(m)
        {
        }
        
        public override bool Obey()
        {
            if (m_Mobile.Deleted)
                return false;

            if (m_Mobile.Target != null)
            {
                if (m_Mobile.InRange(m_Mobile.ControlMaster, m_Mobile.Target.Range))
                {
                    switch (m_Mobile.Target)
                    {
                        case CureSpell.InternalTarget:
                        case GreaterHealSpell.InternalTarget:
                        case ArchProtectionSpell.InternalTarget:
                        case BlessSpell.InternalTarget:
                            m_Mobile.Target.Invoke(m_Mobile, m_Mobile.ControlMaster);
                            break;
                        default:
                            m_Mobile.Target.Cancel(m_Mobile, TargetCancelType.Canceled);
                            break;
                    }
                }
                else
                {
                    m_Mobile.Target.Cancel(m_Mobile, TargetCancelType.Canceled);
                }
            }

            return base.Obey();
        }

        public override bool DoOrderFollow(bool alwaysRun = false)
        {
            if (Math.Abs(m_Mobile.CurrentSpeed - m_Mobile.ActiveSpeed) > 0.001)
                m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;

            return base.DoOrderFollow(true);
        }

        public override bool DoActionCombat()
        {
            if (m_Mobile.CanFlee)
            {
                double hitPercent = (double) m_Mobile.Hits / m_Mobile.HitsMax;

                if (hitPercent < 0.1)
                {
                    m_Mobile.DebugSay("I am low on health!");
                    Action = ActionType.Flee;
                }
            }

            return true;
        }
        
        public override void OnSpeech(SpeechEventArgs e)
        {
            if (e.Mobile == m_Mobile.ControlMaster && m_Mobile.ControlMaster is PlayerMobile player)
            {
                var entry = FamiliarActions.FirstOrDefault(kv => e.Speech.InsensitiveContains(kv.Key));
                
                if (entry.Key != null) 
                    entry.Value(m_Mobile, player);
            }


            base.OnSpeech(e);
        }

        public override void BeginPickTarget(Mobile from, OrderType order)
        {
            switch (order)
            {
                case OrderType.Attack: return;
                case OrderType.Friend: return;
                case OrderType.Unfriend: return;
                case OrderType.Guard: return;
                case OrderType.Release: return;
                case OrderType.Transfer: return;
            }

            base.BeginPickTarget(from, order);
        }

        public override bool DoOrderGuard() => false;
        public override bool DoOrderRelease(bool frenzySummoned = false) => false;
        public override bool DoOrderTransfer() => false;
        public override bool DoOrderAttack() => false;
        public override bool DoOrderUnfriend() => false;
        public override bool DoOrderFriend() => false;

        public override bool DoActionBackoff()
        {
            double hitPercent = (double) m_Mobile.Hits / m_Mobile.HitsMax;

            if (!m_Mobile.Summoned && !m_Mobile.Controlled && hitPercent < 0.1 && m_Mobile.CanFlee) 
            {
                // Less than 10% health
                Action = ActionType.Flee;
            }
            else
            {
                if (AcquireFocusMob(m_Mobile.RangePerception * 2, FightMode.Closest, true, false, true))
                {
                    if (WalkMobileRange(m_Mobile.FocusMob, 1, false, m_Mobile.RangePerception,
                        m_Mobile.RangePerception * 2))
                    {
                        m_Mobile.DebugSay("Well, here I am safe, I will hide now");
                        m_Mobile.ControlOrder = OrderType.Stay;
                        m_Mobile.Hidden = true;
                    }
                }
                else
                {
                    m_Mobile.DebugSay("I have lost my focus, lets relax");
                    Action = ActionType.Wander;
                }
            }

            return true;
        }

        public override bool DoActionFlee()
        {
            AcquireFocusMob(m_Mobile.RangePerception * 2, m_Mobile.FightMode, true, false, true);

            m_Mobile.FocusMob ??= m_Mobile.Combatant;

            return base.DoActionFlee();
        }
    }
}