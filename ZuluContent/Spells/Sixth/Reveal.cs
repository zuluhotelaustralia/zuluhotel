using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class RevealSpell : MagerySpell
    {
        public RevealSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p),
                        1 + (int) (Caster.Skills[SkillName.Magery].Value / 20.0));

                    foreach (Mobile m in eable)
                        if (m.Hidden && (m.AccessLevel == AccessLevel.Player || Caster.AccessLevel > m.AccessLevel) &&
                            CheckDifficulty(Caster, m))
                            targets.Add(m);

                    eable.Free();
                }

                for (var i = 0; i < targets.Count; ++i)
                {
                    var m = targets[i];

                    m.RevealingAction();

                    m.FixedParticles(0x375A, 9, 20, 5049, EffectLayer.Head);
                    m.PlaySound(0x1FD);
                }
            }

            FinishSequence();
        }

        // Reveal uses magery and detect hidden vs. hide and stealth 
        private static bool CheckDifficulty(Mobile from, Mobile m)
        {
            return true;
        }

        public class InternalTarget : Target
        {
            private readonly RevealSpell m_Owner;

            public InternalTarget(RevealSpell owner) : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                var p = o as IPoint3D;

                if (p != null)
                    m_Owner.Target(p);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}