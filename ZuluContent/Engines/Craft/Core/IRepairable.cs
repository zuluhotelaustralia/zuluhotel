namespace Server.Engines.Craft
{
    public interface IRepairable
    {
        public int HitPoints { get; set; }

        public int MaxHitPoints { get; set; }

        public double Quality { get; set; }

        public void Delete();
    }
}