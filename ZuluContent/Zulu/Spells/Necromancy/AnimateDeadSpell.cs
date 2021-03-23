using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class AnimateDeadSpell : NecromancerSpell
    {
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

        public AnimateDeadSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(object obj)
        {
            if (!Caster.CanSee(obj))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence()) goto Return;

            SpellHelper.Turn(Caster, obj);

            var c = obj as Corpse;

            if (c == null)
            {
                Caster.SendLocalizedMessage(1061084); // You cannot animate that.
            }
            else
            {
                Type type = null;

                if (c.Owner != null) type = c.Owner.GetType();

                if (c.ItemID != 0x2006 ||
                    c.Animated ||
                    type == typeof(PlayerMobile) ||
                    type == null ||
                    c.Owner != null && c.Owner.Fame < 100 ||
                    c.Owner != null && c.Owner is BaseCreature && ((BaseCreature) c.Owner).Summoned)
                {
                    Caster.SendLocalizedMessage(1061085); // There's not enough life force there to animate.
                }
                else
                {
                    var p = c.GetWorldLocation();
                    var map = c.Map;

                    if (map != null)
                    {
                        Effects.SendLocationParticles(EffectItem.Create(p, map, EffectItem.DefaultDuration), 0x3789, 1,
                            40, 0x3F, 3, 9907, 0);

                        var duration = TimeSpan.FromSeconds(2 * (int) Caster.Skills[DamageSkill].Value / 5);

                        SpellHelper.Summon(new Zombie(), Caster, 0x1FB, duration, false);
                    }
                }
            }

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
                var time = caster.Skills[SkillName.Magery].Value * 1.2;
                if (time > 144)
                    time = 144;
                Delay = TimeSpan.FromSeconds(time);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Target.EndAction(typeof(AnimateDeadSpell));
            }
        }

        private class InternalTarget : Target
        {
            private AnimateDeadSpell m_Owner;

            public InternalTarget(AnimateDeadSpell owner) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                m_Owner.Target(o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}