using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Eighth
{
    public class EnergyVortexSpell : MagerySpell
    {
        public EnergyVortexSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if (Caster.Followers + 1 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            var map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref p);

            if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                var duration = TimeSpan.FromSeconds(Utility.Random(80, 40));

                BaseCreature.Summon(new EnergyVortex(), false, Caster, new Point3D(p), 0x212, duration);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private EnergyVortexSpell m_Owner;

            public InternalTarget(EnergyVortexSpell owner) : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                    m_Owner.Target((IPoint3D) o);
            }

            protected override void OnTargetOutOfLOS(Mobile from, object o)
            {
                from.SendLocalizedMessage(501943); // Target cannot be seen. Try again.
                from.Target = new InternalTarget(m_Owner);
                from.Target.BeginTimeout(from, TimeoutTime - DateTime.Now);
                m_Owner = null;
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_Owner != null)
                    m_Owner.FinishSequence();
            }
        }
    }
}