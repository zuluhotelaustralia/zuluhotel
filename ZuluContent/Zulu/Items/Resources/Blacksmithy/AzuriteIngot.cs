// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AzuriteIngot : BaseIngot
    {
        [Constructible]
        public AzuriteIngot() : this(1)
        {
        }


        [Constructible]
        public AzuriteIngot(int amount) : base(CraftResource.Azurite, amount)
        {
        }

        [Constructible]
        public AzuriteIngot(Serial serial) : base(serial)
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