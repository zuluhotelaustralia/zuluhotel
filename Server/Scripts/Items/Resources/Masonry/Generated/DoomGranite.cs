// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DoomGranite : BaseGranite
    {
        [Constructable]
        public DoomGranite() : base(CraftResource.Doom)
        {
            this.Hue = 2772;
        }

        public DoomGranite(Serial serial) : base(serial) { }

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
            from.SendMessage("Doom Granite");
        }
    }
}
