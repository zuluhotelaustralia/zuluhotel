// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DarkSableRubyOre : BaseOre
    {
        [Constructable]
        public DarkSableRubyOre() : this(1) { }

        [Constructable]
        public DarkSableRubyOre(int amount) : base(CraftResource.DarkSableRuby, amount)
        {
            this.Hue = 2761;
        }

        public DarkSableRubyOre(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
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
