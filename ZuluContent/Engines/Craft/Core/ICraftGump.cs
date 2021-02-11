using Server.Items;

namespace Server.Engines.Craft
{
    public interface ICraftGump
    {
        public Mobile From { get; }
        public CraftSystem CraftSystem { get; }
        public BaseTool Tool { get; }
    }
}