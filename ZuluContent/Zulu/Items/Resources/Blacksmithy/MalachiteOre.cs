// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class MalachiteOre : BaseOre
    {
        [Constructible]
        public MalachiteOre() : this(1)
        {
        }


        [Constructible]
        public MalachiteOre(int amount) : base(CraftResource.Malachite, amount)
        {
        }

        [Constructible]
        public MalachiteOre(Serial serial) : base(serial)
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
            return new MalachiteIngot();
        }
    }
}