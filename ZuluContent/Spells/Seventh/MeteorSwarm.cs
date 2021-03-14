using System.Collections.Generic;
using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class MeteorSwarmSpell : MagerySpell
    {
        public MeteorSwarmSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override bool DelayedDamage
        {
            get { return true; }
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

                if (p is Item)
                    p = ((Item) p).GetWorldLocation();

                var targets = new List<Mobile>();

                var map = Caster.Map;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 2);

                    foreach (Mobile m in eable)
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                            targets.Add(m);

                    eable.Free();
                }

                double damage = Utility.Random(27, 22);

                if (targets.Count > 0)
                {
                    Effects.PlaySound((Point3D)p, Caster.Map, 0x160);

                    damage /= targets.Count;

                    double toDeal;
                    for (var i = 0; i < targets.Count; ++i)
                    {
                        var m = targets[i];

                        toDeal = damage;

                        if (CheckResisted(m))
                        {
                            damage *= 0.5;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        toDeal *= GetDamageScalar(m);
                        Caster.DoHarmful(m);
                        SpellHelper.Damage((int) toDeal, m, Caster, this);

                        Caster.MovingParticles(m, 0x36D4, 7, 0, false, true, 9501, 1, 0, 0x100);
                    }
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MeteorSwarmSpell m_Owner;

            public InternalTarget(MeteorSwarmSpell owner) : base(12, true, TargetFlags.None)
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