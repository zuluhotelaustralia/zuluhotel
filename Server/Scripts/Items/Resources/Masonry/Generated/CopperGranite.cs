// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class CopperGranite : BaseGranite
    {
        [Constructable]
        public CopperGranite() : base(CraftResource.Copper)
        {
            this.Hue = 0x602;
        }

        public CopperGranite(Serial serial) : base(serial) { }

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

        public override void OnSingleClick(Mobile from)
        {
            from.SendMessage("Copper Granite");
        }
    }
}
