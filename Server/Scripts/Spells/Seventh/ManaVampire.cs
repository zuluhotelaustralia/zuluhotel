using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Seventh
{
    public class ManaVampireSpell : MagerySpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                "Mana Vampire", "Ort Sanct",
                                221,
                                9032,
                                Reagent.BlackPearl,
                                Reagent.Bloodmoss,
                                Reagent.MandrakeRoot,
                                Reagent.SpidersSilk
                                );

        public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

        public ManaVampireSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                int toDrain = 0;

                if (CheckResisted(m))
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                else
                    toDrain = m.Mana;

                if (toDrain > (Caster.ManaMax - Caster.Mana))
                    toDrain = Caster.ManaMax - Caster.Mana;

                m.Mana -= toDrain;
                Caster.Mana += toDrain;

                m.FixedParticles(0x374A, 10, 15, 5054, EffectLayer.Head);
                m.PlaySound(0x1F9);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        // previously this had a flat 98% success rate, wtf
        public override double GetResistPercent(Mobile target)
        {
            // so for example Ancient Lich with 200 resist vs well-geared mage w 150 magery:
            // 200 / 150 = 1.33 = 133%, cap it at 95%

            //whereas a drake with 80 resist against same character would be 80 / 150 = 0.53 = 53%
            double percent = target.Skills.MagicResist.Value / Caster.Skills.Magery.Value;

            if (percent > 0.95)
            {
                percent = 0.95;
            }
            if (percent < 0.01)
            {
                percent = 0.01;
            }

            return (percent * 100);
        }

        private class InternalTarget : Target
        {
            private ManaVampireSpell m_Owner;

            public InternalTarget(ManaVampireSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
