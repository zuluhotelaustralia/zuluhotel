// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class IronOre : BaseOre
    {
        [Constructible]
        public IronOre() : this(1)
        {
        }


        [Constructible]
        public IronOre(int amount) : base(CraftResource.Iron, amount)
        {
        }

        [Constructible]
        public IronOre(Serial serial) : base(serial)
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
            return new IronIngot();
        }
    }
}