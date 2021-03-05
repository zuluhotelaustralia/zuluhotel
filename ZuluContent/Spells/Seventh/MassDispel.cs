using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class MassDispelSpell : MagerySpell
    {
        public MassDispelSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 8);

                    foreach (Mobile m in eable)
                        if (m is BaseCreature && (m as BaseCreature).IsDispellable && Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                    eable.Free();
                }

                for (var i = 0; i < targets.Count; ++i)
                {
                    var m = targets[i];

                    var bc = m as BaseCreature;

                    if (bc == null)
                        continue;

                    var dispelChance =
                        (50.0 + 100 * (Caster.Skills.Magery.Value - bc.DispelDifficulty) / (bc.DispelFocus * 2)) / 100;

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration),
                            0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        Caster.DoHarmful(m);

                        m.FixedEffect(0x3779, 10, 20);
                    }
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MassDispelSpell m_Owner;

            public InternalTarget(MassDispelSpell owner) : base(12, true, TargetFlags.None)
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