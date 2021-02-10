using System;
using Server.Engines.Craft;

namespace Server.Items
{
    public class HeatingStand : BaseTool
    {
        public override CraftSystem CraftSystem => DefAlchemy.PlusCraftSystem;

        public Timer Timer { get; private set; }
        
        public int LitItemId { get; } = 0x184A;

        public int UnlitItemId { get; } = 0x1849;
        
        public override int ItemID 
        {
            get => base.ItemID;
            set
            {
                Light = value == LitItemId ? LightType.Circle150 : LightType.Empty;
                base.ItemID = value;
            }
        }

        
        [Constructible]
        public HeatingStand(int uses) : base(uses, 0xE9B)
        {
            Weight = 1.0;
        }
        
        [Constructible]
        public HeatingStand() : base(0x1849)
        {
            Light = LightType.Empty;
            Weight = 1.0;
        }
        
        [Constructible]
        public HeatingStand(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            ItemID = UnlitItemId;
        }

        public override void OnDoubleClick(Mobile @from)
        {
            base.OnDoubleClick(@from);

            if (ItemID == UnlitItemId)
            {
                ItemID = LitItemId;
                Effects.PlaySound(GetWorldLocation(), Map, 0x47);
                Timer = new InternalTimer(this, TimeSpan.FromSeconds(60.0));
                Timer.Start();
            }
        }

        private class InternalTimer : Timer
        {
            private readonly HeatingStand m_Stand;

            public InternalTimer(HeatingStand stand, TimeSpan delay) : base(delay)
            {
                m_Stand = stand;
                Priority = TimerPriority.FiveSeconds;
            }

            protected override void OnTick()
            {
                if (m_Stand != null && !m_Stand.Deleted)
                {
                    m_Stand.ItemID = m_Stand.UnlitItemId;
                    Effects.PlaySound(m_Stand.GetWorldLocation(), m_Stand.Map, 0x3be);
                }
            }
        }
    }
}