// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class RadiantNimbusDiamondGranite : BaseGranite
    {
        [Constructable]
        public RadiantNimbusDiamondGranite() : base(CraftResource.RadiantNimbusDiamond)
        {
            this.Hue = 2765;
        }

        public RadiantNimbusDiamondGranite(Serial serial) : base(serial) { }

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
            from.SendMessage("Radiant Nimbus Diamond Granite");
        }
    }
}
