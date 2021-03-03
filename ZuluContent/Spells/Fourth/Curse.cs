using System.Collections;
using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class CurseSpell : MagerySpell
    {
        private static readonly Hashtable m_UnderEffect = new Hashtable();

        public CurseSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public static void RemoveEffect(object state)
        {
            var m = (Mobile) state;

            m_UnderEffect.Remove(m);
        }

        public static bool UnderEffect(Mobile m)
        {
            return m_UnderEffect.Contains(m);
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

                SpellHelper.AddStatCurse(Caster, m, StatType.Str);
                SpellHelper.DisableSkillCheck = true;
                SpellHelper.AddStatCurse(Caster, m, StatType.Dex);
                SpellHelper.AddStatCurse(Caster, m, StatType.Int);
                SpellHelper.DisableSkillCheck = false;

                var t = (Timer) m_UnderEffect[m];

                if (Caster.Player && m.Player /*&& Caster != m */ && t == null
                ) //On OSI you CAN curse yourself and get this effect.
                {
                    var duration = SpellHelper.GetDuration(Caster, m);
                    m_UnderEffect[m] = t = Timer.DelayCall(duration, RemoveEffect, m);
                }

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                m.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                m.PlaySound(0x1E1);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, true) * 100);
                var length = SpellHelper.GetDuration(Caster, m);

                var args = $"{percentage}\t{percentage}\t{percentage}\t{10}\t{10}\t{10}\t{10}";

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly CurseSpell m_Owner;

            public InternalTarget(CurseSpell owner) : base(12, false, TargetFlags.Harmful)
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