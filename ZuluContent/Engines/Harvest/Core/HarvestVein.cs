namespace Server.Engines.Harvest
{
    public class HarvestVein
    {
        private double m_VeinChance;
        private HarvestResource m_Resource;

        public double VeinChance
        {
            get => m_VeinChance;
            set => m_VeinChance = value;
        }

        public HarvestResource Resource
        {
            get => m_Resource;
            set => m_Resource = value;
        }

        public HarvestVein(double veinChance, HarvestResource resource)
        {
            m_VeinChance = veinChance;
            m_Resource = resource;
        }
    }
}