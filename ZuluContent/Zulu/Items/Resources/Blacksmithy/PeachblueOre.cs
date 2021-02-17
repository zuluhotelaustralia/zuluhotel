// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class PeachblueOre : BaseOre
    {
        [Constructible]
        public PeachblueOre() : this(1)
        {
        }


        [Constructible]
        public PeachblueOre(int amount) : base(CraftResource.Peachblue, amount)
        {
        }

        [Constructible]
        public PeachblueOre(Serial serial) : base(serial)
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
            return new PeachblueIngot();
        }
    }
}