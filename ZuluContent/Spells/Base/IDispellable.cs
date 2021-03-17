namespace Server.Spells
{
    public interface IDispellable
    {
        public bool Dispellable { get; set; }
        public Point3D Location { get; set; }
        public Map Map { get; set; }
        public void Delete();
    }
}