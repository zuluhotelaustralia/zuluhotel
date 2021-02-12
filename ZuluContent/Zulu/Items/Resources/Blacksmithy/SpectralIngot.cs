// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SpectralIngot : BaseIngot
    {
        [Constructible]
        public SpectralIngot() : this(1)
        {
        }


        [Constructible]
        public SpectralIngot(int amount) : base(CraftResource.Spectral, amount)
        {
        }

        [Constructible]
        public SpectralIngot(Serial serial) : base(serial)
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