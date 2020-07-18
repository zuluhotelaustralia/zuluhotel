using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace RunZH.Scripts.Zulu.Spells.Necromancy
{
    public class ControlUndeadSpell : NecromancerSpell
    {
        public override SpellInfo GetSpellInfo() => m_Info;

        private static SpellInfo m_Info = new SpellInfo(
            "Control Undead", "Nutu Magistri Supplicare",
            227, 9031,
            Reagent.Bloodspawn, Reagent.Blackmoor, Reagent.Bone
        );

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        public override double RequiredSkill
        {
            get { return 100.0; }
        }

        public override int RequiredMana
        {
            get { return 60; }
        }

        public ControlUndeadSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile targeted)
        {
            if (!Caster.CanSee(targeted))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence())
            {
                goto Return;
            }

            if (targeted is Mobile)
            {
                if (targeted is BaseCreature)
                {
                    BaseCreature creature = (BaseCreature) targeted;
                    OppositionGroup group = creature.OppositionGroup;

                    if (group != OppositionGroup.FeyAndUndead)
                    {
                        creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655,
                            Caster.NetState); // That creature cannot be tamed.
                    }
                    else if (Caster.Followers + creature.ControlSlots > Caster.FollowersMax)
                    {
                        Caster.SendLocalizedMessage(1049611); // You have too many followers to tame that creature.
                    }
                    else
                    {
                        creature.SetControlMaster(Caster);
                    }
                }
                else
                {
                    Caster.SendLocalizedMessage(502469); // That being cannot be tamed. check this --sith
                }
            }
            else
            {
                Caster.SendLocalizedMessage(502801); // You can't tame that!
            }

            SpellHelper.Turn(Caster, targeted);

            // TODO: Spell graphical and sound effects.

            Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Target;

            public InternalTimer(Mobile target, Mobile caster) : base(TimeSpan.FromSeconds(0))
            {
                m_Target = target;

                // TODO: Compute a reasonable duration, this is stolen from ArchProtection
                double time = caster.Skills[SkillName.Magery].Value * 1.2;
                if (time > 144)
                    time = 144;
                Delay = TimeSpan.FromSeconds(time);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Target.EndAction(typeof(ControlUndeadSpell));
            }
        }

        private class InternalTarget : Target
        {
            private ControlUndeadSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(ControlUndeadSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile) o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}