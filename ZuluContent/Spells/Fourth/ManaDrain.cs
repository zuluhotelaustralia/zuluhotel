using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class ManaDrainSpell : MagerySpell
    {
        private static readonly Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

        public ManaDrainSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        private void AosDelay_Callback(object state)
        {
            var states = (object[]) state;

            var m = (Mobile) states[0];
            var mana = (int) states[1];

            if (m.Alive)
            {
                m.Mana += mana;

                m.FixedEffect(0x3779, 10, 25);
                m.PlaySound(0x28E);
            }

            m_Table.Remove(m);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHarmfulSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int) Circle, Caster, ref m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                if (CheckResisted(m))
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                else if (m.Mana >= 100)
                    m.Mana -= Utility.Random(1, 100);
                else
                    m.Mana -= Utility.Random(1, m.Mana);

                m.FixedParticles(0x374A, 10, 15, 5032, EffectLayer.Head);
                m.PlaySound(0x1F8);

            }

            FinishSequence();
        }

        public override double GetResistPercent(Mobile target)
        {
            return 99.0;
        }

        private class InternalTarget : Target
        {
            private readonly ManaDrainSpell m_Owner;

            public InternalTarget(ManaDrainSpell owner) : base(12, false, TargetFlags.Harmful)
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