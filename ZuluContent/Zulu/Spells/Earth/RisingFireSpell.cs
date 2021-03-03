using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public class RisingFireSpell : AbstractEarthSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(0); }
        }

        public override double RequiredSkill
        {
            get { return 100.0; }
        }

        public override int RequiredMana
        {
            get { return 15; }
        }

        public RisingFireSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence()) goto Return;

            var range = 3.0;
            if (ZuluClass.GetClass(Caster).Type == ZuluClassType.Mage) range *= ZuluClass.GetClass(Caster).Bonus;

            var dmg = Caster.Skills[DamageSkill].Value / 6.0;
            var map = Caster.Map;
            if (map != null)
                foreach (var mob in m.GetMobilesInRange((int) range))
                    if (Caster != mob &&
                        SpellHelper.ValidIndirectTarget(Caster, mob) &&
                        Caster.CanBeHarmful(mob, false) &&
                        m.InLOS(mob))
                    {
                        Caster.DoHarmful(mob);
                        mob.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                        SpellHelper.Damage(dmg, m, Caster, this, TimeSpan.Zero);
                    }

            Caster.PlaySound(0x208);

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
                m_Target.EndAction(typeof(RisingFireSpell));
            }
        }

        private class InternalTarget : Target
        {
            private RisingFireSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(RisingFireSpell owner) : base(12, false, TargetFlags.Harmful)
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