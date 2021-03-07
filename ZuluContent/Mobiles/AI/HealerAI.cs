using Server.Spells;
using Server.Targeting;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Fourth;

namespace Server.Mobiles
{
    public class HealerAi : BaseAI
    {
        private static readonly NeedDelegate Cure = NeedCure;
        private static readonly NeedDelegate GHeal = NeedGHeal;
        private static readonly NeedDelegate LHeal = NeedLHeal;
        private static readonly NeedDelegate[] ACure = {Cure};
        private static readonly NeedDelegate[] AgHeal = {GHeal};
        private static readonly NeedDelegate[] AlHeal = {LHeal};
        private static readonly NeedDelegate[] All = {Cure, GHeal, LHeal};

        public HealerAi(BaseCreature m) : base(m)
        {
        }

        public override bool Think()
        {
            if (m_Mobile.Deleted)
                return false;

            if (m_Mobile.Target is AsyncSpellTarget target)
            {
                switch (target.Spell)
                {
                    case CureSpell:
                        ProcessTarget( target, ACure );
                        break;
                    case GreaterHealSpell:
                        ProcessTarget( target, AgHeal );
                        break;
                    case HealSpell:
                        ProcessTarget( target, AlHeal );
                        break;
                    default:
                        m_Mobile.Target.Cancel(m_Mobile, TargetCancelType.Canceled);
                        break;
                }
            }
            else
            {
                Mobile toHelp = Find(All);

                if (toHelp != null)
                {
                    if (NeedCure(toHelp))
                    {
                        if (m_Mobile.Debug)
                            m_Mobile.DebugSay("{0} needs a cure", toHelp.Name);

                        var spell = new CureSpell(m_Mobile, null);
                        if (spell.CanCast())
                            spell.Cast();
                    }
                    else if (NeedGHeal(toHelp))
                    {
                        if (m_Mobile.Debug)
                            m_Mobile.DebugSay("{0} needs a greater heal", toHelp.Name);

                        var spell = new GreaterHealSpell(m_Mobile, null);
                        if (spell.CanCast())
                            spell.Cast();
                    }
                    else if (NeedLHeal(toHelp))
                    {
                        if (m_Mobile.Debug)
                            m_Mobile.DebugSay("{0} needs a lesser heal", toHelp.Name);

                        new HealSpell(m_Mobile, null).Cast();
                    }
                }
                else
                {
                    if (AcquireFocusMob(m_Mobile.RangePerception, FightMode.Weakest, false, true, false))
                    {
                        WalkMobileRange(m_Mobile.FocusMob, 1, false, 4, 7);
                    }
                    else
                    {
                        WalkRandomInHome(3, 2, 1);
                    }
                }
            }

            return true;
        }

        private delegate bool NeedDelegate(Mobile m);

        private void ProcessTarget(Target targ, NeedDelegate[] func)
        {
            Mobile toHelp = Find(func);

            if (toHelp != null)
            {
                if (targ.Range != -1 && !m_Mobile.InRange(toHelp, targ.Range))
                {
                    DoMove(m_Mobile.GetDirectionTo(toHelp) | Direction.Running);
                }
                else
                {
                    targ.Invoke(m_Mobile, toHelp);
                }
            }
            else
            {
                targ.Cancel(m_Mobile, TargetCancelType.Canceled);
            }
        }

        private Mobile Find(params NeedDelegate[] funcs)
        {
            if (m_Mobile.Deleted)
                return null;

            Map map = m_Mobile.Map;

            if (map != null)
            {
                double prio = 0.0;
                Mobile found = null;

                foreach (Mobile m in m_Mobile.GetMobilesInRange(m_Mobile.RangePerception))
                {
                    if (!m_Mobile.CanSee(m) || !(m is BaseCreature) || ((BaseCreature) m).Team != m_Mobile.Team)
                        continue;

                    for (int i = 0; i < funcs.Length; ++i)
                    {
                        if (funcs[i](m))
                        {
                            double val = -m_Mobile.GetDistanceToSqrt(m);

                            if (found == null || val > prio)
                            {
                                prio = val;
                                found = m;
                            }

                            break;
                        }
                    }
                }

                return found;
            }

            return null;
        }

        private static bool NeedCure(Mobile m)
        {
            return m.Poisoned;
        }

        private static bool NeedGHeal(Mobile m)
        {
            return m.Hits < m.HitsMax - 40;
        }

        private static bool NeedLHeal(Mobile m)
        {
            return m.Hits < m.HitsMax - 10;
        }
    }
}