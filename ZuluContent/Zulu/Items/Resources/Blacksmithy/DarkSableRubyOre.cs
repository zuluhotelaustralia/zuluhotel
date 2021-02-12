// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class DarkSableRubyOre : BaseOre
    {
        [Constructible]
        public DarkSableRubyOre() : this(1)
        {
        }


        [Constructible]
        public DarkSableRubyOre(int amount) : base(CraftResource.DarkSableRuby, amount)
        {
            ItemID = 0xF25;
        }

        [Constructible]
        public DarkSableRubyOre(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override BaseIngot GetIngot()
        {
            return new DarkSableRubyIngot();
        }
    }
}