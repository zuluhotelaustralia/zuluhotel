using Server;
using Server.Items;
using Server.Mobiles;

namespace Scripts.Zulu.Engines.CustomSpellHotBar
{
    public class CustomSpellHotBar
    {
        private PlayerMobile Parent { get; }
        
        public Point2D Location { get; }
        
        public CustomSpellbook Book { get; }
        
        public Direction Direction { get; set; }

        public CustomSpellHotBar(PlayerMobile parent, Point2D location, CustomSpellbook book,
            Direction? direction = null)
        {
            Parent = parent;
            Location = location;
            Book = book;
            Direction = direction ?? Direction.Right;
        }

        public static CustomSpellHotBar Deserialize(IGenericReader reader)
        {
            var version = reader.ReadInt();

            return new CustomSpellHotBar(
                reader.ReadEntity<PlayerMobile>(),
                reader.ReadPoint2D(),
                reader.ReadEntity<CustomSpellbook>(),
                (Direction)reader.ReadInt());
        }

        public void Serialize(IGenericWriter writer)
        {
            writer.Write(1);
            
            writer.Write(Parent);
            writer.Write(Location);
            writer.Write(Book);
            writer.Write((int) Direction);
        }
    }
}