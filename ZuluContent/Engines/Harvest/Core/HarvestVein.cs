namespace Server.Engines.Harvest
{
    public class HarvestVein
    {
        public double VeinChance { get; set; }

        public HarvestResource Resource { get; set; }

        public HarvestVein(double veinChance, HarvestResource resource)
        {
            VeinChance = veinChance;
            Resource = resource;
        }
    }
}