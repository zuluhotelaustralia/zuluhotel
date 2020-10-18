using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Scripts.Zulu.Spells.Earth
{
    public class OwlSightSpell : AbstractEarthSpell
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

        public OwlSightSpell(Mobile caster, Item scroll) : base(caster, scroll)
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

            if (!CheckBSequence(m)) goto Return;

            if (!m.BeginAction(typeof(OwlSightSpell))) goto Return;

            SpellHelper.Turn(Caster, m);

            if (m.BeginAction(typeof(LightCycle)))
            {
                new LightCycle.OwlSightTimer(m).Start();
                var level = (int) LightCycle.DungeonLevel; //0

                m.LightLevel = level;
                m.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                m.PlaySound(0x1E3);

                // BuffInfo.AddBuff(m, new BuffInfo(BuffIcon.NightSight, 1075643));    //Night Sight/You ignore lighting effects
            }

            Return:
            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private OwlSightSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(OwlSightSpell owner) : base(12, false, TargetFlags.Beneficial)
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