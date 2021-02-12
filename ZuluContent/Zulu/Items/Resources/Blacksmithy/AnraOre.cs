// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class AnraOre : BaseOre
    {
        [Constructible]
        public AnraOre() : this(1)
        {
        }


        [Constructible]
        public AnraOre(int amount) : base(CraftResource.Anra, amount)
        {
        }

        [Constructible]
        public AnraOre(Serial serial) : base(serial)
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
            return new AnraIngot();
        }
    }
}