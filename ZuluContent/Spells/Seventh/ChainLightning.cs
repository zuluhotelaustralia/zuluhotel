using System.Collections.Generic;
using System.Linq;
using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class ChainLightningSpell : MagerySpell
    {
        public ChainLightningSpell(Mobile caster, Item scroll) : base(caster, scroll)
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

                if (p is Item item)
                    p = item.GetWorldLocation();


                var map = Caster.Map;

                if (map == null)
                    return;

                var mobilesInRange = map.GetMobilesInRange(new Point3D(p), 2);

                var targets = mobilesInRange
                    .Where(m => SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false));

                mobilesInRange.Free();

                double damage = Utility.Random(27, 22);

                if (targets.Any())
                {
                    damage /= targets.Count();

                    foreach (var t in targets)
                    {
                        var toDeal = damage;
                        var m = t;

                        if (CheckResisted(m))
                        {
                            toDeal *= 0.5;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        toDeal *= GetDamageScalar(m);
                        Caster.DoHarmful(m);
                        SpellHelper.Damage(toDeal, m, Caster, this);


                        m.BoltEffect(0);
                    }
                }
                else
                {
                    Caster.PlaySound(0x29);
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly ChainLightningSpell m_Owner;

            public InternalTarget(ChainLightningSpell owner) : base(12, true, TargetFlags.None)
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