using System;
using System.Collections;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

//earth damage on single target, dex debuff
namespace Scripts.Zulu.Spells.Earth
{
    public class ShiftingEarthSpell : AbstractEarthSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(0); }
        }

        public override double RequiredSkill
        {
            get { return 60.0; }
        }

        public override int RequiredMana
        {
            get { return 5; }
        }

        public ShiftingEarthSpell(Mobile caster, Item scroll) : base(caster, scroll)
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

            SpellHelper.Turn(Caster, m);

            m.FixedParticles(0x3709, 10, 15, 5021, EffectLayer.Waist); //probably wrong particles ID
            m.PlaySound(0x20e);

            Caster.DoHarmful(m);

            //yeah lots of casting is ugly but... fuck it :^)
            var dmg =
                (double) Utility.Dice((uint) (Caster.Skills[DamageSkill].Value / 15.0), 5,
                    0); //caps around 20 damage at 130 skill

            if (CheckResisted(m))
            {
                dmg *= 0.75;

                m.SendLocalizedMessage(501783);
            }

            //m.Damage((int)dmg, Caster, ElementalType.Earth);
            SpellHelper.Damage(dmg, m, Caster, this, TimeSpan.Zero);

            SpellHelper.AddStatCurse(Caster, m, StatType.Dex);
            var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, true) * 100);
            var length = SpellHelper.GetDuration(Caster, m);

            // BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.Clumsy, 1075831, length, m, percentage.ToString()));

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
                m_Target.EndAction(typeof(ShiftingEarthSpell));
            }
        }

        private class InternalTarget : Target
        {
            private ShiftingEarthSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(ShiftingEarthSpell owner) : base(12, false, TargetFlags.Harmful)
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