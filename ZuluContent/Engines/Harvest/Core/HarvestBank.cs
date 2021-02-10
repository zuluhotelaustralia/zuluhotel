using System;

namespace Server.Engines.Harvest
{
    public class HarvestBank
    {
        private int m_Current;
        private int m_Maximum;
        private DateTime m_NextRespawn;
        private HarvestVein m_Vein;

        HarvestDefinition m_Definition;

        public HarvestDefinition Definition
        {
            get { return m_Definition; }
        }

        public int Current
        {
            get
            {
                CheckRespawn();
                return m_Current;
            }
        }

        public HarvestVein Vein
        {
            get
            {
                CheckRespawn();
                return m_Vein;
            }
            set { m_Vein = value; }
        }

        public void CheckRespawn()
        {
            if (m_Current == m_Maximum || m_NextRespawn > DateTime.Now)
                return;

            m_Current = m_Maximum;

            m_Vein = null;
        }

        public void Consume(int amount, Mobile from)
        {
            CheckRespawn();

            if (m_Current == m_Maximum)
            {
                double min = m_Definition.MinRespawn.TotalMinutes;
                double max = m_Definition.MaxRespawn.TotalMinutes;
                double rnd = Utility.RandomDouble();

                m_Current = m_Maximum - amount;

                double minutes = min + rnd * (max - min);

                m_NextRespawn = DateTime.Now + TimeSpan.FromMinutes(minutes);
            }
            else
            {
                m_Current -= amount;
            }

            if (m_Current < 0)
                m_Current = 0;
        }

        public HarvestBank(HarvestDefinition def)
        {
            m_Maximum = Utility.RandomMinMax(def.MinTotal, def.MaxTotal);
            m_Current = m_Maximum;

            m_Definition = def;
        }
    }
}