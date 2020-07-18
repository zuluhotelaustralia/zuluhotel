// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class PlatinumIngot : BaseIngot
    {
        [Constructable]
        public PlatinumIngot() : this(1) { }

        [Constructable]
        public PlatinumIngot(int amount) : base(CraftResource.Platinum, amount)
        {
            this.Hue = 0x457;
        }

        public PlatinumIngot(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
