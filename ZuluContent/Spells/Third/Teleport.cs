using Server.Items;
using Server.Misc;
using Server.Regions;
using Server.Spells.Fifth;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class TeleportSpell : MagerySpell
    {
        public TeleportSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool CheckCast()
        {
            if (WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                return false;
            }

            return SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom);
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            var orig = p;
            var map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref p);

            var from = Caster.Location;
            var to = new Point3D(p);

            if (WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
            {
            }
            else if (!SpellHelper.CheckTravel(Caster, map, to, TravelCheckType.TeleportTo))
            {
            }
            else if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (SpellHelper.CheckMulti(to, map))
            {
                Caster.SendLocalizedMessage(502831); // Cannot teleport to that spot.
            }
            else if (Region.Find(to, map).GetRegion(typeof(HouseRegion)) != null)
            {
                Caster.SendLocalizedMessage(502829); // Cannot teleport to that spot.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, orig);

                var m = Caster;

                m.Location = to;
                m.ProcessDelta();

                if (m.Player)
                {
                    Effects.SendLocationParticles(EffectItem.Create(from, m.Map, EffectItem.DefaultDuration), 0x3728,
                        10, 10, 2023);
                    Effects.SendLocationParticles(EffectItem.Create(to, m.Map, EffectItem.DefaultDuration), 0x3728, 10,
                        10, 5023);
                }
                else
                {
                    m.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
                }

                m.PlaySound(0x1FE);

                IPooledEnumerable eable = m.GetItemsInRange(0);

                foreach (Item item in eable)
                    if (item is ParalyzeFieldSpell.InternalItem || item is PoisonFieldSpell.InternalItem ||
                        item is FireFieldSpell.FireFieldItem)
                        item.OnMoveOver(m);

                eable.Free();
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly TeleportSpell m_Owner;

            public InternalTarget(TeleportSpell owner) : base(12, true, TargetFlags.None)
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