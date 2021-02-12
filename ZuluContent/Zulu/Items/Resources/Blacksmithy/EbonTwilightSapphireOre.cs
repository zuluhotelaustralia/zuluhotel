// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class EbonTwilightSapphireOre : BaseOre
    {
        [Constructible]
        public EbonTwilightSapphireOre() : this(1)
        {
        }


        [Constructible]
        public EbonTwilightSapphireOre(int amount) : base(CraftResource.EbonTwilightSapphire, amount)
        {
            ItemID = 0xF2D;
        }

        [Constructible]
        public EbonTwilightSapphireOre(Serial serial) : base(serial)
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
            return new EbonTwilightSapphireIngot();
        }
    }
}