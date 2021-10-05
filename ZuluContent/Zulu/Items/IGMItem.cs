using Server;

namespace ZuluContent.Zulu.Items
{
    public interface IGMItem
    {
        public string Name { get; }

        public bool AllowEquippedCast(Mobile from) => true;
    }
}