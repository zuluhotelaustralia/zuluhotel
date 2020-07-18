// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class OldBritainIngot : BaseIngot
    {
        [Constructable]
        public OldBritainIngot() : this(1) { }

        [Constructable]
        public OldBritainIngot(int amount) : base(CraftResource.OldBritain, amount)
        {
            this.Hue = 0x852;
        }

        public OldBritainIngot(Serial serial) : base(serial) { }

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
