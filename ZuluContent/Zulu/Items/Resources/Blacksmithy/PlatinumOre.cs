// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class PlatinumOre : BaseOre
    {
        [Constructible]
        public PlatinumOre() : this(1)
        {
        }


        [Constructible]
        public PlatinumOre(int amount) : base(CraftResource.Platinum, amount)
        {
        }

        [Constructible]
        public PlatinumOre(Serial serial) : base(serial)
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
            return new PlatinumIngot();
        }
    }
}