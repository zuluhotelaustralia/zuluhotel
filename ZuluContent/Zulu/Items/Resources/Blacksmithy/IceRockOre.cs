// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class IceRockOre : BaseOre
    {
        [Constructible]
        public IceRockOre() : this(1)
        {
        }


        [Constructible]
        public IceRockOre(int amount) : base(CraftResource.IceRock, amount)
        {
        }

        [Constructible]
        public IceRockOre(Serial serial) : base(serial)
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
            return new IceRockIngot();
        }
    }
}