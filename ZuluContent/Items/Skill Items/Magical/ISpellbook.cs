namespace Server.Items
{
    public interface ISpellbook
    {
        public ulong Content { get; set; }
        
        public Serial Serial { get; }
        
        public string Name { get; }
        
        public int BookOffset { get; }
    }
}