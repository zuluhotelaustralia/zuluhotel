// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class VirginityOre : BaseOre
    {
        [Constructible]
        public VirginityOre() : this(1)
        {
        }


        [Constructible]
        public VirginityOre(int amount) : base(CraftResource.Virginity, amount)
        {
        }

        [Constructible]
        public VirginityOre(Serial serial) : base(serial)
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
            return new VirginityIngot();
        }
    }
}