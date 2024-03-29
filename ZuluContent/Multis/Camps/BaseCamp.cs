using System;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Multis
{
    public abstract class BaseCamp : BaseMulti
    {
        private List<Item> m_Items;
        private List<Mobile> m_Mobiles;
        private DateTime m_DecayTime;
        private Timer m_DecayTimer;
        private TimeSpan m_DecayDelay;

        public virtual int EventRange
        {
            get { return 10; }
        }

        public virtual TimeSpan DecayDelay
        {
            get { return m_DecayDelay; }
            set
            {
                m_DecayDelay = value;
                RefreshDecay(true);
            }
        }

        public BaseCamp(int multiID) : base(multiID)
        {
            m_Items = new List<Item>();
            m_Mobiles = new List<Mobile>();
            m_DecayDelay = TimeSpan.FromMinutes(30.0);
            RefreshDecay(true);

            Timer.DelayCall(TimeSpan.Zero, CheckAddComponents);
        }

        public void CheckAddComponents()
        {
            if (Deleted)
                return;

            AddComponents();
        }

        public virtual void AddComponents()
        {
        }

        public virtual void RefreshDecay(bool setDecayTime)
        {
            if (Deleted)
                return;

            if (m_DecayTimer != null)
                m_DecayTimer.Stop();

            if (setDecayTime)
                m_DecayTime = DateTime.Now + DecayDelay;

            m_DecayTimer = Timer.DelayCall(DecayDelay, Delete);
        }

        public virtual void AddItem(Item item, int xOffset, int yOffset, int zOffset)
        {
            m_Items.Add(item);

            var zavg = Map.GetAverageZ(X + xOffset, Y + yOffset);
            item.MoveToWorld(new Point3D(X + xOffset, Y + yOffset, zavg + zOffset), Map);
        }

        public virtual void AddMobile(BaseCreature bc, int wanderRange, int xOffset, int yOffset, int zOffset)
        {
            m_Mobiles.Add(bc);

            var zavg = Map.GetAverageZ(X + xOffset, Y + yOffset);
            var loc = new Point3D(X + xOffset, Y + yOffset, zavg + zOffset);

            if (bc == null)
                return;

            bc.RangeHome = wanderRange;
            bc.Home = loc;

            if (bc is Banker || bc is BaseVendor)
                bc.Direction = Direction.South;

            bc.MoveToWorld(loc, Map);
        }

        public virtual void OnEnter(Mobile m)
        {
            RefreshDecay(true);
        }

        public virtual void OnExit(Mobile m)
        {
            RefreshDecay(true);
        }

        public override bool HandlesOnMovement
        {
            get { return true; }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            var inDefaultRange = Utility.InRange(oldLocation, Location, EventRange);
            var inNewRange = Utility.InRange(m.Location, Location, EventRange);

            if (inNewRange && !inDefaultRange)
                OnEnter(m);
            else if (inDefaultRange && !inNewRange)
                OnExit(m);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            for (var i = 0; i < m_Items.Count; ++i)
                m_Items[i].Delete();

            for (var i = 0; i < m_Mobiles.Count; ++i)
            {
                var bc = (BaseCreature) m_Mobiles[i];

                if (bc.IsPrisoner == false)
                    m_Mobiles[i].Delete();
                else if (m_Mobiles[i].CantWalk == true)
                    m_Mobiles[i].Delete();
            }

            m_Items.Clear();
            m_Mobiles.Clear();
        }

        public BaseCamp(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write(m_Items);
            writer.Write(m_Mobiles);
            writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_Items = reader.ReadEntityList<Item>();
                    m_Mobiles = reader.ReadEntityList<Mobile>();
                    ;
                    m_DecayTime = reader.ReadDeltaTime();

                    RefreshDecay(false);

                    break;
                }
            }
        }
    }

    public class LockableBarrel : LockableContainer
    {
        [Constructible]
        public LockableBarrel() : base(0xE77)
        {
            Weight = 1.0;
        }

        [Constructible]
        public LockableBarrel(Serial serial) : base(serial)
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

            var version = reader.ReadInt();

            if (Weight == 8.0)
                Weight = 1.0;
        }
    }
}