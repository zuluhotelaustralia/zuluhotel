// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DoomIngot : BaseIngot
    {
        [Constructable]
        public DoomIngot() : this(1) { }

        [Constructable]
        public DoomIngot(int amount) : base(CraftResource.Doom, amount)
        {
            this.Hue = 2772;
        }

        public DoomIngot(Serial serial) : base(serial) { }

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
