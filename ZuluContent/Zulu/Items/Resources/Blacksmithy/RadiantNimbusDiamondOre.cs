// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class RadiantNimbusDiamondOre : BaseOre
    {
        [Constructible]
        public RadiantNimbusDiamondOre() : this(1)
        {
        }


        [Constructible]
        public RadiantNimbusDiamondOre(int amount) : base(CraftResource.RadiantNimbusDiamond, amount)
        {
            ItemID = 0xF21;
        }

        [Constructible]
        public RadiantNimbusDiamondOre(Serial serial) : base(serial)
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
            return new RadiantNimbusDiamondIngot();
        }
    }
}