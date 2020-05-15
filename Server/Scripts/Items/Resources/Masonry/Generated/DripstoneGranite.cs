// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DripstoneGranite : BaseGranite
    {
        [Constructable]
        public DripstoneGranite() : base(CraftResource.Dripstone)
        {
            this.Hue = 2771;
        }

        public DripstoneGranite(Serial serial) : base(serial) { }

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
            from.SendMessage("Dripstone Granite");
        }
    }
}
