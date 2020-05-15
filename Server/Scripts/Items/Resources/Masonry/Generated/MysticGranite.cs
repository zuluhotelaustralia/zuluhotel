// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class MysticGranite : BaseGranite
    {
        [Constructable]
        public MysticGranite() : base(CraftResource.Mystic)
        {
            this.Hue = 0x17f;
        }

        public MysticGranite(Serial serial) : base(serial) { }

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
            from.SendMessage("Mystic Granite");
        }
    }
}
