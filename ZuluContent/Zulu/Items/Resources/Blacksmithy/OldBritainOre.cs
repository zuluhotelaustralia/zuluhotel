// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class OldBritainOre : BaseOre
    {
        [Constructible]
        public OldBritainOre() : this(1)
        {
        }


        [Constructible]
        public OldBritainOre(int amount) : base(CraftResource.OldBritain, amount)
        {
        }

        [Constructible]
        public OldBritainOre(Serial serial) : base(serial)
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
            return new OldBritainIngot();
        }
    }
}