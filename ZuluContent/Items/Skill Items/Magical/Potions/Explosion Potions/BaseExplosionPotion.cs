using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
    public abstract class BaseExplosionPotion : BasePotion
    {
        public override bool RequireFreeHand => false;

        private static bool LeveledExplosion = false; // Should explosion potions explode other nearby potions?
        private static bool InstantExplosion = false; // Should explosion potions explode on impact?
        private static bool RelativeLocation = false; // Is the explosion target location relative for mobiles?
        private const int ExplosionRange = 2; // How long is the blast radius?

        public BaseExplosionPotion(PotionEffect effect) : base(0xF0D, effect)
        {
        }

        public BaseExplosionPotion(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public virtual object FindParent(Mobile from)
        {
            Mobile m = HeldBy;

            if (m != null && m.Holding == this)
                return m;

            object obj = RootParent;

            if (obj != null)
                return obj;

            if (Map == Map.Internal)
                return from;

            return this;
        }

        private TimerExecutionToken _timerToken;

        private ArrayList m_Users;

        public override void Drink(Mobile from)
        {
            ThrowTarget targ = from.Target as ThrowTarget;
            Stackable = false; // Scavenged explosion potions won't stack with those ones in backpack, and still will explode.

            if (targ != null && targ.Potion == this)
                return;

            from.RevealingAction();

            if (m_Users == null)
                m_Users = new ArrayList();

            if (!m_Users.Contains(from))
                m_Users.Add(from);

            from.Target = new ThrowTarget(this);

            if (!_timerToken.Running)
            {
                from.SendLocalizedMessage(500236); // You should throw it now!

                var timer = 3;
                
                Timer.StartTimer(TimeSpan.FromSeconds(0.75), TimeSpan.FromSeconds(1.0), 4, () => Detonate_OnTick(from, timer--), out _timerToken);
            }
        }

        private void Detonate_OnTick(Mobile from, int timer)
        {
            if (Deleted)
                return;

            object parent = FindParent(from);

            if (timer == 0)
            {
                Point3D loc;
                Map map;

                if (parent is Item)
                {
                    Item item = (Item) parent;

                    loc = item.GetWorldLocation();
                    map = item.Map;
                }
                else if (parent is Mobile)
                {
                    Mobile m = (Mobile) parent;

                    loc = m.Location;
                    map = m.Map;
                }
                else
                {
                    return;
                }

                Explode(from, true, loc, map);
                _timerToken.Cancel();
            }
            else
            {
                if (parent is Item item)
                    item.PublicOverheadMessage(MessageType.Regular, 0x22, false, timer.ToString());
                else if (parent is Mobile mobile)
                    mobile.PublicOverheadMessage(MessageType.Regular, 0x22, false, timer.ToString());
            }
        }

        private void Reposition_OnTick(Mobile from, IPoint3D p, Map map)
        {
            if (Deleted)
                return;
            
            Point3D loc = new Point3D(p);

            if (InstantExplosion)
                Explode(from, true, loc, map);
            else
                MoveToWorld(loc, map);
        }

        private class ThrowTarget : Target
        {
            private BaseExplosionPotion m_Potion;

            public BaseExplosionPotion Potion
            {
                get { return m_Potion; }
            }

            public ThrowTarget(BaseExplosionPotion potion) : base(12, true, TargetFlags.None)
            {
                m_Potion = potion;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Potion.Deleted || m_Potion.Map == Map.Internal)
                    return;

                IPoint3D p = targeted as IPoint3D;

                if (p == null)
                    return;

                Map map = from.Map;

                if (map == null)
                    return;

                SpellHelper.GetSurfaceTop(ref p);
                var loc = new Point3D(p);

                from.RevealingAction();

                IEntity to;

                to = new Entity(Serial.Zero, loc, map);

                if (p is Mobile mobile)
                {
                    if (!RelativeLocation) // explosion location = current mob location.
                        loc = mobile.Location;
                    else
                        to = mobile;
                }

                Effects.SendMovingEffect(from, to, m_Potion.ItemID, 7, 0, false, false, m_Potion.Hue, 0);

                if (m_Potion.Amount > 1)
                {
                    Mobile.LiftItemDupe(m_Potion, 1);
                }

                m_Potion.Internalize();
                Timer.StartTimer(TimeSpan.FromSeconds(1.0), () => m_Potion.Reposition_OnTick(from, loc, map));
            }
        }

        public void Explode(Mobile from, bool direct, Point3D loc, Map map)
        {
            if (Deleted)
                return;

            Consume();

            for (int i = 0; m_Users != null && i < m_Users.Count; ++i)
            {
                Mobile m = (Mobile) m_Users[i];
                ThrowTarget targ = m.Target as ThrowTarget;

                if (targ != null && targ.Potion == this)
                    Target.Cancel(m);
            }

            if (map == null)
                return;

            Effects.PlaySound(loc, map, 0x307);

            Effects.SendLocationEffect(loc, map, 0x36B0, 9, 10, 0, 0);
            int alchemyBonus = 0;

            if (direct)
                alchemyBonus = (int) (from.Skills.Alchemy.Value / 10);

            IPooledEnumerable eable = map.GetObjectsInRange(loc, ExplosionRange);
            ArrayList toExplode = new ArrayList();

            int toDamage = 0;

            foreach (object o in eable)
            {
                if (o is Mobile && (from == null || SpellHelper.ValidIndirectTarget(@from, (Mobile) o) &&
                    @from.CanBeHarmful((Mobile) o, false)))
                {
                    toExplode.Add(o);
                    ++toDamage;
                }
                else if (o is BaseExplosionPotion && o != this)
                {
                    toExplode.Add(o);
                }
            }

            eable.Free();
            
            
            for (int i = 0; i < toExplode.Count; ++i)
            {
                object o = toExplode[i];

                if (o is Mobile m)
                {
                    from?.DoHarmful(m);

                    int damage = (Utility.RandomMinMax(0, 9) + 12) * (int)PotionStrength;

                    damage += alchemyBonus;

                    if (damage > 40)
                        damage = 40;

                    m.Damage(damage, from);
                }
                else if (o is BaseExplosionPotion pot)
                {
                    pot.Explode(from, false, pot.GetWorldLocation(), pot.Map);
                }
            }
        }
    }
}