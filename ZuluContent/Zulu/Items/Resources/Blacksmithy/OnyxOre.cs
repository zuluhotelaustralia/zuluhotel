// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class OnyxOre : BaseOre
    {
        [Constructible]
        public OnyxOre() : this(1)
        {
        }


        [Constructible]
        public OnyxOre(int amount) : base(CraftResource.Onyx, amount)
        {
        }

        [Constructible]
        public OnyxOre(Serial serial) : base(serial)
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
            return new OnyxIngot();
        }
    }
}