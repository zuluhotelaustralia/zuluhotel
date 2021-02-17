// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DoomIngot : BaseIngot
    {
        [Constructible]
        public DoomIngot() : this(1)
        {
        }


        [Constructible]
        public DoomIngot(int amount) : base(CraftResource.Doom, amount)
        {
        }

        [Constructible]
        public DoomIngot(Serial serial) : base(serial)
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