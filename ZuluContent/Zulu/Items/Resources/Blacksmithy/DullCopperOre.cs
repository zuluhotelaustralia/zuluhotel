// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class DullCopperOre : BaseOre
    {
        [Constructible]
        public DullCopperOre() : this(1)
        {
        }


        [Constructible]
        public DullCopperOre(int amount) : base(CraftResource.DullCopper, amount)
        {
        }

        [Constructible]
        public DullCopperOre(Serial serial) : base(serial)
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
            return new DullCopperIngot();
        }
    }
}