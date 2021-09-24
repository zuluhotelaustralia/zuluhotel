using System;
using Scripts.Zulu.Utilities;

namespace Server.Items
{
    public class DeathRobe : Robe
    {
        private Timer m_DecayTimer;
        private DateTime m_DecayTime;

        private static readonly TimeSpan m_DefaultDecayTime = TimeSpan.FromMinutes(1.0);

        public override bool DisplayLootType => false;


        [Constructible]
        public DeathRobe()
        {
            LootType = LootType.Newbied;
            Hue = 2301;
            BeginDecay(m_DefaultDecayTime);
        }
        
        public override bool Scissor(Mobile from, Scissors scissors)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendFailureMessage(502437); // Items you wish to cut must be in your backpack.
                return false;
            }

            var bandages = new Bandage(5);
            from.Backpack.TryDropItem(from, bandages, false);
            Delete();

            return true;
        }

        public void BeginDecay()
        {
            BeginDecay(m_DefaultDecayTime);
        }

        private void BeginDecay(TimeSpan delay)
        {
            if (m_DecayTimer != null)
                m_DecayTimer.Stop();

            m_DecayTime = DateTime.Now + delay;

            m_DecayTimer = new InternalTimer(this, delay);
            m_DecayTimer.Start();
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            BeginDecay(m_DefaultDecayTime);

            return true;
        }

        public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            if (m_DecayTimer != null)
            {
                m_DecayTimer.Stop();
                m_DecayTimer = null;
            }

            return true;
        }

        public override void OnAfterDelete()
        {
            if (m_DecayTimer != null)
                m_DecayTimer.Stop();

            m_DecayTimer = null;
        }

        private class InternalTimer : Timer
        {
            private readonly DeathRobe m_Robe;

            public InternalTimer(DeathRobe c, TimeSpan delay) : base(delay)
            {
                m_Robe = c;
            }

            protected override void OnTick()
            {
                if (m_Robe.Parent != null || m_Robe.IsLockedDown)
                    Stop();
                else
                    m_Robe.Delete();
            }
        }

        [Constructible]
        public DeathRobe(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(2); // version

            writer.Write(m_DecayTimer != null);

            if (m_DecayTimer != null)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 2:
                {
                    if (reader.ReadBool())
                    {
                        m_DecayTime = reader.ReadDeltaTime();
                        BeginDecay(m_DecayTime - DateTime.Now);
                    }

                    break;
                }
                case 1:
                case 0:
                {
                    if (Parent == null)
                        BeginDecay(m_DefaultDecayTime);
                    break;
                }
            }

            if (version < 1 && Hue == 0)
                Hue = 2301;
        }
    }
}