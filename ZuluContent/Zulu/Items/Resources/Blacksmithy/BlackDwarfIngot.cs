// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BlackDwarfIngot : BaseIngot
    {
        [Constructible]
        public BlackDwarfIngot() : this(1)
        {
        }


        [Constructible]
        public BlackDwarfIngot(int amount) : base(CraftResource.BlackDwarf, amount)
        {
        }

        [Constructible]
        public BlackDwarfIngot(Serial serial) : base(serial)
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
    }
}