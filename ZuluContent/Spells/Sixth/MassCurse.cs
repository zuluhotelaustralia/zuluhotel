using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class MassCurseSpell : MagerySpell
    {
        public MassCurseSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 2);

                    foreach (Mobile m in eable)
                        if (SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanSee(m) &&
                            Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                    eable.Free();
                }

                for (var i = 0; i < targets.Count; ++i)
                {
                    var m = targets[i];

                    Caster.DoHarmful(m);

                    SpellHelper.AddStatCurse(Caster, m, StatType.Str);
                    SpellHelper.DisableSkillCheck = true;
                    SpellHelper.AddStatCurse(Caster, m, StatType.Dex);
                    SpellHelper.AddStatCurse(Caster, m, StatType.Int);
                    SpellHelper.DisableSkillCheck = false;

                    m.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                    m.PlaySound(0x1FB);

                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MassCurseSpell m_Owner;

            public InternalTarget(MassCurseSpell owner) : base(12, true, TargetFlags.None)
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